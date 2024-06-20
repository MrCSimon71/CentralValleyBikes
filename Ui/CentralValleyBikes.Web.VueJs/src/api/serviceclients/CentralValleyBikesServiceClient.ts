import axios from "axios";
import interceptors from "../interceptors/response.interceptor";

const CentralValleyBikesServieClient = axios.create({
     baseURL: process.env.VUE_APP_CENTRALVALLEYBIKES_SERVICE_URL,
     headers: {
          'Content-Type': 'application/json',
    }
});

CentralValleyBikesServieClient.interceptors.response.use(
  interceptors.responseInterceptor,
  interceptors.errorInterceptor);

export default CentralValleyBikesServieClient;
