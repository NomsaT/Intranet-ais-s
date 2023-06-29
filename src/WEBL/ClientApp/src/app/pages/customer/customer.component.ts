import { Component, OnInit, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { DxDataGridComponent, DxFormComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import DataSource from 'devextreme/data/data_source';
import { exportDataGrid } from 'devextreme/excel_exporter';
import DataGrid from "devextreme/ui/data_grid";
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { CustomerAddress } from 'src/app/core/models/customer-address';
import { accountPattern, branchPattern, contactPattern, currencyZAR, postalPattern, taxPattern, worklandlinePattern } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { popupAutoSelectService } from '../../core/services/popupAutoSelect.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.scss']
})
export class CustomerComponent implements OnInit {


  //#region Editors opitons

  postalCodeEditOptions: any;
  deliveryPostalCodeEditOptions: any;
  yesDeleteButtonOptions: any;
  noDeleteButtonOptions: any;

  //#endregion 

  //#region Global Variables
  dataSource: DataSource;
  SupplierTypeForeignDataSource: any;
  CountryForeignDataSource: any;
  contactDetails: any;
  supplierID: any;
  rowIndex = 0;
  popupVisible = false;
  isVisible: string;
  addContactButtonOptions: any;
  tempContacts: any[] = [];
  tempPhysicalAddress: CustomerAddress;
  tempDeliveryAddress: CustomerAddress;
  pageName: string = "Customer"
  allowModify = false;
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;

  currentPhysicalPostalCode: any;
  currentDeliveryPostalCode: any;
  selectedContact: any;

  contactgroupheading = "Contact Details";
  currencyZAR: any;
  taxPattern: any;
  contactPattern: any;
  postalPattern: any;
  accountPattern: any;
  branchPattern: any;
  worklandlinePattern: any;
  dataGridInstance: DataGrid;
  paymentMethodDatasource: any;

  popupDeleteContactVisible: boolean = false;

  buttonOptions: any = {
    icon: "plus",
    hint: "Add a new payment method",
    type: "default",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisible = true;
    }
  }

  noDownloadButtonOptions: any = {
    text: "No",
    onClick: (e) => {
      this.popupDownloadConfirmVisible = false;
      this.CancelDownload = true;

    }
  }

  yesDownloadButtonOptions = {
    text: "Yes",
    onClick: () => {
      this.popupDownloadConfirmVisible = false;
      //this.CancelDownload = false;

      this.event.component.beginUpdate();
      this.event.component.columnOption('contacts[0]', 'visible', true);
       
      const workbook = new Workbook();
      const worksheet = workbook.addWorksheet('Main sheet');
      exportDataGrid({
        component: this.event.component,
        worksheet: worksheet
      }).then(function () {
        workbook.xlsx.writeBuffer()
          .then(function (buffer: BlobPart) {
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Customer-List.xlsx');
          });
      })
      .then(function () {
        this.event.component.columnOption('contacts[0]', 'visible', false);
        this.event.component.endUpdate();
      });
      this.event.cancel = true;
    }
  }

  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  @ViewChildren("formContact") formContact: QueryList<DxFormComponent>;
  @ViewChild("formPhysicalAddress") formPhysicalAddress: DxFormComponent;
  @ViewChild("formDeliveryAddress") formDeliveryAddress: DxFormComponent;
  @ViewChild('content') content;
  constructor(private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService, private autoSelect: popupAutoSelectService) {
    this.rowIndex = 0;
    this.taxPattern = taxPattern;
    this.contactPattern = contactPattern;
    this.postalPattern = postalPattern;
    this.accountPattern = accountPattern;
    this.branchPattern = branchPattern;
    this.worklandlinePattern = worklandlinePattern;
    this.currencyZAR = currencyZAR;

    this.contactgroupheading = "Contact Details";
    this.dataSource = new DataSource({
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.globalMethodsService.BASEURL + 'api/Customer',
        updateUrl: this.globalMethodsService.BASEURL + 'api/Customer',
        insertUrl: this.globalMethodsService.BASEURL + 'api/Customer',
        deleteUrl: this.globalMethodsService.BASEURL + 'api/Customer',
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
      paginate: true,
      sort: ([
        { selector: "customerName", desc: false }
      ])
    });


    this.paymentMethodDatasource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.globalMethodsService.BASEURL + 'api/PaymentMethod',
        onLoaded: function (result) {
        }.bind(this)
      }),
      sort: [
        { selector: "Type", desc: false }
      ],
      paginate: true
    }

    this.CountryForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.globalMethodsService.BASEURL + 'api/Country',
      }),
      paginate: true
    }

    this.noDeleteButtonOptions = {
      text: "No",
      onClick: () => {
        this.popupDeleteContactVisible = false;
        this.selectedContact = undefined;
      }
    };

    this.yesDeleteButtonOptions = {
      text: "Yes",
      onClick: () => {
        this.tempContacts.splice(this.selectedContact, 1);
        this.popupDeleteContactVisible = false;
        if (this.tempContacts.length > 0) {
          this.contactgroupheading = "";
        } else {
          this.contactgroupheading = "Contact Details";
        }
      }
    };

    this.allowModify = this.authService.HavePermission(3);
  }

  ngOnInit() {
    /*horizontal-vertical layput set*/

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


  //#region Form Popup code
  DeleteItem(e) {
    this.selectedContact = e;
    this.popupDeleteContactVisible = true;
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
    this.tempPhysicalAddress = JSON.parse(JSON.stringify(e.data.physicalAddress));
    this.tempDeliveryAddress = JSON.parse(JSON.stringify(e.data.deliveryAddress));
    this.tempContacts = JSON.parse(JSON.stringify(e.data.contacts));

    if (this.tempContacts.length > 0) {
      this.contactgroupheading = "";
    } else {
      this.contactgroupheading = "Contact Details";
    }
  }

  newRow(e) {
    this.tempContacts = [];
    this.contactgroupheading = "Contact Details";

    this.tempPhysicalAddress = new CustomerAddress();
    this.tempDeliveryAddress = new CustomerAddress();

  }

  fieldDataChanged(cellInfo, temp) {
    cellInfo.setValue(temp);
  }

  onSaving(e) {
    if (!this.formPhysicalAddress.instance.validate().isValid) {
      e.cancel = true;
    }
    if (this.tempDeliveryAddress.streetAddress1.length > 0) {
      if (!this.formDeliveryAddress.instance.validate().isValid) {
        e.cancel = true;
      }
    }
    this.formContact.forEach(function (form) {
      if (!form.instance.validate().isValid) {
        e.cancel = true;
      }
    })
    this.rowIndex = 0;
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

  onHidden(e) {
    this.dataGrid.instance.getDataSource().reload();

    this.autoSelect.GetNewestPaymentMethod().subscribe(data => {
      this.dataGrid.instance.cellValue(this.rowIndex, "paymentMethodId", data);
    }, subError => {
      notify(subError.error, 'error', 5000);
    }); 
  }

}
