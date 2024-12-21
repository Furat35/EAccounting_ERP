<template>
    <app-content-header contentHeaderTitle="Kullanıcılar" previousTab="Ana Sayfa" currentTab="Kullanıcılar" />
    <section class="content" style="margin-left: 10px; margin-right: 10px;">
        <div class="card">
            <div class="card-header">
                <h3 class="card-title">Kullanıcılar Listesi</h3>
                <div class="card-tools"><button type="button" data-card-widget="collapse" title="Collapse"
                        class="btn btn-tool"><i class="fas fa-minus" aria-hidden="true"></i></button><button
                        type="button" data-card-widget="remove" title="Remove" class="btn btn-tool"><i
                            class="fas fa-times" aria-hidden="true"></i></button></div>
            </div>
            <div class="card-body">
                <div class="form-group row">
                    <div class="col-9">
                        <button type="button" class="btn btn-dark" data-toggle="modal" data-target="#createUserModal">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor"
                                class="bi bi-person-add" viewBox="0 0 16 16">
                                <path
                                    d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7m.5-5v1h1a.5.5 0 0 1 0 1h-1v1a.5.5 0 0 1-1 0v-1h-1a.5.5 0 0 1 0-1h1v-1a.5.5 0 0 1 1 0m-2-6a3 3 0 1 1-6 0 3 3 0 0 1 6 0M8 7a2 2 0 1 0 0-4 2 2 0 0 0 0 4" />
                                <path
                                    d="M8.256 14a4.5 4.5 0 0 1-.229-1.004H3c.001-.246.154-.986.832-1.664C4.484 10.68 5.711 10 8 10q.39 0 .74.025c.226-.341.496-.65.804-.918Q8.844 9.002 8 9c-5 0-6 3-6 4s1 1 1 1z" />
                            </svg> Kullanıcı Ekle
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
                                <th>Ad Soyad</th>
                                <th>Kullanıcı Adı</th>
                                <th>Mail Adresi</th>
                                <th>Bağlı Olduğu Şirketler</th>
                                <th>İşlemler</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="(user, index) in users" :key="user.id">
                                <td>{{ index + 1 }}</td>
                                <td>{{ user.fullName }}</td>
                                <td>{{ user.userName }}</td>
                                <td>{{ user.email }}</td>
                                <td>
                                    <ul style="max-height: 80px;overflow-y: auto;">
                                        <li v-for="companyUser in user.companyUsers":key="companyUser.companyId">{{ companyUser.company.name }}</li>
                                        <span v-if="user.companyUsers.length < 1">-</span>
                                    </ul>
                                </td>
                                <td>
                                    <button type="button" data-dismiss="modal" class="btn btn-outline-danger"
                                        style="margin-right: 10px;" @click="setSelectedUser(user); onDelete()">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16"
                                            fill="currentColor" class="bi bi-person-dash" viewBox="0 0 16 16">
                                            <path
                                                d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7M11 12h3a.5.5 0 0 1 0 1h-3a.5.5 0 0 1 0-1m0-7a3 3 0 1 1-6 0 3 3 0 0 1 6 0M8 7a2 2 0 1 0 0-4 2 2 0 0 0 0 4" />
                                            <path
                                                d="M8.256 14a4.5 4.5 0 0 1-.229-1.004H3c.001-.246.154-.986.832-1.664C4.484 10.68 5.711 10 8 10q.39 0 .74.025c.226-.341.496-.65.804-.918Q8.844 9.002 8 9c-5 0-6 3-6 4s1 1 1 1z" />
                                        </svg>
                                    </button>
                                    <button type="button" data-dismiss="modal" class="btn btn-outline-success"
                                        data-toggle="modal" data-target="#updateUserModal"
                                        @click="setSelectedUser(user)">
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
        <userCreateModal @newUserCreated="getUsers()"></userCreateModal>
        <userUpdateModal @userUpdated="getUsers()" :selectedUser="selectedUser"></userUpdateModal>
        <spinner :loading="isLoading"></spinner>
    </section>
</template>

<script lang="ts">
import { UserListDto } from '@/models/Users/UserListDto';
import UserCreateModal from '@/components/users/userCreateModal.vue';
import UserUpdateModal from '@/components/users/userUpdateModal.vue';
import AppContentHeader from '@/components/layouts/content-header/index.vue';
import Spinner from '@/components/layouts/spinner/index.vue';
import Swal from 'sweetalert2';

export default {
    components: {
        'userCreateModal': UserCreateModal,
        'app-content-header': AppContentHeader,
        'userUpdateModal': UserUpdateModal,
        'spinner': Spinner
    },
    data() {
        return {
            users: null as UserListDto[] | null,
            selectedUser: new UserListDto(),
            isLoading: false
        }
    },
    created() {
        this.getUsers();
    },
    methods: {
        setSelectedUser(user: UserListDto) {
            this.selectedUser = Object.assign(new UserListDto(), user);
        },
        getUsers() {
            this.isLoading = true;
            this.$axios.get('users/getall')
                .then(response => {
                    this.users = response.data.data;
                })
                .catch(error => {
                    console.log(error.response.data.errorMessages);
                    this.isLoading = false;
                })
                .finally(() => {
                    this.isLoading = false;
                })
        },
        onDelete() {
            Swal.fire({
                title: "Silmek istediğine emin misin?",
                text: `${this.selectedUser!.firstName} ${this.selectedUser!.firstName} (${this.selectedUser!.email})`,
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#d33",
                cancelButtonColor: "#3085d6",
                cancelButtonText: "İptal Et",
                confirmButtonText: "Sil"
            }).then((result) => {
                if (result.isConfirmed) {
                    this.$axios.post('/users/delete', { id: this.selectedUser!.id })
                        .then(() => {
                            Swal.fire({
                                title: "Silme işlemi tamamlandı! ",
                                text: `Kullanıcı başarıyla silindi. ${this.selectedUser!.firstName} ${this.selectedUser!.firstName}`,
                                icon: "success"
                            });
                            this.getUsers();
                        })
                        .catch(error => {
                            let errorDetail;
                            try {
                                errorDetail = error.response.data.errorMessages[0];
                            }
                            catch {
                                errorDetail = "Kullanıcı silinirken hata oluştu!";
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