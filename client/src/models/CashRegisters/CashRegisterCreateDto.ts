import type { CurrencyTypeModel } from "../currency-type-enum";

export class CashRegisterCreateDto{
    name: string = "";
    currencyType: number = 0;
}