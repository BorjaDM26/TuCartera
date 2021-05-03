import Vue from 'Vue';

declare module 'vue/types/vue' {
  interface Vue {
    $mq: 'xs' | 'sm' | 'md' | 'lg' | 'xl';
  }
}
