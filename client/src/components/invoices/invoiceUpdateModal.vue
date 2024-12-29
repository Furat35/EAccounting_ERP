<template>
    <div class="modal fade" id="updateInvoiceModal" tabindex="-1" role="dialog" aria-labelledby="updateInvoiceModal">
        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5">Fatura Güncelleme</h1><button type="button" data-dismiss="modal" class="btn btn-outline-danger">
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
                        <div class="row">
                            <div class="col-6">
                                <div class="form-group">
                                    <label for="typeValue">Fatura Tipi</label><br>
                                    <select name="typeValue" class="form-control" v-model="updateModel.typeValue">
                                        <option :value="1">Alış Faturası</option>
                                        <option :value="2">Satış Faturası</option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label for="customerId">Müşteri</label><br>
                                    <select name="customerId" class="form-control" v-model="updateModel.customerId">
                                        <option :value="customer.id" v-for="customer in customers">{{ customer.name }}</option>
                                    </select>
                                </div>
                            
                            </div>
                            <div class="col-6">
                                <div class="form-group">
                                    <label for="date">Tarih</label>
                                    <input type="date" name="date" v-model="updateModel.date" class="form-control">
                                </div>
                                <div class="form-group">
                                    <label for="invoiceNumber">Fatura Numarası</label>
                                    <input type="text" name="invoiceNumber" v-model="updateModel.invoiceNumber" class="form-control">
                                </div>
                            </div>
                        </div>
                        

                        <hr>

                        <div class="mt-2 row">
                            <div class="col-3">
                                <div class="form-group">
                                    <label for="product">Ürün</label><br>
                                    <select name="product" class="form-control" v-model="updateModel.productId">
                                        <option :value="product.id" v-for="product in products">{{ product.name }}</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <label for="quantity">Adet</label>
                                    <input type="text" name="quantity" v-model="updateModel.quantity" class="form-control">
                                </div>
                            </div>
                            <div class="col-2">
                                <div class="form-group">
                                    <label for="price">Birim Fiyat</label>
                                    <input type="text" name="price" v-model="updateModel.price" class="form-control">
                                </div>
                            </div>
                            <div class="col-3">
                                <div class="form-group">
                                    <label for="sum">Toplam</label>
                                    <span  class="form-control">{{ updateModel.price * updateModel.quantity }}</span>
                                </div>
                            </div>
                            <div class="col-2">
                                <label>İşlemler</label>
                                <div class="form-group">
                                    <button type="button" class="btn btn-success w-100" @click="addDetail()"> Ekle</button>
                                </div>
                            </div>
                        </div>

                        <hr>
                        
                        <div class="form-group mt-2">
                            <table class="table table-hover">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Ürün Adı</th>
                                        <th>Adet</th>
                                        <th>Birim Fiyat</th>
                                        <th>Toplam</th>
                                        <th>İşlemler</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(detail, i) in updateModel.details">
                                        <th>{{ i + 1 }}</th>
                                        <th>{{ detail.product.name }}</th>
                                        <th>{{ detail.quantity }}</th>
                                        <th>{{ detail.price }}</th>
                                        <th>{{ detail.quantity * detail.price }}</th>
                                        <th>
                                            <button @click="removeDetailItem(i)" class="btn btn-outline-danger btn-sm">
                                                <i class="fa-solid fa-trash"></i>
                                            </button>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
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
import { InvoiceCreateDto } from '@/models/Invoices/InvoiceCreateDto';
import { CustomerListDto } from '@/models/Customers/CustomerListDto';
import { ProductListDto } from '@/models/Products/ProductListDto';
import type { InvoiceDetailListDto } from '@/models/InvoiceDetails/InvoiceDetailListDto';
import { InvoiceListDto } from '@/models/Invoices/InvoiceListDto';
import { InvoiceUpdateDto } from '@/models/Invoices/InvoiceUpdateDto';

export default {
    data() {
        return {
            invalidInputs: null as any,
            isLoading: false,
            updateModel: new InvoiceCreateDto(),
            customers: [] as CustomerListDto[],
            products: [] as  ProductListDto[]
        } 
    },
    components: {
        "spinner": Spinner
    },
    emits: ['invoiceUpdated'],
    props: {
        'selectedInvoice': {
            type: InvoiceListDto,
            required: true
        }
    },
    mounted(){
        this.getAllCustomers();
        this.getProducts();
    },
    watch: {
        selectedInvoice(){
            this.invalidInputs = null;
            this.updateModel = Object.assign(new InvoiceUpdateDto(), this.selectedInvoice);
            console.log(this.updateModel);
        }
    },
    methods: {
        onSave() {
            if(!this.checkInputs())
                return;

            this.isLoading = true;
            this.$axios.post('/invoices/delete', { id: this.selectedInvoice!.id })
                        .then(() => {
                            this.$axios.post('/invoices/create', this.updateModel)
                                .then(() => {
                                    Swal.fire("Fatura başarıyla güncellendi!");
                                    this.invalidInputs = null;
                                    this.$emit('invoiceUpdated');
                                });
                        })
                        .catch(error => {
                            let errorDetail;
                            try {
                                errorDetail = error.response.data.errorMessages[0];
                            }
                            catch {
                                errorDetail = "Fatura güncellendi hata oluştu!";
                            }
                            Swal.fire({
                                title: 'Hata!',
                                text: errorDetail,
                                icon: 'error',
                                confirmButtonText: 'Kapat'
                            });
                        })
        },
        getProducts() {
            this.isLoading = true;
            this.$axios.get('products/getall')
                .then(response => {
                    this.products = response.data.data as ProductListDto[];
                })
                .catch(error => {
                    console.log(error.response.data.errorMessages);
                })
                .finally(() => {
                    this.isLoading = false;
                })
        },
        getAllCustomers(){
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
                });
        },
        addDetail(){
            const detail: InvoiceDetailListDto = {
                price: this.updateModel.price,
                quantity: this.updateModel.quantity,
                productId: this.updateModel.productId,
                id: "",
                invoiceId: "",
                product: this.products.find(p => p.id == this.updateModel.productId) ?? new ProductListDto(),
            }

            this.updateModel.details.push(detail);

            this.updateModel.price = 0;
            this.updateModel.quantity = 0;
            this.updateModel.productId = "";
        },
        removeDetailItem(index: number){
            this.updateModel.details.splice(index, 1);
        },
        checkInputs() {
            // let errorMessages = []
            // if (!this.updateModel.name)
            //     errorMessages.push('Fatura adını giriniz!');
         
            // if (errorMessages.length != 0) {
            //     this.invalidInputs = errorMessages.join('<br>');
            //     return false;
            // }
            
            return true;
        }
    }
}
</script>
