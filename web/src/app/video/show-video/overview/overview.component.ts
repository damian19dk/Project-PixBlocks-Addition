import { Component, OnInit, Input } from '@angular/core';
import { Video } from 'src/app/models/video.model';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.css']
})
export class OverviewComponent implements OnInit {
  @Input() video: Video;
  error: string;

  constructor() { }

  ngOnInit() {
  }

}
