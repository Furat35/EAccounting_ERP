<template>
    <div class="modal fade" id="createCompanyModal" tabindex="-1" role="dialog" aria-labelledby="createCompanyModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Şirket Ekleme</h1><button type="button" data-dismiss="modal"
                        @click="resetForm" class="btn btn-outline-danger">
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
                            <label for="name">Şirket Adı</label>
                            <input type="text" minlength="3" name="name" class="form-control">
                        </div>
                        <div class="form-group mt-2">
                            <label for="fullAddress">Açık Adres</label>
                            <input type="text" minlength="3" name="fullAddress" class="form-control">
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="taxDepartment">Vergi Dairesi</label>
                                    <input type="text" minlength="3" name="taxDepartment" class="form-control">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="taxNumber">Vergi Numarası</label>
                                    <input type="text" minlength="3" name="taxNumber" class="form-control">

                                </div>
                            </div>
                        </div>
                        <div class="form-group mt-2">
                            <label for="server">Server</label>
                            <input type="text" name="server" class="form-control">
                        </div>
                        <div class="form-group mt-2">
                            <label for="databaseName">Veritabanı Adı</label>
                            <input type="text" name="databaseName" class="form-control">
                        </div>
                        <div class="form-group mt-2">
                            <label for="userId">Kullanıcı Id</label>
                            <input type="text" name="userId" class="form-control">
                        </div>
                        <div class="form-group mt-2">
                            <label for="password">Şifre</label>
                            <input type="password" name="password" class="form-control">
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
import { CompanyCreateDto } from '@/models/Companies/CompanyCreateDto';
import { DatabaseCreateDto } from '@/models/Databases/DatabaseCreateDto';
import Spinner from '@/components/layouts/spinner/index.vue';
import Swal from 'sweetalert2';

export default {
    data() {
        return {
            invalidInputs: null as string | null,
            companies: null,
            isLoading: false
        }
    },
    components: {
        "spinner": Spinner
    },
    created() {
        this.setCompanies();
    },
    emits: ['newCompanyCreated'],
    methods: {
        onSave() {
            this.isLoading = true;
            let checkInputs = this.checkInputs();
            if (checkInputs.length != 0) {
                this.invalidInputs = checkInputs.join('<br>');
                return;
            }

            var company = {
                name: $('#createCompanyModal [name="name"]').val(),
                taxDepartment: $('#createCompanyModal [name="taxDepartment"]').val(),
                taxNumber: $('#createCompanyModal [name="taxNumber"]').val(),
                fullAddress: $('#createCompanyModal [name="fullAddress"]').val(),
                database: {
                    server: $('#createCompanyModal [name="server"]').val(),
                    databaseName: $('#createCompanyModal [name="databaseName"]').val(),
                    userId: $('#updateCompanyModal [name="userId"]').val(),
                    password: $('#createCompanyModal [name="password"]').val()
                }
            } as CompanyCreateDto;
            this.$axios.post('/companies/create', company)
                .then(() => {
                    Swal.fire("Şirket başarıyla oluşturuldu!");
                    this.resetForm();
                    this.invalidInputs = null;
                    this.$emit('newCompanyCreated');
                })
                .catch(error => {
                    let errorDetail;
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Şirket oluşturulurken hata oluştu!";
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
                });
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
                        errorDetail = "Şirket oluşturulurken hata oluştu!";
                    }
                    Swal.fire({
                        title: 'Hata!',
                        text: errorDetail,
                        icon: 'error',
                        confirmButtonText: 'Kapat'
                    });
                });
        },
        resetForm() {
            $('#createCompanyModal [name="name"]').val('');
            $('#createCompanyModal [name="taxDepartment"]').val('');
            $('#createCompanyModal [name="taxNumber"]').val('');
            $('#createCompanyModal [name="server"]').val('');
            $('#createCompanyModal [name="databaseName"]').val('');
            $('#createCompanyModal [name="password"]').val('');
            $('#createCompanyModal [name="fullAddress"]').val('');
        },
        checkInputs() {
            let errorMessages = []
            if (!$('#createCompanyModal [name="name"]').val())
                errorMessages.push('Şirket adını giriniz!');
            if (!$('#createCompanyModal [name="fullAddress"]').val())
                errorMessages.push('Açık adres giriniz!');
            if (!$('#createCompanyModal [name="taxDepartment"]').val())
                errorMessages.push('Vergi dairesini giriniz!');
            if (!$('#createCompanyModal [name="taxNumber"]').val())
                errorMessages.push('Vergi no giriniz!');
            if (!$('#createCompanyModal [name="server"]').val())
                errorMessages.push('Server giriniz!');
            if (!$('#createCompanyModal [name="databaseName"]').val())
                errorMessages.push('Veritabanı adı giriniz!');
  

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