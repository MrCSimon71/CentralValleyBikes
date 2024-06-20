import CentralValleyBikesServiceClient from "./serviceclients/CentralValleyBikesServiceClient"
import BaseService from "./BaseService";
import QueryParams from "./QueryParams";

export default class ProductService extends BaseService {
  constructor () {
    super(CentralValleyBikesServiceClient, "products");
  }

  getProducts(queryParams: QueryParams<string>) {
    return this.execute("get", "/", queryParams, null);
  }
} 
