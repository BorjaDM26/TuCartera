import { Currency, Ticker, TransactionType } from '@/models/api';
import { getJSON } from '@/utils/rest';

const baseUrl = `${process.env.BASE_URL}api/selectors`;

export const currencyList = async (): Promise<Currency[]> => {
  return getJSON(`${baseUrl}/currencies`);
};

export const tickerList = async (): Promise<Ticker[]> => {
  return getJSON(`${baseUrl}/tickers`);
};

export const transactionTypeList = async (): Promise<TransactionType[]> => {
  return getJSON(`${baseUrl}/transaction-types`);
};
