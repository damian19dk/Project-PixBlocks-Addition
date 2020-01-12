import {Component, OnInit} from '@angular/core';
import {Language, LanguageService} from '../../services/language.service';
import {FormModal} from '../../models/formModal';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {FormBuilder, Validators} from '@angular/forms';
import {TagDto} from '../../models/tagDto.model';
import {TagService} from '../../services/tag.service';

@Component({
  selector: 'app-new-tag-admin',
  templateUrl: './new-tag-admin.component.html',
  styleUrls: ['./new-tag-admin.component.css']
})
export class NewTagAdminComponent extends FormModal implements OnInit {
  languages: Array<Language>;

  constructor(private languageService: LanguageService,
              private formBuilder: FormBuilder,
              protected modalService: NgbModal,
              private tagService: TagService) {
    super(modalService);
  }

  ngOnInit() {
    this.languages = this.languageService.getAllLanguages();
    this.dataDto = new TagDto();

    this.sent = false;
    this.submitted = false;
    this.loading = false;
    this.error = null;

    this.form = this.formBuilder.group({
      name: [null, Validators.required],
      description: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(10000)]],
      color: ['primary'],
      language: ['pl']
    });
  }

  create() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.dataDto = this.form.value;

    console.log(this.dataDto);

    this.tagService.add(this.dataDto)
      .subscribe(
        data => {
          this.sent = true;
          this.error = null;
          this.loading = false;
        },
        error => {
          this.sent = true;
          this.error = error;
          this.loading = false;
        });
  }
}
