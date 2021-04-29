import Vue from 'vue';
import App from './App.vue';
import router from './router';
import { store } from './store';

import Buefy from 'buefy';
import 'buefy/dist/buefy.css';

import { library } from '@fortawesome/fontawesome-svg-core';
import { faPen, faTrashAlt, faPlus } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

import '@/validation/formValidation';
import { ValidationProvider } from 'vee-validate';

Vue.use(Buefy);

Vue.component('ValidationProvider', ValidationProvider);

library.add(faPen, faTrashAlt, faPlus);
Vue.component('font-awesome-icon', FontAwesomeIcon);

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app');
