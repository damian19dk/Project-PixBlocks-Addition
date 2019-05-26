export class Lesson {
    lessonVideos: [
        {
            id: string,
            mediaId: string,
            category: string,
            premium: true,
            title: string,
            description: string,
            picture: string,
            duration: number,
            publishDate: number,
            language: string,
            tags: [
                string
            ]
        }
    ];
    exercises: [
        {
            exerciseVideos: [
                {
                    id: string,
                    mediaId: string,
                    category: string,
                    premium: true,
                    title: string,
                    description: string,
                    picture: string,
                    duration: number,
                    publishDate: number,
                    language: string,
                    tags: [
                        string
                    ]
                }
            ],
            id: string,
            mediaId: string,
            category: string,
            premium: true,
            title: string,
            description: string,
            picture: string,
            duration: number,
            publishDate: number,
            language: string,
            tags: [
                string
            ]
        }
    ];
    id: string;
    mediaId: string;
    category: string;
    premium: true;
    title: string;
    description: string;
    picture: string;
    duration: number;
    publishDate: number;
    language: string;
    tags: [
        string
    ];
}