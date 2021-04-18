<template>
  <validation-observer
    ref="registerObserver"
    class="register-form"
    v-slot="{ invalid }"
  >
    <section>
      <div class="error-message" v-if="message">
        <span>{{ message }}</span>
      </div>

      <validation-provider
        rules="required|min:3"
        name="Name"
        v-slot="{ errors }"
      >
        <b-field
          label="Name"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-input
            type="text"
            v-model="name"
            @keyup.native.enter="register(invalid)"
          ></b-input>
        </b-field>
      </validation-provider>

      <validation-provider
        rules="required|email"
        name="Email"
        v-slot="{ errors }"
      >
        <b-field
          label="Email"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-input
            type="email"
            v-model="email"
            @keyup.native.enter="register(invalid)"
          ></b-input>
        </b-field>
      </validation-provider>

      <validation-provider
        rules="required|min:8"
        name="Contraseña"
        vid="password"
        v-slot="{ errors }"
      >
        <b-field
          label="Contraseña"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-input
            type="password"
            v-model="pass"
            @keyup.native.enter="register(invalid)"
          ></b-input>
        </b-field>
      </validation-provider>

      <validation-provider
        rules="required|confirmed:password"
        name="Confirmar contraseña"
        v-slot="{ errors }"
      >
        <b-field
          label="Confirmar contraseña"
          :type="{ 'is-danger': errors[0] }"
          :message="errors"
        >
          <b-input
            type="password"
            v-model="confirmPass"
            @keyup.native.enter="register(invalid)"
          ></b-input>
        </b-field>
      </validation-provider>

      <div class="buttons">
        <button
          class="button green-button register-form_submit-button"
          @click="register(invalid)"
          :disabled="invalid"
        >
          Registrarse
        </button>
      </div>
    </section>
  </validation-observer>
</template>

<script lang="ts">
import { Component, Ref, Vue } from 'vue-property-decorator';

import { ValidationObserver, ValidationProvider } from 'vee-validate';

import { userStore } from '@/store/user/userStore';
import { HomeRouteName } from '@/router/routeNames';

@Component({
  name: 'RegisterForm',
  components: {
    ValidationObserver,
    ValidationProvider,
  },
})
export default class RegisterForm extends Vue {
  private userCtx = userStore.context(this.$store);

  @Ref()
  readonly registerObserver!: InstanceType<typeof ValidationObserver>;

  private name = '';
  private email = '';
  private pass = '';
  private confirmPass = '';
  private message = '';

  private async register(isInvalid: boolean) {
    if (isInvalid) {
      this.registerObserver.validate();
      return;
    }

    const registerRequest = {
      name: this.name,
      email: this.email,
      password: this.pass,
    };
    await this.userCtx.actions.register(registerRequest);
    if (this.userCtx.getters.isLoggedIn) {
      const homeRoute = { name: HomeRouteName };
      this.$router.push(homeRoute);
    } else {
      this.message = 'El email ya está registrado';
    }
  }
}
</script>

<style scoped lang="scss">
.register-form {
  text-align: left;

  & .error-message {
    color: $red;
    padding-bottom: 1rem;
  }

  & /deep/ .field {
    margin-bottom: 1rem;
  }

  &_submit-button {
    width: 100%;
    margin-top: 0.75rem;
  }
}
</style>
