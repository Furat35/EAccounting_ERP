<template>
    <div class="modal fade" id="createUserModal" tabindex="-1" role="dialog" aria-labelledby="createUserModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Kullanıcı Ekleme</h1><button type="button" data-dismiss="modal" class="btn btn-outline-danger">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                            class="bi bi-x-lg" viewBox="0 0 16 16">
                            <path
                                d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z" />
                        </svg>
                    </button>
                </div>
                <form @submit.prevent autocomplete="off">
                    <div class="modal-body">
                        <transition name="fade">
                            <div v-html="invalidInputs" class="alert alert-danger mt-3 text-center"
                                v-if="invalidInputs != null" role="alert">
                            </div>
                        </transition>
                        <div class="form-group">
                            <label for="firstName">Ad</label>
                            <input type="text" minlength="3" name="firstName" v-model="createModel.firstName" class="form-control">
                        </div>
                        <div class="form-group mt-2">
                            <label for="lastName">Soyad</label>
                            <input type="text" minlength="3" name="lastName" v-model="createModel.lastName" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="userName">Kullanıcı Adı</label>
                            <input type="text" minlength="3" name="userName" v-model="createModel.userName" class="form-control">
                        </div>
                        <div class="form-group mt-2">
                            <label for="email">Mail Adresi</label>
                            <input type="email" minlength="3" name="email" v-model="createModel.email" class="form-control">
                        </div>
                        <div class="mb-3">
                            <div class="form-check">
                                <input class="form-check-input" type="checkbox"  name="isAdmin" v-model="createModel.isAdmin">
                                <label class="form-check-label" for="isAdmin">
                                    Admin mi?
                                </label>
                            </div>
                        </div>
                        <div class="form-group mt-2">
                            <label for="password">Şifre</label>
                            <input type="password" name="password" v-model="createModel.password" class="form-control">
                        </div>
                        <div class="form-group mt-2">
                            <label for="CompanyIds">Bağlı Olduğu Şirketler</label><br>
                            <select name="companyIds" class="form-control" multiple>
                                <option :value="company.id" v-for="company in companies">{{ company.name }}</option>
                            </select>
                        </div>
                    </div>
                    <div class="modal-footer"><button class="btn btn-dark w-100" @click="onSave">Kaydet</button></div>
                </form>
            </div>
        </div>
    </div>
    <spinner :loading="isLoading"/>
</template>

<script lang="ts">
import { UserCreateDto } from '@/models/Users/UserCreateDto';
import Spinner from '@/components/layouts/spinner/index.vue';
import Swal from 'sweetalert2';
import type { CompanyListDto } from '@/models/Companies/CompanyListDto';

export default {
    components: {
        'spinner': Spinner
    },
    data() {
        return {
            invalidInputs: null as string | null,
            companies: null as CompanyListDto[] | null,
            isLoading: false,
            createModel: new UserCreateDto()
        }
    },
    emits: ['newUserCreated'],
    created() {
        this.setCompanies();
    },
    methods: {
        onSave() {
            if(!this.checkInputs())
                return;
            this.isLoading = true;

            this.createModel.companyIds = $('#createUserModal [name="companyIds"]').val() as string[];
            this.$axios.post('/users/create', this.createModel)
                .then(() => {
                    Swal.fire("Kullanıcı başarıyla oluşturuldu!");
                    this.invalidInputs = null;
                    this.$emit('newUserCreated');
                })
                .catch(error => {
                    let errorDetail
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Kullanıcı oluşturulurken hata oluştu!";
                    }
                    Swal.fire({
                        title: 'Hata!',
                        text: errorDetail,
                        icon: 'error',
                        confirmButtonText: 'Kapat'
                    });
                })
                .finally(() => {
                    this.isLoading = false;
                })
        },
        setCompanies() {
            this.$axios.get('/companies/getall')
                .then(success => {
                    this.companies = success.data.data;
                })
                .catch(error => {
                    let errorDetail
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Kullanıcı oluşturulurken hata oluştu!";
                    }
                    Swal.fire({
                        title: 'Hata!',
                        text: errorDetail,
                        icon: 'error',
                        confirmButtonText: 'Kapat'
                    });
                })
        },
        checkInputs() {
            let errorMessages = []
            if (!this.createModel.userName)
                errorMessages.push('Kullanıcı adını giriniz!');
            if (!this.createModel.email)
                errorMessages.push('Email giriniz!');
            if (!this.createModel.firstName)
                errorMessages.push('Ad giriniz!');
            if (!this.createModel.lastName)
                errorMessages.push('Soyad adını giriniz!');
            if (!$('#createUserModal [name="companyIds"]').val())
                errorMessages.push('Şirket seçiniz!');

            if (errorMessages.length != 0) {
                this.invalidInputs = errorMessages.join('<br>');
                return false;
            }
            return true;
        }
    }
}
</script>
