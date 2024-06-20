<template>
  <div class="product-add">
    <h3>Add Product</h3>
    <form class="pb-3">
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
    <button type="submit" class="btn btn-primary" @click="addProductAction">Save</button>
    <div class="pt-3"><router-link to="/products">Back to list</router-link></div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapActions } from "vuex";
import { ProductModel } from "@/models/ProductModel";
import { BrandModel } from "@/models/BrandModel";
import { CategoryModel } from "@/models/CategoryModel";
import { Money, Currencies } from 'ts-money';

export default defineComponent({
  props: {},
  data() {
    return {
      product: {
        productId: 0,
        productName: '',
        modelYear: 0,
        brand: {
          brandId: 0,
          name: ''
        },
        category: {
          categoryId: 0,
          name: ''
        },
        listPrice: 0
      } as ProductModel | null,
      brands: [] as BrandModel[] | null,
      categories: [] as CategoryModel[] | null
    };
  },
  created() {
    this.loadDropdowns();
  },
  methods: {
    ...mapActions("Product", ["addProduct"]),
    ...mapActions("Brand", ["getBrands"]),
    ...mapActions("Category", ["getCategories"]),
    async loadDropdowns() {
      try {
        this.brands = await this.getBrands();
        this.categories = await this.getCategories();
      } catch (error) {
        console.error(error);
      }
    },
    async addProductAction() {
      try {
        const productId = await this.addProduct(this.product);
        if (this.product != null && productId > 0) {
          this.product.productId = productId;
          alert("Product successfully added");
        }
      } catch (error) {
        alert('Oops! ' + error);
        console.error(error);
      }
    }
  }
});
</script>

<style>
  .product-add {
    text-align: left;
    max-width: 300px;
    margin: auto;
  }
</style>