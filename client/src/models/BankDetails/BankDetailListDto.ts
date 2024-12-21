import BankListDto from "../Banks/BankListDto";

export class BankDetailListDto{
    id: string = "";
    bankId: string = "";
    date: string = "";
    description: string = "";
    depositAmount: number = 0;
    withdrawalAmount: number = 0;
    oppositeBankId: string = "";
    oppositeBank: BankListDto = new BankListDto();
    isCreatedByThis: boolean = false;
}