import { Transaction } from '@/models/api';
import { getJSON, postJSON, putJSON, deleteJSON } from '@/utils/rest';
import {
  TransactionAddRequest,
  TransactionEditRequest,
  TransactionDeleteRequest,
  TransactionItemRequest,
} from './models';

const baseUrl = `${process.env.BASE_URL}api/transactions`;

export const transactionList = async (): Promise<Transaction[]> => {
  return getJSON(`${baseUrl}`);
};

export const transactionItem = async (
  params: TransactionItemRequest
): Promise<Transaction> => {
  return getJSON(`${baseUrl}/${params.id}`);
};

export const transactionAdd = async (
  params: TransactionAddRequest
): Promise<void> => {
  return postJSON(`${baseUrl}`, params);
};

export const transactionEdit = async (
  params: TransactionEditRequest
): Promise<void> => {
  return putJSON(`${baseUrl}`, params);
};

export const transactionDelete = async (
  params: TransactionDeleteRequest
): Promise<void> => {
  return deleteJSON(`${baseUrl}/${params.id}`);
};
