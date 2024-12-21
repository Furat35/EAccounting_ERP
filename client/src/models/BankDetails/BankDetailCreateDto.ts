
export class BankDetailCreateDto{
    bankId: string = "";
    date: string = "";
    type: number = 0;
    amount: number = 0;
    oppositeBankId: string | null = "";
    cashRegisterId: string | null = "";
    description: string = "";
    recordType: number = 0;
}