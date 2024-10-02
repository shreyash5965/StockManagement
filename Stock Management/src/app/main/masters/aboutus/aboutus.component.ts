import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../common/shared-service';

@Component({
  selector: 'app-aboutus',
  templateUrl: './aboutus.component.html',
  styleUrls: ['./aboutus.component.scss']
})
export class AboutusComponent implements OnInit {

  message: string = "N/A";
  constructor(private sharedService: SharedService) { }

  ngOnInit(): void {
    this.sharedService.currentMessage.subscribe(data => {
      this.message = data;
    })
  }

}
