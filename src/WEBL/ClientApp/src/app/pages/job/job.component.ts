import { Component, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.scss']
})

export class JobComponent implements OnInit {

  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;

  searchTerm: string;

  currentJob: any;
  popupVisible = false;
  showInfo: any;

  Data = [
    { jobNr: 1, jobName: 'Chemical Tanks', jobDescription: 'Manufacture 3 x Chemical tanks for Chem-Oil', jobDateCreated: '06/09/2021', jobStatus: 'Pending', statusNote: 'Awaiting customer deposit', jobFor: 'Lauren Howard', jobDateCompleted: 'N/A', transporter: 'Romeo Delta', loadSize: 'N/A', transportOrderNoReq: 'Yes', paymentTerms: 'CIA ', payee: 'Sierra Foxtrot', deliveryAddress: '777 Brockton Avenue, Abington MA 2351', contactPerson: 'Sierra Foxtrot', contactNo: '061 9567 882' },
    { jobNr: 2, jobName: 'Security Guard Huts', jobDescription: 'Manufacture 8 x Guard Huts for Alpha omega Security', jobDateCreated: '28/08/2021', jobStatus: 'Completed', statusNote: '', jobFor: 'Madeleine Gibson', jobDateCompleted: 'In Progress', transporter: 'Adam Apples', loadSize: '2 Ton', transportOrderNoReq: 'No', paymentTerms: 'CND', payee: 'Adam Apples', deliveryAddress: '280 Washington Street, Hudson MA 1749', contactPerson: 'Adam Apples', contactNo: '074 4619 348'},
    { jobNr: 3, jobName: 'Cooler Boxes', jobDescription: '200 x Cooler boxes for Distell promotional combo set', jobDateCreated: '11/07/2021', jobStatus: 'Delay', statusNote: 'Machine Maintenance', jobFor: 'Olivia Kelly', jobDateCompleted: 'N/A', transporter: 'Customer Collection', loadSize: 'N/A', transportOrderNoReq: 'Yes', paymentTerms: 'CIA ', payee: 'Olivia Kelly', deliveryAddress: '11 Jungle Road, Leominster MA 1453', contactPerson: 'Olivia Kelly', contactNo: '084 5132 287'},
    { jobNr: 4, jobName: 'Lifeguard Huts', jobDescription: '7 x Lifeguard Huts to be made in order to replace the exixting ones on the Nelson Mandela Bay beaches', jobDateCreated: '30/07/2021', jobStatus: 'Completed', statusNote: '', jobFor: 'Sarah Ferguson', jobDateCompleted: '28/09/2021', transporter: 'Charles Darwin', loadSize: '500 kg', transportOrderNoReq: 'No', paymentTerms: 'Stage payment', payee: 'Sarah Ferguson', deliveryAddress: '830 Curran Memorial Hwy, North Adams MA 1247', contactPerson: 'Sarah Ferguson', contactNo: '067 9658 9915'},
    { jobNr: 5, jobName: 'Outdoor Furniture', jobDescription: 'Manufacture 11 x 4-seater tables with chairs for Outdoor World', jobDateCreated: '15/06/2021', jobStatus: 'Pending', statusNote: 'Awaiting customer quantity requirements', jobFor: 'Tracey Dyer', jobDateCompleted: 'N/A', transporter: 'Salty Sophie', loadSize: 'N/A', transportOrderNoReq: 'No', paymentTerms: 'COD', payee: 'Tracey Dyer', deliveryAddress: '1105 Boston Road, Springfield MA 1119', contactPerson: 'Tracey Dyer', contactNo: '082 5694 873'},
    { jobNr: 6, jobName: 'Refuse Bins', jobDescription: 'Manufacture 100 x 80l refuse bins for NMBM', jobDateCreated: '22/04/2021', jobStatus: 'Delay', statusNote: 'Factory shutdown due to strike', jobFor: 'James Campbell', jobDateCompleted: 'N/A', transporter: 'Renegade Ruth', loadSize: 'N/A', transportOrderNoReq: 'Yes', paymentTerms: 'COD', payee: 'James Campbell', deliveryAddress: '72 Main St, North Reading MA 1864', contactPerson: 'James Campbell', contactNo: '060 6915 649'},
    { jobNr: 7, jobName: 'Waterpipe Joints', jobDescription: 'Manufacture x 250 joints for Waterwise', jobDateCreated: '17/03/2021', jobStatus: 'Pending', statusNote: 'Awaiting customer deposit', jobFor: 'Max Bond', jobDateCompleted: 'N/A', transporter: 'David Dumdum', loadSize: 'N/A', transportOrderNoReq: 'Yes', paymentTerms: 'CIA  ', payee: 'Max Bond', deliveryAddress: '200 Otis Street, Northborough MA 1532', contactPerson: 'Max Bond', contactNo: '081 7913 852'},
    { jobNr: 8, jobName: 'Jet-ski', jobDescription: '1 x Jet-ski body kit', jobDateCreated: '25/02/2021', jobStatus: 'Completed', statusNote: '', jobFor: 'Stewart Ball', jobDateCompleted: '03/04/2021', transporter: 'Jessica Jolly', loadSize: '80 kg', transportOrderNoReq: 'No', paymentTerms: 'CIA ', payee: 'Stewart Ball', deliveryAddress: '55 Brooksby Village Way, Danvers MA 1923', contactPerson: 'Stewart Ball', contactNo: '078 1679 846'},
    { jobNr: 9, jobName: 'Thermo Main-Tainers', jobDescription: 'Manufacture 10 x Thermo Main-Taniers for Frozen Foods', jobDateCreated: '12/02/2021', jobStatus: 'Completed', statusNote: '', jobFor: 'Nicholas Avery', jobDateCompleted: '12/03/2021', transporter: 'Paul Blart', loadSize: '1,2 Ton', transportOrderNoReq: 'No', paymentTerms: 'CWO  ', payee: 'Nicholas Avery', deliveryAddress: '374 William S Canning Blvd, Fall River MA 2721', contactPerson: 'Nicholas Avery', contactNo: '084 4624 159'},
    { jobNr: 10, jobName: 'Cladding Panels', jobDescription: 'Manufacture x 120 cladding panels for Construction X', jobDateCreated: '30/01/2021', jobStatus: 'Pending', statusNote: 'Unforseen production issues', jobFor: 'Phil Abraham', jobDateCompleted: 'N/A', transporter: 'Customer Collection', loadSize: 'N/A', transportOrderNoReq: 'Yes', paymentTerms: 'EOM ', payee: 'Phil Abraham', deliveryAddress: '591 Memorial Dr, Chicopee MA 1020', contactPerson: 'Phil Abraham', contactNo: '066 3571 945'}];


  @ViewChild('content') content;
  constructor(private modalService: NgbModal) {
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

    this.currentJob = this.Data[0];

    this.showInfo = function (job)
    {
      this.currentJob = job;
      console.log(this.currentJob);
      this.popupVisible = true;
      console.log('clicked');
    }
  }
}
