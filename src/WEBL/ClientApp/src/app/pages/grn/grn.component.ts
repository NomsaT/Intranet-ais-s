import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import notify from 'devextreme/ui/notify';
import { grnData } from '../../core/models/grnData.models';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { OrdersService } from '../../core/services/orders.service';

@Component({
  selector: 'app-grn',
  templateUrl: './grn.component.html',
  styleUrls: ['./grn.component.scss']
})
export class GRNComponent implements OnInit {
  dataAdd: grnData = new grnData();
  dataEdit: grnData = new grnData();
  dataDelete: grnData = new grnData();
  InternalOrdersForeignDataSource: any;
  InternalOrdersForeignDataSourceEdit: any;
  InternalOrdersForeignDataSourceDlt: any;
  GrnForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  pageName: string = "GRN";
  event: any;
  allowModify = false;
  popupVisible = false;
  popupVisibleBarcodeVoid = false;
  id: any;
  action: any;
  popupDeleteData: any;
  baseUrl: any;
  internalOrderId = null;
  enableSelectGRNedit = false;
  updatedValueEdit: number;
  updatedValueDlt: number;
  enableSelectGRNdlt = false;
  voidedBarcodes: any;
  title = "Removed Barcodes";

  CaptureGRN: any = {
    icon: "plus",
    text: "Create GRN",
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

  EditGRN: any = {
    icon: "edit",
    text: " Edit GRN ",
    type: "default",
    useSubmitBehavior: true,
    onClick: (e) => {
      this.action = 2;
      if (this.dataEdit.grnEditid != undefined) {
        this.id = this.dataEdit.grnEditid;
        this.orderService.GetGrnInternalOrderData(this.id, this.action).subscribe(data => {
          if (data != null) {
            this.navigate();
          } else {
            notify("error occured", 'error', 5000);
          }
        }, subError => {
          notify(subError.error, 'error', 5000);
        });
      }
    }
  }

  DeleteGRN: any = {
    icon: "trash",
    text: "Delete GRN",
    type: "default",
    useSubmitBehavior: true,
    onClick: (e) => {
      this.action = 3;
      if (this.dataEdit.grnEditid != undefined) {
        this.id = this.dataEdit.grnEditid;
        this.orderService.GetGrnInternalOrderData(this.id, this.action).subscribe(data => {
          if (data != null) {
            this.popupDeleteData = data;
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

  GrnRemove: any = {
    text: "Delete GRN",
    type: "success",
    useSubmitBehavior: false,
    onClick: () => {
      this.id = this.dataDelete.grnDeleteid;

      this.orderService.RemoveGrn(this.id).subscribe(data => {
        if (data != null) {
          notify("GRN Deleted.", 'success', 5000);
          this.popupVisible = false;
          this.voidedBarcodes = data;
          this.popupVisibleBarcodeVoid = false;
          this.dataDelete = new grnData();
          this.GrnForeignDataSource = {
            store: AspNetData.createStore({
              key: 'id',
              loadUrl: this.baseUrl + 'api/Grn',
            }),
            paginate: true
          }
        } else {
          notify("GRN not deleted", 'error', 5000);
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
    this.setGrn = this.setGrn.bind(this);
    this.setGrnDlt = this.setGrnDlt.bind(this);

    this.InternalOrdersForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/InternalOrder/getApprovedGRNInternalOrder',
      }),
      paginate: true
    }

    this.InternalOrdersForeignDataSourceEdit = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/InternalOrder/getApprovedInternalOrderGrn',
      }),
      paginate: true
    }

    this.InternalOrdersForeignDataSourceDlt = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/InternalOrder/getApprovedInternalOrderGrn',
      }),
      paginate: true
    }

    this.GrnForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Grn',
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

  setGrn(e) {
    this.internalOrderId = e.value;
    if (e.value > 0) {
      this.enableSelectGRNedit = true;
    } else {
      this.enableSelectGRNedit = false;
      this.updatedValueEdit = 0;//resets the Select GRN Value
    }

    this.GrnForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Grn',
      }),
      filter: ["internalOrderId", "=", this.internalOrderId],
      paginate: true
    }
  }

  setGrnDlt(e) {
    this.internalOrderId = e.value;
    if (e.value > 0) {
      this.enableSelectGRNdlt = true;
    } else {
      this.enableSelectGRNdlt = false;
      this.updatedValueDlt = 0;//resets the Select GRN Value
    }
    this.GrnForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: this.baseUrl + 'api/Grn',
      }),
      filter: ["internalOrderId", "=", this.internalOrderId],
      paginate: true
    }
  }

  ngAfterViewInit() {
  }

  navigate() {
    this.router.navigate(['/grn-modify'], { state: { id: this.id, action: this.action } });
  }
}
