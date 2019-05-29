export class CourseDto {
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

    toFormData(): FormData {
        let formData = new FormData();

        this.parentId != null ? formData.append('parentId', this.parentId) : null;
        this.mediaId != null ? formData.append('mediaId', this.mediaId) : null;
        this.premium != null ? formData.append('premium', String(this.premium)) : null;
        this.title != null ? formData.append('title', this.title) : null;
        this.description != null ? formData.append('description', this.description) : null;
        this.pictureUrl != null ? formData.append('pictureUrl', this.pictureUrl) : null;
        this.image != null ? formData.append('image', this.image) : null;
        this.language != null ? formData.append('language', this.language) : null;
        this.tags != null ? formData.append('tags', this.tags.join(" ")) : null;

        return formData;
    }
}