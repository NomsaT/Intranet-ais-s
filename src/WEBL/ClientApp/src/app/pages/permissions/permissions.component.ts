import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import notify from 'devextreme/ui/notify';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { UserManagementService } from '../../core/services/usermanagement.service';

@Component({
  selector: 'app-permissions',
  templateUrl: './permissions.component.html',
  styleUrls: ['./permissions.component.scss']
})
export class PermissionsComponent implements OnInit {
  roles: any;
  users: any;
  isVisible: string;
  permissionsUser: any;
  permissionsRole: any;
  userId: any;
  roleId: any;
  selectedUser: any;
  selectedRole: any;
  pageName: string = "Permissions";
  defaultVisible = false;
  allowModify = false;

  buttonOptions: any = {
    icon: "plus",
    text: "Update User Permissions",
    type: "success",
    useSubmitBehavior: false,
    onClick: () => {
      this.assignPermissions();
    }
  }

  toggleDefault() {
    this.defaultVisible = !this.defaultVisible;
  }

  @ViewChild('content') content;

  constructor(@Inject('BASE_URL') baseUrl: string, private modalService: NgbModal, private usermanagementService: UserManagementService, private globalMethodsService: GlobalMethodsService, private authService: AuthenticationService) {
    this.roles = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Roles',
      }),
      sort: [
        { selector: "Name", desc: false }
      ]
    }
    this.users = {
      store: AspNetData.createStore({
        key: 'id',
        loadUrl: baseUrl + 'api/Users/GetActiveUsers',
      }),
      sort: [
        { selector: "UserName", desc: false }
      ]
    }

    this.allowModify = this.authService.HavePermission(37);
  }

  ngOnInit() {
    /**
     * horizontal-vertical layput set
     */
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

  userChanges(e) {
    this.userId = e.value;
    this.usermanagementService.getUserPermissions(this.userId).subscribe(data => {
      this.permissionsUser = data;
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  roleChanges(e) {
    this.roleId = e.value;
    this.usermanagementService.getRolePermissions(this.roleId).subscribe(data => {
      this.permissionsRole = data;
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  assignPermissions() {
    this.usermanagementService.assignUserPermissions(this.userId, this.permissionsUser).subscribe(data => {
      notify("User Permissions Updated", 'success', 5000);
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  backgroundColour(i) {
    if (this.permissionsUser == undefined || this.permissionsRole == undefined) return "white";
    if (this.permissionsUser.length != this.permissionsRole.length) return "white";
    if (this.permissionsUser[i].permissionChecked && this.permissionsRole[i].permissionChecked) {
      return "green";
    }
    if (!this.permissionsUser[i].permissionChecked && this.permissionsRole[i].permissionChecked) {
      return "yellow";
    }
    if (this.permissionsUser[i].permissionChecked && !this.permissionsRole[i].permissionChecked) {
      return "blue";
    }
    return "white";
  }

  selectAllClick() {
    if (this.permissionsUser == undefined || this.permissionsRole == undefined) return;
    if (this.permissionsUser.length != this.permissionsRole.length) return;

    for (let i = 0; i < this.permissionsUser.length; i++) {
      if (this.permissionsRole[i].permissionChecked) {
        this.permissionsUser[i].permissionChecked = true;
      }
    }
  }

  unselectAllClick() {
    if (this.permissionsUser == undefined) return;

    for (let i = 0; i < this.permissionsUser.length; i++) {
      this.permissionsUser[i].permissionChecked = false;      
    }
  }

  userDisplay(e) {
    if (e != null && e != undefined) {
      return e.userName + " (" + e.name + " " + e.surname + ")";
    } else {
      return "";
    }    
  }
}
