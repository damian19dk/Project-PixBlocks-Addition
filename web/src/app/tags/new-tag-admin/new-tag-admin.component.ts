import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {Language, LanguageService} from '../../services/language.service';
import {FormModal} from '../../models/formModal';
import {NgbModal, NgbModalConfig} from '@ng-bootstrap/ng-bootstrap';
import {FormBuilder, Validators} from '@angular/forms';
import {TagDto} from '../../models/tagDto.model';
import {TagService} from '../../services/tag.service';
import {ColorEvent} from 'ngx-color';

@Component({
  selector: 'app-new-tag-admin',
  templateUrl: './new-tag-admin.component.html',
  styleUrls: ['./new-tag-admin.component.css']
})
export class NewTagAdminComponent extends FormModal implements OnInit {
  languages: Array<Language>;
  exampleTag: TagDto = new TagDto();
  @Output() tagChanged: EventEmitter<any> = new EventEmitter<any>();

  constructor(private languageService: LanguageService,
              private formBuilder: FormBuilder,
              protected modalService: NgbModal,
              private tagService: TagService,
              protected modalConfig: NgbModalConfig) {
    super(modalService, modalConfig);
  }

  ngOnInit() {
    this.languages = this.languageService.getAllLanguages();
    this.dataDto = new TagDto();

    this.exampleTag.name = 'Tag';
    this.exampleTag.description = 'Lorem ipsum...';
    this.exampleTag.color = '#fff';

    this.sent = false;
    this.submitted = false;
    this.loading = false;
    this.error = null;

    this.form = this.formBuilder.group({
      name: [null, Validators.required],
      description: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(10000)]],
      color: ['#fff'],
      language: ['pl']
    });
  }

  create() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.dataDto.name = this.form.value.name;
    this.dataDto.description = this.form.value.description;
    this.dataDto.language = this.form.value.language;

    this.tagService.add(this.dataDto)
      .subscribe(
        data => {
          this.tagService.addTagDto(this.dataDto.name);
          this.sent = true;
          this.error = null;
          this.loading = false;
          this.refreshOtherThumbnails();
        },
        error => {
          this.sent = true;
          this.error = error;
          this.loading = false;
        });
  }

  changeColor($event: ColorEvent) {
    this.dataDto.color = $event.color.hex;
    this.exampleTag.color = this.dataDto.color;
  }

  refreshOtherThumbnails() {
    this.tagChanged.emit(null);
  }
}
