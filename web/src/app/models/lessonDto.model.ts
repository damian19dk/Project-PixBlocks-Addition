export class LessonDto {
    parentId: string;
    mediaId: string;
    premium: boolean;
    title: string;
    description: string;
    picture: string;
    language: string;
    tags: [
        string
    ];
}