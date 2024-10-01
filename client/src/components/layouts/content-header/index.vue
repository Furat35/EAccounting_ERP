<template>
    <section class="content-header">
      <div class="container-fluid px-4">
        <div class="row mb-2">
          <div class="col-sm-6">
            <h1>{{ contentHeaderTitle }}</h1>
          </div>
          <div class="col-sm-6">
            <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="/">{{ previousTab }}</a></li>
              <li class="breadcrumb-item">{{ currentTab }}</li>
            </ol>
          </div>
        </div>
      </div>
    </section>
</template>

<script>
export default{
    props:['contentHeaderTitle', 'previousTab', 'currentTab']
}
</script>