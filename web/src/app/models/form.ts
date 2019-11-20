import {FormGroup} from '@angular/forms';
import {DataDto} from './dataDto';

export class Form {
  form: FormGroup;
  dataDto: DataDto;

  loading: boolean;
  submitted: boolean;
  sent: boolean;
  error: string;

  tagsList = [];
  tagsSettings = {};

  fileToUpload: File = null;
  fileUploadMessage: string;

  get f() {
    return this.form.controls;
  }

}
