import {DataDto} from './dataDto';

export class VideoDto extends DataDto {
    mediaId: string;
    video: any;

  toFormData(): FormData {
    const formData = super.toFormData();
    this.video !== null ? formData.append('video', this.video) : null;
    return formData;
  }
}
