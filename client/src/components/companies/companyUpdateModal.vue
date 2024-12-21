<template>
    <div class="modal fade" id="updateCompanyModal" tabindex="-1" role="dialog" aria-labelledby="updateCompanyModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Şirket Güncelleme</h1><button type="button" data-dismiss="modal"
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
                        <transition name="fade">
                            <div v-html="invalidInputs" class="alert alert-danger mt-3 text-center"
                                v-if="invalidInputs != null" role="alert">
                            </div>
                        </transition>
                        <div class="form-group">
                            <label for="name">Şirket Adı</label>
                            <input type="text" minlength="3" name="name" class="form-control"
                                v-model="updateModel.name">
                        </div>
                        <div class="form-group mt-2">
                            <label for="fullAddress">Açık Adres</label>
                            <input type="text" minlength="3" name="fullAddress" class="form-control"
                            v-model="updateModel.fullAddress">
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="taxDepartment">Vergi Dairesi</label>
                                    <input type="text" minlength="3" name="taxDepartment" class="form-control"
                                    v-model="updateModel.taxDepartment">
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="taxNumber">Vergi Numarası</label>
                                    <input type="text" minlength="3" name="taxNumber" class="form-control"
                                    v-model="updateModel.taxNumber">
                                </div>
                            </div>
                        </div>
                        <div class="form-group mt-2">
                            <label for="server">Server</label>
                            <input type="text" name="server" class="form-control"
                                v-model="updateModel.database.server">
                        </div>
                        <div class="form-group mt-2">
                            <label for="databaseName">Veritabanı Adı</label>
                            <input type="text" name="databaseName" class="form-control"
                                v-model="updateModel.database.databaseName">
                        </div>
                        <div class="form-group mt-2">
                            <label for="userId">Kullanıcı Id</label>
                            <input type="text" name="userId" class="form-control"
                                v-model="updateModel.database.userId">
                        </div>
                        <div class="form-group mt-2">
                            <label for="password">Şifre</label>
                            <input type="password" name="password" class="form-control" v-model="updateModel.database.password">
                        </div>
                    </div>
                    <div class="modal-footer"><button class="btn btn-dark w-100" @click="onUpdate">Güncelle</button>
                    </div>
                </form>
            </div>
        </div>
        <spinner :loading="isLoading"/>
    </div>
</template>

<script lang="ts">
import { CompanyUpdateDto } from '@/models/Companies/CompanyUpdateDto';
import Spinner from '@/components/layouts/spinner/index.vue';
import Swal from 'sweetalert2'
import { CompanyListDto } from '@/models/Companies/CompanyListDto';

export default {
    components: {
        'spinner': Spinner
    },
    data() {
        return {
            invalidInputs: null as string | null,
            isLoading: false,
            updateModel: new CompanyUpdateDto()
        }
    },
    props: {
        'selectedCompany': {
            type: CompanyListDto,
            required: true
        }
    },
    watch: {
        selectedCompany(){
            this.invalidInputs = null;
            this.updateModel = Object.assign(new CompanyUpdateDto(), this.selectedCompany);
        }
    },
    methods: {
        onUpdate() {
            if(!this.checkInputs())
                return;

            this.isLoading = true;
            this.$axios.post('/companies/update', this.updateModel)
                .then(() => {
                    Swal.fire("Şirket başarıyla güncellendi!");
                    this.invalidInputs = null;
                    this.$emit('companyUpdated');
                })
                .catch(error => {
                    let errorDetail;
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Şirket güncellenirken hata oluştu!";
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
        checkInputs() {
            let errorMessages = [];
            if (!this.updateModel.name)
                errorMessages.push('Şirket adını giriniz!');
            if (!this.updateModel.fullAddress)
                errorMessages.push('Açık adres giriniz!');
            if (!this.updateModel.taxDepartment)
                errorMessages.push('Vergi dairesini giriniz!');
            if (!this.updateModel.taxNumber)
                errorMessages.push('Vergi no giriniz!');
            if (!this.updateModel.database.server)
                errorMessages.push('Server giriniz!');
            if (!this.updateModel.database.databaseName)
                errorMessages.push('Veritabanı adı giriniz!');

            if (errorMessages.length != 0) {
                this.invalidInputs = errorMessages.join('<br>');
                return false;
            }
            return true;
        }
    }
}
</script>
