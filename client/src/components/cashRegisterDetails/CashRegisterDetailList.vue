<template>
    <app-content-header :contentHeaderTitle="currentCashRegister?.name" previousTab="Ana Sayfa" currentTab="Kasa Hareketi" />
    <section class="content" style="margin-left: 10px; margin-right: 10px;">
        <div class="card">
            <div class="card-header">
                <h3 class="card-title">Kasa Hareketleri</h3>
                <div class="card-tools"><button type="button" data-card-widget="collapse" title="Collapse"
                        class="btn btn-tool"><i class="fas fa-minus"></i></button><button
                        type="button" data-card-widget="remove" title="Remove" class="btn btn-tool"><i
                            class="fas fa-times"></i></button></div>
            </div>
            <div class="card-body">
                
                <div class="form-group row">
                    <div class="col-auto">
                        <button type="button" class="btn btn-dark" data-toggle="modal" data-target="#createCashRegisterDetailModal">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                                class="bi bi-person-add" viewBox="0 0 16 16">
                                <path
                                    d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7m.5-5v1h1a.5.5 0 0 1 0 1h-1v1a.5.5 0 0 1-1 0v-1h-1a.5.5 0 0 1 0-1h1v-1a.5.5 0 0 1 1 0m-2-6a3 3 0 1 1-6 0 3 3 0 0 1 6 0M8 7a2 2 0 1 0 0-4 2 2 0 0 0 0 4" />
                                <path
                                    d="M8.256 14a4.5 4.5 0 0 1-.229-1.004H3c.001-.246.154-.986.832-1.664C4.484 10.68 5.711 10 8 10q.39 0 .74.025c.226-.341.496-.65.804-.918Q8.844 9.002 8 9c-5 0-6 3-6 4s1 1 1 1z" />
                            </svg> İşlem Ekle
                        </button>
                    </div>
                    <div class="col-auto">
                        <input type="date" class="form-control" name="startDate" placeholder="Başlangıç Tarihi" v-model="startDate">
                    </div>
                    <div class="col-auto">
                        <input type="date" class="form-control" name="endDate" placeholder="Bitiş Tarihi" v-model="endDate">
                    </div>
                    <div class="col-auto">
                        <button class="btn btn-success" @click="getCashRegisterDetails()">Ara</button>
                    </div>
                    <div class="col-2 ml-auto">
                        <input type="search" placeholder="Aranacak değer girin..."
                        class="form-control ng-untouched ng-pristine ng-valid">
                    </div>
            
                   
                </div>
                <div class="form-group mt-2">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Tarih</th>
                                <th>Açıklama</th>
                                <th>Giriş</th>
                                <th>Çıkış</th>
                                <th>İşlemler</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(cashRegisterDetail, index) in cashRegisterDetails" :key="cashRegisterDetail.id">
                                <td>{{ index + 1 }}</td>
                                <td>{{ cashRegisterDetail.date }}</td>
                                <td>{{ cashRegisterDetail.description }}</td>
                                <td>{{ cashRegisterDetail.depositAmount }}</td>
                                <td>{{ cashRegisterDetail.withdrawalAmount }}</td>
                                <td>
                                    <button type="button" data-dismiss="modal" class="btn btn-outline-danger" 
                                        @click="setSelectedCashRegisterDetail(cashRegisterDetail);onDelete()" style="margin-right: 10px;">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16"
                                            fill="currentColor" class="bi bi-person-dash" viewBox="0 0 16 16">
                                            <path
                                                d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7M11 12h3a.5.5 0 0 1 0 1h-3a.5.5 0 0 1 0-1m0-7a3 3 0 1 1-6 0 3 3 0 0 1 6 0M8 7a2 2 0 1 0 0-4 2 2 0 0 0 0 4" />
                                            <path
                                                d="M8.256 14a4.5 4.5 0 0 1-.229-1.004H3c.001-.246.154-.986.832-1.664C4.484 10.68 5.711 10 8 10q.39 0 .74.025c.226-.341.496-.65.804-.918Q8.844 9.002 8 9c-5 0-6 3-6 4s1 1 1 1z" />
                                        </svg>
                                    </button>
                                    <button type="button" data-dismiss="modal" class="btn btn-outline-success"
                                        data-toggle="modal" data-target="#updateCashRegisterDetailModal"  @click="setSelectedCashRegisterDetail(cashRegisterDetail);">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16"
                                            fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                                            <path
                                                d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                            <path fill-rule="evenodd"
                                                d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z" />
                                        </svg>
                                    </button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <cashRegisterDetailCreateModal @newCashRegisterDetailCreated="getCashRegisterDetails()" ></cashRegisterDetailCreateModal>
        <cashRegisterDetailUpdateModal @cashRegisterDetailUpdated="getCashRegisterDetails()" :selectedCashRegisterDetail="selectedCashRegisterDetail"></cashRegisterDetailUpdateModal>
        <spinner :loading="isLoading"></spinner>
    </section>
</template>

<script lang="ts">
import CashRegisterDetailCreateModal from './CashRegisterDetailCreateModal.vue';
import CashRegisterDetailUpdateModal from './CashRegisterDetailUpdateModal.vue';
import AppContentHeader from '@/components/layouts/content-header/index.vue';
import Spinner from '@/components/layouts/spinner/index.vue';
import type { CashRegisterListDto } from '@/models/CashRegisters/CashRegisterListDto';
import { CashRegisterDetailListDto } from '@/models/CashRegistersDetails/CashRegisterDetailList';
import Swal from 'sweetalert2';

export default {
    components: {
        'app-content-header': AppContentHeader,
        'cashRegisterDetailCreateModal': CashRegisterDetailCreateModal,
        'cashRegisterDetailUpdateModal': CashRegisterDetailUpdateModal,
        'spinner': Spinner,
    },
    data() {
        return {
            currentCashRegister: null as CashRegisterListDto | null,
            cashRegisterDetails: [] as CashRegisterDetailListDto[],
            startDate: null as string | null,
            endDate: null as string | null,
            cashRegisterId: null as string | null,
            isLoading: false,
            selectedCashRegisterDetail: new CashRegisterDetailListDto(),
            cashRegisterDetailCreateModalIsOpen: false,
            cashRegisterDetailUpdateModalIsOpen: false,
        }
    },
    created() {
        this.cashRegisterId = this.$route.params.id as string;
        this.initDates();
        this.getCashRegisterDetails();
    },
    methods: {
        initDates(){
            const currentDate = new Date();
            const pastDate = new Date(currentDate); 
            pastDate.setDate(currentDate.getDate() - 30);
            this.startDate = pastDate.toISOString().slice(0, 10);
            this.endDate = currentDate.toISOString().slice(0, 10);
        },
        setSelectedCashRegisterDetail(cashRegisterDetail: CashRegisterDetailListDto) {
            this.selectedCashRegisterDetail = Object.assign(new CashRegisterDetailListDto(), cashRegisterDetail);
        },
        getCashRegisterDetails() {
            this.isLoading = true;
            this.$axios.get('cashRegisterDetails/getall', {
                    params: {
                        cashRegisterId: this.cashRegisterId, startDate: this.startDate, endDate: this.endDate
                    }
                })
                .then(response => {
                    this.cashRegisterDetails = response.data.data.details;
                })
                .catch(error => {
                    console.log(error);
                })
                .finally(() => {
                    this.isLoading = false;
                });
        },
        onDelete() {
            Swal.fire({
                title: "Kasa hareketini silmek istediğine emin misin?",
                text: `İşlem no : ${this.selectedCashRegisterDetail.id} `,
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#d33",
                cancelButtonColor: "#3085d6",
                cancelButtonText: "İptal Et",
                confirmButtonText: "Sil"
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$axios.post('/cashRegisterDetails/delete', { id: this.selectedCashRegisterDetail.id })
                        .then(() => {
                            Swal.fire({
                                title: "Silme işlemi tamamlandı! ",
                                text: `Kasa hareketi başarıyla silindi. (${this.selectedCashRegisterDetail.id})`,
                                icon: "success"
                            });
                            this.getCashRegisterDetails();
                        })
                        .catch(error => {
                            let errorDetail;
                            try {
                                errorDetail = error.response.data.errorMessages[0];
                            }
                            catch {
                                errorDetail = "Kasa hareketi silinirken hata oluştu!";
                            }
                            Swal.fire({
                                title: 'Hata!',
                                text: errorDetail,
                                icon: 'error',
                                confirmButtonText: 'Kapat'
                            });
                        })
                }
            })
        }
    }
}
</script>