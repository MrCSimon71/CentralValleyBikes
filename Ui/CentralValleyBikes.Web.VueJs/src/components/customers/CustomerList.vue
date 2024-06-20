<template>
  <div class="customer-list">
    <div class="page-header">
      <div class="page-title">Customers</div>
      <div class="header-action">
        <router-link :to="{ name: 'customer-add' }" v-slot="{ addCustomer }">
          <button @click="addCustomer" class="btn btn-primary">
            Add Customer
          </button>
        </router-link></div>
    </div>
    <div id="divCustomerList" v-if="customerData" class="pt-3" style="font-size: 14px">
      <table style="text-align:left">
        <thead>
          <tr>
            <th>Id</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email</th>
            <th>Phone</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="customer in customerData" :key="customer.customerId">
            <td width="70" class="no-wrap">{{ customer.customerId }}</td>
            <td width="120">{{ customer.firstName }}</td>
            <td width="120">{{ customer.lastName }}</td>
            <td width="300">{{ customer.email }}</td>
            <td width="200">{{ customer.phone }}</td>
            <td width="auto" class="ps-3">
              <router-link :to="{ name: 'customer-details', params: { id: customer.customerId }}">View</router-link>&nbsp;|&nbsp;
              <router-link :to="{ name: 'customer-edit', params: { id: customer.customerId }}">Edit</router-link>&nbsp;|&nbsp;
              <router-link to="/customers" @click="this.deleteCustomerAction(customer.customerId)">Delete</router-link>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapActions } from "vuex";
import { CustomerModel } from "@/models/CustomerModel";

export default defineComponent({
  props: {},
  data() {
    return {
      customerData: [] as CustomerModel[] | null,
    };
  },
  created() {
    this.getCustomersAction();
  },
  methods: {
    ...mapActions("Customer", ["getCustomers", "deleteCustomer"]),
    async getCustomersAction() {
      this.customerData = null;

      try {
        this.customerData = await this.getCustomers();
      }
      catch (error) {
        alert('Oops! ' + error);
        console.error(error);
      }
    },
    async deleteCustomerAction(id: number) {
      try {
        await this.deleteCustomer(id);
        alert('Customer successfully deleted');
        this.getCustomersAction();
      }
      catch (error) {
        alert('Oops! ' + error);
        console.error(error);
      }
    }
  }
});

</script>

<style scoped>
  .customer-list {
      margin-left: 20px;
      text-align: left;
      width: 1100px;
  }
  .page-header {
    width: 1100px;
    /*background-color: rgb(231, 231, 210);*/
  }
  .page-title {
    font-size: 24px;
    font-weight: bold;
    width: 80%;
    display: inline-block;
  }
  .header-action {
      margin-left: auto;
      /*width: 10%;*/
      display: inline-block;
  }
</style>