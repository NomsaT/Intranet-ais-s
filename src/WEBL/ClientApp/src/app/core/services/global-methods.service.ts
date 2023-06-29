import { EventEmitter, Inject, Injectable, Output } from '@angular/core';
import notify from 'devextreme/ui/notify';
import { DashboardService } from './dashboard.service';

@Injectable({ providedIn: 'root' })
export class GlobalMethodsService {

  //#region Input & Outputs

  @Output() changePriceLookupBadge: EventEmitter<boolean> = new EventEmitter();
  @Output() changePrintingBadge: EventEmitter<boolean> = new EventEmitter();

  //#endregion


  //#region CONSTANTS

  BASEURL: string;
  ONE_MINUTE_IN_MILLIS = 60000;
  meNotifyTimer: number = 3000;


  phoneRules: any = {
    X: /^[0-9]{0,}$/
  }

  cityPattern = "^[^0-9]+$";
  namePattern: any = /^[^0-9]+$/;
  phonePattern: any = /^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$/;

  //#endregion

  //#region Variables

  priceLookupBadgeNumber: any;
  printingBadgeNumber: any;

  //#endregion

  constructor(@Inject('BASE_URL') baseUrl: string, private dashboardService: DashboardService) {
    this.BASEURL = baseUrl;
  }

  ToastErrorMessage(paMessage: string) {
    notify(paMessage, 'error', this.meNotifyTimer);
  }

  ToastSuccessMessage(paMessage: string) {
    notify(paMessage, 'success', this.meNotifyTimer);
  }

  ToastWarningMessage(paMessage: string) {
    notify(paMessage, 'warning', this.meNotifyTimer);
  }

  //#region CRUD Toast Messages

  ToastInsertSuccessMessage(paMessage: string) {
    notify(paMessage + " Added Successfully.", 'success', this.meNotifyTimer);
  }

  ToastUpdateSuccessMessage(paMessage: string) {
    notify(paMessage + " Updated Successfully.", 'success', this.meNotifyTimer);
  }

  ToastDeleteSuccessMessage(paMessage: string) {
    notify(paMessage + " Removed Successfully.", 'success', this.meNotifyTimer);
  }
  //#endregion

  priceLookBadgeRefresh(paBoolean: boolean) {

    this.dashboardService.getPriceLookupBadge().subscribe(PriceLookUpBadge => {

      this.priceLookupBadgeNumber = PriceLookUpBadge;
      this.changePriceLookupBadge.emit(paBoolean);

    });
  }

  printingBadgeRefresh(paBoolean: boolean) {
    this.dashboardService.getPrintingBadge().subscribe(PrintingBadge => {

      this.printingBadgeNumber = PrintingBadge;
      this.changePrintingBadge.emit(paBoolean);

    });
  }

static formatCurrency(amount, currencySymbol: string ) {
  return currencySymbol + parseFloat(amount).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1 ');
  
}
}
