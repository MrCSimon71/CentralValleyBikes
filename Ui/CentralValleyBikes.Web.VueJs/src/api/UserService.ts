import CentralValleyBikesServiceClient from "./serviceclients/CentralValleyBikesServiceClient"
import BaseService from "./BaseService";
import QueryParams from "./QueryParams";

export default class UserService extends BaseService {
  constructor () {
    super(CentralValleyBikesServiceClient, "users");
  }

  getUsers(queryParams: QueryParams<string>) {
    return this.execute("get", "/", queryParams, null);
  }

  registerUser(data: string) {
    return this.execute("post", "/register", null, data);
  }
} 
