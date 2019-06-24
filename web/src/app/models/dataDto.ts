export class DataDto {
    parentId: string;
    id: string;
    premium: boolean;
    title: string;
    description: string;
    pictureUrl: string;
    image: any;
    language: string;
    tags: string;

    toFormData() {
        let formData = new FormData();
        this.parentId != null ? formData.append('parentId', this.parentId) : null;
        this.id != null ? formData.append('id', this.id) : null;
        this.premium != null ? formData.append('premium', String(this.premium)) : null;
        this.title != null ? formData.append('title', this.title) : null;
        this.description != null ? formData.append('description', this.description) : null;
        this.pictureUrl != null ? formData.append('pictureUrl', this.pictureUrl) : null;
        this.image != null ? formData.append('image', this.image) : null;
        this.language != null ? formData.append('language', this.language) : null;
        this.tags != null ? formData.append('tags', this.tags) : null;
        return formData;
    }
}