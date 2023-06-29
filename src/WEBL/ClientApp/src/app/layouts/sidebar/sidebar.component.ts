import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, ElementRef, Input, OnChanges, OnInit, ViewChild } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import MetisMenu from 'metismenujs/dist/metismenujs';
import { AuthenticationService } from '../../core/services/auth.service';
import { DashboardService } from '../../core/services/dashboard.service';
import { EventService } from '../../core/services/event.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { MENU } from './menu';
import { MENUSTORE } from './menuStore';
import { MenuItem } from './menu.model';
import { timer } from 'rxjs';


@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})

/**
 * Sidebar component
 */
export class SidebarComponent implements OnInit, AfterViewInit, OnChanges {
  @ViewChild('componentRef') scrollRef;
  @Input() isCondensed = false;
  @Input() searchValue = '';
  menu: any;
  data: any;
  site: any;
  now: Date;

  menuItems = [];

  @ViewChild('sideMenu') sideMenu: ElementRef;
    

  constructor(private eventService: EventService, private router: Router, public translate: TranslateService, private http: HttpClient,
    private dashboardService: DashboardService, private globalMethodsService: GlobalMethodsService, private authenticationService: AuthenticationService) {
    router.events.forEach((event) => {
      if (event instanceof NavigationEnd) {
        this._activateMenuDropdown();
        this._scrollElement();
      }
    });
    setInterval(() => {

      this.pullLatestPriceLookup();
      this.pullLatestPrinting();

    }, this.globalMethodsService.ONE_MINUTE_IN_MILLIS);

    
      this.menuItems = this.filterMenuItems(JSON.parse(JSON.stringify(MENUSTORE)));
      this.globalMethodsService.changePrintingBadge.subscribe(changePrintingBadge => {
      if (changePrintingBadge) {
        this.getPrintingBadge();
      }

    });
    
      this.menuItems = this.filterMenuItems(JSON.parse(JSON.stringify(MENU)));
      this.globalMethodsService.changePriceLookupBadge.subscribe(changePriceLookupBadge => {
      if (changePriceLookupBadge) {
        this.getPriceLookupBadge();
      }

    });
    

    this.authenticationService.currentUserSubject.subscribe(user => {
      if (user) {
        this.site = user.site;
        if (user.site == "store") {
          this.menuItems = this.filterMenuItems(JSON.parse(JSON.stringify(MENUSTORE)));          
        } else {
          this.menuItems = this.filterMenuItems(JSON.parse(JSON.stringify(MENU)));
        }
        
      }
    })

  }

  //#region Get Methods

  getPriceLookupBadge() {
      let priceLookupIndex = this.menuItems.findIndex(u => u.label === 'MENUITEMS.PRICELOOKUP.TEXT');

    if (priceLookupIndex) {

      if (priceLookupIndex >=0) {
          if (this.globalMethodsService.priceLookupBadgeNumber > 0 && this.menuItems[priceLookupIndex].badge != undefined) {

          this.menuItems[priceLookupIndex].badge.text = this.globalMethodsService.priceLookupBadgeNumber;
        }
        else {
          this.menuItems[priceLookupIndex].badge.text = undefined;
        }
      }
      }

    
  }

  getPrintingBadge() {
    let printingIndex = this.menuItems.findIndex(u => u.label === 'MENUITEMS.BARCODE.TEXT');

    if (printingIndex) {
      if (printingIndex >= 0) {
         if (this.globalMethodsService.printingBadgeNumber > 0 && this.menuItems[printingIndex].badge != undefined) {

        this.menuItems[printingIndex].badge.text = this.globalMethodsService.printingBadgeNumber;
      }
      else {
       this.menuItems[printingIndex].badge.text = undefined;
      }
      }
    }
  }

  havePermission(permissions: number[]) {
    return this.authenticationService.HavePermissions(permissions);
  }

  filterMenuItems(menu: MenuItem[]) {
    for (var i = menu.length - 1; i >= 0; i--) {
      if (menu[i].permissionId && !this.havePermission(menu[i].permissionId)) {
        menu.splice(i, 1);
      } else {
        if (menu[i].subItems) {
          menu[i].subItems = this.filterMenuItems(menu[i].subItems);
          if (menu[i].subItems.length == 0) {
            menu.splice(i, 1);
          }
        }
      }
    }
    return menu;
  }

  pull() {
    this.pullLatestPriceLookup();
    this.pullLatestPrinting();
  }

  pullLatestPriceLookup() {

    this.globalMethodsService.priceLookBadgeRefresh(true);
  }

  pullLatestPrinting() {

    this.globalMethodsService.printingBadgeRefresh(true);
  }
  //#endregion



  ngOnInit() {
    timer(0, 1000).subscribe(() => {
      this.now = new Date();
    })

    this.initialize();

    this._scrollElement();

  }

  ngAfterViewInit() {
    this.menu = new MetisMenu(this.sideMenu.nativeElement);
    this._activateMenuDropdown();
  }

  toggleMenu(event) {
    event.currentTarget.nextElementSibling.classList.toggle('mm-show');
  }

  ngOnChanges() {
    if (!this.isCondensed && this.sideMenu || this.isCondensed) {
      setTimeout(() => {
        this.menu = new MetisMenu(this.sideMenu.nativeElement);
      });
    } else if (this.menu) {
      this.menu.dispose();
    }
  }
  _scrollElement() {
    setTimeout(() => {
      if (document.getElementsByClassName("mm-active").length > 0) {
        const currentPosition = document.getElementsByClassName("mm-active")[0]['offsetTop'];
        if (currentPosition > 500) {
          if (this.scrollRef.SimpleBar !== null) {
            this.scrollRef.SimpleBar.getScrollElement().scrollTop =
                        currentPosition + 300;
          }
        }  
      }
    }, 300);
  }

  /**
   * remove active and mm-active class
   */
  _removeAllClass(className) {
    const els = document.getElementsByClassName(className);
    while (els[0]) {
      els[0].classList.remove(className);
    }
  }

  /**
   * Activate the parent dropdown
   */
  _activateMenuDropdown() {
    this._removeAllClass('mm-active');
    this._removeAllClass('mm-show');
    const links = document.getElementsByClassName('side-nav-link-ref');
    let menuItemEl = null;
    // tslint:disable-next-line: prefer-for-of
    const paths = [];
    for (let i = 0; i < links.length; i++) {
      paths.push(links[i]['pathname']);
    }
    var itemIndex = paths.indexOf(window.location.pathname);
    if (itemIndex === -1) {
      const strIndex = window.location.pathname.lastIndexOf('/');
      const item = window.location.pathname.substr(0, strIndex).toString();
      menuItemEl = links[paths.indexOf(item)];
    } else {
      menuItemEl = links[itemIndex];
    }
    if (menuItemEl) {
      menuItemEl.classList.add('active');
      const parentEl = menuItemEl.parentElement;
      if (parentEl) {
        parentEl.classList.add('mm-active');
        const parent2El = parentEl.parentElement.closest('ul');
        if (parent2El && parent2El.id !== 'side-menu') {
          parent2El.classList.add('mm-show');
          const parent3El = parent2El.parentElement;
          if (parent3El && parent3El.id !== 'side-menu') {
            parent3El.classList.add('mm-active');
            const childAnchor = parent3El.querySelector('.has-arrow');
            const childDropdown = parent3El.querySelector('.has-dropdown');
            if (childAnchor) { childAnchor.classList.add('mm-active'); }
            if (childDropdown) { childDropdown.classList.add('mm-active'); }
            const parent4El = parent3El.parentElement;
            if (parent4El && parent4El.id !== 'side-menu') {
              parent4El.classList.add('mm-show');
              const parent5El = parent4El.parentElement;
              if (parent5El && parent5El.id !== 'side-menu') {
                parent5El.classList.add('mm-active');
                const childanchor = parent5El.querySelector('.is-parent');
                if (childanchor && parent5El.id !== 'side-menu') { childanchor.classList.add('mm-active'); }
              }
            }
          }
        }
      }
    }

  }

  /**
   * Initialize
   */
  initialize(): void {
    if (this.site == "store") {
      this.menuItems = this.filterMenuItems(JSON.parse(JSON.stringify(MENUSTORE)));
    } else {
      this.menuItems = this.filterMenuItems(JSON.parse(JSON.stringify(MENU)));
    }
    this.pullLatestPriceLookup();
    this.pullLatestPrinting();
  }

  /**
   * Returns true or false if given menu item has child or not
   * @param item menuItem
   */
  hasItems(item: MenuItem) {
    return item.subItems !== undefined ? item.subItems.length > 0 : false;
  }
}
