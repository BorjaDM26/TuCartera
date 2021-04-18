import { User } from '@/models/api';
import { FetchStatus } from '@/models/enum';
import { Mutations } from 'vuex-smart-module';
import { UserState } from './userState';

export class UserMutations extends Mutations<UserState> {
  setCurrentUser(user: User | null): void {
    this.state.currentUser = user;
  }

  setFetchGetLoginStatus(status: FetchStatus): void {
    this.state.fetchGetLoginStatus = status;
  }

  setFetchPostLoginStatus(status: FetchStatus): void {
    this.state.fetchPostLoginStatus = status;
  }

  setFetchLogoutStatus(status: FetchStatus): void {
    this.state.fetchLogoutStatus = status;
  }

  setFetchRegisterStatus(status: FetchStatus): void {
    this.state.fetchRegisterStatus = status;
  }
}
