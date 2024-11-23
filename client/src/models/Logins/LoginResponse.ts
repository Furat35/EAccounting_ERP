import type { CompanyListDto } from "../Companies/CompanyListDto"

export class LoginResponse{
    id: string = "";
    email: string = "";
    username: string = "";
    name: string = "";
    companyId: string = "";
    companies: CompanyListDto[] = [];
    isAdmin: boolean = false;
}