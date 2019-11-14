export class HostedVideoDocument {
    mediaId: string;
    description: string;
    pubDate: number;
    tags: string;
    image: string;
    title: string;
    feedId: string;
    sources: [
        {
            width: number,
            height: number,
            type: string,
            file: string,
            label: string
        }
    ];
    tracks: [
        {
            kind: string,
            file: string
        }
    ];
    link: string;
    duration: number;
}
