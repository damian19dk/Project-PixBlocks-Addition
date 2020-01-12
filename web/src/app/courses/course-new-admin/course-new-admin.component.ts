import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {CourseService} from 'src/app/services/course.service';
import {FormBuilder, Validators} from '@angular/forms';
import {CourseDto} from 'src/app/models/courseDto.model';
import {TagService} from 'src/app/services/tag.service';
import {Form} from '../../models/form';
import {LanguageService} from '../../services/language.service';

@Component({
  selector: 'app-new-course',
  templateUrl: './course-new-admin.component.html',
  styleUrls: ['./course-new-admin.component.css']
})
export class CourseNewAdminComponent extends Form implements OnInit {

  @ViewChild('labelImport', null)
  labelImport: ElementRef;

  constructor(private formBuilder: FormBuilder,
              private courseService: CourseService,
              private tagService: TagService,
              private languageService: LanguageService) {
    super();
  }

  ngOnInit() {
    this.getTags(this.tagService);
    this.tagsSettings = this.tagService.getTagSettingsForMultiselect();
    this.languages = this.languageService.getAllLanguages();

    this.dataDto = new CourseDto();

    this.sent = false;
    this.submitted = false;
    this.loading = false;
    this.error = null;

    this.form = this.formBuilder.group({
      parentId: [null],
      title: [null, Validators.required],
      description: [null, [Validators.required, Validators.minLength(3), Validators.maxLength(10000)]],
      premium: [false],
      tags: [null],
      language: ['pl'],
      pictureUrl: [null],
      image: [null]
    });
  }

  create() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.loading = true;

    this.dataDto.from(this.form);
    this.dataDto.image = this.fileToUpload;
    const tags = this.form.value.tags;
    this.dataDto.tags = this.tagService.toTagsString(tags);
    const formData = this.dataDto.toFormData();

    this.courseService.add(formData)
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

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
    this.fileUploadMessage = this.fileToUpload.size > 0 ? 'Gotowy do wysłania' : 'Wybierz plik';
  }

  imitateFileInput() {
    document.getElementById('image').click();
  }
}
