import { Component, OnInit, Input } from '@angular/core';
import { HostedVideoDocument } from 'src/app/models/hostedVideoDocument.model';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit {
  @Input() video: HostedVideoDocument;
  tags: string[];
  error: string;

  constructor() { }

  ngOnInit() {
    if(this.video.tags != null) {
      this.tags = this.video.tags.split(",");
    }
  }

}
