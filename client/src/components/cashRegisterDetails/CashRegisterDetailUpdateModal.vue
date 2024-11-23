<template>
    <div class="modal fade" id="updateCashRegisterDetailModal" tabindex="-1" role="dialog" aria-labelledby="updateCashRegisterDetailModal">
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
                        <div class="form-group mt-2">
                            <label for="cashRegisterDetailType">İşlem Tipi</label><br>
                            <select name="cashRegisterDetailType" class="form-control" v-model="updateModel.type">
                                <option value="0">Giriş</option>
                                <option value="1">Çıkış</option>
                            </select>
                        </div>
                        <div class="form-group">
                            <div>
                                <label for="cashRegisterDetailAmount">Giriş/Çıkış Tutarı</label>
                                <input type="text" name="cashRegisterDetailAmount" class="form-control"  v-model="updateModel.amount">
                            </div>
                        </div>
                        <div v-if="selectedCashRegisterDetail?.oppositeCashRegisterId != null && selectedCashRegisterDetail?.oppositeCashRegisterId != ''">
                            <div class="form-group mt-2">
                                <label for="oppositeCashRegister">Kasa</label><br>
                                <input type="text" name="oppositeCashRegister" class="form-control" :value="getOppositeCashRegister()" >
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="cashRegisterDetailDescription">Açıklama</label>
                            <input type="text" minlength="3" name="cashRegisterDetailDescription" class="form-control"  v-model="updateModel.description">
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
import type { CashRegisterListDto } from '@/models/CashRegisters/CashRegisterListDto';
import { CashRegisterDetailUpdateDto } from '@/models/CashRegistersDetails/CashRegisterDetailUpdateDto';
import { CashRegisterDetailListDto } from '@/models/CashRegistersDetails/CashRegisterDetailList';

export default {
    components: {
        "spinner": Spinner
    },
    props: {
        'selectedCashRegisterDetail': {
            type: CashRegisterDetailListDto,
            required: true
        }
    },
    emits: [
        'cashRegisterDetailUpdated',
    ],
    watch: {
        selectedCashRegisterDetail(){
            this.updateModel = { ...this.selectedCashRegisterDetail } ;
            this.updateModel.amount = this.selectedCashRegisterDetail.withdrawalAmount + this.selectedCashRegisterDetail.depositAmount;
        }
    },
    data() {
        return {
            invalidInputs: null as any,
            updateModel: new CashRegisterDetailUpdateDto(),
            cashRegisters: null as CashRegisterListDto[] | null,
            oppositeCashRegister: null as CashRegisterListDto | null,
            isLoading: false,
            currencyTypes: [] as CurrencyTypeModel[],
        }
    },
    methods: {
        onSave() {
            let checkInputs = this.checkInputs();
            if (checkInputs.length != 0) {
                this.invalidInputs = checkInputs.join('<br>');
                return;
            }
            this.isLoading = true;
            this.updateModel.cashRegisterId = this.$route.params.id.toString();
            this.$axios.post('/cashRegisterDetails/update', this.updateModel)
                .then(() => {
                    Swal.fire("Kasa işlemi başarıyla güncellendi!");
                    this.invalidInputs = null;
                    this.$emit('cashRegisterDetailUpdated');
                    this.updateModel = new CashRegisterDetailUpdateDto();
                })
                .catch(error => {
                    let errorDetail;
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Kasa işlemi oluşturulurken hata oluştu!";
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
        getOppositeCashRegister(){
            this.$axios.get('cashRegisters/getall')
                .then(response => {
                    var cashRegisters = response.data.data as CashRegisterListDto[];
                    this.oppositeCashRegister = cashRegisters.find(c => c.id == this.selectedCashRegisterDetail.oppositeCashRegisterId) as CashRegisterListDto;
                });
        },
        checkInputs() {
            let errorMessages = []
            if (!$('#updateCashRegisterDetailModal [name="cashRegisterDetailDescription"]').val())
                errorMessages.push('Açıklama giriniz!');
            if (!$('#updateCashRegisterDetailModal [name="cashRegisterDetailAmount"]').val())
                errorMessages.push('Açıklama giriniz!');

            return errorMessages;
        },
       
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