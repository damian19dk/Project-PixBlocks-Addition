import {Component, Input, OnInit} from '@angular/core';
import {TagDto} from '../../models/tagDto.model';

@Component({
  selector: 'app-tag-thumbnail',
  templateUrl: './tag-thumnail.component.html',
  styleUrls: ['./tag-thumbnail.component.css']
})
export class TagThumbnailComponent implements OnInit {

  @Input() tagDto: TagDto;

  constructor() {
  }

  ngOnInit() {
  }
}
