import {FormGroup} from '@angular/forms';
import {Language} from '../services/language.service';
import {TagDto} from './tagDto.model';
import {TagService} from '../services/tag.service';

export class Form {
  form: FormGroup;
  dataDto: any;

  loading = false;
  submitted = false;
  sent = false;
  error: string = null;

  tagsList: Array<TagDto>;
  tagsSettings = {};
  languages: Array<Language>;

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

  resetFlags() {
    this.loading = false;
    this.submitted = false;
    this.sent = false;
    this.error = null;
  }
}
