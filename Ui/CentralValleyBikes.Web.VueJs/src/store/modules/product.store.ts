import { ActionContext } from "vuex";
import productService from "@/api/ProductService";
import { ProductModel } from "@/models/ProductModel";
import { PagingAttributesModel } from "@/models/PagingAttributesModel";
import QueryParams from "@/api/QueryParams";

type Context = ActionContext<ProductsState, State>;

export interface ProductsState {
  items: Array<ProductModel>;
  pagingAttributes: PagingAttributesModel;
};

export interface State {
  products: ProductsState;
}

const state: ProductsState = { 
  items: Array<ProductModel>(),
  pagingAttributes: {} as PagingAttributesModel
};

const getters = { 
  getProducts(state: ProductsState): Array<ProductModel> {
    return state.items
  },
  getPagingAttributes(state: ProductsState): PagingAttributesModel {
    return state.pagingAttributes;
  }
};

const mutations = {
  setProducts: function (state: ProductsState, products: Array<ProductModel>): void {
    state.items = products;
  },
  setPagingAttributes: function (state: ProductsState, pagingAttributes: PagingAttributesModel): void {
    state.pagingAttributes = pagingAttributes;
  }
};

const actions = {
  async getProducts(context: Context, queryParams: QueryParams<string>): Promise<Array<ProductModel>> {
    return await new productService().getProducts(queryParams)
      .then(function (response) {

        const pagingAttributes: PagingAttributesModel = response.data.meta;
        const responseData = response.data.products;

        const products: ProductModel[] = responseData.map((p: any) => ({
          productId: p.productId,
          productName: p.productName,
          modelYear: p.modelYear,
          brand: {
            brandId: p.brand.brandId,
            name: p.brand.brandName
          },
          category: {
            categoryId: p.category.categoryId,
            name: p.category.categoryName
          },
          listPrice: p.listPrice
        }));

        context.commit("setProducts", products);
        context.commit("setPagingAttributes", pagingAttributes);

        return context.state.items;

      }).catch((err) => {
        throw err;
      });
  },
  async getProduct(context: Context, id: number): Promise<ProductModel> {
    return await new productService().getById(id)
      .then(function (response) {
        const responseData = response.data;
        return {
          productId: responseData.productId,
          productName: responseData.productName,
          modelYear: responseData.modelYear,
          brand: {
            brandId: responseData.brand.brandId,
            name: responseData.brand.brandName
          },
          category: {
            categoryId: responseData.category.categoryId,
            name: responseData.category.categoryName
          },
          listPrice: responseData.listPrice
        }
      }).catch((err) => {
        throw err;
      });
  },
  async addProduct(context: Context, data: ProductModel): Promise<number> {
    return await new productService().add(JSON.stringify(data))
      .then(function (response) {
        return response.data.productId;
      }).catch((err) => {
        throw err;
      });
  },
  async updateProduct(context: Context, data: ProductModel) {
    await new productService().update(data.productId, JSON.stringify(data))
      .then(function (response) {
        const responseData = response.data;
      }).catch((err) => {
        throw err;
      });
  },
  async deleteProduct(context: Context, id: number) {
    await new productService().delete(id)
      .then(function (response) {
        const responseData = response.data;
      }).catch((err) => {
        throw err;
      });
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
