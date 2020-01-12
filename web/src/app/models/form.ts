import {FormGroup} from '@angular/forms';
import {Language} from '../services/language.service';
import {TagDto} from './tagDto.model';
import {TagService} from '../services/tag.service';

export class Form {
  form: FormGroup;
  dataDto: any;

  loading: boolean;
  submitted: boolean;
  sent: boolean;
  error: string;

  tagsList: Array<TagDto>;
  tagsSettings = {};
  languages: Array<Language>;

  fileToUpload: File = null;
  fileUploadMessage: string;

  get f() {
    return this.form.controls;
  }

  getTags(tagService: TagService) {
    tagService.getAll().subscribe(
      data => {
        this.tagsList = data.map(tag => tag.name);
      }
    );
  }
}
