import { Actions } from 'vuex-smart-module';
import { UserGetters } from './userGetters';
import { UserMutations } from './userMutations';
import { UserState } from './userState';

import {
  getLogin,
  postLogin,
  logout,
  register,
} from '@/webservices/user/userWebservices';
import { PostLoginRequest, RegisterRequest } from '@/webservices/user/models';
import { FetchStatus } from '@/models/enum';

export class UserActions extends Actions<
  UserState,
  UserGetters,
  UserMutations,
  UserActions
> {
  public async initialise(): Promise<void> {
    await this.dispatch('getLogin');
  }

  public async getLogin(): Promise<void> {
    try {
      this.commit('setFetchGetLoginStatus', FetchStatus.LOADING);
      const user = await getLogin();
      this.commit('setCurrentUser', user);
      this.commit('setFetchGetLoginStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchGetLoginStatus', FetchStatus.FAILURE);
    }
  }

  public async doLogin({ email, password }: PostLoginRequest): Promise<void> {
    try {
      this.commit('setFetchPostLoginStatus', FetchStatus.LOADING);
      await postLogin({ email, password });
      await this.dispatch('getLogin');
      this.commit('setFetchPostLoginStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchPostLoginStatus', FetchStatus.FAILURE);
    }
  }

  public async logout(): Promise<void> {
    try {
      this.commit('setFetchLogoutStatus', FetchStatus.LOADING);
      await logout();
      this.commit('setCurrentUser', null);
      this.commit('setFetchLogoutStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchLogoutStatus', FetchStatus.FAILURE);
    }
  }

  public async register({
    name,
    email,
    password,
  }: RegisterRequest): Promise<void> {
    try {
      this.commit('setFetchRegisterStatus', FetchStatus.LOADING);
      await register({ name, email, password });
      await this.dispatch('getLogin');
      this.commit('setFetchRegisterStatus', FetchStatus.SUCCESS);
    } catch (error) {
      this.commit('setFetchRegisterStatus', FetchStatus.FAILURE);
    }
  }
}
