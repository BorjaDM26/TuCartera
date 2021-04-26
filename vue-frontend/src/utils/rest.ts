import axios from 'axios';

export const getJSON = async <R>(url: string, params?: any): Promise<R> => {
  const res = await axios.get<R>(url, { params });
  return res.data;
};

export const postJSON = async <R>(url: string, params?: any): Promise<R> => {
  const res = await axios.post<R>(url, params);
  return res.data;
};

export const putJSON = async <R>(url: string, params?: any): Promise<R> => {
  const res = await axios.put<R>(url, params);
  return res.data;
};

export const deleteJSON = async <R>(url: string, params?: any): Promise<R> => {
  const res = await axios.delete<R>(url, { params });
  return res.data;
};
