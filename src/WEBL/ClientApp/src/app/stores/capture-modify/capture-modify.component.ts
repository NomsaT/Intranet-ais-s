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

@Component({
  selector: 'app-capture-modify',
  templateUrl: './capture-modify.component.html',
  styleUrls: ['./capture-modify.component.scss']
})
export class CaptureModifyComponent implements OnInit {
  DiscrepencyForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Invoice";
  event: any;
  action: any;
  id: any;
  data: any;
  currencyZAR: any;

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
      this.router.navigate([`/stores/capture`], { relativeTo: this.route }); 
    }
  }

  @ViewChild('content') content;
  @ViewChild("invoiceitems", { static: false }) itemGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private globalMethodsService: GlobalMethodsService, private orderService: OrdersService, private router: Router, private route: ActivatedRoute,private authService: AuthenticationService) {
    this.action = router.getCurrentNavigation().extras.state.action;
    this.id = router.getCurrentNavigation().extras.state.id;
    this.GetInvoiceInternalOrderData();
    this.currencyZAR = currencyZAR;

    this.DiscrepencyForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Discrepency',
      }),
      paginate: true
    }
  }

  GetInvoiceInternalOrderData() {
    this.orderService.GetInvoiceInternalOrderData(this.id, this.action).subscribe(data => {
      this.data = data;        
    }, subError => {
        notify(subError.error, 'error', 5000);
    });
  }

  AddUpdateInvoice() {
    this.orderService.AddUpdateInvoice(this.data.invoice).subscribe(data => {
      notify('Items Captured', 'success', 2000);
      this.router.navigate([`/stores/capture`], { relativeTo: this.route }); 
    }, subError => {
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
      return rowData.requiredQuantity - rowData.receivedQuantity - rowData.quantity;
    } else {
      return "";
    }
  }

  setCellValueQuantity(newData, value, currentRowData) {   
    if (currentRowData.requiredQuantity) {
      newData.quantity = value;
    }
  }

  checkComparison() {
    return 0;
  }

}
