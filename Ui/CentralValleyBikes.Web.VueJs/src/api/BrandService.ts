import CentralValleyBikesServiceClient from "./serviceclients/CentralValleyBikesServiceClient"
import BaseService from "./BaseService";

export default class ProductService extends BaseService {
  constructor () {
    super(CentralValleyBikesServiceClient, "brands");
  }
} 
