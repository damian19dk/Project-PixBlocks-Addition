import { DataDocument } from './dataDocument';
import { VideoDocument } from './videoDocument.model';
import { LessonDocument } from './lessonDocument.model';

export class CourseDocument extends DataDocument {
    courseVideos: VideoDocument[];
    lessons: LessonDocument[];
}