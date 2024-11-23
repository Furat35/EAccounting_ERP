<template>
    <app-content-header contentHeaderTitle="Ana Sayfa" previousTab="Ana Sayfa" currentTab="." />
    <section class="content" style="margin-left: 10px; margin-right: 10px;">
        <div class="card">
            <div class="card-header">
                <h3 class="card-title">Ana Sayfa</h3>
                <div class="card-tools"><button type="button" data-card-widget="collapse" title="Collapse"
                        class="btn btn-tool"><i class="fas fa-minus" aria-hidden="true"></i></button><button
                        type="button" data-card-widget="remove" title="Remove" class="btn btn-tool"><i
                            class="fas fa-times" aria-hidden="true"></i></button></div>
            </div>
            <div class="card-body">
                <div class="form-group row">
                    <div class="col-3"><input type="search" placeholder="Aranacak değer girin..."
                            class="form-control ng-untouched ng-pristine ng-valid"></div>
                </div>
                <div class="form-group mt-2">
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>#</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>test</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </section>
</template>

<script>
import AppContentHeader from '@/components/layouts/content-header/index.vue';

export default {
    components: {
        'app-content-header': AppContentHeader,
    }
}
</script>