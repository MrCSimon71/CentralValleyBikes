import { ActionContext } from "vuex";
import { CustomerModel } from "@/models/CustomerModel";
import customerService from "@/api/CustomerService";

export interface State {
  customers: CustomersState;
}

type Context = ActionContext<CustomersState, State>;

export interface CustomersState {
  items: Array<CustomerModel>;
};

const state: CustomersState = { 
  items: Array<CustomerModel>()
};

const getters = { 
  getCustomers(): Array<CustomerModel> {
    return state.items
  },
};

const mutations = {
  setCustomers: function (state: CustomersState, customers: Array<CustomerModel>): void {
    state.items = customers;
  }
};

const actions = {
  async getCustomers(context: Context): Promise<Array<CustomerModel>> {
    return await new customerService().getCustomers()
      .then(function (response) {
        const responseData = response.data.customers;
        const customers: CustomerModel[] = responseData.map((c: any) => ({
          customerId: c.customerId,
          firstName: c.firstName,
          lastName: c.lastName,
          email: c.email,
          phone: c.phone
        }));
        context.commit("setCustomers", customers);
        return context.state.items;
      }).catch((err) => {
        throw err;
      });
  },
  async getCustomer(context: Context, id: number): Promise<CustomerModel> {
    return await new customerService().getById(id)
      .then(function (response) {
        const responseData = response.data;
        return {
          customerId: responseData.customerId,
          firstName: responseData.firstName,
          lastName: responseData.lastName,
          email: responseData.email,
          phone: responseData.phone
        }
      }).catch((err) => {
        throw err;
      });
  },
  async addCustomer(context: Context, data: CustomerModel) {
    await new customerService().add(JSON.stringify(data))
      .then(function (response) {
        const responseData = response.data;
        console.log(responseData);
      }).catch((err) => {
        throw err;
      });
  },
  async updateCustomer(context: Context, data: CustomerModel) {
    await new customerService().update(data.customerId, JSON.stringify(data))
      .then(function (response) {
        const responseData = response.data;
      }).catch((err) => {
        throw err;
      });
  },
  async deleteCustomer(context: Context, id: number) {
    await new customerService().delete(id)
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
