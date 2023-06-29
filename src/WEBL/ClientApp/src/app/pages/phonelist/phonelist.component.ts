import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { tab } from "../models/tab";

@Component({
  selector: 'app-phonelist',
  templateUrl: './phonelist.component.html',
  styleUrls: ['./phonelist.component.scss']
})
export class PhonelistComponent implements OnInit {
  dataSourceSupplier: any;
  dataSourceCustomer: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "VAT";
  event: any;
  allowModify = false;
  tabs: tab[];
  breadcrumbs: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  popupDownloadConfirmCustomerVisible = false;

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
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Supplier-Phonelist.xlsx');
          });
      });
    }
  };


  noDownloadCustomerButtonOptions: any = {
    text: "No",
    onClick: (e) => {
      this.popupDownloadConfirmCustomerVisible = false;
      this.CancelDownload = true;

    }
  };

  yesDownloadCustomerButtonOptions = {
    text: "Yes",
    onClick: () => {
      this.popupDownloadConfirmCustomerVisible = false;

      const workbook = new Workbook();
      const worksheet = workbook.addWorksheet('Main sheet');
      exportDataGrid({
        component: this.event.component,
        worksheet: worksheet
      }).then(function () {
        workbook.xlsx.writeBuffer()
          .then(function (buffer: BlobPart) {
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Customer-Phonelist.xlsx');
          });
      });
    }
  };

  @ViewChild('content') content;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.breadcrumbs = "Suppliers";
    this.tabs = [
      { text: "Suppliers" },
      { text: "Customers" }
    ];

    this.dataSourceSupplier = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Phonelist/getPhonelistSuppliers',
        onUpdated: () => {
          this.globalMethodsService.ToastUpdateSuccessMessage(this.pageName);
        }
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.authService.getToken()
          };
        }*/
      }),
      paginate: true,
    }

    this.dataSourceCustomer = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Phonelist/getPhonelistCustomers',
        onUpdated: () => {
          this.globalMethodsService.ToastUpdateSuccessMessage(this.pageName);
        }
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.authService.getToken()
          };
        }*/
      }),
      paginate: true,
    }

    this.allowModify = this.authService.HavePermission(90);
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

  onOptionChanged(e) {

    if (e != null || e != undefined || e != 0) {
      if (e.addedItems[0].text == "Suppliers" || e.addedItems[0].text == "Customers") {
        this.breadcrumbs = e.addedItems[0].text;
      } else {
        this.breadcrumbs = "";
      }
    }
  }

  ngAfterViewInit() {
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

  onExportingCustomer(e) {
    this.event = e;
    this.popupDownloadConfirmCustomerVisible = true;
    if (this.CancelDownload) {
      e.cancel = true;
    }
  }
}
