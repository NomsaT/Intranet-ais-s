import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import notify from 'devextreme/ui/notify';
import { invoiceData } from '../../core/models/invoiceData.models';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { OrdersService } from '../../core/services/orders.service';
import { toBeInvoicedItems } from '../../core/models/ToBeInvoicedItems.models';

@Component({
  selector: 'app-invoices',
  templateUrl: './invoices.component.html',
  styleUrls: ['./invoices.component.scss']
})
export class InvoiceComponent implements OnInit {
  dataAdd: invoiceData = new invoiceData();
  dataEdit: invoiceData = new invoiceData();
  dataDelete: invoiceData = new invoiceData();
  InternalOrdersForeignDataSource: any;
  InvoicesForeignDataSource: any;
  InternalOrdersForeignDataSourceEdit: any;
  InternalOrdersForeignDataSourceDlt: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Invoice";
  event: any;
  RemoveInvoice = "Delete Invoice";
  allowModify = false;
  popupVisible = false;
  id: any;
  action: any;
  popupDeleteData: any;
  baseUrl: any;
  internalOrderId = null;
  invoicedItems: any;
  currency: " ";

  supplierInvoiceNumber: number;
  internalInvoiceNumber: number;

  supplierInvoiceNumberDlt: number;
  internalInvoiceNumberDlt: number;
  AddInvoice: any = {
    icon: "plus",
    text: " Add Invoice  ",
    type: "default",
    useSubmitBehavior: true,
    onClick: (e) => {
      this.action = 1;
      this.id = this.dataAdd.internalOrderid;
      if (this.id != undefined) {
        this.navigate();
      }

    }
  }

  EditInvoice: any = {
    icon: "edit",
    text: " Edit Invoice ",
    type: "default",
    useSubmitBehavior: true,
    onClick: (e) => {
      this.action = 2;
      if (this.dataEdit.invoiceEditid != undefined || this.dataEdit.id != undefined) {
        this.id = this.dataEdit.invoiceEditid != undefined ? this.dataEdit.invoiceEditid : this.dataEdit.id;
        this.navigate();
      }
    }
  }

  DeleteInvoice: any = {
    icon: "trash",
    text: "Delete Invoice",
    type: "default",
    useSubmitBehavior: true,
    onClick: (e) => {
      this.action = 3;

      if (this.dataDelete.invoiceDeleteid != undefined || this.dataDelete.id != undefined) {
        this.id = this.dataDelete.invoiceDeleteid != undefined ? this.dataDelete.invoiceDeleteid : this.dataDelete.id;
        this.invoicedItems = new toBeInvoicedItems();
        this.invoicedItems.InternalOrderId = this.id;
        this.invoicedItems.action = this.action;
        this.orderService.GetInvoiceInternalOrderData(this.invoicedItems).subscribe(data => {
          if (data != null) {
            this.popupDeleteData = data;
            this.popupDeleteData.invoice.invoiceItems.forEach((value) => {
              this.popupDeleteData.invoice.expectedTotal += value.quantity * value.invoicePrice;
            });
            this.popupDeleteData.invoice.invoiceOnceOffItems.forEach((value) => {
              this.popupDeleteData.invoice.expectedTotal += value.quantity * value.invoicePrice;
            });
            this.popupDeleteData.invoice.invoiceServiceItems.forEach((value) => {
              this.popupDeleteData.invoice.expectedTotal += value.quantity * value.invoicePrice;
            });

            this.RemoveInvoice = "Delete Invoice - " + this.popupDeleteData.invoice.invoiceNumber;
            this.currency = this.popupDeleteData.internalOrder.supplierCurrency;
            this.popupVisible = true;
          } else {
            notify("error occured", 'error', 5000);
          }
        }, subError => {
          notify(subError.error, 'error', 5000);
        });

      }
    }
  }

  InvoiceRemove: any = {
    text: "Delete Invoice",
    type: "success",
    useSubmitBehavior: false,
    onClick: () => {
      this.id = this.dataDelete.invoiceDeleteid;

      this.orderService.RemoveInvoice(this.id).subscribe(data => {
        if (data != null) {
          notify("Invoice Deleted.", 'success', 5000);
          this.popupVisible = false;
          this.dataDelete = new invoiceData();
          this.InvoicesForeignDataSource = {
            store: AspNetData.createStore({
              key: 'id',
              loadUrl: this.baseUrl + 'api/Invoice',
            }),
            paginate: true
          }
        } else {
          notify("Invoice not deleted", 'error', 5000);
        }
      }, subError => {
        notify(subError.error, 'error', 5000);
      });

    }
  }

  CancelRemove: any = {
    text: "Cancel",
    type: "danger",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisible = false;
    }
  }

  @ViewChild('content') content;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private orderService: OrdersService, private router: Router, private route: ActivatedRoute, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.baseUrl = baseUrl;
    this.setInvEdit = this.setInvEdit.bind(this);
    this.setInvDlt = this.setInvDlt.bind(this);
    this.onValueChangedInternalNr = this.onValueChangedInternalNr.bind(this);
    this.onValueChangedSupplierNr = this.onValueChangedSupplierNr.bind(this);
    this.onValueChangedInternalNrDlt = this.onValueChangedInternalNrDlt.bind(this);
    this.onValueChangedSupplierNrDlt = this.onValueChangedSupplierNrDlt.bind(this);

    this.RemoveInvoice = "Delete Invoice";
    this.InternalOrdersForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        /*loadUrl: baseUrl + 'api/InternalOrder/getApprovedInternalOrder',*/
        loadUrl: baseUrl + 'api/InternalOrder/getApprovedInternalOrderAppClose',
      }),
      paginate: true
    }

    this.InternalOrdersForeignDataSourceEdit = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/InternalOrder/getApprovedInternalOrderInvoice',
      }),
      paginate: true
    }

    this.InternalOrdersForeignDataSourceDlt = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/InternalOrder/getApprovedInternalOrderInvoice',
      }),
      paginate: true
    }

    this.InvoicesForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Invoice',
      }),
      paginate: true
    }

    this.allowModify = this.authService.HavePermission(73);
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
  onValueChangedSupplierNr(e: any) {
    this.internalInvoiceNumber = e.value;
  }
  onValueChangedInternalNr(e: any) {
    this.supplierInvoiceNumber = e.value;
  }
  onValueChangedSupplierNrDlt(e: any) {
    this.internalInvoiceNumberDlt = e.value;
  }
  onValueChangedInternalNrDlt(e: any) {
    this.supplierInvoiceNumberDlt = e.value;
  }

  setInvEdit(e) {
    this.internalOrderId = e.value;
    this.InvoicesForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Invoice',
      }),
      filter: ["internalOrderId", "=", this.internalOrderId],
      paginate: true
    }
  }

  setInvDlt(e) {
    this.internalOrderId = e.value;
    this.InvoicesForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Invoice',
      }),
      filter: ["internalOrderId", "=", this.internalOrderId],
      paginate: true
    }
  }

  ngAfterViewInit() {
  }

  getInternalOrder(rowData) {
    if (rowData) {
      return rowData.id + " - " + rowData.supplierFullName;
    } else {
      return "";
    }
  }

  navigate() {
    this.router.navigate(['/invoices-modify'], { state: { id: this.id, action: this.action } });
  }
}
