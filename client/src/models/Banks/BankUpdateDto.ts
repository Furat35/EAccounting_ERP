import type { CashRegisterDetailListDto } from "../CashRegistersDetails/CashRegisterDetailList";
import { CurrencyTypeModel } from "../currency-type-enum";

export default class CashRegisterDto{
    id: string = "";
    name: string = "";
    iban: string = "";
    depositAmount: number = 0;
    withdrawalAmount: number = 0;
    currencyType: number = 1;
    details: CashRegisterDetailListDto[] = [];
}