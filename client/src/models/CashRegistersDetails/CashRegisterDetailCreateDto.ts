export class CashRegisterDetailCreateDto{
    cashRegisterId: string = "";
    date: string = "";
    type: number = 0;
    amount: number = 0;
    oppositeCashRegisterId: string | null = "";
    bankId: string | null = "";
    customerId: string | null = "";
    description: string = "";
    recordType: number = 0;
    isCreatedByThis: boolean = false;
}