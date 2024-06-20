<template>
  <div class="customer-edit">
    <h3>Edit Customer</h3>
    <div v-if="customerData.customerId" class="pt-3">
      <form>
        <div class="form-group">
          <label for="firstName">First Name</label>
          <input type="text"
                 class="form-control"
                 id="firstName"
                 v-model="customerData.firstName" />
        </div>
        <div class="form-group">
          <label for="lastName">Last Name</label>
          <input type="text"
                 class="form-control"
                 id="lastName"
                 v-model="customerData.lastName" />
        </div>
        <div class="form-group">
          <label for="email">Email</label>
          <input type="text"
                 class="form-control"
                 id="email"
                 v-model="customerData.email" />
        </div>
        <div class="form-group">
          <label for="phone">Phone</label>
          <input type="text"
                 class="form-control"
                 id="phone"
                 v-model="customerData.phone" />
        </div>
      </form>
      <button type="submit" class="btn btn-primary" @click="updateCustomerAction">
        Save
      </button>
    </div>
    <div class="pt-3"><router-link to="/customers">Back to list</router-link></div>
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
    ...mapActions("Customer", ["getCustomer", "updateCustomer"]),
    async getCustomerAction() {
      try {
        this.customerData = await this.getCustomer(this.$route.params.id);
      } catch (error) {
        console.error(error);
      }
    },
    async updateCustomerAction() {
      try {
        await this.updateCustomer(this.customerData);
        alert("Update successful");
      } catch (error) {
        alert('Oops! ' + error);
        console.error(error);
      }
    }
  }
});
</script>

<style>
  .customer-edit {
    text-align: left;
    max-width: 300px;
    margin: auto;
  }
</style>