import { ActionContext } from "vuex";
import { CategoryModel } from "@/models/CategoryModel";
import categoryService from "@/api/CategoryService";

export interface State {
  categories: CategoriesState;
}

type Context = ActionContext<CategoriesState, State>;

export interface CategoriesState {
  items: Array<CategoryModel>;
};

const state: CategoriesState = { 
  items: Array<CategoryModel>()
};

const getters = { 
  getCategories(): Array<CategoryModel> {
    return state.items
  },
};

const mutations = {
  setCategories: function (state: CategoriesState, categories: Array<CategoryModel>): void {
    state.items = categories;
  }
};

const actions = {
  async getCategories(context: Context): Promise<Array<CategoryModel>> {
    return await new categoryService().getAll()
      .then(function (response) {
        const responseData = response.data;
        const categories: CategoryModel[] = responseData.map((c: any) => ({
          categoryId: c.categoryId,
          name: c.categoryName
        }));
        context.commit("setCategories", categories);
        return context.state.items;
      }).catch((err) => {
        throw err;
      });
  },
  async getCategory(context: Context, id: number): Promise<CategoryModel> {
    return await new categoryService().getById(id)
      .then(function (response) {
        const responseData = response.data;
        return {
          categoryId: responseData.categoryId,
          name: responseData.categoryName,
        }
      }).catch((err) => {
        throw err;
      });
  },
  async addCategory(context: Context, data: CategoryModel) {
    await new categoryService().add(JSON.stringify(data))
      .then(function (response) {
        const responseData = response.data;
        console.log(responseData);
      }).catch((err) => {
        throw err;
      });
  },
  async updateCategory(context: Context, data: CategoryModel) {
    await new categoryService().update(data.categoryId, JSON.stringify(data))
      .then(function (response) {
        const responseData = response.data;
      }).catch((err) => {
        throw err;
      });
  },
  async deleteCategory(context: Context, id: number) {
    await new categoryService().delete(id)
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
