export class VideoDto {
    parentId: string;
    mediaId: string;
    premium: boolean;
    title: string;
    description: string;
    pictureUrl: string;
    image: any;
    language: string;
    tags: [
        string
    ];
}