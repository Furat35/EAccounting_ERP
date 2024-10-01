export class UserCreateDto{
    private userName: string
    private email: string
    private firstName: string
    private lastName: string
    private password: string
    private companyIds: string[]

    constructor(username: string, email: string, 
        firstName: string, lastName: string, password: string, companyIds: string[]) {
        this.userName = username
        this.email = email
        this.firstName = firstName
        this.lastName = lastName
        this.password = password
        this.companyIds = companyIds
    }
}