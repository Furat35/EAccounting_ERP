<template>
  <aside class="main-sidebar sidebar-dark-primary elevation-4">
    <a class="brand-link">
      <img src="../../../assets/logo.jpg" alt="AdminLTE Logo" class="brand-image img-circle elevation-3"
        style="opacity: .8">
      <span class="brand-text font-weight-light">E-Muhasebe</span>
    </a>

    <div class="sidebar">
      <div class="user-panel mt-3 pb-3 mb-3 d-flex">
        <div class="image">
          <img src="https://www.transparentpng.com/download/user/gray-user-profile-icon-png-fP8Q1P.png"
            class="img-circle elevation-2" alt="User Image" style="background-color: white;">
        </div>
        <div class="info">
          <a href="#" class="d-block"> {{ this.$store.state.name }}</a>
        </div>
      </div>

      <!-- SidebarSearch Form -->
      <div class="form-inline">
        <div class="input-group">
          <input class="form-control form-control-sidebar" type="search" placeholder="Search">
          <div class="input-group-append">
            <button class="btn btn-sidebar">
              <i class="fas fa-search fa-fw"></i>
            </button>
          </div>
        </div>
      </div>

      <nav class="mt-2">
        <ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
          <li class="nav-item">
            <router-link to="/home" class="nav-link active" style="cursor: pointer;">
              <i class="nav-icon"></i>
              <p>
                Ana Sayfa
              </p>
            </router-link>
          </li>
          <li class="nav-item" v-if="userIsAdmin">
            <router-link to="/users" class="nav-link" style="cursor: pointer;">
              <i class="nav-icon"></i>
              <p>
                Kullanıcılar
              </p>
            </router-link>
          </li>
          <li class="nav-item" v-if="userIsAdmin">
            <router-link to="/companies" class="nav-link" style="cursor: pointer;">
              <i class="nav-icon"></i>
              <p>
                Şirketler
              </p>
            </router-link>
          </li>
          <li class="nav-item" v-if="userIsAdmin">
            <router-link to="/cashRegisters" class="nav-link" style="cursor: pointer;">
              <i class="nav-icon"></i>
              <p>
                Kasalar
              </p>
            </router-link>
          </li>
          <li class="nav-item" v-if="userIsAdmin">
            <router-link to="/banks" class="nav-link" style="cursor: pointer;">
              <i class="nav-icon"></i>
              <p>
                Bankalar
              </p>
            </router-link>
          </li>
          <li class="nav-item" v-if="userIsAdmin">
            <router-link to="/customers" class="nav-link" style="cursor: pointer;">
              <i class="nav-icon"></i>
              <p>
                Cariler
              </p>
            </router-link>
          </li>
          <li class="nav-item" v-if="userIsAdmin">
            <router-link to="/products" class="nav-link" style="cursor: pointer;">
              <i class="nav-icon"></i>
              <p>
                Stoklar
              </p>
            </router-link>
          </li>
        </ul>
      </nav>
    </div>
  </aside>
</template>

<script >
export default{
  data(){
    return {
      activeLinkName: 'homeList',
      user: false
    }
  },
  created(){
    this.user = this.$store.getters._getUser;
  },
  mounted(){
    this.setActiveLink();

  },
  emits: ['activeComponent'],
  methods:{
    setActiveLink(){
      $('.main-sidebar .nav .nav-link').on('click', function(){
        $(`.main-sidebar .nav .nav-link`).removeClass('active');
        $(this).addClass('active')
      });
    }
  },
  computed:{
    userIsAdmin(){
      return this.user.isAdmin;
    }
  }
 
}
</script>