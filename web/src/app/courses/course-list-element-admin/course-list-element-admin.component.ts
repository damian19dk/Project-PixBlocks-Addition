import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {CourseDocument} from '../../models/courseDocument.model';
import {FormBuilder, Validators} from '@angular/forms';
import {CourseService} from '../../services/course.service';
import {LoadingService} from '../../services/loading.service';
import {TagService} from '../../services/tag.service';
import {LanguageService} from '../../services/language.service';
import {NgbModal, NgbModalConfig} from '@ng-bootstrap/ng-bootstrap';
import {CourseDto} from '../../models/courseDto.model';
import {FormModal} from '../../models/formModal';
import {CdkDragDrop, moveItemInArray} from '@angular/cdk/drag-drop';
import {VideoService} from '../../services/video.service';

@Component({
  selector: 'app-course-list-element-admin',
  templateUrl: './course-list-element-admin.component.html',
  styleUrls: ['./course-list-element-admin.component.css']
})
export class CourseListElementAdminComponent extends FormModal implements OnInit {

  @Input() course: CourseDocument;
  @Output() courseChanged: EventEmitter<any> = new EventEmitter<any>();

  constructor(private formBuilder: FormBuilder,
              private courseService: CourseService,
              private videoService: VideoService,
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
    this.getCourse();
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

  getCourse() {
    this.courseService.getOne(this.course.id).subscribe(
      (data: CourseDocument) => {
        this.course = data;
        this.error = null;
      },
      error => {
        this.error = error;
      }
    );
  }

  remove() {
    this.courseService.remove(this.course.id).subscribe(
      data => {
        this.refreshOtherCourses();
        this.error = null;
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

  drop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.course.courseVideos, event.previousIndex, event.currentIndex);
    this.videoService.changeOrder(this.course.id, this.course.courseVideos.map(e => e.id)).subscribe(
      data => {
        this.error = null;
      },
      error => {
        this.error = error;
      }
    );
  }
}
