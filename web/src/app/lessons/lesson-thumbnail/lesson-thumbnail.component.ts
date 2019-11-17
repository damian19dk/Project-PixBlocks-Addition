import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { LessonDto } from 'src/app/models/lessonDto.model';
import { LessonService } from 'src/app/services/lesson.service';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { TagService } from 'src/app/services/tag.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ImageService } from 'src/app/services/image.service';
import { LessonDocument } from 'src/app/models/lessonDocument.model';
import {debounceTime, filter, switchMap} from 'rxjs/operators';
import { Observable } from 'rxjs';
import { CourseService } from 'src/app/services/course.service';
import { CourseDocument } from 'src/app/models/courseDocument.model';

@Component({
  selector: 'app-lesson-thumbnail',
  templateUrl: './lesson-thumbnail.component.html',
  styleUrls: ['./lesson-thumbnail.component.css']
})
export class LessonThumbnailComponent implements OnInit {

  @Input() lesson: LessonDocument;
  @Output() editLessonComponent: EventEmitter<any> = new EventEmitter<any>();
  image: any;
  error: string;

  editLessonForm: FormGroup;
  loading: boolean;
  submitted: boolean;
  sent: boolean;
  returnUrl: string;
  lessonDto: LessonDto;

  tagsList = [];
  tagsSettings = {};

  fileToUpload: File = null;
  fileUploadMessage = 'Wybierz plik';


  constructor(public imageService: ImageService,
              private modalService: NgbModal,
              private formBuilder: FormBuilder,
              private lessonService: LessonService,
              private tagService: TagService,
              private courseService: CourseService) { }


  ngOnInit() {
    this.getPicture();

    this.tagsList = this.tagService.getTags();
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();

    this.lessonDto = new LessonDto();

    this.sent = false;
    this.submitted = false;
    this.loading = false;
    this.error = null;

    this.editLessonForm = this.formBuilder.group({
      parentId: [null],
      id: [this.lesson.id],
      title: [this.lesson.title, Validators.required],
      description: [this.lesson.description, [Validators.required, Validators.minLength(3), Validators.maxLength(10000)]],
      premium: [this.lesson.premium],
      tags: [this.tagService.toTagsList(this.lesson.tags)],
      language: [this.lesson.language],
      pictureUrl: [this.lesson.picture],
      image: [null]
    });
  }

  get f() { return this.editLessonForm.controls; }

  editLesson() {
    this.submitted = true;

    if (this.editLessonForm.invalid) {
      return;
    }

    this.loading = true;

    this.lessonDto = this.editLessonForm.value;
    this.lessonDto.image = this.fileToUpload;
    this.lessonDto.parentId = this.editLessonForm.value.parentId.id;
    const formData = new FormData();

    this.lessonDto.parentId != null ? formData.append('parentId', this.lessonDto.parentId) : null;
    this.lessonDto.id != null ? formData.append('id', this.lessonDto.id) : null;
    this.lessonDto.premium != null ? formData.append('premium', String(this.lessonDto.premium)) : null;
    this.lessonDto.title != null ? formData.append('title', this.lessonDto.title) : null;
    this.lessonDto.description != null ? formData.append('description', this.lessonDto.description) : null;
    this.lessonDto.pictureUrl != null ? formData.append('pictureUrl', this.lessonDto.pictureUrl) : null;
    this.lessonDto.image != null ? formData.append('image', this.fileToUpload) : null;
    this.lessonDto.language != null ? formData.append('language', this.lessonDto.language) : null;
    this.lessonDto.tags != null ? formData.append('tags', this.lessonDto.tags) : null;

    this.lessonService.update(formData)
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

  removeLesson() {
    this.lessonService.remove(this.lesson.id).subscribe(
      data => {
        this.refreshOtherThumbnails();
      },
      error => {
        this.error = error;
      }
    );
  }

  refreshOtherThumbnails() {
    this.editLessonComponent.emit(null);
  }


  private getPicture() {
    const picture = null;
    if (this.lesson.picture == null) {
      this.lesson.picture = 'https://mdrao.ca/wp-content/uploads/2018/03/DistanceEdCourse_ResitExam.png';
      return;
    }
  }

  openVerticallyCentered(content) {
    this.modalService.open(content, { centered: true });
  }

  searchCourse = (text$: Observable<string>) => {
    return text$.pipe(
      debounceTime(300),
      filter(text => text !== ''),
      switchMap((searchText) => this.courseService.findByTitle(searchText))
    );
  }

  formatter = (x: CourseDocument) =>
    x.title;

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    this.fileUploadMessage = this.fileToUpload.size > 0 ? 'Gotowy do wysłania' : 'Wybierz plik';
  }

  imitateFileInput() {
    document.getElementById('image').click();
  }
}
