import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { DEFAULT_INTERRUPTSOURCES, Idle } from '@ng-idle/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  constructor(private idle: Idle, cd: ChangeDetectorRef) {

    // set idle parameters
    idle.setIdle(60); // how long can they be inactive before considered idle, in seconds
    idle.setTimeout(60 * 30); // how long can they be idle before considered timed out, in seconds
    idle.setInterrupts(DEFAULT_INTERRUPTSOURCES); // provide sources that will "interrupt" aka provide events indicating the user is active
  }

  ngOnInit() {

    // document.getElementsByTagName("html")[0].setAttribute("dir", "rtl");
    this.idle.watch();
  }
}
