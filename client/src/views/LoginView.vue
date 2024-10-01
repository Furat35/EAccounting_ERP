<template>
    <div class="hold-transition login-div">
        <div class="row login-row">
            <div class="col-md-8 login-part-1">
                <img src="https://www.b-fast.it/wp-content/uploads/2020/07/cosa-fare-quando-si-e-esclusi-da-wp-admin-in-wordpress.jpg"
                    style="width: 100%; height: 100%; object-fit: cover;">
            </div>
            <div class="col-md-4 login-part-2">
                <h1 class="text-center"><b>E-Muhasebe</b></h1>

                <p class="login-box-msg">Giriş yapmak için bilgilerinizi doldurun</p>

                <div class="input-group mb-3">
                    <input type="email" id="email" class="form-control" placeholder="Mail">
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fas fa-envelope"></span>
                        </div>
                    </div>
                </div>
                <div class="input-group mb-3">
                    <input type="password" id="password" class="form-control" placeholder="Şifre">
                    <div class="input-group-append">
                        <div class="input-group-text">
                            <span class="fas fa-lock"></span>
                        </div>
                    </div>
                </div>

                <button v-if="!spinnerIsActive" class="btn btn-dark btn-block" @click="login()">
                    <i class="fa-solid fa-lock me-1"></i>
                    Giriş Yap
                </button>
                <button v-else class="btn btn-dark" type="button" disabled>
                    <span class="spinner-border spinner-border-sm me-1" aria-hidden="true"></span>
                    <span role="status">Giriş yapılıyor...</span>
                </button>
                {{ this.$store.state.name }}
                <transition name="fade" >
                    <div  class="alert alert-danger mt-3 text-center" id="errorMessage" 
                    v-if="this.errorMessage != null" role="alert">
                        {{ errorMessage }}
                    </div>
                </transition>
            </div>
        </div>
    </div>
</template>

<script>
import { Login } from '@/models/Logins/Login'
import { LoginResponse } from '@/models/Logins/LoginResponse';
import { jwtDecode } from "jwt-decode"

export default {
    data() {
        return {
            spinnerIsActive: false,
            errorMessage: null
        }
    },
    methods: {
        login() {
            this.spinnerIsActive = true
            let loginData = new Login($('#email').val(), $('#password').val());
            this.$axios.post('auth/login', loginData)
                .then(response => {
                    console.log(response.data.data)
                    var jsonData = JSON.stringify(response.data.data)
                    const decodedToken = jwtDecode(response.data.data.token)
                    let companies = JSON.parse(decodedToken.Companies);
                    let loginResponse = new LoginResponse(decodedToken.Id, decodedToken.Email, 
                    decodedToken.UserName, decodedToken.Name, decodedToken.CompanyId, companies);
                    console.log(loginResponse);
                     this.$store.commit("setUser", loginResponse)
                    localStorage.setItem('tokenInfo', jsonData)
                    this.$router.push({name: 'home'})

                })
                .catch(error => {
                    let errorDetail
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Kullanıcı oluşturulurken hata oluştu!";
                    }
                    this.errorMessage = errorDetail
                    setTimeout(() => {
                        this.errorMessage = null
                    }, 5000);
                })
                .finally(() => {
                    this.spinnerIsActive = false;
                });
        }
    }
}
</script>

<style>
.login-row {
    width: 99%;
}

.login-part-2 {
    height: 100vh;
    display: flex;
    flex-direction: column;
    justify-content: center;

}

.fade-enter-active,
.fade-leave-active {
    transition: opacity 0.7s ease;
}

.fade-enter,
.fade-leave-to {
    opacity: 0;
}
</style>