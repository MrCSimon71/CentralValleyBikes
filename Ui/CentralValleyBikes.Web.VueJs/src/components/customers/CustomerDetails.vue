<template>
  <div class="customer-details">
    <h3>Customer Details</h3>
    <div v-if="customerData.customerId" class="pt-3">
      <div>
        <label><strong>First Name:</strong></label> {{ customerData.firstName }}
      </div>
      <div>
        <label><strong>Last Name:</strong></label> {{ customerData.lastName }}
      </div>
      <div>
        <label><strong>Email:</strong></label> {{ customerData.email }}
      </div>
      <div>
        <label><strong>Phone:</strong></label> {{ customerData.phone }}
      </div>
    </div>
    <div class="pt-3">
      <router-link to="/customers">Back to list</router-link>&nbsp;|&nbsp;
      <router-link :to="{ name: 'customer-edit', params: { id: customerData.customerId }}">Edit</router-link>&nbsp;|&nbsp;
      <router-link to="/customers" @click="this.deleteCustomerAction(customerData.customerId)">Delete</router-link>
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
      customerData: {} as CustomerModel | null,
    };
  },
  created() {
    this.getCustomerAction();
  },
  methods: {
    ...mapActions("Customer", [ "getCustomer", "deleteCustomer" ]),
    async getCustomerAction() {
      try {
        this.customerData = await this.getCustomer(this.$route.params.id);
        //console.log(this.customerData);
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
        this.$router.push('/customers');
      }
      catch (error) {
        alert('Oops! ' + error);
        console.error(error);
      }
    }
  }
});
</script>

<style>
  .customer-details {
    text-align: left;
    margin-left: 20px;
  }
</style>