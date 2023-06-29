import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import notify from 'devextreme/ui/notify';
import { AuthenticationService } from '../../../core/services/auth.service';
import { BarcodesService } from '../../../core/services/barcodes.service';
import { DashboardService } from '../../../core/services/dashboard.service';
import { EventService } from '../../../core/services/event.service';
import { GlobalMethodsService } from '../../../core/services/global-methods.service';
import { ChartType } from './dashboard.model';

@Component({
  selector: 'app-default-stores',
  templateUrl: './default-stores.component.html',
  styleUrls: ['./default-stores.component.scss']
})

export class DefaultStoresComponent implements OnInit {
  dataSource: any;
  isVisible: string;
  emailSentBarChart: ChartType;
  monthlyEarningChart: ChartType;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  userId: any;
  flaggedItemsObj: any;
  PriceLookUpBadgeObj: any;
  PrintingBadgeObj: any;
  scrollvalue = "never";
  nativevalue = false;
  todaysDeliveriesObj: any;
  upcomingDeliveriesObj: any;
  lateDeliveriesObj: any;
  selectedTodaysD: any;
  selectedUpcomingD: any;
  selectedLateD: any;
  IOTitle: any;
  IOId: any;
  ApproverLoggedIn = true;
  popupVisibleToday = false;
  popupVisibleUpcomming = false;
  popupVisibleLate = false;
  loadingVisible = false;

  @ViewChild('content') content;
  @ViewChild(DxDataGridComponent, { static: false }) dataGrid: DxDataGridComponent;
  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private eventService: EventService, private authService: AuthenticationService, private dashboardService: DashboardService, private router: Router, private route: ActivatedRoute, private globalMethodsService: GlobalMethodsService, private barcodesService: BarcodesService) {
    this.authService.currentUserSubject.subscribe(user => {
      if (user) {
        this.userId = user.id
      }
    });

    this.authService.SetSite("store");

    this.dataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/PrintBarcode/getPrintBarcode'
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.authService.getToken()
          };
        }*/
      }),
      paginate: true
    }

    this.barcodesService.getTodaysDeliveries().subscribe(todaysDeliveries => {
      this.todaysDeliveriesObj = todaysDeliveries;
    });

    this.barcodesService.getUpcommingDeliveries().subscribe(upcomingDeliveries => {
      this.upcomingDeliveriesObj = upcomingDeliveries;
    });

    this.barcodesService.getLateDeliveries().subscribe(lateDeliveries => {
      this.lateDeliveriesObj = lateDeliveries;
    });

    setInterval(() => {
      this.getPriceLookupBadgeContent();
      this.getPrintingBadgeContent();
    }, globalMethodsService.ONE_MINUTE_IN_MILLIS);
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

  
  calculateCellValue(rowData) {
    return 'STQ' + rowData.barcode.slice(3).padStart(9, '0');
  }

  changeLayout(layout: string) {
    this.eventService.broadcast('changeLayout', layout);
  }

  navigatePrintBarcode() {
    this.router.navigate([`/stores/print-barcode`]);
  }

  getPriceLookupBadgeContent() {
    this.dashboardService.getFlaggedItems().subscribe(FlaggedItems => this.flaggedItemsObj = FlaggedItems);

    this.dashboardService.getPriceLookupBadge().subscribe(PriceLookUpBadge => {

      this.PriceLookUpBadgeObj = PriceLookUpBadge;
      this.globalMethodsService.priceLookBadgeRefresh(true);
     
    });
  }

  getPrintingBadgeContent() {

    this.dashboardService.getPrintingBadge().subscribe(PrintingBadge => {

      this.PrintingBadgeObj = PrintingBadge;
      this.globalMethodsService.printingBadgeRefresh(true);

    });
  }

  barcodeOnClick(e) {
    if (e.rowType === "data" && e.column.caption == "Print Barcode") {
      
      this.barcodesService.Printing([{ Id: e.data.id, Code: e.data.code, Barcode: e.data.barcode }]).subscribe(data => {
        if (data != null && data != undefined) {
          this.barcodesService.PrintingBarcode(e.data.id).subscribe(data => {
            if (data != null && data != undefined) {
              this.dataGrid.instance.getDataSource().reload();
            }
          }, subError => {
            notify(subError, 'error', 5000);
          });
        }
      }, subError => {
        notify(subError, 'error', 5000);
      });

      
    }
  }
 
  openTodaysDeliveries(todaysDeliveries) {
    this.selectedTodaysD = todaysDeliveries;
    this.IOTitle = "Pending Delivery of Internal Order #" + todaysDeliveries.id;
    this.IOId = todaysDeliveries.id;

    if (this.userId == todaysDeliveries.approveById) {
      this.ApproverLoggedIn = true;
    } else {
      this.ApproverLoggedIn = false;
    }

    this.popupVisibleToday = true;   
  }

  openUpcomingDeliveries(upcomingDeliveries) {
    this.selectedUpcomingD = upcomingDeliveries;
    this.IOTitle = "Pending Delivery of Internal Order #" + upcomingDeliveries.id;
    this.IOId = upcomingDeliveries.id;

    if (this.userId == upcomingDeliveries.approveById) {
      this.ApproverLoggedIn = true;
    } else {
      this.ApproverLoggedIn = false;
    }

    this.popupVisibleUpcomming = true;
  }

  openLateDeliveries(lateDeliveries) {
    this.selectedLateD = lateDeliveries;
    this.IOTitle = "Pending Delivery of Internal Order #" + lateDeliveries.id;
    this.IOId = lateDeliveries.id;

    if (this.userId == lateDeliveries.approveById) {
      this.ApproverLoggedIn = true;
    } else {
      this.ApproverLoggedIn = false;
    }

    this.popupVisibleLate = true;
  }

  sendReminder(e,id,type) {
    e.stopPropagation();
    this.loadingVisible = true;
    this.barcodesService.DeliveryReminderEmailing(id, type).subscribe(data => {
      if (data != null && data != undefined) {
        this.loadingVisible = false;
        notify("Reminder Emailed", 'success',5000);
      }
    }, subError => {
      this.loadingVisible = false;
      notify(subError, 'error', 5000);
    });
  }
}
