import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";
import './assets/css/main.css';
//import './assets/css/sidebar.css';
//import './assets/css/sidebar2.css'
//import './assets/css/responsive.css';
//import 'boxicons';


const theApp = createApp({
  extends: App,
  beforeCreate() {
    this.$store.dispatch('Auth/initialize');
  }
});

theApp.use(store).use(router).mount("#app");
