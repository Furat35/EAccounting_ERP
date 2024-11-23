export class UserUpdateDto{
    id: string = "";
    userName: string = "";
    email: string = "";
    firstName: string = "";
    lastName: string = "";
    password: string = "";
    companyIds: string[] = [];
    isAdmin: boolean = false;
}