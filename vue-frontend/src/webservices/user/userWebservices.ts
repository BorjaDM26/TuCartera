import { User } from '@/models/api';
import { getJSON, postJSON } from '@/utils/rest';
import { PostLoginRequest, RegisterRequest } from './models';

const baseUrl = `${process.env.BASE_URL}api/users`;

export const getLogin = async (): Promise<User> => {
  return getJSON(`${baseUrl}/login`);
};

export const postLogin = async (params: PostLoginRequest): Promise<void> => {
  return postJSON(`${baseUrl}/login`, params);
};

export const logout = async (): Promise<void> => {
  return getJSON(`${baseUrl}/logout`);
};

export const register = async (params: RegisterRequest): Promise<void> => {
  return postJSON(`${baseUrl}/register`, params);
};
