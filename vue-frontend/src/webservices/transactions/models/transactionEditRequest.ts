export interface TransactionEditRequest {
  id: number;
  shares: number;
  unitPrice: number;
  date: Date;
  comment?: string;
  tickerId: number;
  currencyId: number;
  transactionTypeId: number;
}
