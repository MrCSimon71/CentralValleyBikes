<template>
  <div class="product-details">
    <h3>Product Details</h3>
    <div v-if="productData.productId" class="pt-3">
      <div>
        <label><strong>Product Name:</strong></label> {{ productData.productName }}
      </div>
      <div>
        <label><strong>Model Year:</strong></label> {{ productData.modelYear }}
      </div>
      <div>
  <label><strong>Brand:</strong></label> {{ productData.brand.name }}
</div>
        <div>
          <label><strong>Category:</strong></label> {{ productData.category.name }}
        </div>
      <div>
        <label><strong>List Price:</strong></label> {{ productData.listPrice }}
      </div>
    </div>
    <div class="pt-3">
      <router-link to="/products">Back to list</router-link>&nbsp;|&nbsp;
      <router-link :to="{ name: 'product-edit', params: { id: productData.productId }}">Edit</router-link>&nbsp;|&nbsp;
      <router-link to="/products" @click="this.deleteProductAction(productData.productId)">Delete</router-link>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapActions } from "vuex";
import { ProductModel } from "@/models/ProductModel";

export default defineComponent({
  props: {},
  data() {
    return {
      productData: {} as ProductModel | null,
    };
  },
  created() {
    this.getProductAction();
  },
  methods: {
    ...mapActions("Product", [ "getProduct", "deleteProduct" ]),
    async getProductAction() {
      try {
        this.productData = await this.getProduct(this.$route.params.id);
      }
      catch (error) {
        alert('Oops! ' + error);
        console.error(error);
      }
    },
    async deleteProductAction(id: number) {
      try {
        await this.deleteProduct(id);
        alert('Product successfully deleted');
        this.$router.push('/products');
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
  .product-details {
    text-align: left;
    margin-left: 20px;
  }
</style>