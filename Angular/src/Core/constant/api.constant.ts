export const baseUrl = 'https://localhost:7026/';

// account & users management module
export const signIn = `${baseUrl}api/Authentication/Authenticate`;


export const getCategory = `${baseUrl}api/Lookup/GetALLCategory`;

export const getAllProduct = `${baseUrl}api/Product/GetAll`;
export const createProduct = `${baseUrl}api/Product/CreateProduct`;
export const getProduct = `${baseUrl}api/Product/Get`;

export const createOrder = `${baseUrl}api/Order/CreateOrder`;
export const getOrder = `${baseUrl}api/Order/Get`;
