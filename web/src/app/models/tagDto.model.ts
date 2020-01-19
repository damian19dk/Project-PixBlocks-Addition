import {FormGroup} from '@angular/forms';

export class TagDto {
  id?: string;
  name: string;
  description: string;
  backgroundColor: string;
  fontColor: string;
  language: string;

  from(form: FormGroup): void {
    this.name = form.controls.name.value;
    this.description = form.controls.description.value;
    this.backgroundColor = form.controls.backgroundColor.value;
    this.fontColor = form.controls.fontColor.value;
    this.language = form.controls.language.value;
  }
}
