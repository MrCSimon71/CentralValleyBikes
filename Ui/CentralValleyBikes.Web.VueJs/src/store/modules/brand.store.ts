import { ActionContext } from "vuex";
import { BrandModel } from "@/models/BrandModel";
import brandService from "@/api/BrandService";

export interface State {
  brands: BrandsState;
}

type Context = ActionContext<BrandsState, State>;

export interface BrandsState {
  items: Array<BrandModel>;
};

const state: BrandsState = { 
  items: Array<BrandModel>()
};

const getters = { 
  getBrands(): Array<BrandModel> {
    return state.items
  },
};

const mutations = {
  setBrands: function (state: BrandsState, brands: Array<BrandModel>): void {
    state.items = brands;
  }
};

const actions = {
  async getBrands(context: Context): Promise<Array<BrandModel>> {
    return await new brandService().getAll()
      .then(function (response) {
        const responseData = response.data;
        const brands: BrandModel[] = responseData.map((b: any) => ({
          brandId: b.brandId,
          name: b.brandName
        }));
        context.commit("setBrands", brands);
        return context.state.items;
      }).catch((err) => {
        throw err;
      });
  },
  async getBrand(context: Context, id: number): Promise<BrandModel> {
    return await new brandService().getById(id)
      .then(function (response) {
        const responseData = response.data;
        return {
          brandId: responseData.brandId,
          name: responseData.brandName,
        }
      }).catch((err) => {
        throw err;
      });
  },
  async addBrand(context: Context, data: BrandModel) {
    await new brandService().add(JSON.stringify(data))
      .then(function (response) {
        const responseData = response.data;
        console.log(responseData);
      }).catch((err) => {
        throw err;
      });
  },
  async updateBrand(context: Context, data: BrandModel) {
    await new brandService().update(data.brandId, JSON.stringify(data))
      .then(function (response) {
        const responseData = response.data;
      }).catch((err) => {
        throw err;
      });
  },
  async deleteBrand(context: Context, id: number) {
    await new brandService().delete(id)
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
