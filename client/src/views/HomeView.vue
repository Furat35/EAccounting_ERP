<script>
import headerNavbar from '@/components/layouts/header-navbar/index.vue'
import mainsidebar from '@/components/layouts/main-sidebar/index.vue'
import footer from '@/components/layouts/footer/index.vue'
import companyList from '@/components/companies/companyList.vue'
import userList from '@/components/users/userList.vue'
import homeList from '@/components/home/homeList.vue'

export default {
  data(){
    return{
      activeComponentName: 'homeList'
    }
  },
  components: {
    'app-header-navbar': headerNavbar,
    'app-main-sidebar': mainsidebar,
    'app-footer': footer,
    'companyList': companyList,
    'userList': userList,
    'homeList': homeList
  },
  methods: {
    setActiveComponent(activeComponentName){
      this.updateUrl(activeComponentName);
      this.activeComponentName = activeComponentName;
    },
    updateUrl(component) {
      // if (component === 'homeList') {
      //   this.$router.push('/');
      // }
      // else if (component === 'userList') {
      //   this.$router.push('/users');
      // } 
      // else if (component === 'companyList') {
      //   this.$router.push('/companies');
      // }
    }
  }
}
</script>

<template>
  <app-main-sidebar @activeComponent="setActiveComponent"></app-main-sidebar>
  <app-header-navbar></app-header-navbar>
  <div class="content-wrapper">
    <!-- <router-view></router-view>  -->
    <component :is="activeComponentName"></component>
  </div>
  <app-footer></app-footer>
</template>


