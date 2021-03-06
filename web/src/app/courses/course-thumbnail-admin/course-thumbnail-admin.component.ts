import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {NgbModal, NgbModalConfig} from '@ng-bootstrap/ng-bootstrap';
import {CourseDocument} from 'src/app/models/courseDocument.model';
import {CourseDto} from 'src/app/models/courseDto.model';
import {CourseService} from 'src/app/services/course.service';
import {TagService} from 'src/app/services/tag.service';
import {LanguageService} from '../../services/language.service';
import {LoadingService} from '../../services/loading.service';
import {FormModal} from '../../models/formModal';

@Component({
  selector: 'app-course-thumbnail-admin',
  templateUrl: './course-thumbnail-admin.component.html',
  styleUrls: ['./course-thumbnail-admin.component.css']
})
export class CourseThumbnailAdminComponent extends FormModal implements OnInit {

  @Input() course: CourseDocument;
  @Output() courseChanged: EventEmitter<any> = new EventEmitter<any>();

  constructor(private formBuilder: FormBuilder,
              private courseService: CourseService,
              private loadingService: LoadingService,
              private tagService: TagService,
              private languageService: LanguageService,
              protected modalService: NgbModal,
              protected modalConfig: NgbModalConfig) {
    super(modalService, modalConfig);
  }

  ngOnInit() {
    this.initFormModal();
  }

  initFormModal() {
    this.getTags(this.tagService);
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();
    this.languages = this.languageService.getAllLanguages();

    this.dataDto = new CourseDto();

    this.form = this.formBuilder.group({
      parentId: [null],
      id: [this.course.id],
      title: [this.course.title, Validators.required],
      description: [this.course.description, [Validators.required, Validators.minLength(3), Validators.maxLength(10000)]],
      premium: [this.course.premium],
      tags: [this.tagService.toTagsList(this.course.tags)],
      language: [this.course.language],
      pictureUrl: [this.course.picture],
      image: [null]
    });
  }

  edit() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    this.dataDto.from(this.form);
    const formData = this.dataDto.toFormData();

    this.courseService.update(formData)
      .subscribe(
        data => {
          this.sent = true;
          this.error = null;
          this.loading = false;
          this.refreshOtherCourses();
        },
        error => {
          this.sent = true;
          this.error = error;
          this.loading = false;
        });
  }

  remove() {
    this.courseService.remove(this.course.id).subscribe(
      data => {
        this.error = null;
        this.refreshOtherCourses();
      },
      error => {
        this.error = error;
      }
    );
  }

  refreshOtherCourses() {
    this.courseChanged.emit(null);
  }

  imitateImageInput() {
    document.getElementById('image').click();
  }

  handleFileInput(files: FileList) {
    this.form.controls.image.setValue(files.item(0));
  }
}
