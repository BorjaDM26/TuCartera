export interface TransactionAddRequest {
  shares: number;
  unitPrice: number;
  exchangeToUSD: number;
  date: Date;
  comment?: string;
  tickerId: number;
  currencyId: number;
  transactionTypeId: number;
}
