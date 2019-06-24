export class DataDocument {
    id: string;
    mediaId: string;
    category: string;
    premium: boolean;
    title: string;
    description: string;
    picture: string;
    duration: number;
    publishDate: string;
    language: string;
    tags: [
        string
    ];
}