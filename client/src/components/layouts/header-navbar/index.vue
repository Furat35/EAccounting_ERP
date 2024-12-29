<template>
    <nav class=" main-header navbar navbar-expand navbar-white navbar-light">
        <ul class="navbar-nav">
            <li class="nav-item">
                <a class="nav-link" data-widget="pushmenu" role="button"><i class="fas fa-bars"></i></a>
            </li>
            <li class="nav-item">
                <select class="form-control " v-model="selectedCompanyId"  @change="companyChange()">
                    <option v-for="userCompany in userCompanies" :value="userCompany.id" :key="userCompany.id">
                        {{ userCompany.name }}
                    </option>
                </select>
            </li>
        </ul>

        <!-- Right navbar links -->
        <ul class="navbar-nav ml-auto">
            <li class="nav-item" @click="logout">
                <a class="nav-link btn btn-outline-danger">
                    <i class="fas fa-solid fa-power-off text-danger"></i>
                </a>
            </li>
        </ul>
    </nav>
</template>

<script>
import { LoginResponse } from '@/models/Logins/LoginResponse';
import { jwtDecode } from "jwt-decode"

export default {
    data(){
        return {
            isLoading: false,
            userCompanies: null,
            selectedCompanyId: null,
        }
    },
    methods:{
        logout(){
            localStorage.clear();
            this.$store.commit('resetState');
            this.$router.push({name: 'login'});
        },
        companyChange(){
            this.isLoading = true;
            this.$axios.post('auth/ChangeCompany', { companyId: this.selectedCompanyId})
                .then(response => {
                    localStorage.clear();
                    var jsonData = JSON.stringify(response.data.data);
                    const decodedToken = jwtDecode(response.data.data.token);
                    let companies = JSON.parse(decodedToken.Companies);
                    let isAdmin = decodedToken.IsAdmin.toLowerCase() === 'true';
                    
                    let loginResponse = new LoginResponse();
                    loginResponse.id = decodedToken.Id, 
                    loginResponse.email = decodedToken.Email, 
                    loginResponse.username = decodedToken.UserName, 
                    loginResponse.name = decodedToken.Name, 
                    loginResponse.companyId = decodedToken.CompanyId, 
                    loginResponse.companies = companies; 
                    loginResponse.isAdmin = isAdmin;
                    this.$store.commit("setUser", loginResponse);
                    localStorage.setItem('tokenInfo', jsonData);
                    location.reload(true);
                    // this.$router.push(currentPage);
                })
                .catch(error => {
                    let errorDetail
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Şirket değiştirilirken hata oluştu!";
                    }
                    this.errorMessage = errorDetail;
                    setTimeout(() => {
                        this.errorMessage = null;
                    }, 5000);
                })
                .finally(() => {
                    this.isLoading = false;
                });
        }
    },
    mounted(){
        this.userCompanies = this.$store.getters._getUser.companies;
        this.selectedCompanyId = this.$store.getters._getUser.companyId;
    }
}
</script>

<style scoped>
a:hover i {
    color: white !important;
}
</style>