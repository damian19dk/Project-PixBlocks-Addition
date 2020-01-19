import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {TagDto} from '../../models/tagDto.model';
import {TagService} from '../../services/tag.service';
import {FormModal} from '../../models/formModal';
import {NgbModal, NgbModalConfig} from '@ng-bootstrap/ng-bootstrap';
import {FormBuilder, Validators} from '@angular/forms';
import {LanguageService} from '../../services/language.service';
import {ColorEvent} from 'ngx-color';

@Component({
  selector: 'app-tag-thumbnail-edit-admin',
  templateUrl: './tag-thumbnail-edit-admin.component.html',
  styleUrls: ['./tag-thumbnail-edit-admin.component.css']
})
export class TagThumbnailEditAdminComponent extends FormModal implements OnInit {
  @Input() tagDto: TagDto;
  @Output() tagChanged: EventEmitter<any> = new EventEmitter<any>();
  exampleTag: TagDto = new TagDto();

  constructor(private tagService: TagService,
              private languageService: LanguageService,
              private formBuilder: FormBuilder,
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
    this.exampleTag.fontColor = '#000';


    this.form = this.formBuilder.group({
      id: [this.tagDto.id],
      name: [this.tagDto.name, Validators.required],
      description: [this.tagDto.description, [Validators.required, Validators.minLength(3), Validators.maxLength(10000)]],
      backgroundColor: [this.tagDto.backgroundColor],
      fontColor: [this.tagDto.fontColor],
      language: ['pl']
    });
  }

  edit() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.dataDto.from(this.form);

    this.tagService.update(this.form.controls.id.value, this.dataDto)
      .subscribe(
        data => {
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

  remove() {
    this.tagService.remove(this.form.controls.id.value).subscribe(
      data => {
        this.emitTagChangedEvent();
      },
      error => {
        this.error = error;
      }
    );
  }

  emitTagChangedEvent() {
    this.tagChanged.emit(null);
  }

  changeBgColor($event: ColorEvent) {
    this.form.controls.backgroundColor.setValue($event.color.hex);
    this.exampleTag.backgroundColor = $event.color.hex;
  }

  changeFgColor($event: ColorEvent) {
    this.form.controls.fontColor.setValue($event.color.hex);
    this.exampleTag.fontColor = $event.color.hex;
  }
}
