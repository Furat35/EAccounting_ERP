import { CashRegisterListDto } from "../CashRegisters/CashRegisterListDto";

export class CashRegisterDetailListDto{
    id: string = "";
    cashRegisterId: string = "";
    date: string = "";
    type: number = 0;
    amount: number = 0;
    depositAmount: number = 0;
    withdrawalAmount: number = 0;
    oppositeCashRegisterId: string = "";
    oppositeCashRegister: CashRegisterListDto = new CashRegisterListDto();
    description: string = "";
    recordType: number = 0;
    isCreatedByThis: boolean = false;
}