import { Component, Inject, OnInit } from '@angular/core';
import RemoteFileSystemProvider from 'devextreme/file_management/remote_provider';
import { AuthenticationService } from '../../core/services/auth.service';


@Component({
  selector: 'app-file-management',
  templateUrl: './file-management.component.html',
  styleUrls: ['./file-management.component.scss'],
  preserveWhitespaces: true
})

export class FileManagementComponent implements OnInit
{
  baseUrl: any;
  allowModify = false;
  allowedFileExtensions: string[];
  remoteProvider: RemoteFileSystemProvider;

  constructor(@Inject('BASE_URL') baseUrl: string, private authService: AuthenticationService)
  {
    this.remoteProvider = new RemoteFileSystemProvider({
      endpointUrl: baseUrl + 'api/FileManagement',
    });
    this.allowModify = this.authService.HavePermission(63);
    this.allowedFileExtensions = [".jpeg", ".jpg", ".pdf"];
  }

  ngOnInit(): void {

  }


}
