import { ProductListDto } from "../Products/ProductListDto";

export class InvoiceDetailListDto{
    id: string = "";
    invoiceId: string = "";
    productId: string = "";
    product: ProductListDto = new ProductListDto();
    quantity: number = 0;
    price: number = 0;
}