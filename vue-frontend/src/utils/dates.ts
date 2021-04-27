export const formatDateToAPI = (date: Date): Date => {
  const formatedDate = new Date(date);
  const offSet = formatedDate.getTimezoneOffset();
  formatedDate.setMinutes(formatedDate.getMinutes() - offSet);
  return formatedDate;
};
