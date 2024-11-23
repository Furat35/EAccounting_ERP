import { DatabaseCreateDto } from "../Databases/DatabaseCreateDto";

export class CompanyCreateDto{
    name: string = "";
    fullAddress: string = "";
    taxDepartment: string = "";
    taxNumber: string = "";
    database: DatabaseCreateDto | null = null;
}