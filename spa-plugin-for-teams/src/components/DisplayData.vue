<template>
  <div class="hello">
    <canvas id="dataDisplay">
    </canvas>
    <PulseLoader class="pulse-loader" v-if="this.loading"/>
  </div>
</template>

<script>
    import axios from 'axios'
    import Chart from 'chart.js'
    import PulseLoader from 'vue-spinner/src/PulseLoader.vue'
    export default {
        name: 'DisplayData',
        emits: ['nameRecieved'],
        components: {
            'PulseLoader': PulseLoader,
        },
        mounted() {
            this.applyDataToPie();
            this.$nextTick(() => {
                    window.setInterval(() => {
                    this.updateChartValues()
                }, 5000)
            });
        },
        data() {
            return {
                participant: null,
                loading: true,
                data: [],
                pie: null
            }
        },
        methods: {
            updateData() {
                const id = this.$route.params.slug;
                return axios
                .get(`https://bb31-82-24-11-13.ngrok.io/api/displaydata/${id}`)
                    .then((response) => {
                        this.participant = response.data;
                    }).finally(() => (this.loading = false))
            },
            applyDataToPie() {
                const ctx = document.getElementById('dataDisplay');
                this.pie = new Chart(ctx, {
                    type: 'doughnut',
                    data: {
                        labels: ["Time Spent Speaking", "Time in Meeting Total"],
                        datasets: [{
                            label: "Time in meeting speaking breakdown",
                            backgroundColor: ["#3e95cd", "#8e5ea2"],
                            data: [1, 1]
                        }]
                    },
                    options: {
                        title: {
                            display: false,
                            text: 'Breakdown of talking time'
                        },
                    }
                })
            },
            updateChartValues() {
                this.updateData();
                if (this.participant['timeSpoken']) {
                    this.loading = false;
                    this.$emit("nameRecieved", this.participant['participantName']);
                    this.pie.data.datasets[0].data = [this.participant['timeSpoken']['elapsedMilliseconds'], this.participant['timeInMeeting']['elapsedMilliseconds']];
                }
                this.pie.update();
            }
        }
    }
</script>

<style>
    .pulse-loader{
        padding: 20px;
    }
</style>