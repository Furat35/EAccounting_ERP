export class UserUpdateDto{
    id: string
    userName: string
    email: string
    firstName: string
    lastName: string
    password: string
    companyIds: string[]

    constructor(id: string, username: string, email: string, 
        firstName: string, lastName: string, password: string, companyIds: string[]) {
        this.id = id
        this.userName = username
        this.email = email
        this.firstName = firstName
        this.lastName = lastName
        this.password = password
        this.companyIds = companyIds
    }
}