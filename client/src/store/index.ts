import type { LoginResponse } from '@/models/Logins/LoginResponse';
import { createStore } from 'vuex';
import createPersistedState from 'vuex-persistedstate'
import CryptoJS from 'crypto-js';

let secretKey: 'C3EB081F-8ACA-4D88-AA46-DA8BFAC053E9'

interface State {
  secretKey: string,
  user: any
}

const getDefaultState = () => ({
  user: {
    id: '',
    email: '',
    username: '',
    name: '',
    companyId: '',
    companies: []
  }
});

export default createStore<State>({
  state: {
    user: {
      id: '',
      email: '',
      username: '',
      name: '',
      companyId: '',
      companies: [],
      isAdmin: false
    },
  },
  getters: {
    _secretKey: (state: State): string => secretKey,
    _isAuthorized: (state: State): boolean => !!state.user.id,
    _getUser: (state: State) => state.user 
  },
  mutations: {
    setUser(state: any, user: LoginResponse){
      state.user = { ...user}  
    },
    resetState(state: State){
      Object.assign(state, getDefaultState())
    }
  },
  plugins: [
    createPersistedState({
    key: 'user', 
    storage: window.localStorage,
    setState: (key, state) => {
      const encryptedState = CryptoJS.AES.encrypt(JSON.stringify(state), '29E7CCB9-CE83-4DFE-B8F8-3B706F6335F1').toString();
      window.localStorage.setItem(key, encryptedState);
    },
    getState: (key) => {
      const encryptedState = window.localStorage.getItem(key);
      if (encryptedState) {
        const bytes = CryptoJS.AES.decrypt(encryptedState, '29E7CCB9-CE83-4DFE-B8F8-3B706F6335F1');
        const decryptedState = bytes.toString(CryptoJS.enc.Utf8);
        return JSON.parse(decryptedState);
      }
      return null; 
    }  
  })
]
});
