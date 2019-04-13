export class RegistrationData {
    constructor(
        public name?: string,
        public email?: string,
        public password?: string,
        public password_confirmation?: string
    ) { }
}

export class UserUpdateData {
    constructor(
        public name?: string,
        public email?: string,
        public password_old?: string,
        public password_new?: string,
        public password_new_confirmation?: string
    ) { }

    static fromUser(user: User) {
        return new UserUpdateData(user.name, user.email);
    }
}

export class LoginData {
    client_secret: string = '2ahj6CXgRvietdgzJ4iNPaxlsseP2VIOiHh6bLxO';
    client_id: number = 1;
    grant_type: string = 'password';

    constructor(
        public username?: string,
        public password?: string
    ) { }
}

export class User {
    constructor(
        public id: string,
        public name: string,
        public email: string
    ) { }
}

export class ValidationError {
    public message: string;
    public errors: Map<string, string>;
}
