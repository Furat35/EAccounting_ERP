import type { CompanyListDto } from "@/models/Companies/CompanyListDto"

export class CompanyUserListDto{
    companyId: string
    company: CompanyListDto
    userId: string
    
    constructor(companyId: any, company: CompanyListDto, userId: string) {
        this.companyId = companyId
        this.company = company
        this.userId = userId
    }
}