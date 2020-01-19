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
              private tagService: TagService,
              protected modalService: NgbModal,
              protected modalConfig: NgbModalConfig) {
    super(modalService, modalConfig);
  }

  ngOnInit() {
    this.initFormModal();
  }

  initFormModal() {
    this.languages = this.languageService.getAllLanguages();
    this.dataDto = new TagDto();

    this.exampleTag.name = 'Tag';
    this.exampleTag.description = 'Lorem ipsum...';
    this.exampleTag.backgroundColor = '#fff';
    this.exampleTag.fontColor = '#fff';


    this.form = this.formBuilder.group({
      name: [null, Validators.required],
      description: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(10000)]],
      backgroundColor: ['#fff'],
      fontColor: ['#000'],
      language: ['pl']
    });
  }

  create() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.dataDto.from(this.form);

    this.tagService.add(this.dataDto)
      .subscribe(
        data => {
          this.tagService.addTagDto(this.dataDto.id);
          this.sent = true;
          this.error = null;
          this.loading = false;
          this.emitTagChangedEvent();
        },
        error => {
          this.sent = true;
          this.error = error;
          this.loading = false;
        });
  }

  changeBgColor($event: ColorEvent) {
    this.form.controls.backgroundColor.setValue($event.color.hex);
    this.exampleTag.backgroundColor = $event.color.hex;
  }

  changeFgColor($event: ColorEvent) {
    this.form.controls.fontColor.setValue($event.color.hex);
    this.exampleTag.fontColor = $event.color.hex;
  }

  emitTagChangedEvent() {
    this.tagChanged.emit(null);
  }
}
