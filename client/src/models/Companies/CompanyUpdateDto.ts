import { DatabaseUpdateDto } from "../Databases/DatabaseUpdateDto";

export class CompanyUpdateDto {
  id: string = "";
  name: string = "";
  fullAddress: string = "";
  taxDepartment: string = "";
  taxNumber: string = "";
  database: DatabaseUpdateDto = new DatabaseUpdateDto() ;
}
