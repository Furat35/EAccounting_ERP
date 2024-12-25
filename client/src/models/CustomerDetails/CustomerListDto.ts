export class CustomerDetailListDto{
    id: string = "";
    type: CustomerDetailTypeEnum = new CustomerDetailTypeEnum();
    date: string = "";
    description: string = "";
    depositAmount: number = 0;
    withdrawalAmount: number = 0;
    details: CustomerDetailListDto[] = [];
    bankDetailId: string = "";
    customerDetailId: string = "";
}

export class CustomerDetailTypeEnum{
    name: string = "";
    value: number = 0;
}