import {DataDocument} from './dataDocument';

export class VideoDocument extends DataDocument {
  parentId: string;
  status: string;
}
