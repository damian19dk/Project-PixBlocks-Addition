import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {CourseDocument} from 'src/app/models/courseDocument.model';
import {CourseDto} from 'src/app/models/courseDto.model';
import {CourseService} from 'src/app/services/course.service';
import {TagService} from 'src/app/services/tag.service';
import {LanguageService} from '../../services/language.service';
import {LoadingService} from '../../services/loading.service';
import {FormModal} from '../../models/formModal';

@Component({
  selector: 'app-course-thumbnail',
  templateUrl: './course-thumbnail.component.html',
  styleUrls: ['./course-thumbnail.component.css']
})
export class CourseThumbnailComponent extends FormModal implements OnInit {

  @Input() course: CourseDocument;
  @Output() editVideoComponent: EventEmitter<any> = new EventEmitter<any>();
  image: any;

  constructor(private formBuilder: FormBuilder,
              private courseService: CourseService,
              private loadingService: LoadingService,
              private tagService: TagService,
              private languageService: LanguageService,
              protected modalService: NgbModal) {
    super(modalService);
  }


  ngOnInit() {
    this.getPicture();

    this.tagsList = this.tagService.getTags();
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();
    this.languages = this.languageService.getAllLanguages();

    this.dataDto = new CourseDto();

    this.sent = false;
    this.submitted = false;
    this.loading = false;
    this.error = null;

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
    this.dataDto.image = this.fileToUpload;
    const tags = this.form.value.tags;
    this.dataDto.tags = this.tagService.toTagsString(tags === null ? null : tags.map(e => e.text));
    const formData = this.dataDto.toFormData();

    this.courseService.update(formData)
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
    this.courseService.remove(this.course.id).subscribe(
      data => {
        this.refreshOtherThumbnails();
      },
      error => {
        this.error = error;
      }
    );
  }

  refreshOtherThumbnails() {
    this.editVideoComponent.emit(null);
  }

  private getPicture() {
    if (this.course.picture == null) {
      this.course.picture = 'https://mdrao.ca/wp-content/uploads/2018/03/DistanceEdCourse_ResitExam.png';
      return;
    }
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    this.fileUploadMessage = this.fileToUpload.size > 0 ? 'Gotowy do wysłania' : 'Wybierz plik';
  }

  imitateFileInput() {
    document.getElementById('image').click();
  }


}
