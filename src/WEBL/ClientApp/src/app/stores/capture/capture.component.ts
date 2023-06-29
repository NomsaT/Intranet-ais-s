import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import notify from 'devextreme/ui/notify';
import { invoiceData } from '../../core/models/invoiceData.models';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { OrdersService } from '../../core/services/orders.service';

@Component({
  selector: 'app-capture',
  templateUrl: './capture.component.html',
  styleUrls: ['./capture.component.scss']
})
export class CaptureComponent implements OnInit {
  data: invoiceData = new invoiceData();
  InternalOrdersForeignDataSource: any;
  InvoicesForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "Capture";
  event: any;
  allowModify = false;
  popupVisible = false;
  id: any;
  action: any;
  popupDeleteData: any;
  baseUrl: any;

  CaptureOrder: any = {
    icon: "plus",
    text: "Capture Order",
    type: "default",
    useSubmitBehavior: true,
    onClick: (e) => {
      this.action = 1;
      this.id = this.data.internalOrderid;
      if (this.id != undefined) {
        this.navigate();
      }
      
    }
  }

  @ViewChild('content') content;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private orderService: OrdersService, private router: Router, private route: ActivatedRoute, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.baseUrl = baseUrl;
    this.InternalOrdersForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/InternalOrder/getApprovedInternalOrder',
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

    //this.allowModify = this.authService.HavePermission(73);
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

  getInternalOrder(rowData) {
    if (rowData) {
      return rowData.id + " - " + rowData.supplierFullName;
    } else {
      return "";
    }
  }

  navigate() {
    this.router.navigate(['/stores/capture-modify'], { state: { id: this.id, action: this.action } });
  }
}
