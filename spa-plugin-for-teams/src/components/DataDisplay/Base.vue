<template>
  <div class="main">
    <DisplayData :name="this.name" v-on:nameRecieved="updateName"/>
  </div>
</template>

<script>
  import DisplayData from '@/components/DataDisplay/DisplayData.vue'
      export default {
        name: 'Base',
        components: {
          DisplayData,
        },
        /**
         * On mounted display the name attribute (with a simple animation)
         */
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

        /**
         * name: The name of the user.
         * nameUpdated: set to true if a name is recieved from the API.
         */
        data() {
          return {
            name: ".",
            nameUpdated: false,
          }
        },
        methods: {
          /**
           * Update the name graphic.
           */
          updateName(name) {
            this.nameUpdated = true;
            this.name = name.split(" ")[0];
          },

          /**
           * Resets number of periods after 3.
           */
          nameDisplayGraphic() {
            this.name.length == 3 ? this.name = "." : this.name += ".";
          }
        }
  }
</script>