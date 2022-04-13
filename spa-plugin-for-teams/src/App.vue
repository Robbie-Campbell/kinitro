<template>
    <nav class="d-flex bg-light p-4 justify-content-around">
        <router-link to="/">Home</router-link>
        <router-link to="/about">About</router-link>
    </nav>
  <router-view/>
</template>

<style>
  #app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    background: #22303C;
    text-align: center;
    min-height:100vh;
  }

  @media screen and (max-width: 992px) {
      #app{
          height:100%;
      }
  }
</style>
