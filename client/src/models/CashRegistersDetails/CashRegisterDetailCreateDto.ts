import { CashRegisterListDto } from "../CashRegisters/CashRegisterListDto";

export class CashRegisterDetailCreateDto{
    cashRegisterId: string = "";
    date: string = "";
    type: number = 0;
    amount: number = 0;
    oppositeCashRegisterId: string | null = "";
    description: string = "";
    recordType: number = 0;
}