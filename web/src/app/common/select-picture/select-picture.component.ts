import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';

@Component({
  selector: 'app-select-picture',
  templateUrl: './select-picture.component.html',
  styleUrls: ['./select-picture.component.css']
})
export class SelectPictureComponent implements OnInit {
  form: FormGroup;
  @Output() imageSelected: EventEmitter<any> = new EventEmitter<any>();

  constructor() {
  }

  ngOnInit() {
    this.form = new FormBuilder().group({
      image: [null]
    });
  }

  handleImageInput(files: FileList) {
    this.form.controls.image.setValue(files.item(0));
  }

  imitateImageInput() {
    document.getElementById('image');
  }
}
