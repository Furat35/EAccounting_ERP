import type { CashRegisterDetailListDto } from "../CashRegistersDetails/CashRegisterDetailList";
import { CurrencyTypeModel, CurrencyTypes } from "../currency-type-enum";

export default class CashRegisterDto{
    id: string = "";
    name: string = "";
    iban: string = "";
    depositAmount: number = 0;
    withdrawalAmount: number = 0;
    currencyType: CurrencyTypeModel = CurrencyTypes[0];
    details: CashRegisterDetailListDto[] = [];
}