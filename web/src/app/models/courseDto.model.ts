export class CourseDto {
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