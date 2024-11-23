export class UserCreateDto{
    userName: string = "";
    email: string = "";
    firstName: string = "";
    lastName: string = "";
    password: string = "";
    companyIds: string[] = [];
    isAdmin: boolean = false;
}