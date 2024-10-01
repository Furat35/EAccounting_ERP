import type { DatabaseListDto } from "../Databases/DatabaseListDto"

export class CompanyListDto{
    id: string
    name: string
    fullAddress: string
    isDeleted: boolean
    taxDepartment: string
    taxNumber: string
    database: DatabaseListDto


    constructor(id:string, name: string, isDeleted: boolean, taxDepartment: string, 
        taxNumber: string, database: DatabaseListDto, fullAddress: string) {
        this.id = id
        this.name = name
        this.isDeleted = isDeleted
        this.taxDepartment = taxDepartment
        this.taxNumber = taxNumber
        this.database = database
        this.fullAddress = fullAddress
    }
}