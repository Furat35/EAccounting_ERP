<template>
    <div class="modal fade" id="updateProductModal" tabindex="-1" role="dialog" aria-labelledby="updateProductModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Stok Güncelleme</h1><button type="button" data-dismiss="modal"
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
                            <label for="name">Stok Adı</label>
                            <input type="text" minlength="3" name="name" v-model="updateModel.name" class="form-control">
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
import { ProductUpdateDto }  from '@/models/Products/ProductUpdateDto';
import { ProductListDto } from '@/models/Products/ProductListDto';

export default {
    data() {
        return {
            invalidInputs: null as string | null,
            // cashRegisters: null,
            isLoading: false,
            updateModel: new ProductUpdateDto()
        }
    },
    props: {
        'selectedProduct': {
            type: ProductListDto,
            required: true
        }
    },
    components: {
        "spinner": Spinner
    },
    watch: {
        selectedProduct(){
            this.invalidInputs = null;
            this.updateModel = Object.assign(new ProductUpdateDto(), this.selectedProduct);
        }
    },
    emits: ['productUpdated'],
    methods: {
        onUpdate() {
            if(!this.checkInputs())
                return;

            this.isLoading = true;
            this.$axios.post('/products/update', this.updateModel)
                .then(() => {
                    Swal.fire("Stok başarıyla güncellendi!");
                    this.invalidInputs = null;
                    this.$emit('productUpdated');
                })
                .catch(error => {
                    let errorDetail;
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Stok güncellenirken hata oluştu!";
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
        setActiveCurrency(currencyType: CurrencyTypeModel){
            return this.selectedProduct?.currencyType.value === currencyType.value;
        },
        checkInputs() {
            let errorMessages = []
            if (!this.updateModel.name)
                errorMessages.push('Stok adını giriniz!');
     
            if (errorMessages.length != 0) {
                this.invalidInputs = errorMessages.join('<br>');
                return false;
            }
            
            return true;
        }
    }
}
</script>
