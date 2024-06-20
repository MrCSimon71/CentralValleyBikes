<template>
  <div class="product-edit">
    <h3>Edit Product</h3>
    <div v-if="product.productId" class="pt-3">
      <form>
        <div class="form-group">
          <label for="productName">Product Name</label>
          <input type="text"
                 class="form-control"
                 id="productName"
                 v-model="product.productName" />
        </div>
        <div class="form-group">
          <label for="modelYear">Model Year</label>
          <input type="text"
                 class="form-control"
                 id="modelYear"
                 v-model="product.modelYear" />
        </div>
        <div class="form-group">
          <label for="brands">Brand</label>
          <select id="brands" name="brand_id" v-model="product.brand.brandId" class="form-control form-select">
            <option v-for="(brand, index) in brands" :value="brand.brandId" :key="index">{{ brand.name }}</option>
          </select>
        </div>
        <div class="form-group">
          <label for="categories">Category</label>
          <select id="categories" name="category_id" v-model="product.category.categoryId" class="form-control form-select">
            <option v-for="(category, index) in categories" :value="category.categoryId" :key="index">{{ category.name }}</option>
          </select>
        </div>
        <div class="form-group">
          <label for="listPrice">List Price</label>
          <input type="text"
                 class="form-control"
                 id="listPrice"
                 v-model="product.listPrice" />
        </div>
      </form>
      <button type="submit" class="btn btn-primary" @click="updateProductAction">
        Save
      </button>
    </div>
    <div class="pt-3"><router-link to="/products">Back to list</router-link></div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapActions } from "vuex";
import { ProductModel } from "@/models/ProductModel";
import { BrandModel } from "@/models/BrandModel";
import { CategoryModel } from "@/models/CategoryModel";

export default defineComponent({
  props: {},
  data() {
    return {
      product: {} as ProductModel | null,
      brands: [] as BrandModel[] | null,
      categories: [] as CategoryModel[] | null
    };
  },
  created() {
    this.loadDropdowns();
    this.getProductAction();
  },
  methods: {
    ...mapActions("Product", ["getProduct", "updateProduct"]),
    ...mapActions("Brand", [ "getBrands" ]),
    ...mapActions("Category", ["getCategories"]),
    async loadDropdowns() {
      try {
        this.brands = await this.getBrands();
        this.categories = await this.getCategories();
      } catch (error) {
        console.error(error);
      }
    },
    async getProductAction() {
      try {
        this.product = await this.getProduct(this.$route.params.id);
      } catch (error) {
        console.error(error);
      }
    },
    async updateProductAction() {
      try {
        await this.updateProduct(this.product);
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
  .product-edit {
    text-align: left;
    max-width: 300px;
    margin: auto;
  }
</style>