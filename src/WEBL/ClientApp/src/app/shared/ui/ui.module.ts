import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgbCollapseModule, NgbDatepickerModule, NgbDropdownModule, NgbTimepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { PagetitleComponent } from './pagetitle/pagetitle.component';

@NgModule({
  declarations: [PagetitleComponent],
  imports: [
    CommonModule,
    FormsModule,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbTimepickerModule,
    NgbDropdownModule
  ],
  exports: [PagetitleComponent]
})
export class UIModule { }
