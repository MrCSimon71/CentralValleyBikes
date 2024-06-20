<template>
  <div class="product-listing">
    <div id="pageHeader" class="page-header">
      <div class="page-title">Products</div>
      <div class="d-flex float-end">
        <div id="searchForm" class="d-flex pe-2">
          <label for="searchValue" style="font-weight: bold;float:left;padding-top: 7px">Search:</label>
          <select id="searchField" name="searchField" class="form-control form-select search-field"
                  v-model="searchField" style="width: 120px">
            <option value="productName">Name</option>
            <option value="modelYear">Model Year</option>
            <option value="brand">Brand</option>
            <option value="category">Category</option>
          </select>
          <span>
            <input type="text" id="searchValue" class="form-control" v-model="searchValue" />
          </span>
          <button type="button" class="btn btn-primary" @click="getProductsAction(1)"> Search </button>
        </div>
        <router-link :to="{ name: 'product-add' }" v-slot="{ addProduct }">
          <button @click="addProduct" class="btn btn-primary pl-3">
            Add Product
          </button>
        </router-link>
      </div>
    </div>
    <div id="productsGrid" v-if="products && products.length > 0" class="pt-3" style="font-size: 14px;">
      <div id="gridData" style="height:500px">
        <table style="text-align: left; width: 100%; overflow-y: scroll; display: inline-block; height: 485px ">
          <thead>
            <tr>
              <th width="70">Id</th>
              <th width="700">Name</th>
              <th width="200">Model Year</th>
              <th width="300">Brand</th>
              <th width="300">Category</th>
              <th width="300" style="text-align: right">List Price</th>
              <th width="40%"></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="product in products" :key="product.productId" >
              <td class="no-wrap">{{ product.productId }}</td>
              <td>{{ product.productName }}</td>
              <td>{{ product.modelYear }}</td>
              <td>{{ product.brand.name }}</td>
              <td>{{ product.category.name }}</td>
              <td style="text-align: right">{{ product.listPrice }}</td>
              <td style="padding: 0 20px 0 50px; text-align: left">
                <router-link :to="{ name: 'product-details', params: { id: product.productId }}">View</router-link>&nbsp;|&nbsp;
                <router-link :to="{ name: 'product-edit', params: { id: product.productId }}">Edit</router-link>&nbsp;|&nbsp;
                <router-link to="/products" @click="this.deleteProductAction(product.productId)">Delete</router-link>
              </td>
            </tr>
            <tr>&nbsp;</tr>
          </tbody>
        </table>
      </div>
      <div id="gridNavigation">
        <div class="float-start d-flex">
          <div class="form-group">
            <!--<label for="pageSize" class="pageSize-label">Size</label>-->
            <span>
              <select id="pageSize" name="pageSize" v-model="pageSize" class="form-control form-select"
                      @change="getProductsAction(1)">
                <option value="10">10 items per page</option>
                <option value="20">20 items per page</option>
                <option value="30">30 items per page</option>
                <option value="50">50 items per page</option>
              </select>
            </span>
          </div>
        </div>
        <nav id="pageNavigation">
          <ul class="pagination">
            <li class="page-item">
              <button type="button" class="page-link" :disabled="disablePrevious" @click="getProductsAction(1)"> First </button>
            </li>
            <li class="page-item">
              <button type="button" class="page-link" :disabled="disablePrevious" @click="getProductsAction(currentPage-1)"> Previous </button>
            </li>
            <li class="page-item">
              <button type="button" class="page-link" :class="currentPage == pageNumber ? 'active-page' : ''"
                      v-for="pageNumber in pageButtonsToDisplay" v-bind:key="pageNumber" :disabled="currentPage == pageNumber ? true : false"
                      @click="getProductsAction(pageNumber)">
                {{pageNumber}}
              </button>
            </li>
            <li class="page-item">
              <button type="button" @click="getProductsAction(currentPage+1)" :disabled="disableNext" class="page-link"> Next </button>
            </li>
            <li class="page-item">
              <button type="button" @click="getProductsAction(totalPages)" :disabled="disableNext" class="page-link"> Last </button>
            </li>
          </ul>
        </nav>
      </div>
    </div>
    <div v-else>
      No products found.
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapActions, mapGetters } from "vuex";
import { ProductModel } from "@/models/ProductModel";
import { PagingAttributesModel } from "@/models/PagingAttributesModel";
import QueryParams from "@/api/QueryParams";

  export default defineComponent({
    props: {},
    data() {
      return {
        currentPage: 1,
        pageNumber: 1,
        pageSize: 20,
        totalPages: 0,
        pageButtonsToDisplay: [] as number[],
        products: [] as ProductModel[] | null,
        searchField: 'productName',
        searchValue: '',
      };
    },
    created() {
      this.getProductsAction(this.pageNumber);
    },
    methods: {
      ...mapActions("Product", [ "getProducts", "deleteProduct" ]),
      ...mapGetters("Product", [ "getPagingAttributes" ]),
      async getProductsAction(pageNumber: number) {
        this.products = null;
        try {

          const queryParams: QueryParams<string> = {
            pageNumber: pageNumber.toString(),
            pageSize: this.pageSize.toString(),
          };

          if (this.searchField !== '' && this.searchValue !== '') {
            queryParams[this.searchField] = this.searchValue;
          }

          this.products = await this.getProducts(queryParams);

          this.setPagingAttributes();
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
          this.getProductsAction(this.currentPage);
        }
        catch (error) {
          alert('Oops! ' + error);
          console.error(error);
        }
      },
      setPagingAttributes() {

        const pagingAttributes: PagingAttributesModel = this.getPagingAttributes();
        this.currentPage = pagingAttributes.currentPage;
        this.totalPages = pagingAttributes.totalPages;

        let maxViewablePageButtons = 6;
        this.pageButtonsToDisplay = [];
        let startingPage: number;

        if (this.totalPages < maxViewablePageButtons)
          maxViewablePageButtons = this.totalPages;

        if (this.currentPage == 1) {
          startingPage = 1;
        } else if (this.currentPage == this.totalPages) {
          startingPage = this.totalPages - maxViewablePageButtons+1;
        } else {
          startingPage = (this.currentPage - 2) > 0 ? this.currentPage - 2 : 1;
        }

        let endingPage = Math.min(startingPage + maxViewablePageButtons - 1, this.totalPages);

        const c = (endingPage - startingPage) + 1;

        if (c < maxViewablePageButtons) {
          startingPage = startingPage - (maxViewablePageButtons - c);
        }

        for (let i = startingPage; i <= endingPage; i++) {
          this.pageButtonsToDisplay.push(i);
        }
      }
    },
    computed: {
      disablePrevious(): boolean {
        return (this.currentPage == 1);
      },
      disableNext(): boolean {
        return (this.currentPage == this.totalPages);
      },
      isActivePage(pageNumber: number): boolean {
        return (this.currentPage == pageNumber);
      }
  }
});

</script>

<style scoped>
  .product-listing {
      padding: 12px;
      text-align: left;
      width: 100%;
  }
  
  .page-header {
      margin-bottom: 20px;
  }
  
  .page-title {
    font-size: 24px;
    font-weight: bold;
    display: inline-flex;
  }

  #gridData {
      margin-bottom: 30px;
  }

  #gridData thead th {
    top: 0;
    position: sticky;
    z-index: 20;
    min-height: 30px;
    height: 30px;
    padding-bottom: 12px;
    background-color: burlywood;
    padding-top: 8px;
    margin-bottom: 6px;
  }

  #gridData tbody > tr:first-child td {
      padding-top: 10px;
  }

  #gridNavigation {
      margin-bottom: 20px;
  }

  .header-action {
      margin-left: auto;
      /*width: 10%;*/
      display: inline-block;
      padding: 5px;
  }

  #pageNavigation {
      padding: 0px;
      float: right;
  }

  .pageSize-label {
    display: flex;
    align-items: center;
    font-weight: bold;
    float: left;
    font-size: 12pt;
    padding-top: 7px;
  }

  button.page-link {
    display: inline-block;
  }

  button.page-link {
    font-size: 20px;
    color: #29b3ed;
    font-weight: 500;
  }

  button.page-link:disabled {
    color: #dddddd;
    pointer-events: none;
  }

  button.active-page {
    background-color: blue;
    color: #ffffff;
  }
  span {
    display: block;
    overflow: hidden;
    padding: 0px 4px 0px 6px;
  }
  search-field {
    width: 120px !important;
    background-color: burlywood;
  }
</style>