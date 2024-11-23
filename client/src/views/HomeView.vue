<script>
import headerNavbar from '@/components/layouts/header-navbar/index.vue'
import mainsidebar from '@/components/layouts/main-sidebar/index.vue'
import footer from '@/components/layouts/footer/index.vue'
import companyList from '@/components/companies/companyList.vue'
import userList from '@/components/users/userList.vue'
import homeList from '@/components/home/homeList.vue'
import cashRegisterList from '@/components/cashRegisters/cashRegisterList.vue'
import Spinner from '@/components/layouts/spinner/index.vue';

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
    'homeList': homeList,
    'cashRegisterList': cashRegisterList
  }
}
</script>

<template>
  <app-main-sidebar></app-main-sidebar>
  <app-header-navbar></app-header-navbar>
  <div class="content-wrapper">
    <router-view></router-view> 
  </div>
  <app-footer></app-footer>
</template>


