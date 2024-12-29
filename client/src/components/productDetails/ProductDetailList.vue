<template>
    <app-content-header :contentHeaderTitle="currentProduct?.name" previousTab="Ana Sayfa" currentTab="Stok Hareketi" />
    <section class="content" style="margin-left: 10px; margin-right: 10px;">
        <div class="card">
            <div class="card-header">
                <h3 class="card-title">Stok Hareketleri</h3>
                <div class="card-tools"><button type="button" data-card-widget="collapse" title="Collapse"
                        class="btn btn-tool"><i class="fas fa-minus"></i></button><button
                        type="button" data-card-widget="remove" title="Remove" class="btn btn-tool"><i
                            class="fas fa-times"></i></button></div>
            </div>
            <div class="card-body">
                <div class="form-group mt-2">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Tarih</th>
                                <th>Açıklama</th>
                                <th>Giriş</th>
                                <th>Çıkış</th>
                                <th>Birim Fiyat</th>
                                <th>Toplam Tutar</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(productDetail, index) in productDetails" :key="productDetail.id">
                                <td>{{ index + 1 }}</td>
                                <td>{{ productDetail.date }}</td>
                                <td>{{ productDetail.description }}</td>
                                <td>{{ productDetail.deposit }}</td>
                                <td>{{ productDetail.withdrawal }}</td>
                                <td>{{ productDetail.price }}</td>
                                <td>{{ productDetail.withdrawal * (productDetail.deposit + productDetail.withdrawal)}}</td>
                             </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <spinner :loading="isLoading"></spinner>
    </section>
</template>

<script lang="ts">
import AppContentHeader from '@/components/layouts/content-header/index.vue';
import Spinner from '@/components/layouts/spinner/index.vue';
import { ProductDetailListDto } from '@/models/ProductDetails/ProductDetailListDto';
import {ProductListDto} from '@/models/Products/ProductListDto';
import Swal from 'sweetalert2';

export default {
    components: {
        'app-content-header': AppContentHeader,
        'spinner': Spinner,
    },
    data() {
        return {
            product: null as ProductListDto | null,
            productId: null as string | null,
            productDetails: [] as ProductDetailListDto[],
            isLoading: false,
        }
    },
    created() {
        this.productId = this.$route.params.id as string;
        this.getProductDetails();
    },
    methods: {
        getProductDetails() {
            this.isLoading = true;
            this.$axios.get('productDetails/getall', {
                    params: {
                        productId: this.productId
                    }
                })
                .then(response => {
                    this.productDetails = response.data.data.details;
                })
                .catch(error => {
                    console.log(error);
                })
                .finally(() => {
                    this.isLoading = false;
                });
        }
    }
}
</script>