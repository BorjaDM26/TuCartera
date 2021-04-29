export interface TransactionAddRequest {
  shares: number;
  unitPrice: number;
  date: Date;
  comment?: string;
  tickerId: number;
  currencyId: number;
  transactionTypeId: number;
}
