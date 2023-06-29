import { Component } from '@angular/core';
import { DashboardService } from '../../core/services/dashboard.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})

/**
 * Footer component
 */
export class FooterComponent{
  version: any={ };
  // set the currenr year
  year: number = new Date().getFullYear();

  constructor(private dashboardService: DashboardService) {
    this.dashboardService.getDBVersion().subscribe(data => {
      if (data) {
        this.version = data;
      } else {
        this.version = "no version";
      }
    });
  }

}
