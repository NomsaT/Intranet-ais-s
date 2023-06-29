import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DxDataGridComponent } from 'devextreme-angular';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import { exportDataGrid } from 'devextreme/excel_exporter';
import notify from 'devextreme/ui/notify';
import { Workbook } from 'exceljs';
import saveAs from 'file-saver';
import { accountPattern, branchPattern, contactPattern, currencyZAR, idPattern, postalPattern, taxPattern } from 'src/globalConstants';
import { activateUser } from '../../core/models/activateUser.models';
import { disableUser } from '../../core/models/disableUser.models';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { UserProfileService } from '../../core/services/user.service';
import { tab } from "../models/tab";
import { popupAutoSelectService } from '../../core/services/popupAutoSelect.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  activateUsers: activateUser = new activateUser();
  disableUser: disableUser = new disableUser();
  tabs: tab[];
  breadcrumbs: any;
  ActiveDataSource: any;
  DeactivatedDataSource: any;
  PositionForeignDataSource: any;
  PaymentIntervalsForeignDataSource: any;
  LawsForeignDataSource: any;
  GenderForeignDataSource: any;
  RaceForeignDataSource: any;
  MaritalStatusForeignDataSource: any;
  TypeEmploymentForeignDataSource: any;
  ProfitCenterForeignDataSource: any;
  CountryForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  baseUrl: any;
  popupVisible = false;
  passwordPopupVisible = false;
  error = false;
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  popupDownloadConfirmDisabledVisible = false;
  passwordManageData: any;
  passwordResetMethods: string[];
  passwordError = false;
  allowModify = false;
  allowActivate = false;
  allowDeactivate = false;
  BankNameForeignDataSource: any;
  popupVisibleBanking = false;
  rowIndex = 0;
  noDownloadButtonOptions: any = {
    text: "No",
    onClick: (e) => {
      this.popupDownloadConfirmVisible = false;
      this.CancelDownload = true;

    }
  };

  yesDownloadButtonOptions = {
    text: "Yes",
    onClick: () => {
      this.popupDownloadConfirmVisible = false;

      this.event.component.beginUpdate();
      this.event.component.columnOption('isDisabled', 'visible', true);
      this.event.component.columnOption('idnumber', 'visible', true);
      this.event.component.columnOption('employeeNumber', 'visible', true);
      this.event.component.columnOption('contactNumber', 'visible', true);
      this.event.component.columnOption('birthday', 'visible', true);
      this.event.component.columnOption('incomeTaxNumber', 'visible', true);
      this.event.component.columnOption('raceId', 'visible', true);
      this.event.component.columnOption('genderId', 'visible', true);
      this.event.component.columnOption('maritalStatusId', 'visible', true);
      this.event.component.columnOption('nextOfKinName', 'visible', true);
      this.event.component.columnOption('nextOfKinContactNumber', 'visible', true);
      this.event.component.columnOption('hourlyRate', 'visible', true);
      this.event.component.columnOption('baseSalaryPerMonth', 'visible', true);
      this.event.component.columnOption('highestQualification', 'visible', true);
      this.event.component.columnOption('numberOfDependants', 'visible', true);
      this.event.component.columnOption('typeOfEmploymentId', 'visible', true);
      this.event.component.columnOption('email', 'visible', true);
      this.event.component.columnOption('startDate', 'visible', true);
      this.event.component.columnOption('employeePositionId', 'visible', true);
      this.event.component.columnOption('paymentIntervalsId', 'visible', true);
      this.event.component.columnOption('streetAddress1', 'visible', true);
      this.event.component.columnOption('streetAddress2', 'visible', true);
      this.event.component.columnOption('suburb', 'visible', true);
      this.event.component.columnOption('city', 'visible', true);
      this.event.component.columnOption('postCode', 'visible', true);
      this.event.component.columnOption('countryId', 'visible', true);
      this.event.component.columnOption('bankName', 'visible', true);
      this.event.component.columnOption('accountNumber', 'visible', true);
      this.event.component.columnOption('branchCode', 'visible', true);
       
      const workbook = new Workbook();
      const worksheet = workbook.addWorksheet('Main sheet');
      exportDataGrid({
        component: this.event.component,
        worksheet: worksheet
      }).then(function () {
        workbook.xlsx.writeBuffer()
          .then(function (buffer: BlobPart) {
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Active-Users-List.xlsx');
          });
      })
      .then(function () {
        this.event.component.columnOption('isDisabled', 'visible', false);
        this.event.component.columnOption('idnumber', 'visible', false);
        this.event.component.columnOption('employeeNumber', 'visible', false);
        this.event.component.columnOption('contactNumber', 'visible', false);
        this.event.component.columnOption('birthday', 'visible', false);
        this.event.component.columnOption('incomeTaxNumber', 'visible', false);
        this.event.component.columnOption('raceId', 'visible', false);
        this.event.component.columnOption('genderId', 'visible', false);
        this.event.component.columnOption('maritalStatusId', 'visible', false);
        this.event.component.columnOption('nextOfKinName', 'visible', false);
        this.event.component.columnOption('nextOfKinContactNumber', 'visible', false);
        this.event.component.columnOption('hourlyRate', 'visible', false);
        this.event.component.columnOption('baseSalaryPerMonth', 'visible', false);
        this.event.component.columnOption('highestQualification', 'visible', false);
        this.event.component.columnOption('numberOfDependants', 'visible', false);
        this.event.component.columnOption('typeOfEmploymentId', 'visible', false);
        this.event.component.columnOption('email', 'visible', false);
        this.event.component.columnOption('startDate', 'visible', false);
        this.event.component.columnOption('employeePositionId', 'visible', false);
        this.event.component.columnOption('paymentIntervalsId', 'visible', false);
        this.event.component.columnOption('streetAddress1', 'visible', false);
        this.event.component.columnOption('streetAddress2', 'visible', false);
        this.event.component.columnOption('suburb', 'visible', false);
        this.event.component.columnOption('city', 'visible', false);
        this.event.component.columnOption('postCode', 'visible', false);
        this.event.component.columnOption('countryId', 'visible', false);
        this.event.component.columnOption('bankName', 'visible', false);
        this.event.component.columnOption('accountNumber', 'visible', false);
        this.event.component.columnOption('branchCode', 'visible', false);
        this.event.component.endUpdate();
      });
      this.event.cancel = true;
    }
  };

  noDownloadDisabledButtonOptions: any = {
    text: "No",
    onClick: (e) => {
      this.popupDownloadConfirmDisabledVisible = false;
      this.CancelDownload = true;

    }
  };

  yesDownloadDisabledButtonOptions = {
    text: "Yes",
    onClick: () => {
      this.popupDownloadConfirmDisabledVisible = false;

      const workbook = new Workbook();
      const worksheet = workbook.addWorksheet('Main sheet');
      exportDataGrid({
        component: this.event.component,
        worksheet: worksheet
      }).then(function () {
        workbook.xlsx.writeBuffer()
          .then(function (buffer: BlobPart) {
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Deactivated-Users-List.xlsx');
          });
      });

    }
  };

  pageName: string = "User";
  idPattern: any;
  taxPattern: any;
  contactPattern: any;
  postalPattern: any;
  accountPattern: any;
  branchPattern: any;
  currencyZAR: any;
  //#region Editor options

  buttonCancel: any = {
    text: "Cancel",
    type: "danger",
    useSubmitBehavior: true,
    onClick: () => {
      this.onVoidCancel();
    }
  }

  buttonDisable: any = {
    text: "Disable",
    type: "success",
    useSubmitBehavior: true,
    onClick: ($event) => {
      this.onVoidDisable($event);
    }
  }

  passwordButtonCancel: any = {
    text: "Cancel",
    type: "danger",
    useSubmitBehavior: true,
    onClick: () => {
      this.onPasswordCancel();
    }
  }

  passwordButtonOK: any = {
    text: "Manage",
    type: "success",
    useSubmitBehavior: true,
    onClick: ($event) => {
      this.onPasswordOK($event);
    }
  }

  buttonOptions: any = {
    icon: "plus",
    type: "default",
    hint: "Add a new Bank",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleBanking = true;
    }
  }

  //#endregion


  @ViewChild('activeUsers', { static: false }) dataGridActive: DxDataGridComponent;
  @ViewChild('deactivatedUsers', { static: false }) dataGridDeactive: DxDataGridComponent;
  @ViewChild('content') content;

  refreshData() {
    this.dataGridActive.instance.refresh();
    this.dataGridDeactive.instance.refresh();
  }

  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private userService: UserProfileService, private autoSelect: popupAutoSelectService, private authenticationService: AuthenticationService, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.rowIndex = 0;
    this.currencyZAR = currencyZAR;
    this.idPattern = idPattern;
    this.taxPattern = taxPattern;
    this.contactPattern = contactPattern;
    this.postalPattern = postalPattern;
    this.accountPattern = accountPattern;
    this.branchPattern = branchPattern;
    this.breadcrumbs = "Active";
    this.baseUrl = baseUrl;
    this.tabs = [
      { text: "Active" },
      { text: "Deactivated" }
    ];
    this.passwordResetMethods = ["Reset Password (Send Email Link)", "Admin set new password"]

    this.ActiveDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Users',
        updateUrl: baseUrl + 'api/Users',
        insertUrl: baseUrl + 'api/Users',
        deleteUrl: baseUrl + 'api/Users',
        onInserted: () => {
          this.globalMethodsService.ToastInsertSuccessMessage(this.pageName);
        },
        onUpdated: () => {
          this.globalMethodsService.ToastUpdateSuccessMessage(this.pageName);
        },
        onRemoved: () => {
          this.globalMethodsService.ToastDeleteSuccessMessage(this.pageName);
        }
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.authService.getToken()
          };
        }*/
      }),
      paginate: true,
      sort: ([
        { selector: "name", desc: false }
      ]),
      filter: ["isDisabled", "=", 0]
    }

    this.DeactivatedDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Users',
        updateUrl: baseUrl + 'api/Users',
        insertUrl: baseUrl + 'api/Users',
        deleteUrl: baseUrl + 'api/Users',
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.authService.getToken()
          };
        }*/
      }),
      paginate: true,
      sort: ([
        { selector: "name", desc: false }
      ]),
      filter: ["isDisabled", "=", 1]
    }

    this.PositionForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/EmployeePosition',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.PaymentIntervalsForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/PaymentIntervals',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.LawsForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Laws',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.GenderForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Gender',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.RaceForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Race',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.MaritalStatusForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/MaritalStatus',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.TypeEmploymentForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/TypeEmployment',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.ProfitCenterForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/ProfitCenter',
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      paginate: true
    }

    this.CountryForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Country',
      }),
      paginate: true
    }    

    this.BankNameForeignDataSource = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/BankNames',
      }),
      paginate: true
    }

    this.allowModify = this.authService.HavePermission(27);
    this.allowActivate = this.authService.HavePermission(28);
    this.allowDeactivate = this.authService.HavePermission(29);
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

  onOptionChanged(e) {
    if (e != null || e != undefined || e != 0) {
      if (e.addedItems[0].text == "Active" || e.addedItems[0].text == "Deactivated") {
        this.breadcrumbs = e.addedItems[0].text;
        this.refreshData();
      } else {
        this.breadcrumbs = "";
      }

      if (e.addedItems[0].text == "Active") {
        this.ActiveDataSource = {
          store: AspNetData.createStore({
            key: 'id',
            loadUrl: this.baseUrl + 'api/Users',
          }),
          filter: ["isDisabled", "=", 0]
        }
      } else {
        this.DeactivatedDataSource = {
          store: AspNetData.createStore({
            key: 'id',
            loadUrl: this.baseUrl + 'api/Users',
          }),
          filter: ["isDisabled", "=", 1]
        }
      }
    }
  }

  activateUser(e) {
    this.activateUsers = new activateUser();
    if (e.column.caption == "Activate User") {
      this.activateUsers.id = e.data.id;
      this.userService.activeUser(this.activateUsers).subscribe(data => {
        if (data != "") {
          notify("User Activated.", 'success', 5000);
          this.refreshData();
        } else {
          notify("User not activated.", 'error', 5000);
        }
      }, subError => {
        notify(subError.error, 'error', 5000);
      });
    }
  }

  EditStart(e) {
    this.rowIndex = this.dataGridActive.instance.getRowIndexByKey(e.data.id);
  }

  onSaving(e) {
    this.rowIndex = 0;
  }

  onEditorPreparing(e) {
    if (e.parentType == "dataRow" && e.dataField == "birthday") {
      e.editorOptions.invalidDateMessage = "The date must have the following format: yyyy/MM/dd";
    }
    if (e.parentType === "dataRow") {
      switch (e.dataField) {
        case "birthday": {
          e.editorOptions.invalidDateMessage = "The date must have the following format: yyyy/MM/dd";
          break;
        }
        case "startDate": {
          e.editorOptions.invalidDateMessage = "The date must have the following format: yyyy/MM/dd";
          break;
        }
      }
    }
  }

  onCellClickActive(e) {
    this.activateUsers = new activateUser();
    this.disableUser = new disableUser();
    if (e.column.caption == "Deactivate User") {
      this.popupVisible = true;
      this.activateUsers.id = e.data.id;
    }
    if (e.column.caption == "Password") {
      this.passwordError = false;
      this.passwordManageData = new Object;
      this.passwordManageData.Id = e.data.id;
      this.passwordManageData.Account = e.data.userName;
      this.passwordManageData.PasswordResetMethod = this.passwordResetMethods[0]
      this.passwordPopupVisible = true;
    }
  }

  onCellPrepared(e) {
    if (e.rowType === "data" && e.column.caption == "Activate User") {
      e.cellElement.style.color = "#1f7556";
      e.cellElement.style.backgroundColor = "#d6f3e9";
    }
  }

  deactivateUserPrepared(e) {
    if (e.rowType === "data" && e.column.caption == "Deactivate User") {
      e.cellElement.style.color = "#dc7773";
      e.cellElement.style.backgroundColor = "#fbdad9";
    }
  }

  onPasswordCancel() {
    this.passwordPopupVisible = false;
  }

  onVoidCancel() {
    this.disableUser = new disableUser();
    this.activateUsers = new activateUser();
    this.popupVisible = false;
  }

  onPasswordOK(e) {
    this.passwordError = false;
    if (this.passwordManageData.PasswordResetMethod == this.passwordResetMethods[0]) {
      this.authenticationService.ResetPassword(this.passwordManageData.Account).subscribe(data => {
        this.passwordPopupVisible = false;
        notify("Password Reset Link Sent.", 'success', 5000);
      }, subError => {
        notify(subError.error, 'error', 5000);
      })
    } else {
      if (this.passwordManageData.NewPassword) {
        this.authenticationService.AdminSetPassword(this.passwordManageData.Id, this.passwordManageData.NewPassword).subscribe(data => {
          this.passwordPopupVisible = false;
          notify("Password Changed.", 'success', 5000);
        }, subError => {
          notify(subError.error, 'error', 5000);
        })
      } else {
        this.passwordError = true;
      }
    }
  }

  onVoidDisable(e) {
    if (this.disableUser.confirmation == "DISABLE") {
      this.userService.deactiveUser(this.activateUsers).subscribe(data => {
        if (data != "") {
          this.disableUser = new disableUser();
          this.popupVisible = false;
          notify("User dectivated.", 'success', 5000);
          this.refreshData();
        } else {
          notify("User not deactivated.", 'error', 5000);
        }
      }, subError => {
        notify(subError.error, 'error', 5000);
      });
    } else {
      this.disableUser = new disableUser();
      this.error = true;
    }
  }

  newRow(e) {
    e.data.countryId = 190;
  }

  onExporting(e) {
    this.event = e;
    this.popupDownloadConfirmVisible = true;
    if (this.CancelDownload) {
      e.cancel = true;
    }
  }

  onHidden(e) {
    this.dataGridActive.instance.getDataSource().reload();
    this.autoSelect.GetNewestBankName().subscribe(data => {
      this.dataGridActive.instance.cellValue(this.rowIndex, "bankNameId", data);
    }, subError => {
      notify(subError.error, 'error', 5000);
    }); 
  }

  onExported(e) {
    notify("Successfully Downloaded", 'success', 5000);
  }

  onExportingDisable(e) {
    this.event = e;
    this.popupDownloadConfirmDisabledVisible = true;
    if (this.CancelDownload) {
      e.cancel = true;
    }
  }

  onExportedDisable(e) {
    notify("Successfully Downloaded", 'success', 5000);
  }
}
