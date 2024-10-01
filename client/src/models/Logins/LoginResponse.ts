import type { CompanyListDto } from "../Companies/CompanyListDto"

export class LoginResponse{
    id: string
    email: string
    username: string
    name: string
    companyId: string
    companies: CompanyListDto[]

    constructor(id: string, email: string, username: string, name: string, 
        companyId: string, companies: CompanyListDto[]) {
        this.id = id
        this.email = email
        this.username = username
        this.name = name
        this.companyId = companyId
        this.companies = companies
    }
}