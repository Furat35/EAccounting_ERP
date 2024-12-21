<template>
    <div class="modal fade" id="updateUserModal" tabindex="-1" role="dialog" aria-labelledby="updateUserModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Kullanıcı Güncelleme</h1><button type="button" data-dismiss="modal"
                        class="btn btn-outline-danger">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                            class="bi bi-x-lg" viewBox="0 0 16 16">
                            <path
                                d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z" />
                        </svg>
                    </button>
                </div>
                <form @submit.prevent autocomplete="off">
                    <div class="modal-body">
                        <div class="form-group">
                            <input type="text" name="id" hidden class="form-control" v-model="updateModel.id">
                        </div>
                        <div class="form-group">
                            <label for="firstName">Ad</label>
                            <input type="text" minlength="3" name="firstName" class="form-control"
                                v-model="updateModel.firstName">
                        </div>
                        <div class="form-group mt-2">
                            <label for="lastName">Soyad</label>
                            <input type="text" minlength="3" name="lastName" class="form-control"
                                v-model="updateModel.lastName">
                        </div>
                        <div class="form-group">
                            <label for="userName">Kullanıcı Adı</label>
                            <input type="text" minlength="3" name="userName" class="form-control"
                                v-model="updateModel.userName">
                        </div>
                        <div class="form-group mt-2">
                            <label for="email">Mail Adresi</label>
                            <input type="email" minlength="3" name="email" class="form-control"
                                v-model="updateModel.email">
                        </div>
                        <div class="mb-3">
                            <div class="form-check">
                                <input class="form-check-input" type="checkbox" name="isAdmin" v-model="updateModel.isAdmin">
                                <label class="form-check-label" for="isAdmin">
                                    Admin mi?
                                </label>
                            </div>
                        </div>
                        <div class="form-group mt-2">
                            <label for="password">Şifre</label>
                            <input type="password" name="password" class="form-control" v-model="updateModel.password">
                        </div>
                        <div class="form-group mt-2">
                            <label for="CompanyIds">Bağlı Olduğu Şirketler</label><br>
                            <select name="companyIds" class="form-control" multiple>
                                <option :value="company.id" v-for="company in companies"
                                    :selected="isUsersCompany(company.id)">{{ company.name }}</option>
                            </select>
                        </div>
                    </div>
                    <div class="modal-footer"><button class="btn btn-dark w-100" @click="onUpdate">Güncelle</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
    <spinner :loading="isLoading"/>
</template>

<script lang="ts">
import { UserUpdateDto } from '@/models/Users/UserUpdateDto';
import Spinner from '@/components/layouts/spinner/index.vue';
import Swal from 'sweetalert2';
import type { CompanyListDto } from '@/models/Companies/CompanyListDto';
import { UserListDto } from '@/models/Users/UserListDto';

export default {
    components: {
        'spinner': Spinner
    },
    emits: ['userUpdated'],
    props: {
        'selectedUser': {
            type: UserListDto,
            required: true
        }
    },
    watch: {
        selectedUser(){
            this.invalidInputs = null;
            this.updateModel = Object.assign(new UserUpdateDto(), this.selectedUser);
        }
    },
    data() {
        return {
            invalidInputs: null as string | null,
            companies: null as CompanyListDto[] | null,
            isLoading: false,
            updateModel: new UserUpdateDto()
        }
    },
    created() {
        this.setCompanies();
    },
    methods: {
        onUpdate() {
            if(!this.checkInputs())
                return;

            this.isLoading = true;
            this.updateModel.companyIds = $('#updateUserModal [name="companyIds"]').val() as string[];

            this.$axios.post('/users/update', this.updateModel)
                .then(() => {
                    Swal.fire("Kullanıcı başarıyla güncellendi!");
                    this.invalidInputs = null;
                    this.$emit('userUpdated');
                })
                .catch(error => {
                    let errorDetail;
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Kullanıcı güncellenirken hata oluştu!";
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
        isUsersCompany(companyId: any) {
            return this.selectedUser?.companyUsers?.find((cu: any) => cu.companyId == companyId);
        },
        setCompanies() {
            this.$axios.get('/companies/getall')
                .then(success => {
                    this.companies = success.data.data;
                })
                .catch(error => {
                    let errorDetail;
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Kullanıcı güncellenirken hata oluştu!";
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
            if (!this.updateModel.userName)
                errorMessages.push('Kullanıcı adını giriniz!');
            if (!this.updateModel.email)
                errorMessages.push('Email giriniz!');
            if (!this.updateModel.firstName)
                errorMessages.push('Ad giriniz!');
            if (!this.updateModel.lastName)
                errorMessages.push('Soyad adını giriniz!');
            if (!$('#updateUserModal [name="companyIds"]').val())
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