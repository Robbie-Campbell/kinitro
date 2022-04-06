<template>
  <div class="hello">
    <h1 v-if="loading">
        
    </h1>
    <h1 v-else>
        Time spoken: {{ participant['timeSpoken']['elapsed'] }}
        Time In Meeting: {{ participant['timeInMeeting']['elapsed'] }}</h1>
  </div>
</template>

<script>
    import axios from 'axios'
    export default {
    name: 'DisplayData',
    mounted: function () {
        this.participant = "hello";
        this.$nextTick(() => {
                window.setInterval(() => {
                this.updateData()
            }, 1000)
        })
    },
    data() {
        return {
            participant: this.updateData(),
            loading: true,
        }
    },
    methods: {
        updateData() {
            const id = this.$route.params.slug;
            axios
            .get(`https://9ab1-82-24-11-13.ngrok.io/api/displaydata/test/${id}`)
                .then((response) => {
                    this.participant = response.data;
                }).finally(() => (this.loading = false))
        },
    },
}
</script>

<style>
</style>