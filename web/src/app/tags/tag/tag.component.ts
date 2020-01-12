import {Component, Input, OnInit} from '@angular/core';
import {TagDto} from '../../models/tagDto.model';

@Component({
  selector: 'app-tag',
  templateUrl: './tag.component.html',
  styleUrls: ['./tag.component.css']
})
export class TagComponent implements OnInit {

  @Input() tagDto: TagDto;

  constructor() {
  }

  ngOnInit() {
  }
}
