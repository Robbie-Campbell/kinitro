<template>
  <div class="main align-items-center">
    <DisplayData :name="this.name" v-on:nameRecieved="updateName"/>
  </div>
</template>

<script>
  import DisplayData from '@/components/DisplayData.vue'
      export default {
        name: 'Base',
        components: {
          DisplayData,
        },
        mounted() {
          this.$nextTick(() => {
                  window.setInterval(() => {
                  if (!this.nameUpdated)
                  {
                    this.nameDisplayGraphic();
                  }
              }, 1000)
          });
        },
        data() {
          return {
            name: ".",
            nameUpdated: false,
          }
        },
        methods: {
          updateName(name) {
            this.nameUpdated = true;
            this.name = name.split(" ")[0];
          },
          nameDisplayGraphic() {
            this.name.length == 3 ? this.name = "." : this.name += ".";
          }
        }
  }
</script>