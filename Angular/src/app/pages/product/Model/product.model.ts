export interface IProduct {
    id: string;
    name: string;
    description: string;
    price: number;
    discountPercentage: number;
    stock: number;
    image: string;
    categoryId: string;
}