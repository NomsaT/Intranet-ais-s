import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import notify from 'devextreme/ui/notify';
import { currencyZAR } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { OrdersService } from '../../core/services/orders.service';
import { tab } from "../models/tab";
import { toBeInvoicedItems } from '../../core/models/ToBeInvoicedItems.models';
import { StockService } from '../../core/services/stock.service';

@Component({
  selector: 'app-invoices-modify',
  templateUrl: './invoices-modify.component.html',
  styleUrls: ['./invoices-modify.component.scss']
})
export class InvoiceModifyComponent implements OnInit {
  DepartmentForeignDataSource: any;
  GLCodeForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Invoice";
  event: any;
  actionDisplay: any;
  action: any;
  id: any;
  data: any;
  currencyZAR: any;
  allowModify = false;
  currency = "ZAR";
  tabs: tab[];
  loadingVisible = false;
  //preview
  supplier = null;
  requestedBy = null;
  placedBy = null;
  dateApproved: any;
  vat: any;
  total: any;
  additionalComment: any;
  comment: any;
  builderVisible = false;
  capturedItems: any;
  listedItems: string[] = [];
  OnceOffItems: string[] = [];
  invoicedItems: any;
  vatperc: any;
  invoiceid: any;

  AddUpdateInvoiceBtn: any = {
    text: "Submit",
    type: "success",
    useSubmitBehavior: true,
    validationGroup:"invoiceData",
    onClick: (e) => {
      if (e.validationGroup.validate().isValid){
        this.itemGrid.instance.saveEditData();
        this.itemGrid.instance.clearSelection();
        this.itemGrid.instance.option("focusedRowIndex", -1);
        this.AddUpdateInvoice();
      }
    }
  }

  AddUpdateInvoiceCancelBtn: any = {
    text: "Cancel",
    type: "default",
    useSubmitBehavior: true,
    onClick: () => {
      this.data = "";
      this.router.navigate([`/invoices`], { relativeTo: this.route });
    }
  }

  InvoiceBuilder: any = {
    text: "Generate Invoice",
    type: "success",
    useSubmitBehavior: false,
    onClick: () => {
      this.BuildArray();
      if (this.invoicedItems.listedItem.length > 0 || this.invoicedItems.onceOffItem.length > 0 || this.invoicedItems.service.length > 0) {
        this.builderVisible = false;
        this.AddTemplate();
      } else {
        notify("No items selected", 'error', 3000);
      }
      
    }
  }

  InvoiceBuilderCancel: any = {
    text: "Cancel",
    type: "danger",
    useSubmitBehavior: false,
    onClick: () => {
      this.data = "";
      this.router.navigate([`/invoices`], { relativeTo: this.route });
    }
  }

  @ViewChild('content') content;
  @ViewChild("invoiceitems", { static: false }) itemGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private globalMethodsService: GlobalMethodsService, private orderService: OrdersService, private router: Router, private route: ActivatedRoute, private authService: AuthenticationService, private stockService: StockService) {
    this.action = router.getCurrentNavigation().extras.state.action;
    this.id = router.getCurrentNavigation().extras.state.id;
    this.GetVat();
    this.currencyZAR = currencyZAR;
    /* Declaring */
    this.invoicedItems = new toBeInvoicedItems();
    this.invoicedItems.listedItem = Array<number>();
    this.invoicedItems.onceOffItem = Array<number>();
    this.invoicedItems.service = Array<number>();
    this.invoicedItems.InternalOrderId = this.id;
    this.invoicedItems.action = this.action;
    /* --- */
    switch (this.action) {
      case 1:
        console.log(this.data);
        this.GetPopupData();
        break;
      case 2: this.EditTemplate();
        break;
    }
    
    this.tabs = [
      { text: "Listed Items" },
      { text: "Once-Off Items" },
      { text: "Services" },
      { text: "Summary" },
    ];

    this.DepartmentForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Department',
      }),
      paginate: true
    }

    this.GLCodeForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/GLCodes',
      }),
      paginate: true
    }

    this.allowModify = this.authService.HavePermission(73);
  }

  BuildArray() {
    this.capturedItems.invoiceableGrnItems.forEach((value) => {
      value.grnItems.forEach((item) => {
        if (item.itemChecked) {
          this.invoicedItems.listedItem.push(item.id);
        }
      })
    });
    this.capturedItems.invoiceableGrnItems.forEach((value) => {
      value.grnonceOffItems.forEach((item) => {
        if (item.itemChecked) {
          this.invoicedItems.onceOffItem.push(item.id);
        }
      })
    });
    this.capturedItems.invoiceableServices.forEach((item) => {
      if (item.itemChecked) {
        this.invoicedItems.service.push(item.id);
      }
    });
  }

  GetPopupData() {
    this.orderService.GetInvoiceableItems(this.id).subscribe(data => {
      if (data != null) {
        this.capturedItems = data;        
        //set the checkboxes to default
        this.capturedItems.invoiceableGrnItems.forEach((value) => {
          value.grnItems.forEach((item) => {
            item.itemChecked = false;
          })
        });
        this.capturedItems.invoiceableGrnItems.forEach((value) => {
          value.grnonceOffItems.forEach((item) => {
            item.itemChecked = false;
          })
        });
        this.capturedItems.invoiceableServices.forEach((item) => {
          item.itemChecked = false;
        });
        //

        this.builderVisible = true;
      }
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  GetVat() {
    this.stockService.GetVat().subscribe(data => {
      if (data != null) {
        this.vatperc = data;
      }
    });
  }

  AddTemplate() {
    this.orderService.GetInvoiceInternalOrderData(this.invoicedItems).subscribe(data => {
      this.data = data;
      if (this.data != undefined) {
        //Summary
        this.requestedBy = this.data.internalOrder.requestByFullName;
        this.placedBy = this.data.internalOrder.placedByFullName;
        this.supplier = this.data.internalOrder.supplierFullName;
        this.dateApproved = this.data.internalOrder.dateApproved;
        this.vat = this.data.internalOrder.vat;
        this.total = this.data.internalOrder.total;
        this.comment = this.data.internalOrder.comment;
        this.additionalComment = this.data.internalOrder.internalComment;

        this.actionDisplay = "Add Invoice for Internal Order: #" + this.id;
        this.currency = this.data.internalOrder.supplierCurrency;        

        this.data.invoice.invoiceItems.forEach((value) => {
          value.invoicePrice = value.itemValue;  
          value.getTotal = value.quantity * value.invoicePrice;        
                  
          this.data.invoice.expectedTotal += value.quantity * value.invoicePrice;
          this.data.invoice.total += value.quantity * value.itemValue;
          var calvat = value.quantity * value.invoicePrice;
          if (value.vatAppl == true) {            
            this.data.invoice.vat += calvat * this.vatperc / 100;
          }
        });
        this.data.invoice.invoiceOnceOffItems.forEach((value) => {
          value.invoicePrice = value.itemValue;  
          value.getTotal = value.quantity * value.invoicePrice;          
                 
          this.data.invoice.expectedTotal += value.quantity * value.invoicePrice;
          this.data.invoice.total += value.quantity * value.itemValue;
          var calvat = value.quantity * value.invoicePrice;
          if (value.vatAppl == true) {
            this.data.invoice.vat += calvat * this.vatperc / 100;
          }
        });
        this.data.invoice.invoiceServiceItems.forEach((value) => {
          value.invoicePrice = value.itemValue;   
          value.getTotal = value.quantity * value.invoicePrice;
                 
          this.data.invoice.expectedTotal += value.quantity * value.invoicePrice;
          this.data.invoice.total += value.quantity * value.itemValue;
          var calvat = value.quantity * value.invoicePrice;
          if (value.vatAppl == true) {
            this.data.invoice.vat += calvat * this.vatperc / 100;
          }
        });        
      }
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
      
  }

  EditTemplate() {
    this.orderService.GetInvoiceInternalOrderData(this.invoicedItems).subscribe(data => {
      this.data = data;
      if (this.data != undefined) {
        //Summary
        this.requestedBy = this.data.internalOrder.requestByFullName;
        this.placedBy = this.data.internalOrder.placedByFullName;
        this.supplier = this.data.internalOrder.supplierFullName;
        this.dateApproved = this.data.internalOrder.dateApproved;
        this.vat = this.data.internalOrder.vat;
        this.total = this.data.internalOrder.total;
        this.comment = this.data.internalOrder.comment;
        this.additionalComment = this.data.internalOrder.internalComment;

        this.actionDisplay = "Update Invoice #" + this.id + " for Internal Order: #" + this.data.internalOrder.id;
        this.currency = this.data.internalOrder.supplierCurrency;

        this.data.invoice.invoiceItems.forEach((value) => {             
          if (value.invoicePrice) {
            value.getTotal = value.quantity * value.invoicePrice;
            var calvat = value.quantity * value.invoicePrice;
          } else {
            value.getTotal = value.quantity * value.itemValue;
            var calvat = value.quantity * value.itemValue;
          }
          this.data.invoice.expectedTotal += value.getTotal; 
          this.data.invoice.total += value.quantity * value.itemValue;
          if (value.vatAppl == true) {
            this.data.invoice.vat += calvat * this.vatperc / 100;
          }
        });
        this.data.invoice.invoiceOnceOffItems.forEach((value) => {                          
          if (value.invoicePrice) {
            value.getTotal = value.quantity * value.invoicePrice;
            var calvat = value.quantity * value.invoicePrice;
          } else {
            value.getTotal = value.quantity * value.itemValue;
            var calvat = value.quantity * value.itemValue;
          }
          this.data.invoice.expectedTotal += value.getTotal; 
          this.data.invoice.total += value.quantity * value.itemValue;
          if (value.vatAppl == true) {
            this.data.invoice.vat += calvat * this.vatperc / 100;
          }
        });
        this.data.invoice.invoiceServiceItems.forEach((value) => {                            
          if (value.invoicePrice) {
            value.getTotal = value.quantity * value.invoicePrice;
            var calvat = value.quantity * value.invoicePrice;
          } else {
            value.getTotal = value.quantity * value.itemValue;
            var calvat = value.quantity * value.itemValue;
          }
          this.data.invoice.expectedTotal += value.getTotal; 
          this.data.invoice.total += value.quantity * value.itemValue;
          if (value.vatAppl == true) {
            this.data.invoice.vat += calvat * this.vatperc / 100;
          }
        });
      }
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
    
  }

  onSaved(e) {
    this.data.invoice.expectedTotal = 0;
    this.data.invoice.total = 0;
    this.data.invoice.vat = 0;
    this.data.invoice.invoiceItems.forEach((value) => {
      value.getTotal = value.quantity * value.invoicePrice;
      this.data.invoice.expectedTotal += value.getTotal;
      this.data.invoice.total += value.quantity * value.itemValue;
      var calvat = value.quantity * value.invoicePrice;
      if (value.vatAppl == true) {
        this.data.invoice.vat += calvat * this.vatperc / 100;
      }
    });
    this.data.invoice.invoiceOnceOffItems.forEach((value) => {
      value.getTotal = value.quantity * value.invoicePrice;
      this.data.invoice.expectedTotal += value.getTotal;
      this.data.invoice.total += value.quantity * value.itemValue;
      var calvat = value.quantity * value.invoicePrice;
      if (value.vatAppl == true) {
        this.data.invoice.vat += calvat * this.vatperc / 100;
      }
    });
    this.data.invoice.invoiceServiceItems.forEach((value) => {
      value.getTotal = value.quantity * value.invoicePrice;
      this.data.invoice.expectedTotal += value.getTotal;
      this.data.invoice.total += value.quantity * value.itemValue;
      var calvat = value.quantity * value.invoicePrice;
      if (value.vatAppl == true) {
        this.data.invoice.vat += calvat * this.vatperc / 100;
      }
    });
  }

  AddUpdateInvoice() {
    this.loadingVisible = true;
    this.orderService.AddUpdateInvoice(this.data.invoice).subscribe(data => {
      this.loadingVisible = false;
      this.invoiceid = data;
      notify('Invoice Updated Internal INV: ' + this.invoiceid + ' | Supplier INV: ' + this.data.invoice.invoiceNumber, 'success', 6000);
      this.router.navigate([`/invoices`], { relativeTo: this.route });
    }, subError => {
      this.loadingVisible = false;
      notify(subError.error, 'error', 2000);
    });
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

  getRemaining(rowData) {
    if (rowData) {
      //return rowData.requiredQuantity - rowData.quantity;
      return rowData.requiredQuantity - rowData.receivedQuantity - rowData.quantity;
      //previous captured items
      //return rowData.requiredQuantity - rowData.receivedQuantity
    } else {
      return "";
    }
  }

  setCellValueQuantity(newData, value, currentRowData) {
    if (currentRowData.requiredQuantity) {
      newData.quantity = value;
    }
  }

  setInvoiceTotal(newData, value, currentRowData) {
    if (currentRowData.itemValue) {
      newData.invoicePrice = value;
      this.data.invoice.total += newData.invoicePrice;
    }
  }

  autoAddService(rowData) {
    if (rowData) {
      return rowData.requiredQuantity;
    } else {
      return "";
    }
  }

  getGLName(rowData) {
    if (rowData) {
      return rowData.code + " - " + rowData.name;
    } else {
      return "";
    }
  }

  onEditorPreparingChild(e) {
    if (e.parentType === "dataRow") {
      switch (e.dataField) {
        case "glcodeId":
        case "departmentId":{
          e.editorOptions.onOpened = e2 => {
            e2.component._popup.option('width', 400);
            e2.component._popup.on(args => {
              args.component.option('width', 400);
            });
            e2.component._popup.off(args => {
              args.component.option('width', 400);
            });
          };
          break;
        }
      }
    }
  }

  checkComparison() {
    return 0;
  }

  getTotalUomReceived(rowData) {
    if (rowData) {
      return rowData.grnQtyTotal * rowData.packSize;
    } else {
      return "";
    }
  }

  getTotal(rowData) {
    if (rowData) {
      if (rowData.invoicePrice) {
        return rowData.invoicePrice * rowData.quantity;
      } else {
        return rowData.itemValue * rowData.quantity;
      }
      
    } else {
      return "";
    }
  }

  getTotalService(rowData) {
    if (rowData) {
      if (rowData.invoicePrice) {
        return rowData.invoicePrice * rowData.quantity;
      } else {
        return rowData.itemValue * rowData.quantity;
      }      
    } else {
      return "";
    }
  }
}
