<template>
  <div class="home container p-5 bg-dark">
    <Base />
  </div>
</template>

<script>
// @ is an alias to /src
import Base from '@/components/Base.vue'

export default {
  name: 'Main',
  components: {
    Base
  }
}
</script>