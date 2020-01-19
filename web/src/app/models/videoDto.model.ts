import {DataDto} from './dataDto';
import {FormGroup} from '@angular/forms';

export class VideoDto extends DataDto {
  mediaId: string;
  video: any;

  from(form: FormGroup): void {
    super.from(form);
    this.video = form.controls.video.value;
  }

  toFormData(): FormData {
    const formData = super.toFormData();
    this.video !== null ? formData.append('video', this.video) : null;
    return formData;
  }
}
