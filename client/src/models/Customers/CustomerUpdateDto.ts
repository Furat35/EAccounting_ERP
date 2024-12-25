import { CustomerTypeEnum } from "./CustomerListDto";

export class CustomerUpdateDto {
  id: string = "";
  name: string = "";
  // type: CustomerTypeEnum = new CustomerTypeEnum();
  typeValue: number = 1;
  city: string = "";
  town: string = "";
  fullAddress: string = "";
  taxDepartment: string = "";
  taxNumber: string = "";
  depositAmount: number = 0;
  withdrawalAmount: number = 0;
}
