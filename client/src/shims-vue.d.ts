import { Store } from 'vuex';
import { Router, RouteLocationNormalizedLoaded } from 'vue-router';
import { State } from './store';

declare module '*.vue' {
    import type { DefineComponent } from 'vue';
    const component: DefineComponent<{}, {}, any>;
    export default component;
  }

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $store: Store<State>; // Change State to your actual Vuex state type
    $router: Router;
    $route: RouteLocationNormalizedLoaded;
  }
}