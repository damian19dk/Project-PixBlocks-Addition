import { OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DataDto } from '../models/dataDto';
import { DataService } from '../services/data.service';
import { TagService } from '../services/tag.service';

export class NewDataForm implements OnInit {

    form: FormGroup;
    loading: boolean;
    submitted: boolean;
    sent: boolean;
    returnUrl: string;
    dataDto: DataDto;
    error: string;
  
    tagsList = [];
    tagsSettings = {};
  
    fileToUpload: File = null;
    fileUploadMessage: string = 'Wybierz plik';
  
    constructor(private formBuilder: FormBuilder,
      private dataService: DataService,
      private tagService: TagService) { }
  
    ngOnInit() {
      this.tagsList = this.tagService.getTags();
      this.tagsSettings = this.tagService.getTagSettingsForMultiselect();
  
      this.dataDto = new DataDto();
  
      this.sent = false;
      this.submitted = false;
      this.loading = false;
      this.error = null;
  
      this.form = this.formBuilder.group({
        parentId: [null],
        mediaId: [null],
        title: [null, Validators.required],
        description: [null, Validators.required],
        premium: [false],
        tags: [null],
        language: ['Polski'],
        pictureUrl: [null],
        image: [null]
      });
    }
  
  
    get f() { return this.form.controls; }
  
    createNewCourse() {
      this.submitted = true;
  
      if (this.form.invalid) {
        return;
      }
  
      this.loading = true;
  
      this.dataDto.image = this.fileToUpload;
      let formData = this.dataDto.toFormData();
  
      this.dataService.add(formData)
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