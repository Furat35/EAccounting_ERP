import axios from 'axios';

let localStorageToken = localStorage.getItem('tokenInfo');
let authToken = localStorageToken != null ? JSON.parse(localStorageToken).token : ''; 

export default axios.create({
  baseURL: 'https://localhost:7054/api',
  // timeout: 1000
  headers: { 'Authorization': authToken != '' ? `Bearer ${authToken}` : '' }
});