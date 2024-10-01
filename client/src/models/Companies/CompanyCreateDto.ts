import type { DatabaseCreateDto } from "../Databases/DatabaseCreateDto"

export class CompanyCreateDto{
    name: string
    fullAddress: string
    taxDepartment: string
    taxNumber: string
    database: DatabaseCreateDto


    constructor(name: string, taxDepartment: string, 
        taxNumber: string, database: DatabaseCreateDto, fullAddress: string) {
        this.name = name
        this.taxDepartment = taxDepartment
        this.taxNumber = taxNumber
        this.database = database
        this.fullAddress = fullAddress
    }
}