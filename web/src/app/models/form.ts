import {FormGroup} from '@angular/forms';
import {Language} from '../services/language.service';

export class Form {
  form: FormGroup;
  dataDto: any;

  loading: boolean;
  submitted: boolean;
  sent: boolean;
  error: string;

  tagsList = [];
  tagsSettings = {};
  languages: Array<Language>;

  fileToUpload: File = null;
  fileUploadMessage: string;

  get f() {
    return this.form.controls;
  }
}
