<template>
    <div class="modal fade" id="createCustomerModal" tabindex="-1" role="dialog" aria-labelledby="createCustomerModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Cari Ekleme</h1><button type="button" data-dismiss="modal" class="btn btn-outline-danger">
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
                            <label for="name">Cari Adı</label>
                            <input type="text" minlength="3" name="name" v-model="createModel.name" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="type">Tipi</label>
                            <select name="type" class="form-control"  v-model="createModel.typeValue">
                                <option :value="customerType.value" v-for="customerType in customerTypes">{{ customerType.name }}</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="city">Şehir</label>
                            <input type="text" minlength="3" name="city" v-model="createModel.city" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="town">İlçe</label>
                            <input type="text" minlength="3" name="town" v-model="createModel.town" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="fullAddress">Açık Adres</label>
                            <input type="text" minlength="3" name="fullAddress" v-model="createModel.fullAddress" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="taxDepartment">Vergi Dairesi</label>
                            <input type="text" minlength="3" name="taxDepartment" v-model="createModel.taxDepartment" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="taxNumber">Vergi Numarası</label>
                            <input type="text" minlength="3" name="name" v-model="createModel.taxNumber" class="form-control">
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
import { CustomerCreateDto }  from '@/models/customers/CustomerCreateDto';
import Spinner from '@/components/layouts/spinner/index.vue';
import Swal from 'sweetalert2';
import  { CustomerTypes } from '@/models/Customers/CustomerListDto';

export default {
    data() {
        return {
            invalidInputs: null as any,
            isLoading: false,
            customerTypes: [] as CustomerTypeEnum[],
            createModel: new CustomerCreateDto()
        }
    },
    components: {
        "spinner": Spinner
    },
    created() {
        this.customerTypes = CustomerTypes;
    },
    emits: ['newCustomerCreated'],
    methods: {
        onSave() {
            if(!this.checkInputs())
                return;

            this.isLoading = true;
            this.$axios.post('/customers/create', this.createModel)
                .then(() => {
                    Swal.fire("Cari başarıyla oluşturuldu!");
                    this.invalidInputs = null;
                    this.$emit('newCustomerCreated');
                })
                .catch(error => {
                    let errorDetail;
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Cari oluşturulurken hata oluştu!";
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
        checkInputs() {
            let errorMessages = []
            if (!this.createModel.name)
                errorMessages.push('Cari adını giriniz!');
            if (!this.createModel.city)
                errorMessages.push('Şehir giriniz!');
            if (!this.createModel.town)
                errorMessages.push('İlçe giriniz!');
            if (!this.createModel.fullAddress)
                errorMessages.push('Açık adres giriniz!');
            if (!this.createModel.taxDepartment)
                errorMessages.push('Vergi dairesi giriniz!');
            if (!this.createModel.taxNumber)
                errorMessages.push('Vergi numarası giriniz!');
            if (errorMessages.length != 0) {
                this.invalidInputs = errorMessages.join('<br>');
                return false;
            }
            
            return true;
        }
    }
}
</script>
