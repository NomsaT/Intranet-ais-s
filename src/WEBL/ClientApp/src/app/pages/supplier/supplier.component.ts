import { Component, Inject, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent, DxFormComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import DataSource from 'devextreme/data/data_source';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { accountPattern, branchPattern,contactPattern, currencyZAR, EnumLocalLocation, postalPattern, worklandlinePattern } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { SupplierService } from '../../core/services/supplier.service';
import { popupAutoSelectService } from '../../core/services/popupAutoSelect.service';

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
  styleUrls: ['./supplier.component.scss']
})

export class SupplierComponent implements OnInit {
  dataSource: DataSource;
  CountryForeignDataSource: any;
  BankNameForeignDataSource: any;
  CurrencyForeignDataSource: any;
  contactDetails: any;
  supplierID: any;
  rowIndex = 0;
  baseUrl: any;
  isVisible: string;
  addContactButtonOptions: any;
  tempContacts: any[] = [];
  pageName: string = "Supplier";
  tempAddress: any;
  contactgroupheading = "Contact Details";
  mainFormInstance: any;
  contactFormInstance: any = [];
  addressFormInstance: any;
  allowModify = false;
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  popupVisible = false;
  popupVisiblePayment = false;
  paymentMethodForeignDatasource: any;
  RequireBankingDetails:any;
  paymentMethodId = 0;
  required: any;

  popupDeleteContactVisible: boolean = false;
  currentDeleteContact: any; 
  contactPattern: any;
  worklandlinePattern: any;
  postalPattern: any;
  accountPattern: any;
  branchPattern: any;
  currencyZAR: any;

  noDeleteButtonOptions:any = {
    text: "No",
    onClick: () => {
      this.hideDeletePopup();
    }
  };

  yesDeleteButtonOptions = {
    text: "Yes",
    onClick: () => {
      this.DeleteItem(this.currentDeleteContact)
      this.hideDeletePopup();
    }
  };

  noDownloadButtonOptions: any = {
    text: "No",
    onClick: (e) => {
      this.popupDownloadConfirmVisible = false;
      this.CancelDownload = true;

    }
  };

  yesDownloadButtonOptions = {
    text: "Yes",
    onClick: () => {
      this.popupDownloadConfirmVisible = false;

      const workbook = new Workbook();
      const worksheet = workbook.addWorksheet('Main sheet');
      exportDataGrid({
        component: this.event.component,
        worksheet: worksheet
      }).then(function () {
        workbook.xlsx.writeBuffer()
          .then(function (buffer: BlobPart) {
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Supplier-List.xlsx');
          });
      });
    }
  };

  buttonOptions: any = {
    icon: "plus",
    type: "default",
    hint: "Add a new Bank",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisible = true;
    }
  }

  buttonOptionPayment: any = {
    icon: "plus",
    hint: "Add a new payment method",
    type: "default",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisiblePayment = true;
    }
  }

  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  @ViewChildren("formContact") formContact: QueryList<DxFormComponent>;
  @ViewChild("formAddress") formAddress: DxFormComponent;
  @ViewChild('content') content;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private supplierService: SupplierService, private autoSelect: popupAutoSelectService,private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.currencyZAR = currencyZAR;
    this.contactPattern = contactPattern;
    this.worklandlinePattern = worklandlinePattern;
    this.postalPattern = postalPattern;
    this.accountPattern = accountPattern;
    this.branchPattern = branchPattern;
    this.baseUrl = baseUrl;
    this.contactgroupheading = "Contact Details";
    this.rowIndex = 0;

    this.dataSource = new DataSource({
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Supplier',
        updateUrl: baseUrl + 'api/Supplier',
        insertUrl: baseUrl + 'api/Supplier',
        deleteUrl: baseUrl + 'api/Supplier',
        onLoaded: function (result) { 
        }.bind(this),
        onInserted: () => {
          this.globalMethodsService.ToastInsertSuccessMessage(this.pageName);
        },
        onUpdated: () => {
          this.globalMethodsService.ToastUpdateSuccessMessage(this.pageName);
        },
        onRemoved: () => {
          this.globalMethodsService.ToastDeleteSuccessMessage(this.pageName);
        }
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.authService.getToken()
          };
        }*/
      }),
      paginate: true
    });

    this.CountryForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Country',
      }),
      paginate: true
    }

    this.BankNameForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/BankNames',
      }),
      sort: [
        { selector: "BankName1", desc: false }
      ],
      paginate: true
    }

    this.CurrencyForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Currency',
      }),
      sort: [
        { selector: "Iso", desc: false }
      ],
      paginate: true
    }

    this.paymentMethodForeignDatasource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/PaymentMethod',
      }),
      sort: [
        { selector: "Type", desc: false }
      ],
      paginate: true
    }

    this.allowModify = this.authService.HavePermission(5);
  }

  ngOnInit() {
     const attribute = document.body.getAttribute('data-layout');

     this.isVisible = attribute;
     const vertical = document.getElementById('layout-vertical');
     if (vertical != null) {
       vertical.setAttribute('checked', 'true');
     }
     if (attribute == 'horizontal') {
       const horizontal = document.getElementById('layout-horizontal');
       if (horizontal != null) {
         horizontal.setAttribute('checked', 'true');
         console.log(horizontal);
       }
     }
  }

  ngAfterViewInit() {
  }

  DeleteItem(e) {
    this.contactFormInstance.splice(e, 1);
    this.tempContacts.splice(e, 1);
    if (this.tempContacts.length > 0) {
      this.contactgroupheading = "";
    } else {
      this.contactgroupheading = "Contact Details";
    }
  }

  AddItem() {
    this.tempContacts.push({ personName: "", personPosition: "", contactNumber: "", contactEmail: "", workLandlineNumber: "" });
    if (this.tempContacts.length > 0) {
      this.contactgroupheading = "";
    } else {
      this.contactgroupheading = "Contact Details";
    }
  }

  EditStart(e) {
    this.rowIndex = this.dataGrid.instance.getRowIndexByKey(e.data.id);
    this.tempContacts = JSON.parse(JSON.stringify(e.data.contacts));
    this.tempAddress = JSON.parse(JSON.stringify(e.data.address));
    if (this.tempContacts.length > 0) {
      this.contactgroupheading = "";
    } else {
      this.contactgroupheading = "Contact Details";
    }
  }

  showDeletePopup(i) {
    this.currentDeleteContact = i;
    this.popupDeleteContactVisible = true;
  }

  hideDeletePopup() {
    this.currentDeleteContact = undefined;
    this.popupDeleteContactVisible = false;
  }

  newRow(e) {
    this.tempContacts = [];
    this.tempAddress = {countryId: 190};
    this.contactgroupheading = "Contact Details";
    e.data.locationTypeId = EnumLocalLocation;
    e.data.creditLimit = 0;
    e.data.creditTerms = 0;
    e.data.discount = 0;
  }

  fieldDataChanged(cellInfo, temp) {
    cellInfo.setValue(temp);
  } 

  onSaving(e) {
    if (!this.formAddress.instance.validate().isValid) {
      e.cancel = true;
    }
    this.formContact.forEach(function (form) {
      if (!form.instance.validate().isValid) {
        e.cancel = true;
      }
    })
    this.rowIndex = 0;
  }

  onHiddenBankName(e) {
    this.dataGrid.instance.getDataSource().reload();
    this.autoSelect.GetNewestBankName().subscribe(data => {
      this.dataGrid.instance.cellValue(this.rowIndex, "bankNameId", data);
    }, subError => {
      notify(subError.error, 'error', 5000);
    }); 
  }

  onHiddenPayment(e) {
    this.dataGrid.instance.getDataSource().reload();
    this.autoSelect.GetNewestPaymentMethod().subscribe(data => {
      this.dataGrid.instance.cellValue(this.rowIndex, "paymentMethodId", data);
    }, subError => {
      notify(subError.error, 'error', 5000);
    }); 
  }

  onExporting(e) {
    this.event = e;
    this.popupDownloadConfirmVisible = true;
    if (this.CancelDownload) {
      e.cancel = true;
    }
  }

  onExported(e) {
    notify("Successfully Downloaded", 'success', 5000);
  }
  
}


