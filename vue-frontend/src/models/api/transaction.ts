export interface Transaction {
  id: number;
  shares: number;
  unitPrice: number;
  date: Date;
  comment?: string;
  tickerId: number;
  tickerCode?: string;
  tickerName?: string;
  currencyId: number;
  currencyCode?: string;
  transactionTypeId: number;
  transactionTypeType?: string;
}
