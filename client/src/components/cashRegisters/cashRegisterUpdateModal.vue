<template>
    <div class="modal fade" id="updateCashRegisterModal" tabindex="-1" role="dialog" aria-labelledby="updateCashRegisterModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Kasa Güncelleme</h1><button type="button" data-dismiss="modal"
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
                            <label for="cashRegisterName">Kasa Adı</label>
                            <input type="text" minlength="3" name="cashRegisterName" class="form-control" :value="selectedCashRegister?.name">
                        </div>
                        <div class="form-group mt-2">
                            <label for="currencyType">Döviz Tipi</label><br>
                            <select name="currencyType" class="form-control">
                                <option :value="currencyType.value" v-for="currencyType in currencyTypes" :selected="setActiveCurrency(currencyType)">{{ currencyType.name }}</option>
                            </select>
                        </div>
                    </div>
                    <div class="modal-footer"><button class="btn btn-dark w-100" @click="onUpdate">Güncelle</button></div>
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
import { CashRegisterListDto } from '@/models/CashRegisters/CashRegisterListDto';
import { CashRegisterUpdateDto } from '@/models/CashRegisters/CashRegisterUpdateDto';

export default {
    data() {
        return {
            invalidInputs: null as string | null,
            cashRegisters: null,
            isLoading: false,
            currencyTypes: [] as CurrencyTypeModel[]
        }
    },
    props: {
        'selectedCashRegister': {
            type: CashRegisterListDto,
            required: true
        }
    },
    components: {
        "spinner": Spinner
    },
    created() {
        this.setCashRegisters();
        this.currencyTypes = CurrencyTypes;
    },
    emits: ['cashRegisterUpdated'],
    methods: {
        onUpdate() {
            this.isLoading = true;
            let checkInputs = this.checkInputs();
            if (checkInputs.length != 0) {
                this.invalidInputs = checkInputs.join('<br>');
                return;
            }
            var cashRegister = {
                id: this.selectedCashRegister.id,
                name: $('#updateCashRegisterModal [name="cashRegisterName"]').val(),
                currencyType:  $('#updateCashRegisterModal [name="currencyType"]').val()
            } as CashRegisterUpdateDto;
            this.$axios.post('/cashRegisters/update', cashRegister)
                .then(() => {
                    Swal.fire("Kasa başarıyla güncellendi!");
                    this.resetForm();
                    this.invalidInputs = null;
                    this.$emit('cashRegisterUpdated');
                })
                .catch(error => {
                    let errorDetail;
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Kasa güncellenirken hata oluştu!";
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
        setCashRegisters() {
            this.$axios.get('/cashRegisters/getall')
                .then(success => {
                    this.cashRegisters = success.data.data;
                })
                .catch(error => {
                    let errorDetail;
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Kasa oluşturulurken hata oluştu!";
                    }
                    Swal.fire({
                        title: 'Hata!',
                        text: errorDetail,
                        icon: 'error',
                        confirmButtonText: 'Kapat'
                    });
                });
        },
        setActiveCurrency(currencyType: CurrencyTypeModel){
            return this.selectedCashRegister?.currencyType.value === currencyType.value;
        },
        resetForm() {
            $('#createCashRegisterModal [name="cashRegisterName"]').val('');
        },
        checkInputs() {
            let errorMessages = []
            if (!$('#updateCashRegisterModal [name="cashRegisterName"]').val())
                errorMessages.push('Kasa adını giriniz!');

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