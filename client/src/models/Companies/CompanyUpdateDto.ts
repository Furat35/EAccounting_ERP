import type { DatabaseCreateDto } from "../Databases/DatabaseCreateDto";

export class CompanyUpdateDto {
  id: string;
  name: string;
  fullAddress: string;
  taxDepartment: string;
  taxNumber: string;
  database: DatabaseCreateDto;

  constructor(
    id: string,
    name: string,
    taxDepartment: string,
    taxNumber: string,
    fullAddress: string,
    database: DatabaseCreateDto
  ) {
    this.id = id;
    this.name = name;
    this.taxDepartment = taxDepartment;
    this.taxNumber = taxNumber;
    this.database = database;
    this.fullAddress = fullAddress;
  }
}
