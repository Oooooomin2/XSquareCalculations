import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import store from './store/store';
import './assets/style/tailwind.css'
import AnimateCss from 'animate.css';
import axios from 'axios';

axios.defaults.baseURL = "https://localhost:44301/api"

store.dispatch('autoLogin')
createApp(App).use(AnimateCss).use(router).use(store).mount('#app')