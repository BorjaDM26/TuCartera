<template>
  <div class="app-footer" :class="isSmallScreen && 'app-footer--mobile'">
    <router-link class="app-footer_logo-container" :to="{ path: '/' }">
      <img
        class="app-footer_logo"
        alt="Tu Cartera logo"
        src="../../assets/logo.svg"
      />
    </router-link>
    <div class="app-footer_right">
      <span class="app-footer_attibution">
        Data provided by Financial Modeling Prep
      </span>
      <span class="app-footer_copyright">&copy;2020 Borja Delgado Martín</span>
    </div>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component({
  name: 'AppFooter',
})
export default class AppFooter extends Vue {
  private get isSmallScreen(): boolean {
    console.log('MQ', this.$mq, ['xs', 'sm'].includes(this.$mq));
    return ['xs', 'sm'].includes(this.$mq);
  }
}
</script>

<style scoped lang="scss">
.app-footer {
  display: flex;
  background-color: $light-black;
  color: $white;
  padding: 0 $navbar-h-pad;
  min-height: $header-height;
  font-size: $footer-font-size;

  &_logo {
    width: auto;
    min-height: $header-height;
    max-height: $header-height;
  }

  &_logo-container {
    display: flex;
    padding: 0 0.75rem;
  }

  &_right {
    display: flex;
    justify-content: center;
    align-items: flex-end;
    flex-direction: column;
    flex-grow: 1;
    padding: 0.5rem 0.75rem;

    & > span {
      padding: 0.25rem 0;
    }
  }

  &--mobile {
    flex-direction: column;
    align-items: center;

    & .app-footer_right {
      align-items: center;
    }
  }
}
</style>
