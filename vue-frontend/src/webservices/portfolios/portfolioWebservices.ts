import { Portfolio } from '@/models/api';
import { getJSON, postJSON, putJSON, deleteJSON } from '@/utils/rest';
import {
  PortfolioAddRequest,
  PortfolioEditRequest,
  PortfolioDeleteRequest,
  PortfolioItemRequest,
} from './models';

const baseUrl = `${process.env.BASE_URL}api/portfolios`;

export const portfolioList = async (): Promise<Portfolio[]> => {
  return getJSON(`${baseUrl}`);
};

export const portfolioItem = async (
  params: PortfolioItemRequest
): Promise<Portfolio> => {
  return getJSON(`${baseUrl}/${params.id}`);
};

export const portfolioAdd = async (
  params: PortfolioAddRequest
): Promise<void> => {
  return postJSON(`${baseUrl}`, params);
};

export const portfolioEdit = async (
  params: PortfolioEditRequest
): Promise<void> => {
  return putJSON(`${baseUrl}`, params);
};

export const portfolioDelete = async (
  params: PortfolioDeleteRequest
): Promise<void> => {
  return deleteJSON(`${baseUrl}/${params.id}`);
};
