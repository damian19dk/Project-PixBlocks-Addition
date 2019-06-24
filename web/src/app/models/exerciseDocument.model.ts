import { DataDocument } from './dataDocument';
import { VideoDocument } from './videoDocument.model';

export class ExerciseDocument extends DataDocument {
    exerciseVideos: VideoDocument[];
}