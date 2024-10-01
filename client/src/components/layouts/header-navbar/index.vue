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
import Spinner from '@/components/layouts/spinner/index.vue';
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
                    let loginResponse = new LoginResponse(decodedToken.Id, decodedToken.Email, 
                    decodedToken.UserName, decodedToken.Name, decodedToken.CompanyId, companies); 
                    this.$store.commit("setUser", loginResponse);
                    localStorage.setItem('tokenInfo', jsonData);
                })
                .catch(error => {
                    let errorDetail
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Kullanıcı oluşturulurken hata oluştu!";
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