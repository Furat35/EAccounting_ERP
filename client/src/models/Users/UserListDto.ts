import type { CompanyUserListDto } from "./CompanyUserDto"

export class UserListDto{
    id: string
    userName: string
    email: string
    phoneNumber: string
    firstName: string
    lastName: string
    fullName: string
    isDeleted: boolean
    companyUsers: CompanyUserListDto[]

    constructor(id: string, username: string, email: string, phoneNumber: string, 
        firstName: string, lastName: string, fullName: string, isDeleted:boolean, companyUsers: CompanyUserListDto[]) {
        this.id = id
        this.userName = username
        this.email = email
        this.phoneNumber = phoneNumber
        this.firstName = firstName
        this.lastName = lastName
        this.fullName = fullName
        this.isDeleted = isDeleted
        this.companyUsers = companyUsers
    }
}