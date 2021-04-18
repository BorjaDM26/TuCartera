import { FetchStatus } from '@/models/enum';
import { Getters } from 'vuex-smart-module';
import { UserState } from './userState';

export class UserGetters extends Getters<UserState> {
  get isLoggedIn(): boolean {
    return !!this.state.currentUser;
  }

  get isLoading(): boolean {
    const {
      fetchGetLoginStatus,
      fetchPostLoginStatus,
      fetchLogoutStatus,
      fetchRegisterStatus,
    } = this.state;
    return [
      fetchGetLoginStatus,
      fetchPostLoginStatus,
      fetchLogoutStatus,
      fetchRegisterStatus,
    ].includes(FetchStatus.LOADING);
  }
}
