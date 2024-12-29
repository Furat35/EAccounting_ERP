import type { ProductDetailListDto } from "../ProductDetails/ProductDetailListDto";

export class ProductCreateDto{
    id: string = "";
    name: string = "";
    deposit: number = 0;
    withdrawal: number = 0;
    details: ProductDetailListDto[] = [];
}