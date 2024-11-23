import type { CompanyUserListDto } from '@/models/CompanyUsers/CompanyUserListDto'

export class UserListDto{
    id: string = "";
    userName: string = "";
    email: string = "";
    phoneNumber: string = "";
    firstName: string = "";
    lastName: string = "";
    fullName: string = "";
    isDeleted: boolean = false;
    companyUsers: CompanyUserListDto[] = [];
    isAdmin: boolean = false;
}