import { CashRegisterDetailListDto } from "../CashRegistersDetails/CashRegisterDetailList";
import { CurrencyTypeModel, CurrencyTypes } from "../currency-type-enum";

export class CashRegisterListDto{
    id: string = "";
    name: string = "";
    depositAmount: number = 0;
    withdrawalAmount: number = 0;
    currencyType: CurrencyTypeModel = CurrencyTypes[0];
    details: CashRegisterDetailListDto[] = [];
}