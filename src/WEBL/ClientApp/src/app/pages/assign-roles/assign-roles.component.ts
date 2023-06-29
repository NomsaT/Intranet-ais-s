import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as AspNetData from 'devextreme-aspnet-data-nojquery';
import notify from 'devextreme/ui/notify';
import { AuthenticationService } from '../../core/services/auth.service';
import { GlobalMethodsService } from '../../core/services/global-methods.service';
import { UserManagementService } from '../../core/services/usermanagement.service';

@Component({
  selector: 'app-assign-roles',
  templateUrl: './assign-roles.component.html',
  styleUrls: ['./assign-roles.component.scss']
})
export class AssignRolesComponent implements OnInit {
  roles: any;
  isVisible: string;
  permissions: any;
  roleId: any;
  selected: any;
  pageName: string = "Permission Role";
  allowModify = false;

  buttonOptions: any = {
    icon: "plus",
    text: "Update Role Permissions",
    type: "success",
    useSubmitBehavior: false,
    onClick: () => {
      this.assignPermissions();
    }
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

    this.allowModify = this.authService.HavePermission(35);
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

  roleChanges(e) {
    this.roleId = e.value;
    this.usermanagementService.getRolePermissions(this.roleId).subscribe(data => {
      this.permissions = data;
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }

  assignPermissions() {
    this.usermanagementService.assignRolesPermissions(this.roleId, this.permissions).subscribe(data => {
      notify("Role Permissions Updated", 'success', 5000);
    }, subError => {
      notify(subError.error, 'error', 5000);
    });
  }
}
