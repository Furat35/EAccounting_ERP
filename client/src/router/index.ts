import { createRouter, createWebHashHistory, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import UserListComponent from '@/components/users/userList.vue'
import CompanyListComponent from '@/components/companies/companyList.vue'
import HomeListComponent from '@/components/home/homeList.vue'
import CashRegisterListComponent from '@/components/cashRegisters/cashRegisterList.vue'
import BankDetailListComponent from '@/components/bankDetails/BankDetailList.vue'
import store from '../store/index'
import CashRegisterDetailListComponent from '@/components/cashRegisterDetails/CashRegisterDetailList.vue'
import BanksListComponent from '@/components/banks/bankList.vue'
import CustomerListComponent from '@/components/customers/customerList.vue'
import CustomerDetailListComponent from '@/components/customerDetails/customerDetailList.vue'
import ProductListComponent from '@/components/products/productList.vue'
import ProductDetailListComponent from '@/components/productDetails/productDetailList.vue'

let authNotRequiredRoutes =[
  'login',
  'statuscode'
]


const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    {
      name: 'home',
      path: '/',
      component: HomeView,
      children: [
        {
          path: 'home',
          component: HomeListComponent
        },
        {
          path: 'users',
          component: UserListComponent
        },
        {
          path: 'companies',
          component: CompanyListComponent
        },
        {
          path: 'cashRegisters',
          component: CashRegisterListComponent
        },
        {
            path: 'cashRegisterDetails/:id',
            component: CashRegisterDetailListComponent,
        },
        {
          path: '/banks',
          component: BanksListComponent,
        },
        {
          path: '/bankDetails/:id',
          component: BankDetailListComponent,
        },
        {
          path: '/customers',
          component: CustomerListComponent,
        },
        {
          path: '/customerDetails/:id',
          component: CustomerDetailListComponent,
        },
        {
          path: '/products',
          component: ProductListComponent,
        },
        {
          path: '/productDetails/:id',
          component: ProductDetailListComponent,
        }
      ],
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue')
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('../views/LoginView.vue')
    },
    {
      path: '/statuscode',
      name: 'statuscode',
      component: () => import('../views/StatusCodeView.vue')
    }
  ]
})

router.beforeEach((to, from, next) => {
  if(!router.getRoutes().find(r => r.name == to.name))
    return

  if (store.getters._isAuthorized) {
    // if(authNotRequiredRoutes.indexOf(to.name) != -1)
      next();
  }
  else {
    if(authNotRequiredRoutes.indexOf(to.name) > -1)
      next();
    else if(from.name == 'statuscode' && authNotRequiredRoutes.indexOf(to.name) == -1)
      next({name : 'login'});
    else
      next({ name: 'statuscode'})
  }
});

export default router
