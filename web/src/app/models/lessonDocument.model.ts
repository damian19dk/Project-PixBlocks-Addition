import { DataDocument } from './dataDocument';
import { VideoDocument } from './videoDocument.model';
import { ExerciseDocument } from './exerciseDocument.model';

export class LessonDocument extends DataDocument {
    lessonVideos: VideoDocument[];
    exercises: ExerciseDocument[];
}