import { DOCUMENT } from '@angular/common';
import { Component, EventEmitter, Inject, OnInit, Output, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import notify from 'devextreme/ui/notify';
import * as moment from 'moment';
import { CookieService } from 'ngx-cookie-service';
import { timer } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthenticationService } from '../../core/services/auth.service';
import { DashboardService } from '../../core/services/dashboard.service';
import { LanguageService } from '../../core/services/language.service';


export interface CurrencyApi {
  base: string
  end_date: string
  fluctuation: boolean
  rates: Rates
  start_date: string
  success: boolean
  message: string
}

export interface Rates {
  ZAR: Zar
}

export interface Zar {
  change: number
  change_pct: number
  end_rate: number
  start_rate: number
}

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.scss']
})

/**
 * Topbar component
 */
export class TopbarComponent implements OnInit {
  birthdayObj: any;
  ordersObj: any;
  element;
  cookieValue;
  flagvalue;
  countryName;
  valueset;
  searchValue = '';
  username;
  valueUSD = "";
  valueEUR = "";
  prevValueUSD = "";
  prevValueEUR = "";
  bxUSDCurrency: any;
  bxEURCurrency: any;
  spanUSDCurrency: any;
  spanEURCurrency: any;
  eur: number;
  usd: number;
  usdZar: number;
  eurZar: number;
  arrowUp = "bx bx-up-arrow-alt";
  arrowDown = "bx bx-down-arrow-alt";
  spanUp = "currency-span-down";
  spanDown = "currency-span-up";

  constructor(@Inject(DOCUMENT) private document: any, private router: Router, private authService: AuthenticationService,
    public languageService: LanguageService,
    public translate: TranslateService,
    private dashboardService: DashboardService,
    public _cookiesService: CookieService) {

    const myHeaders = new Headers();
    myHeaders.append("apikey", "eKqcDRcH05vqm6yM4GNg7X1UgcsSWQtY");

    const currencyApiCalls = timer(1000, 18000000);

    currencyApiCalls.subscribe(r => {
      let start_date = moment();
      start_date = moment().subtract(1, 'day');
      let end_date = new Date();
      let prev = start_date.format("YYYY-MM-DD");
      const currencyApiUrl = "https://api.apilayer.com/exchangerates_data/fluctuation?"

      const [onlyDate] = end_date.toISOString().split('T')

      const requestOptions = {
        method: 'GET',
        redirect: 'follow',
        headers: myHeaders
      } as RequestInit;

      //USD, EUR
      const symbols = ['ZAR'];
      const usdBase = 'USD';
      const eurBase = 'EUR';

      fetch(`${currencyApiUrl}start_date=${prev}&end_date=${onlyDate}&symbols=${symbols}&base=${usdBase}`, requestOptions)
        .then(response => {
          if (response.status != 200) {
            return response.text();
          }
          return response.text();
        })
        .then(result => {
          const currency: CurrencyApi = JSON.parse(result);

          if (currency.message !== "" && currency.message !== undefined) {
            this.usd = 0;
            this.usdZar = 0;
            /*this.showToast(currency.message);*/
            return;
          }
          this.usd = currency.rates.ZAR.change;
          this.usdZar = currency.rates.ZAR.end_rate;
        })
        .catch(error => console.log('error', error));

      fetch(`${currencyApiUrl}start_date=${prev}&end_date=${onlyDate}&symbols=${symbols}&base=${eurBase}`, requestOptions)
        .then(response => {
          if (response.status != 200) {
            return response.text();
          }
          return response.text();
        })
        .then(result => {
          const currency: CurrencyApi = JSON.parse(result);
          if (currency.message !== "" && currency.message !== undefined) {
            this.eur = 0;
            this.eurZar = 0;
            return;
          }
          this.eur = currency.rates.ZAR.change;
          this.eurZar = currency.rates.ZAR.end_rate;
        })
        .catch(error => console.log('error', error));
    })

    this.authService.currentUserSubject.subscribe(user => {
      if (user) {
        this.username = user.username
      }
    })

    this.dashboardService.getBirthdays().subscribe(Birthdays => {
      this.birthdayObj = Birthdays;
    });

    this.dashboardService.getOrders().subscribe(Orders => {
      this.ordersObj = Orders;
    });
  }
  showToast(message: string) {
    notify(message, "error", 2000);
  }


  listLang = [
    { text: 'English', flag: 'assets/images/flags/us.jpg', lang: 'en' },
    { text: 'Spanish', flag: 'assets/images/flags/spain.jpg', lang: 'es' },
    { text: 'German', flag: 'assets/images/flags/germany.jpg', lang: 'de' },
    { text: 'Italian', flag: 'assets/images/flags/italy.jpg', lang: 'it' },
    { text: 'Russian', flag: 'assets/images/flags/russia.jpg', lang: 'ru' },
  ];

  openMobileMenu: boolean;

  @Output() settingsButtonClicked = new EventEmitter();
  @Output() mobileMenuButtonClicked = new EventEmitter();
  @Output() searchValueChanged = new EventEmitter<String>();

  ngOnInit() {
    this.openMobileMenu = false;
    this.element = document.documentElement;

    this.cookieValue = this._cookiesService.get('lang');
    const val = this.listLang.filter(x => x.lang === this.cookieValue);
    this.countryName = val.map(element => element.text);
    if (val.length === 0) {
      if (this.flagvalue === undefined) { this.valueset = 'assets/images/flags/us.jpg'; }
    } else {
      this.flagvalue = val.map(element => element.flag);
    }
  }

  isNegative(num) {
    if (num == 0) {

    }
    if (Math.sign(num) === -1) {
      return true;
    }
    return false;
  }
  getUSDClass() {
    return this.isNegative(this.usd) ? this.arrowDown : this.arrowUp;
  }

  getEURClass() {
    return this.isNegative(this.eur) ? this.arrowDown : this.arrowUp;
  }

  getUSDSpan() {
    return this.isNegative(this.usd) ? this.spanDown : this.spanUp;
  }

  getEURSpan() {
    return this.isNegative(this.eur) ? this.spanDown : this.spanUp;
  }

  onKey(event: any) { // without type info
    this.searchValue = event.target.value;
    this.searchValueChanged.emit(this.searchValue);
  }

  setLanguage(text: string, lang: string, flag: string) {
    this.countryName = text;
    this.flagvalue = flag;
    this.cookieValue = lang;
    this.languageService.setLanguage(lang);
  }

  /**
   * Toggles the right sidebar
   */
  toggleRightSidebar() {
    this.settingsButtonClicked.emit();
  }

  /**
   * Toggle the menu bar when having mobile screen
   */
  toggleMobileMenu(event: any) {
    event.preventDefault();
    this.mobileMenuButtonClicked.emit();
  }

  /**
   * Logout the user
   */
  logout() {
    this.authService.logout();
  }

  changePassword() {
    this.router.navigate(['/account/setpassword']);
  }

  /**
   * Fullscreen method
   */
  fullscreen() {
    document.body.classList.toggle('fullscreen-enable');
    if (
      !document.fullscreenElement && !this.element.mozFullScreenElement &&
      !this.element.webkitFullscreenElement) {
      if (this.element.requestFullscreen) {
        this.element.requestFullscreen();
      } else if (this.element.mozRequestFullScreen) {
        /* Firefox */
        this.element.mozRequestFullScreen();
      } else if (this.element.webkitRequestFullscreen) {
        /* Chrome, Safari and Opera */
        this.element.webkitRequestFullscreen();
      } else if (this.element.msRequestFullscreen) {
        /* IE/Edge */
        this.element.msRequestFullscreen();
      }
    } else {
      if (this.document.exitFullscreen) {
        this.document.exitFullscreen();
      } else if (this.document.mozCancelFullScreen) {
        /* Firefox */
        this.document.mozCancelFullScreen();
      } else if (this.document.webkitExitFullscreen) {
        /* Chrome, Safari and Opera */
        this.document.webkitExitFullscreen();
      } else if (this.document.msExitFullscreen) {
        /* IE/Edge */
        this.document.msExitFullscreen();
      }
    }
  }

  navigateDash() {
    this.router.navigate([`/dashboard`]);
  }

  navigateOrders() {
    this.router.navigate([`/orders`]);
  }
  doCurrencyCheck(current, previous, bxElement, spanElement) {
    //need to do deletes start here
    //dont forget about 2nd currencyds
    if (previous < current) {
      bxElement.className = "";
      spanElement.className = "";
      bxElement.classList.add('bx');
      bxElement.classList.add('bx-up-arrow-alt');
      spanElement.classList.add('currency-span-up');
      //green text and arrow up
    } else if (previous > current) {
      bxElement.className = "";
      spanElement.className = "";
      bxElement.classList.add('bx');
      bxElement.classList.add('bx-down-arrow-alt');
      spanElement.classList.add('currency-span-down');
      //red text and down
    }
    //else {
    //  bxElement.className = "";
    //  spanElement.className = "";
    //  bxElement.classList.add('bx');
    //  bxElement.classList.add('bx-right-arrow-alt');
    //  spanElement.classList.add('');//find out more here
    //orange text and side
    /*}*/
    /*console.log(previous + " "+current);*/
  }
}
