<template>
  <div class="home">
    <DisplayData />
  </div>
</template>

<script>
// @ is an alias to /src
import DisplayData from '@/components/DisplayData.vue'

export default {
  name: 'Home',
  components: {
    DisplayData
  }
}
</script>
