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
import {ImageService} from "../../services/image.service";

@Component({
  selector: 'app-courses-browse',
  templateUrl: './courses-browse.component.html',
  styleUrls: ['./courses-browse.component.css']
})
export class CoursesBrowseComponent extends FormModal implements OnInit {

  @Input() course: CourseDocument;
  @Output() editCourseComponent: EventEmitter<any> = new EventEmitter<any>();
  picture: string;

  constructor(private formBuilder: FormBuilder,
              private courseService: CourseService,
              private loadingService: LoadingService,
              private tagService: TagService,
              private languageService: LanguageService,
              public imageService: ImageService,
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

    console.log(this.dataDto);

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
    this.editCourseComponent.emit(null);
  }

  imitateImageInput() {
    document.getElementById('image').click();
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    this.fileUploadMessage = this.fileToUpload.size > 0 ? 'Gotowy do wysłania' : 'Wybierz plik';
  }

  private getPicture() {
    if (this.course.picture === null) {
      this.course.picture = 'https://mdrao.ca/wp-content/uploads/2018/03/DistanceEdCourse_ResitExam.png';
    }
  }
}
