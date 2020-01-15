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
      name: [this.tagDto.name, Validators.required],
      description: [this.tagDto.description, [Validators.required, Validators.minLength(3), Validators.maxLength(10000)]],
      color: [this.tagDto.color],
      language: ['pl']
    });
  }

  edit() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.applyDataDto();

    this.tagService.update(this.dataDto.name, this.dataDto)
      .subscribe(
        data => {
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

  remove() {
    this.applyDataDto();
    this.tagService.remove(this.dataDto.name).subscribe(
      data => {
        this.refreshOtherThumbnails();
      },
      error => {
        this.error = error;
      }
    );
  }

  refreshOtherThumbnails() {
    this.tagChanged.emit(null);
  }

  changeColor($event: ColorEvent) {
    this.dataDto.color = $event.color.hex;
    this.exampleTag.color = this.dataDto.color;
  }

  applyDataDto() {
    this.dataDto.name = this.form.value.name;
    this.dataDto.description = this.form.value.description;
    this.dataDto.language = this.form.value.language;
  }
}
