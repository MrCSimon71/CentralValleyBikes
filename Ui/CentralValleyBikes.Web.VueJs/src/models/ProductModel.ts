//import { Money } from 'ts-money';
import { BrandModel } from '@/models/BrandModel';
import { CategoryModel } from '@/models/CategoryModel';

export interface ProductModel {
  productId: number;
  productName: string;
  modelYear: number;
  brand: BrandModel,
  category: CategoryModel,
  listPrice: number;
}
