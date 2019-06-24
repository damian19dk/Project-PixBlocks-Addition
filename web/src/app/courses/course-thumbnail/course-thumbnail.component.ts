import { Component, OnInit, Input } from '@angular/core';
import { CourseDocument } from 'src/app/models/courseDocument.model';
import { ImageService } from 'src/app/services/image.service';
import { environment } from 'src/environments/environment.prod';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CourseDto } from 'src/app/models/courseDto.model';
import { CourseService } from 'src/app/services/course.service';
import { TagService } from 'src/app/services/tag.service';

@Component({
  selector: 'app-course-thumbnail',
  templateUrl: './course-thumbnail.component.html',
  styleUrls: ['./course-thumbnail.component.css']
})
export class CourseThumbnailComponent implements OnInit {

  @Input() course: CourseDocument;
  image: any;
  error: string;
  
  editCourseForm: FormGroup;
  loading: boolean;
  submitted: boolean;
  sent: boolean;
  returnUrl: string;
  courseDto: CourseDto;

  tagsList = [];
  tagsSettings = {};

  fileToUpload: File = null;
  fileUploadMessage: string = 'Wybierz plik';
  

  constructor(public imageService: ImageService,
    private modalService: NgbModal,
    private formBuilder: FormBuilder,
    private courseService: CourseService,
    private tagService: TagService) { }


  ngOnInit() {
    this.getPicture();

    this.tagsList = this.tagService.getTags();
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();

    this.courseDto = new CourseDto();

    this.sent = false;
    this.submitted = false;
    this.loading = false;
    this.error = null;

    this.editCourseForm = this.formBuilder.group({
      parentId: [null],
      id: [this.course.id],
      mediaId: [this.course.mediaId],
      title: [this.course.title, Validators.required],
      description: [this.course.description, Validators.required],
      premium: [this.course.premium],
      tags: [this.tagService.toTagsList(this.course.tags)],
      language: [this.course.language],
      pictureUrl: [this.course.picture],
      image: [null]
    });
  }

  get f() { return this.editCourseForm.controls; }

  editCourse() {
    this.submitted = true;

    if (this.editCourseForm.invalid) {
      return;
    }

    this.loading = true;

    this.courseDto = this.editCourseForm.value;
    this.courseDto.image = this.fileToUpload;
    let formData = new FormData();
    console.log(this.courseDto.pictureUrl)

    this.courseDto.parentId != null ? formData.append('parentId', this.courseDto.parentId) : null;
    this.courseDto.id != null ? formData.append('id', this.courseDto.id) : null;
    this.courseDto.premium != null ? formData.append('premium', String(this.courseDto.premium)) : null;
    this.courseDto.title != null ? formData.append('title', this.courseDto.title) : null;
    this.courseDto.description != null ? formData.append('description', this.courseDto.description) : null;
    this.courseDto.pictureUrl != null ? formData.append('pictureUrl', this.courseDto.pictureUrl) : null;
    this.courseDto.image != null ? formData.append('image', this.fileToUpload) : null;
    this.courseDto.language != null ? formData.append('language', this.courseDto.language) : null;
    this.courseDto.tags != null ? formData.append('tags', this.courseDto.tags) : null;


    this.courseService.update(formData)
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


  private getPicture() {
    let picture = null;
    if (this.course.picture == null) {
      this.course.picture = "https://mdrao.ca/wp-content/uploads/2018/03/DistanceEdCourse_ResitExam.png";
      return;
    }

    picture = this.course.picture.substring(this.course.picture.indexOf('Image/') + 6);
    this.course.picture = environment.baseUrl + "/api/Image/" + picture;
  }

  openVerticallyCentered(content) {
    this.modalService.open(content, { centered: true });
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    if(this.fileToUpload.size > 0) {
      this.fileUploadMessage = 'Gotowy do wysłania';
    }
    else {
      this.fileUploadMessage = 'Wybierz plik';
    } 
  }

  imitateFileInput() {
    document.getElementById('image').click();
  }

}
