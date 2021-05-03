import { TickerState, TickerValue } from '@/models/api';
import { getJSON } from '@/utils/rest';

const baseUrl = `${process.env.BASE_URL}api/tickers`;

export const tickersState = async (): Promise<TickerState[]> => {
  return getJSON(`${baseUrl}/states`);
};

export const tickersValue = async (): Promise<TickerValue[]> => {
  return getJSON(`${baseUrl}/values`);
};
