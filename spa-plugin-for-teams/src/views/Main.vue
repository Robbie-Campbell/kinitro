<template>
  <div class="home container mt-5">
    <Base />
  </div>
</template>

<script>
// @ is an alias to /src
import Base from '@/components/DataDisplay/Base.vue'

export default {
  name: 'Main',
  components: {
    Base
  }
}
</script>