import type { CashRegisterDetailListDto } from "../CashRegistersDetails/CashRegisterDetailList";

export default class CashRegisterDto{
    name: string = "";
    iban: string = "";
    depositAmount: number = 0;
    withdrawalAmount: number = 0;
    currencyType: number = 1;
    details: CashRegisterDetailListDto[] = [];
}