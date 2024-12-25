<template>
    <div class="modal fade" id="createBankDetailModal" tabindex="-1" role="dialog" aria-labelledby="createBankDetailModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Bank Hareketi Ekleme</h1>
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
                            <label for="bankDetail">Tarih</label>
                            <input type="date" name="bankDetailDate" class="form-control" v-model="createModel.date">
                        </div>
                        <div class="form-group mt-2">
                            <label for="bankDetailType">İşlem Tipi</label><br>
                            <select name="bankDetailType" class="form-control" v-model="createModel.type">
                                <option value="0">Giriş</option>
                                <option value="1">Çıkış</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <label for="bankDetailAmount">Tutar</label>
                            <input type="text" name="bankDetailAmount" class="form-control"  v-model="createModel.amount">
                        </div>
                        <div class="form-group mt-2">
                            <label for="bankDetailRecordType">Kayıt Tipi</label><br>
                            <select name="bankDetailRecordType" class="form-control"  v-model="createModel.recordType">
                                <option value="0">Diğer</option>
                                <option value="1">Banka</option>
                                <option value="2">Kasa</option>
                                <option value="3">Cari</option>
                            </select>
                        </div>
                        <div v-show="createModel.recordType == 1">
                            <div class="form-group mt-2">
                                <label for="oppositeBankId">Banka</label><br>
                                <select name="oppositeBankId" class="form-control" v-model="createModel.oppositeBankId">
                                    <option :value="bank.id" v-for="bank in banks">{{ bank.name }}</option>
                                </select>
                            </div>
                        </div>
                        <div v-show="createModel.recordType == 2">
                            <div class="form-group mt-2">
                                <label for="cashRegisterId">Kasa</label><br>
                                <select name="cashRegisterId" class="form-control" v-model="createModel.cashRegisterId">
                                    <option :value="cashRegister.id" v-for="cashRegister in cashRegisters">{{ cashRegister.name }}</option>
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
                            <label for="bankDetailDescription">Açıklama</label>
                            <input type="text" minlength="3" name="bankDetailDescription" class="form-control"  v-model="createModel.description">
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
import { BankDetailCreateDto } from '@/models/BankDetails/BankDetailCreateDto';
import type BankListDto from '@/models/Banks/BankListDto';
import type { CashRegisterListDto } from '@/models/CashRegisters/CashRegisterListDto';
import { CustomerListDto } from '@/models/Customers/CustomerListDto';

export default {
    data() {
        return {
            invalidInputs: null as any,
            createModel: new BankDetailCreateDto(),
            banks: null as BankListDto[] | null,
            bank: null as BankListDto | null,
            isLoading: false,
            currencyTypes: [] as CurrencyTypeModel[],
            cashRegisters: [] as CashRegisterListDto[],
            customers: [] as CustomerListDto[]
        }
    },
    components: {
        "spinner": Spinner
    },
    emits: [
        'newBankDetailCreated',
    ],
    created() {
        this.setBanks();
        this.getCashRegisters();
        this.getCustomers();
        this.currencyTypes = CurrencyTypes;
    },
    methods: {
        setBanks() {
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
        getCashRegisters() {
            this.isLoading = true;
            this.$axios.get('cashRegisters/getall')
                .then(response => {
                    this.cashRegisters = response.data.data as CashRegisterListDto[];
                    console.log(this.cashRegisters);
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
            if(this.createModel.oppositeBankId == ""){
                this.createModel.oppositeBankId = null;
            }
            if(this.createModel.cashRegisterId == ""){
                this.createModel.cashRegisterId = null;
            }
            if(this.createModel.customerId == ""){
                this.createModel.customerId = null;
            }
            this.createModel.bankId = this.$route.params.id.toString();
            this.$axios.post('/bankDetails/create', this.createModel)
                .then(() => {
                    Swal.fire("Banka hareketi başarıyla oluşturuldu!");
                    this.$emit('newBankDetailCreated');
                    this.invalidInputs = null;
                })
                .catch(error => {
                    let errorDetail;
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Banka hareketi oluşturulurken hata oluştu!";
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
