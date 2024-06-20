import CentralValleyBikesServiceClient from "./serviceclients/CentralValleyBikesServiceClient"
import BaseService from "./BaseService";

export default class CustomerService extends BaseService {
  constructor () {
    super(CentralValleyBikesServiceClient, "customers");
  }

  getCustomers() {
    //console.log('[CustomerService.ts] => getCustomers');
    return this.execute("get", "/", null, null);
  }

  //getById(id: number) : number{
  //  return this.getById(id);
  //}

} 
