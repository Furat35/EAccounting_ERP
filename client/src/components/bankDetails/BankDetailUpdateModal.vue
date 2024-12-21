<template>
    <div class="modal fade" id="updateBankDetailModal" tabindex="-1" role="dialog" aria-labelledby="updateBankDetailModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Banka Hareketi Ekleme</h1>
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
                        <div class="form-group mt-2">
                            <label for="bankDetailType">İşlem Tipi</label><br>
                            <select name="bankDetailType" class="form-control" v-model="updateModel.type">
                                <option value="0">Giriş</option>
                                <option value="1">Çıkış</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <div>
                                <label for="bankDetailAmount">Giriş/Çıkış Tutarı</label>
                                <input type="text" name="bankDetailAmount" class="form-control"  v-model="updateModel.amount">
                            </div>
                        </div>
                        <div v-if="selectedBankDetail?.oppositeBankId != null && selectedBankDetail?.oppositeBankId != ''">
                            <div class="form-group mt-2">
                                <label for="oppositeBank">Banka</label><br>
                                <input type="text" name="oppositeBank" class="form-control" :value="getOppositeBank()" >
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="bankDetailDescription">Açıklama</label>
                            <input type="text" minlength="3" name="bankDetailDescription" class="form-control"  v-model="updateModel.description">
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
import { type CurrencyTypeModel } from '@/models/currency-type-enum';
import type BankListDto from '@/models/Banks/BankListDto';
import { BankDetailUpdateDto } from '@/models/BankDetails/BankDetailUpdateDto';
import { BankDetailListDto } from '@/models/BankDetails/BankDetailListDto';

export default {
    components: {
        "spinner": Spinner
    },
    props: {
        'selectedBankDetail': {
            type: BankDetailListDto,
            required: true
        }
    },
    emits: [
        'bankDetailUpdated',
    ],
    watch: {
        selectedBankDetail(){
            this.invalidInputs = null;
            this.updateModel = Object.assign(new BankDetailUpdateDto(), this.selectedBankDetail);
            this.updateModel.amount = this.selectedBankDetail.withdrawalAmount + this.selectedBankDetail.depositAmount;
        }
    },
    data() {
        return {
            invalidInputs: null as any,
            updateModel: new BankDetailUpdateDto(),
            banks: null as BankListDto[] | null,
            oppositeBank: null as BankListDto | null,
            isLoading: false,
            currencyTypes: [] as CurrencyTypeModel[],
        }
    },
    methods: {
        onSave() {
            if(!this.checkInputs())
                return;

            this.isLoading = true;
            this.updateModel.bankId = this.$route.params.id.toString();
            this.$axios.post('/bankDetails/update', this.updateModel)
                .then(() => {
                    Swal.fire("Banka işlemi başarıyla güncellendi!");
                    this.invalidInputs = null;
                    this.$emit('bankDetailUpdated');
                })
                .catch(error => {
                    let errorDetail;
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Banka işlemi oluşturulurken hata oluştu!";
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
        getOppositeBank(){
            this.$axios.get('banks/getall')
                .then(response => {
                    var banks = response.data.data as BankListDto[];
                    this.oppositeBank = banks.find(c => c.id == this.selectedBankDetail.oppositeBankId) as BankListDto;
                });
        },
        checkInputs() {
            let errorMessages = []
            if (!this.updateModel.description)
                errorMessages.push('Açıklama giriniz!');
            if (!this.updateModel.amount)
                errorMessages.push('Miktar giriniz!');

            if (errorMessages.length != 0) {
                this.invalidInputs = errorMessages.join('<br>');
                return false;
            }
            return true;
        },
       
    }
}
</script>
