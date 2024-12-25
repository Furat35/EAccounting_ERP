<template>
    <div class="modal fade" id="createProductModal" tabindex="-1" role="dialog" aria-labelledby="createProductModal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Stok Ekleme</h1><button type="button" data-dismiss="modal" class="btn btn-outline-danger">
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
                            <input type="text" minlength="3" name="name" v-model="createModel.name" class="form-control">
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
import { ProductCreateDto } from '@/models/Products/ProductCreateDto';

export default {
    data() {
        return {
            invalidInputs: null as any,
            isLoading: false,
            createModel: new ProductCreateDto()
        }
    },
    components: {
        "spinner": Spinner
    },
    emits: ['newProductCreated'],
    methods: {
        onSave() {
            if(!this.checkInputs())
                return;

            this.isLoading = true;
            this.$axios.post('/products/create', this.createModel)
                .then(() => {
                    Swal.fire("Stok başarıyla oluşturuldu!");
                    this.invalidInputs = null;
                    this.$emit('newProductCreated');
                })
                .catch(error => {
                    let errorDetail;
                    try {
                        errorDetail = error.response.data.errorMessages[0];
                    }
                    catch {
                        errorDetail = "Stok oluşturulurken hata oluştu!";
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
