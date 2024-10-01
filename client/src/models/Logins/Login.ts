export class Login{
    emailOrUserName: string
    password: string
    
    constructor(emailOrUserName: string, password: string) {
        this.emailOrUserName = emailOrUserName
        this.password = password
    }
}