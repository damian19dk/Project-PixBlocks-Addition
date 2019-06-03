export class Course {
    courseVideos: [
        {
            id: string,
            mediaId: string,
            category: string,
            premium: boolean,
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
    lessons: [
        {
            lessonVideos: [
                {
                    id: string,
                    mediaId: string,
                    category: string,
                    premium: boolean,
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
            exercises: [
                {
                    exerciseVideos: [
                        {
                            id: string,
                            mediaId: string,
                            category: string,
                            premium: boolean,
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
                    premium: boolean,
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
            premium: boolean,
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
    premium: boolean;
    title: string;
    description: string;
    picture: string;
    duration: number;
    publishDate: number;
    language: string;
    tags: [
        string
    ]
}