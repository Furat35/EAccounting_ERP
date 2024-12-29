import { CustomerCreateDto } from "../customers/CustomerCreateDto";
import type { InvoiceDetailCreateDto } from "../InvoiceDetails/InvoiceDetailCreateDto";
import { InvoiceTypeEnum } from "./InvoiceListDto";

export class InvoiceCreateDto{
    id: string = "";
    date: string = "";
    amount: number = 0;
    type: InvoiceTypeEnum = new InvoiceTypeEnum();
    typeValue: number = 0;
    customerId: string = "";
    customer: CustomerCreateDto = new CustomerCreateDto();
    details: InvoiceDetailCreateDto[] = [];
    invoiceNumber: string = "";
    productId: string = "";
    quantity: number = 0;
    price: number = 0;
}
