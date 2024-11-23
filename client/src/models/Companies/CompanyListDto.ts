import type { DatabaseListDto } from "../Databases/DatabaseListDto"

export class CompanyListDto{
    id: string = "";
    name: string = "";
    fullAddress: string = "";
    isDeleted: boolean = false;
    taxDepartment: string = "";
    taxNumber: string = "";
    database: DatabaseListDto | null= null
}