<template>
    <div class="not-found">
      <h1 class="title">404</h1>
      <p class="message">Oops! Aranan sayfa bulunamadı.</p>
      <router-link to="/" class="home-link">Ana Sayfaya Dön</router-link>
    </div>
  </template>
  
  <script lang="ts">
  import { defineComponent } from 'vue';
  
  export default defineComponent({
    name: 'NotFound'
  });
  </script>
  
  <style scoped>
  .not-found {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    height: 100vh;
    text-align: center;
    background-color: #f9f9f9;
    color: #333;
  }
  
  .title {
    font-size: 6rem;
    font-weight: bold;
    margin: 0;
  }
  
  .message {
    font-size: 1.5rem;
    margin: 1rem 0;
  }
  
  .home-link {
    font-size: 1rem;
    color: #007bff;
    text-decoration: none;
    border: 1px solid #007bff;
    border-radius: 5px;
    padding: 0.5rem 1rem;
    transition: background-color 0.3s, color 0.3s;
  }
  
  .home-link:hover {
    background-color: #007bff;
    color: #fff;
  }
  </style>
  