import { ComponentCustomProperties } from 'vue';
import { AxiosInstance } from 'axios';

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $axios: AxiosInstance; // Add this line to declare $axios
  }
}