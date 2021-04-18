import { User } from '@/models/api';
import { FetchStatus } from '@/models/enum';

export class UserState {
  currentUser: User | null = null;
  fetchGetLoginStatus: FetchStatus = FetchStatus.PENDING;
  fetchPostLoginStatus: FetchStatus = FetchStatus.PENDING;
  fetchLogoutStatus: FetchStatus = FetchStatus.PENDING;
  fetchRegisterStatus: FetchStatus = FetchStatus.PENDING;
}
