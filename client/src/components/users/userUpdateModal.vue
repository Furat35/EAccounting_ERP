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
                            <input type="text" name="id" hidden class="form-control" :value="selectedUser?.id">
                        </div>
                        <div class="form-group">
                            <label for="firstName">Ad</label>
                            <input type="text" minlength="3" name="firstName" class="form-control"
                                :value="selectedUser?.firstName">
                        </div>
                        <div class="form-group mt-2">
                            <label for="lastName">Soyad</label>
                            <input type="text" minlength="3" name="lastName" class="form-control"
                                :value="selectedUser?.lastName">
                        </div>
                        <div class="form-group">
                            <label for="userName">Kullanıcı Adı</label>
                            <input type="text" minlength="3" name="userName" class="form-control"
                                :value="selectedUser?.userName">
                        </div>
                        <div class="form-group mt-2">
                            <label for="email">Mail Adresi</label>
                            <input type="email" minlength="3" name="email" class="form-control"
                                :value="selectedUser?.email">
                        </div>
                        <div class="mb-3">
                            <div class="form-check">
                                <input class="form-check-input" type="checkbox" name="isAdmin" :checked="selectedUser?.isAdmin">
                                <label class="form-check-label" for="isAdmin">
                                    Admin mi?
                                </label>
                            </div>
                        </div>
                        <div class="form-group mt-2">
                            <label for="password">Şifre</label>
                            <input type="password" name="password" class="form-control" :value="selectedUser?.password">
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

export default {
    components: {
        'spinner': Spinner
    },
    data() {
        return {
            invalidInputs: null as string | null,
            companies: null as CompanyListDto[] | null,
            isLoading: false
        }
    },
    created() {
        this.setCompanies();
    },
    emits: ['userUpdated'],
    props: [
        'selectedUser'
    ],
    mounted(){
    },
    methods: {
        onUpdate() {
            this.isLoading = true;
            let checkInputs = this.checkInputs();
            if (checkInputs.length != 0) {
                this.invalidInputs = checkInputs.join('<br>');
                return;
            }

            var user = {
                id: $('#updateUserModal [name="id"]').val(), 
                userName: $('#updateUserModal [name="userName"]').val(),
                email: $('#updateUserModal [name="email"]').val(), 
                firstName: $('#updateUserModal [name="firstName"]').val(), 
                lastName: $('#updateUserModal [name="lastName"]').val(), 
                password: $('#updateUserModal [name="password"]').val(), 
                companyIds: $('#updateUserModal [name="companyIds"]').val(),
                isAdmin: $('#updateUserModal [name="isAdmin"]').is(':checked')} as UserUpdateDto
            this.$axios.post('/users/update', user)
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
            if (!$('#updateUserModal [name="userName"]').val())
                errorMessages.push('Kullanıcı adını giriniz!');
            if (!$('#updateUserModal [name="email"]').val())
                errorMessages.push('Email giriniz!');
            if (!$('#updateUserModal [name="firstName"]').val())
                errorMessages.push('Ad giriniz!');
            if (!$('#updateUserModal [name="lastName"]').val())
                errorMessages.push('Soyad adını giriniz!');
            if (!$('#updateUserModal [name="companyIds"]').val())
                errorMessages.push('Şirket seçiniz!');

            return errorMessages;
        }
    }
}
</script>

<style scoped>
.position-relative {
    position: relative;
}

.show-password-toggle {
    position: absolute;
    right: 10px;
    top: 50%;
    transform: translateY(-50%);
    font-size: 0.9em;
    cursor: pointer;
    border: none;
    background: none;
    padding: 0;
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