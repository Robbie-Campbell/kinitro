<template>
  <div class="main">
      <p>{{this.hello}}</p>
      <LinkList />
  </div>
</template>

<script>
    import LinkList from '@/components/GetLinks/LinkList.vue'
    export default {
        name: 'Base',
        components: {
            LinkList,
        },
        data() {
            return {
                hello: 'testing...'
            }
        }
    }
</script>

<style>
    main{
        color:white;
    }
</style>