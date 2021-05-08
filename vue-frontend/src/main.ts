import Vue from 'vue';
import App from './App.vue';
import router from './router';
import { store } from './store';

import Buefy from 'buefy';
import 'buefy/dist/buefy.css';

import VueMq from 'vue-mq';

import { library } from '@fortawesome/fontawesome-svg-core';
import {
  faPen,
  faTrashAlt,
  faPlus,
  faChevronLeft,
  faChevronRight,
} from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

import '@/validation/formValidation';
import { ValidationProvider } from 'vee-validate';

import VueVirtualScroller from 'vue-virtual-scroller';
import 'vue-virtual-scroller/dist/vue-virtual-scroller.css';

Vue.use(VueVirtualScroller);

Vue.use(Buefy);

Vue.use(VueMq, {
  breakpoints: {
    xs: 450,
    sm: 768,
    md: 1024,
    lg: 1200,
    xl: Infinity,
  },
  defaultBreakpoint: 'xs',
});

Vue.component('ValidationProvider', ValidationProvider);

library.add(faPen, faTrashAlt, faPlus, faChevronLeft, faChevronRight);
Vue.component('font-awesome-icon', FontAwesomeIcon);

Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app');
