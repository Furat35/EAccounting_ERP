import { ProductListDto } from "../Products/ProductListDto";

export class InvoiceDetailCreateDto{
    id: string = "";
    invoiceId: string = "";
    productId: string = "";
    product: ProductListDto = new ProductListDto();
    quantity: number = 0;
    price: number = 0;
}