<template>
    <div class="modal fade" id="updateCustomerModal" tabindex="-1" role="dialog" aria-labelledby="updateCustomerModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Cari Güncelleme</h1><button type="button" data-dismiss="modal"
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
                            <label for="name">Cari Adı</label>
                            <input type="text" minlength="3" name="name" v-model="updateModel.name" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="type">Tipi</label>
                            <select name="type" class="form-control" v-model="updateModel.typeValue">
                                <option :value="customerType.value" v-for="customerType in customerTypes">{{ customerType.name }}</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="city">Şehir</label>
                            <input type="text" minlength="3" name="city" v-model="updateModel.city" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="town">İlçe</label>
                            <input type="text" minlength="3" name="town" v-model="updateModel.town" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="fullAddress">Açık Adres</label>
                            <input type="text" minlength="3" name="fullAddress" v-model="updateModel.fullAddress" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="taxDepartment">Vergi Dairesi</label>
                            <input type="text" minlength="3" name="taxDepartment" v-model="updateModel.taxDepartment" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="taxNumber">Vergi Numarası</label>
                            <input type="text" minlength="3" name="name" v-model="updateModel.taxNumber" class="form-control">
                        </div>
                    </div>
                    <div class="modal-footer"><button class="btn btn-dark w-100" @click="onUpdate">Kaydet</button></div>
                </form>
            </div>
        </div>
    </div>
    <spinner :loading="isLoading"/>
</template>

<script lang="ts">
import Spinner from '@/components/layouts/spinner/index.vue';
import Swal from 'sweetalert2';
import { CustomerUpdateDto } from '@/models/Customers/CustomerUpdateDto';
import { CustomerListDto } from '@/models/Customers/CustomerListDto';
import  { CustomerTypes } from '@/models/Customers/CustomerListDto';

export default {
    data() {
        return {
            invalidInputs: null as string | null,
            isLoading: false,
            customerTypes: [] as CustomerTypeEnum[],
            updateModel: new CustomerUpdateDto()
        }
    },
    props: {
        'selectedCustomer': {
            type: CustomerListDto,
            required: true
        }
    },
    components: {
        "spinner": Spinner
    },
    watch: {
        selectedCustomer(){
            this.invalidInputs = null;
            this.updateModel = Object.assign(new CustomerUpdateDto(), this.selectedCustomer);
        }
    },
    created() {
        this.customerTypes = CustomerTypes;
    },
    emits: ['customerUpdated'],
    methods: {
        onUpdate() {
            if(!this.checkInputs())
                return;

            this.isLoading = true;
            this.updateModel.currencyType = $('#updateCustomerModal [name="currencyType"]').val() as number;
            this.$axios.post('/customers/update', this.updateModel)
                .then(() => {
                    Swal.fire("Cari başarıyla güncellendi!");
                    this.invalidInputs = null;
                    this.$emit('customerUpdated');
                })
                .catch(error => {
                    let errorDetail;
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Cari güncellenirken hata oluştu!";
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
            if (!this.updateModel.name)
                errorMessages.push('Cari adını giriniz!');
            if (!this.updateModel.city)
                errorMessages.push('Şehir giriniz!');
            if (!this.updateModel.town)
                errorMessages.push('İlçe giriniz!');
            if (!this.updateModel.fullAddress)
                errorMessages.push('Açık adres giriniz!');
            if (!this.updateModel.taxDepartment)
                errorMessages.push('Vergi dairesi giriniz!');
            if (!this.updateModel.taxNumber)
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
