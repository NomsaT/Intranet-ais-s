import { Component, Inject, OnInit } from '@angular/core';
import RemoteFileSystemProvider from 'devextreme/file_management/remote_provider';
import { AuthenticationService } from '../../core/services/auth.service';


@Component({
  selector: 'app-import-files',
  templateUrl: './import-files.component.html',
  styleUrls: ['./import-files.component.scss']
})
export class ImportFilesComponent implements OnInit {
  baseUrl: any;
  allowedFileExtensions: string[];
  remoteProvider: RemoteFileSystemProvider;
  allowModify = false;


  constructor(@Inject('BASE_URL') baseUrl: string, private authService: AuthenticationService) {
    this.remoteProvider = new RemoteFileSystemProvider({
      endpointUrl: baseUrl + 'api/ImportFiles',
    });
    this.allowModify = this.authService.HavePermission(65);
    this.allowedFileExtensions = [".csv", ".txt"];
  }

  ngOnInit(): void {
  }

}
