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

export default {
    data() {
        return {
            invalidInputs: null as any,
            createModel: new CashRegisterDetailCreateDto(),
            cashRegisters: null as CashRegisterListDto[] | null,
            cashRegister: null as CashRegisterListDto | null,
            isLoading: false,
            currencyTypes: [] as CurrencyTypeModel[]
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
        onSave() {
            let checkInputs = this.checkInputs();
            if (checkInputs.length != 0) {
                this.invalidInputs = checkInputs.join('<br>');
                return;
            }
            this.isLoading = true;
            if(this.createModel.oppositeCashRegisterId === ""){
                this.createModel.oppositeCashRegisterId = null;
            }
            this.createModel.cashRegisterId = this.$route.params.id.toString();
            this.$axios.post('/cashRegisterDetails/create', this.createModel)
                .then(() => {
                    Swal.fire("Kasa hareketi başarıyla oluşturuldu!");
                    this.$emit('newCashRegisterDetailCreated');
                    this.invalidInputs = null;
                    this.createModel = new CashRegisterDetailCreateDto();
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
            if (!$('#createCashRegisterDetailModal [name="cashRegisterDetailDate"]').val())
                errorMessages.push('Tarih giriniz!');
            if (!$('#createCashRegisterDetailModal [name="cashRegisterDetailDescription"]').val())
                errorMessages.push('Açıklama giriniz!');
             
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