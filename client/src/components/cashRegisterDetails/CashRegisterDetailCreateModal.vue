<template>
    <div class="modal fade" id="createCashRegisterDetailModal" tabindex="-1" role="dialog" aria-labelledby="createCashRegisterDetailModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Kasa Hareketi Ekleme</h1>
                    <button type="button" data-dismiss="modal" class="btn btn-outline-danger">
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
                            <label for="cashRegisterDetail">Tarih</label>
                            <input type="date" name="cashRegisterDetailDate" class="form-control" v-model="createModel.date">
                        </div>
                        <div class="form-group mt-2">
                            <label for="cashRegisterDetailType">İşlem Tipi</label><br>
                            <select name="cashRegisterDetailType" class="form-control" v-model="createModel.type">
                                <option value="0">Giriş</option>
                                <option value="1">Çıkış</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="cashRegisterDetailAmount">Tutar</label>
                            <input type="text" name="cashRegisterDetailAmount" class="form-control"  v-model="createModel.amount">
                        </div>
                        <div class="form-group mt-2">
                            <label for="cashRegisterDetailRecordType">Kayıt Tipi</label><br>
                            <select name="cashRegisterDetailRecordType" class="form-control"  v-model="createModel.recordType">
                                <option value="0">Diğer</option>
                                <option value="1">Kasa</option>
                                <option value="2">Banka</option>
                                <option value="3">Cari</option>
                            </select>
                        </div>
                        <div v-show="createModel.recordType == 1">
                            <div class="form-group mt-2">
                                <label for="oppositeCashRegisterId">Kasa</label><br>
                                <select name="oppositeCashRegisterId" class="form-control" v-model="createModel.oppositeCashRegisterId">
                                    <option :value="cashRegister.id" v-for="cashRegister in cashRegisters">{{ cashRegister.name }}</option>
                                </select>
                            </div>
                        </div>
                        <div v-show="createModel.recordType == 2">
                            <div class="form-group mt-2">
                                <label for="bankId">Banka</label><br>
                                <select name="bankId" class="form-control" v-model="createModel.bankId">
                                    <option :value="bank.id" v-for="bank in banks">{{ bank.name }}</option>
                                </select>
                            </div>
                        </div>
                        <div v-show="createModel.recordType == 3">
                            <div class="form-group mt-2">
                                <label for="customerId">Cari</label><br>
                                <select name="customerId" class="form-control" v-model="createModel.customerId">
                                    <option :value="customer.id" v-for="customer in customers">{{ customer.name }}</option>
                                </select>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="cashRegisterDetailDescription">Açıklama</label>
                            <input type="text" minlength="3" name="cashRegisterDetailDescription" class="form-control"  v-model="createModel.description">
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
import Spinner from '@/components/layouts/spinner/index.vue';
import Swal from 'sweetalert2';
import { CurrencyTypes, type CurrencyTypeModel } from '@/models/currency-type-enum';
import type { CashRegisterListDto } from '@/models/CashRegisters/CashRegisterListDto';
import { CashRegisterDetailCreateDto } from '@/models/CashRegistersDetails/CashRegisterDetailCreateDto';
import type BankListDto from '@/models/Banks/BankListDto';
import { CustomerListDto } from '@/models/Customers/CustomerListDto';

export default {
    data() {
        return {
            invalidInputs: null as any,
            createModel: new CashRegisterDetailCreateDto(),
            cashRegisters: null as CashRegisterListDto[] | null,
            cashRegister: null as CashRegisterListDto | null,
            isLoading: false,
            currencyTypes: [] as CurrencyTypeModel[],
            banks: [] as BankListDto[],
            customers: [] as CustomerListDto[]
        }
    },
    components: {
        "spinner": Spinner
    },
    emits: [
        'newCashRegisterDetailCreated',
    ],
    created() {
        this.setCashRegisters();
        this.getBanks();
        this.getCustomers();
        this.currencyTypes = CurrencyTypes;
    },
    methods: {
        setCashRegisters() {
            this.isLoading = true;
            this.$axios.get('cashRegisters/getall')
                .then(response => {
                    this.cashRegisters = response.data.data as CashRegisterListDto[];
                })
                .catch(error => {
                    console.log(error.response.data.errorMessages);
                })
                .finally(() => {
                    this.isLoading = false;
                })
        },
        getBanks() {
            this.isLoading = true;
            this.$axios.get('banks/getall')
                .then(response => {
                    this.banks = response.data.data as BankListDto[];
                })
                .catch(error => {
                    console.log(error.response.data.errorMessages);
                })
                .finally(() => {
                    this.isLoading = false;
                })
        },
        getCustomers() {
            this.isLoading = true;
            this.$axios.get('customers/getall')
                .then(response => {
                    this.customers = response.data.data as CustomerListDto[];
                })
                .catch(error => {
                    console.log(error.response.data.errorMessages);
                })
                .finally(() => {
                    this.isLoading = false;
                })
        },
        onSave() {
            if(!this.checkInputs())
                return;

            this.isLoading = true;
            if(this.createModel.oppositeCashRegisterId == ""){
                this.createModel.oppositeCashRegisterId = null;
            }
            if(this.createModel.bankId == ""){
                this.createModel.bankId = null;
            }
            if(this.createModel.customerId == ""){
                this.createModel.customerId = null;
            }
            this.createModel.cashRegisterId = this.$route.params.id.toString();
            this.$axios.post('/cashRegisterDetails/create', this.createModel)
                .then(() => {
                    Swal.fire("Kasa hareketi başarıyla oluşturuldu!");
                    this.$emit('newCashRegisterDetailCreated');
                    this.invalidInputs = null;
                })
                .catch(error => {
                    let errorDetail;
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Kasa hareketi oluşturulurken hata oluştu!";
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
            if (!this.createModel.description)
                errorMessages.push('Açıklama giriniz!');
            if (!this.createModel.amount)
                errorMessages.push('Miktar giriniz!');

            if (errorMessages.length != 0) {
                this.invalidInputs = errorMessages.join('<br>');
                return false;
            }
            return true;
        }
    }
}
</script>
