<template>
    <app-content-header :contentHeaderTitle="currentCustomer?.name" previousTab="Ana Sayfa" currentTab="Cari Hareketi" />
    <section class="content" style="margin-left: 10px; margin-right: 10px;">
        <div class="card">
            <div class="card-header">
                <h3 class="card-title">Cari Hareketleri</h3>
                <div class="card-tools"><button type="button" data-card-widget="collapse" title="Collapse"
                        class="btn btn-tool"><i class="fas fa-minus"></i></button><button
                        type="button" data-card-widget="remove" title="Remove" class="btn btn-tool"><i
                            class="fas fa-times"></i></button></div>
            </div>
            <div class="card-body">
                
                <div class="form-group row">
                </div>
                <div class="form-group mt-2">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Tarih</th>
                                <th>Tip</th>
                                <th>Açıklama</th>
                                <th>Giriş</th>
                                <th>Çıkış</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(customerDetail, index) in customerDetails" :key="customerDetail.id">
                                <td>{{ index + 1 }}</td>
                                <td>{{ customerDetail.date }}</td>
                                <td>{{ customerDetail.type.name }}</td>
                                <td>{{ customerDetail.description }}</td>
                                <td>{{ customerDetail.depositAmount }}</td>
                                <td>{{ customerDetail.withdrawalAmount }}</td>
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
import { CustomerDetailListDto } from '@/models/CustomerDetails/CustomerDetailListDto';
import {CustomerListDto} from '@/models/Customers/CustomerListDto';
import Swal from 'sweetalert2';

export default {
    components: {
        'app-content-header': AppContentHeader,
        'spinner': Spinner,
    },
    data() {
        return {
            customer: null as CustomerListDto | null,
            customerId: null as string | null,
            customerDetails: [] as CustomerDetailListDto[],
            isLoading: false,
        }
    },
    created() {
        this.customerId = this.$route.params.id as string;
        this.getCustomerDetails();
    },
    methods: {
        getCustomerDetails() {
            this.isLoading = true;
            this.$axios.get('customerDetails/getall', {
                    params: {
                        customerId: this.customerId
                    }
                })
                .then(response => {
                    this.customerDetails = response.data.data.details;
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