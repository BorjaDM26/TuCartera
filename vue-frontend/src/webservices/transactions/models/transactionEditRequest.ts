import { TransactionAddRequest } from './transactionAddRequest';

export interface TransactionEditRequest extends TransactionAddRequest {
  id: number;
}
