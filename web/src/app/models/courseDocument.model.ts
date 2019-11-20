import {DataDocument} from './dataDocument';
import {VideoDocument} from './videoDocument.model';

export class CourseDocument extends DataDocument {
  courseVideos: Array<VideoDocument>;
}
