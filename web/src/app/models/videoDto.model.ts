import {DataDto} from './dataDto';
import {FormGroup} from '@angular/forms';

export class VideoDto extends DataDto {
  mediaId: string;
  video: any;
  duration: number;

  from(form: FormGroup): void {
    super.from(form);
    this.video = form.controls.video.value;
    this.duration = form.controls.duration.value;
  }

  toFormData(): FormData {
    const formData = super.toFormData();
    this.video !== null ? formData.append('video', this.video) : null;
    this.duration !== null ? formData.append('duration', String(this.duration)) : null;
    return formData;
  }
}
