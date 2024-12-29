import { CustomerListDto } from "../Customers/CustomerListDto";
import { InvoiceDetailListDto } from "../InvoiceDetails/InvoiceDetailListDto";

export class InvoiceListDto{
    id: string = "";
    date: string = "";
    amount: number = 0;
    type: InvoiceTypeEnum = new InvoiceTypeEnum();
    typeValue: number = 0;
    customerId: string = "";
    customer: CustomerListDto = new CustomerListDto();
    details: InvoiceDetailListDto[] = [];
    invoiceNumber: string = "";
}

export class InvoiceTypeEnum{
    name: string = "";
    value: number = 1;
}