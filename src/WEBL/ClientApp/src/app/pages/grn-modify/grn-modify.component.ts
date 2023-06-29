import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import notify from 'devextreme/ui/notify';
import { currencyZAR } from 'src/globalConstants';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { OrdersService } from '../../core/services/orders.service';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { tab } from "../models/tab";
import { grncomment } from '../../core/models/grncomment.models';

@Component({
  selector: 'app-grn-modify',
  templateUrl: './grn-modify.component.html',
  styleUrls: ['./grn-modify.component.scss']
})
export class GRNModifyComponent implements OnInit {  
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "GRN";
  event: any;
  actionDisplay: any;
  action: any;
  id: any;
  grn: any;
  data: any;
  currencyZAR: any;
  allowModify = false;
  StoreForeignDataSource: any;
  currency = "ZAR";
  tabs: tab[];
  loadingVisible = false;
  voidedBarcodes: any;
  popupVisibleBarcodeVoid = false;
  popupVisibleComment = false;
  title = "Removed Barcodes";
  UsersForeignDataSource: any;
  editor = false;
  commentVar: grncomment;

  AddUpdateGrnBtn: any = {
    text: "Submit",
    type: "success",
    useSubmitBehavior: true,
    validationGroup:"grnData",
    onClick: (e) => {
      if (e.validationGroup.validate().isValid) {
        if (this.itemGrid != undefined) {
          this.itemGrid.instance.saveEditData();
          this.itemGrid.instance.clearSelection();
          this.itemGrid.instance.option("focusedRowIndex", -1);
        }

        if (this.OnceitemGrid != undefined) {
          this.OnceitemGrid.instance.saveEditData();
          this.OnceitemGrid.instance.clearSelection();
          this.OnceitemGrid.instance.option("focusedRowIndex", -1);
        }

        let total = 0;
        this.data.grn.grnItems.forEach(q => total += q.quantity);
        this.data.grn.grnonceOffItems.forEach(t => total += t.quantity);

        if (total > 0) {
          this.AddUpdateGrn();
        } else {
          let tot = 0;
          this.data.grn.grnItems.forEach(q => tot += q.requiredQuantity - q.receivedQuantity);
          this.data.grn.grnonceOffItems.forEach(t => tot += t.requiredQuantity - t.receivedQuantity);
          if (total == 0 && tot == 0) {
            this.AddUpdateGrn();
          } else {
            notify("Enter a received quantity for one item.", 'error', 5000);
            e.cancel = true;
          }       
          return;
        }           
      }
    }
  }

  AddUpdateGrnCancelBtn: any = {
    text: "Cancel",
    type: "default",
    useSubmitBehavior: true,
    onClick: () => {
      this.data = "";
      this.router.navigate([`/grn`], { relativeTo: this.route }); 
    }
  }

  CancelCommentButtonOptions: any = {
    text: "Cancel",
    type: "default",
    onClick: (e) => {
      this.popupVisibleComment = false;
    }
  }


  SaveCommentButtonOptions: any = {
    text: "Save",
    type: "success",
    onClick: (e) => {
      if (this.commentVar.grntable == "ListedItems") {
        this.data.grn.grnItems.find(r => r.internalOrderItemId == this.commentVar.id).comment = this.commentVar.comment;
      } else if (this.commentVar.grntable == "OnceOffItem") {
        this.data.grn.grnonceOffItems.find(r => r.onceOffItemsId == this.commentVar.id).comment = this.commentVar.comment;
      }
      this.popupVisibleComment = false;
    }
  }

  @ViewChild('content') content;
  @ViewChild("grnitems", { static: false }) itemGrid: DxDataGridComponent;
  @ViewChild("grnonceOffItems", { static: false }) OnceitemGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private globalMethodsService: GlobalMethodsService, private orderService: OrdersService, private router: Router, private route: ActivatedRoute,private authService: AuthenticationService) {
    this.action = router.getCurrentNavigation().extras.state.action;
    this.id = router.getCurrentNavigation().extras.state.id;
    this.GetGrnInternalOrderData();
    this.currencyZAR = currencyZAR;
    this.allowModify = this.authService.HavePermission(73);

    this.StoreForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Stores',
      }),
      sort: [
        { selector: "Name", desc: false }
      ],
      paginate: true
    }

    this.UsersForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/FullEmployeeName',
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.token
          };
        }*/
      }),
      sort: [
        { selector: "FullName", desc: false }
      ],
      paginate: true
    }

    this.tabs = [
      { text: "Listed Items" },
      { text: "Once-Off Items" }
    ];
  }

  GetGrnInternalOrderData() {
    this.orderService.GetGrnInternalOrderData(this.id, this.action).subscribe(data => {
      this.data = data;
      if (this.data != undefined) {
        switch (this.action) {
          case 1: this.actionDisplay = "Add GRN for Internal Order: #" + this.id;
            this.authService.currentUserSubject.subscribe(user => {
              if (user) {
                this.data.grn.capturerId = user.id;
                this.data.grn.editorId = user.id;
              }
            });
            //get the grn for this internal order
            this.orderService.GetLatestGrn(this.id).subscribe(grnreturn => {
              if (grnreturn != undefined) {
                this.grn = grnreturn;
                this.data.grn.grnNumber = this.data.internalOrder.id + "_" + this.grn;
              }              
            }, subError => {
              notify(subError.error, 'error', 5000);
            });

            this.currency = this.data.internalOrder.supplierCurrency;            

            break;
          case 2: this.actionDisplay = "Update GRN #" + this.id + " for Internal Order: #" + this.data.internalOrder.id;
            this.currency = this.data.internalOrder.supplierCurrency;
            this.editor = true;
            this.authService.currentUserSubject.subscribe(user => {
              if (user) {
                this.data.grn.editorId = user.id;
              }
            });
            break;
        }
      }        
    }, subError => {
        notify(subError.error, 'error', 5000);
    });
  }

  AddUpdateGrn() {
    this.loadingVisible = true;
    this.orderService.AddUpdateGrn(this.data.grn).subscribe(data => {
      this.loadingVisible = false;
      this.voidedBarcodes = data;
      if (this.voidedBarcodes.length > 0) {
        this.popupVisibleBarcodeVoid = true;
      } else {
        notify('GRN Updated', 'success', 2000);
        this.router.navigate([`/grn`], { relativeTo: this.route });
      }
    }, subError => {
      this.loadingVisible = false;
      notify(subError.error, 'error', 2000);
    });
  }


  Close() {
    notify('GRN Updated', 'success', 2000);
    this.router.navigate([`/grn`], { relativeTo: this.route });
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
      return rowData.requiredQuantity - rowData.receivedQuantity - rowData.quantity;
    } else {
      return "";
    }
  }

  getRemainingOnce(rowData) {
    if (rowData) {
      return rowData.requiredQuantity - rowData.receivedQuantity - rowData.quantity;
    } else {
      return "";
    }
  }

  getTotalUomOutstanding(rowData) {
    if (rowData) {
      //return rowData.requiredQuantity * rowData.packSize;
      return (rowData.requiredQuantity - rowData.receivedQuantity - rowData.quantity) * rowData.packSize
    } else {
      return "";
    }
  }

  getTotalUomReceived(rowData) {
    if (rowData) {
      return rowData.quantity * rowData.packSize;
    } else {
      return "";
    }
  }

  getTotalUomOutstandingOnce(rowData) {
    if (rowData) {      
      return (rowData.requiredQuantity - rowData.receivedQuantity - rowData.quantity) * rowData.packSize
    } else {
      return "";
    }
  }

  getTotalUomReceivedOnce(rowData) {
    if (rowData) {
      return rowData.quantity * rowData.packSize;
    } else {
      return "";
    }
  }

  setCellValueQuantity(newData, value, currentRowData) {   
    if (currentRowData.requiredQuantity) {
      newData.quantity = value;
      newData.totalUomOutstanding = (currentRowData.requiredQuantity - value) * currentRowData.packSize;
    }
  }

  setCellValueQuantityOnce(newData, value, currentRowData) {
    if (currentRowData.requiredQuantity) {
      newData.quantity = value;
      newData.totalUomOutstanding = (currentRowData.requiredQuantity - value) * currentRowData.packSize;
    }
  }

  onEditorPreparingChild(e) {
    if (e.parentType === "dataRow") {
      if (e.dataField == "storeLocationId") {
        e.editorOptions.onOpened = e2 => {
          e2.component._popup.option('width', 400);
          e2.component._popup.on(args => {
            args.component.option('width', 400);
          });
          e2.component._popup.off(args => {
            args.component.option('width', 400);
          });
        };
      }
    }
  }

  validateQty(e) {
    console.log(e);
    if (e.value > (e.data.requiredQuantity - e.data.receivedQuantity)) {
      return false;
    }
    return true;
  }

  ListedCommentOnClick(e) {
    if (e.rowType === "data" && e.column.caption == "Comment") {
      this.commentVar = new grncomment();
      this.commentVar.comment = e.data.comment;
      this.commentVar.id = e.data.internalOrderItemId;
      this.commentVar.grntable = "ListedItems";
      this.popupVisibleComment = true;

    }
  }

  OnceOffCommentOnClick(e) {
    if (e.rowType === "data" && e.column.caption == "Comment") {
      this.commentVar = new grncomment();
      this.commentVar.comment = e.data.comment;
      this.commentVar.id = e.data.onceOffItemsId;
      this.commentVar.grntable = "OnceOffItem";
      this.popupVisibleComment = true;

    }
  }

  ListedCommentCellPrepared(e) {
    if (e.rowType === "data" && e.column.caption == "Comment") {
      e.cellElement.style.textAlign = "center";
      this.data.grn.grnItems.forEach(q => {
        if (q.comment) {
          e.cellElement.style.color = "#1f7556";
          e.cellElement.style.backgroundColor = "#d6f3e9";          
        }
      });      
    }
  }

  OnceOffCommentCellPrepared(e) {
    if (e.rowType === "data" && e.column.caption == "Comment") {
      e.cellElement.style.textAlign = "center";
      this.data.grn.grnonceOffItems.forEach(q => {
        if (q.comment) {
          e.cellElement.style.color = "#1f7556";
          e.cellElement.style.backgroundColor = "#d6f3e9";          
        }
      });
    }
  }
}
