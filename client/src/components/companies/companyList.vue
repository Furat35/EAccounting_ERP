<template>
    <app-content-header contentHeaderTitle="Şirketler" previousTab="Ana Sayfa" currentTab="Şirketler" />
    <section class="content" style="margin-left: 10px; margin-right: 10px;">
        <div class="card">
            <div class="card-header">
                <h3 class="card-title">Şirketler Listesi</h3>
                <div class="card-tools"><button type="button" data-card-widget="collapse" title="Collapse"
                        class="btn btn-tool"><i class="fas fa-minus" aria-hidden="true"></i></button><button
                        type="button" data-card-widget="remove" title="Remove" class="btn btn-tool"><i
                            class="fas fa-times" aria-hidden="true"></i></button></div>
            </div>
            <div class="card-body">
                <div class="form-group row">
                    <div class="col-9">
                        <button type="button" class="btn btn-success" style="margin-right:25px;" data-toggle="modal"
                            data-target="#createCompanyModal">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                                class="bi bi-building-add" viewBox="0 0 16 16">
                                <path
                                    d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7m.5-5v1h1a.5.5 0 0 1 0 1h-1v1a.5.5 0 0 1-1 0v-1h-1a.5.5 0 0 1 0-1h1v-1a.5.5 0 0 1 1 0" />
                                <path
                                    d="M2 1a1 1 0 0 1 1-1h10a1 1 0 0 1 1 1v6.5a.5.5 0 0 1-1 0V1H3v14h3v-2.5a.5.5 0 0 1 .5-.5H8v4H3a1 1 0 0 1-1-1z" />
                                <path
                                    d="M4.5 2a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm3 0a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm3 0a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm-6 3a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm3 0a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm3 0a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm-6 3a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm3 0a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5z" />
                            </svg> Şirket Ekle
                        </button>
                        <button type="button" class="btn btn-dark" @click="migrateAll()">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                                class="bi bi-arrow-clockwise" viewBox="0 0 16 16">
                                <path fill-rule="evenodd"
                                    d="M8 3a5 5 0 1 0 4.546 2.914.5.5 0 0 1 .908-.417A6 6 0 1 1 8 2z" />
                                <path
                                    d="M8 4.466V.534a.25.25 0 0 1 .41-.192l2.36 1.966c.12.1.12.284 0 .384L8.41 4.658A.25.25 0 0 1 8 4.466" />
                            </svg>
                            Tüm Veritabanları Güncelle
                        </button>
                    </div>
                    <div class="col-3"><input type="search" placeholder="Aranacak değer girin..."
                            class="form-control ng-untouched ng-pristine ng-valid"></div>
                </div>
                <div class="form-group mt-2">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Şirket Adı</th>
                                <th>Adres</th>
                                <th>Vergi Dairesi</th>
                                <th>Vergi Numarası</th>
                                <th>Server</th>
                                <th>Database Adı</th>
                                <th>İşlemler</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(company, index) in companies" :key="company.id">
                                <td>{{ index + 1 }}</td>
                                <td>{{ company.name }}</td>
                                <td>{{ company.fullAddress }}</td>
                                <td>{{ company.taxDepartment }}</td>
                                <td>{{ company.taxNumber }}</td>
                                <td>{{ company.database?.server }}</td>
                                <td>{{ company.database?.databaseName }}</td>
                                <td>
                                    <button type="button" data-dismiss="modal" class="btn btn-outline-danger"
                                        style="margin-right: 10px;" @click="setSelectedCompany(company); onDelete()">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16"
                                            fill="currentColor" class="bi bi-building-dash" viewBox="0 0 16 16">
                                            <path
                                                d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7M11 12h3a.5.5 0 0 1 0 1h-3a.5.5 0 0 1 0-1" />
                                            <path
                                                d="M2 1a1 1 0 0 1 1-1h10a1 1 0 0 1 1 1v6.5a.5.5 0 0 1-1 0V1H3v14h3v-2.5a.5.5 0 0 1 .5-.5H8v4H3a1 1 0 0 1-1-1z" />
                                            <path
                                                d="M4.5 2a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm3 0a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm3 0a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm-6 3a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm3 0a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm3 0a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm-6 3a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5zm3 0a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5z" />
                                        </svg>
                                    </button>
                                    <button type="button" data-dismiss="modal" class="btn btn-outline-success"
                                        data-toggle="modal" data-target="#updateCompanyModal"
                                        @click="setSelectedCompany(company)">
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
        <companyCreateModal @newCompanyCreated="getCompanies()"></companyCreateModal>
        <companyUpdateModal @companyUpdated="getCompanies()" :selectedCompany="selectedCompany"></companyUpdateModal>
        <spinner :loading="isLoading"></spinner>
    </section>
</template>

<script lang="ts">
import CompanyCreateModal from '@/components/companies/companyCreateModal.vue';
import CompanyUpdateModal from '@/components/companies/companyUpdateModal.vue';
import AppContentHeader from '@/components/layouts/content-header/index.vue';
import Spinner from '@/components/layouts/spinner/index.vue';
import type { CompanyListDto } from '@/models/Companies/CompanyListDto';

import Swal from 'sweetalert2';

export default {
    components: {
        'companyCreateModal': CompanyCreateModal,
        'appContentHeader': AppContentHeader,
        'companyUpdateModal': CompanyUpdateModal,
        'spinner': Spinner
    },
    data() {
        return {
            companies: null as CompanyListDto[] | null,
            isLoading: false,
            selectedCompany: null as CompanyListDto | null,
        }
    },
    created() {
        this.getCompanies();
    },
    methods: {
        setSelectedCompany(company: CompanyListDto) {
            this.selectedCompany = company;
        },
        getCompanies() {
            this.isLoading = true;
            this.$axios.get('companies/getall')
                .then(response => {
                    this.companies = response.data.data
                })
                .catch(error => {
                    console.log(error.response.data.errorMessages)
                })
                .finally(() => {
                    this.isLoading = false;
                })
        },
        onDelete() {
            Swal.fire({
                title: "Silmek istediğine emin misin?",
                text: `${this.selectedCompany!.name} şirketi silinecektir.`,
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#d33",
                cancelButtonColor: "#3085d6",
                cancelButtonText: "İptal Et",
                confirmButtonText: "Sil"
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$axios.post('/companies/delete', { id: this.selectedCompany!.id })
                        .then(() => {
                            Swal.fire({
                                title: "Silme işlemi tamamlandı! ",
                                text: `Şirket başarıyla silindi. (${this.selectedCompany!.name})`,
                                icon: "success"
                            });
                            this.getCompanies();
                        })
                        .catch(error => {
                            let errorDetail;
                            try {
                                errorDetail = error.response.data.errorMessages[0];
                            }
                            catch {
                                errorDetail = "Şirket silinirken hata oluştu!";
                            }
                            Swal.fire({
                                title: 'Hata!',
                                text: errorDetail,
                                icon: 'error',
                                confirmButtonText: 'Kapat'
                            });
                        })
                }
            });
        },
        migrateAll() {
            Swal.fire({
                title: "Veritabanlarını güncellemek istediğine emin misin?",
                text: `Tüm veritabanları güncellenecektir.`,
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#d33",
                cancelButtonColor: "#3085d6",
                cancelButtonText: "İptal Et",
                confirmButtonText: "Güncelle"
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$axios.post('companies/migrateAll')
                        .then(() => {
                            Swal.fire({
                                title: "Veritabanı güncelleme işlemi tamamlandı! ",
                                text: `Şirketler başarıyla güncellendi.`,
                                icon: "success"
                            });
                        })
                        .catch(error => {
                            console.log(error.response.data.errorMessages);
                        })
                }
            })


        }
    }
}
</script>