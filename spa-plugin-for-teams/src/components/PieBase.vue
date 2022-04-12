<template>
  <div class="m-2 p-2 bg-light chart w-100">
    <h5 class="border-bottom p-2">{{this.setup.title}}</h5>
    <canvas class="drawnChart" :id="this.setup.id">
    </canvas>
    <div v-if="this.data.timeValues">
        <div v-for="(time, index) in this.data.timeValues" :key="time" v-bind:time="this.data.timeValues[index]">
            <p>{{this.setup.labels[index]}}: {{this.msToTime(time)}}</p>
        </div>
    </div>
  </div>
</template>

<script>
    import Chart from 'chart.js'
    export default {
        name: 'PartPie',
        props: ['data', 'setup'],
        watch: {
            data: function() {
                this.updatePieData();
            }
        },
        data() {
            return {
                pie: null,
                id: this.setup.id
            }
        },
        mounted() {
            this.applyDataToPie();
        },
        methods: {
            applyDataToPie() {
                const ctx = document.getElementById(this.setup.id);
                this.pie = new Chart(ctx, {
                    type: 'doughnut',
                    data: {
                        labels: this.setup.labels,
                        datasets: [{
                            label: this.setup.title,
                            backgroundColor: this.setup.colors,
                            data: [1, 1],
                            color: ['#fff']
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
            updatePieData() {
                if (this.data.values) {
                    this.pie.data.datasets[0].data = this.data.values;
                }
                this.pie.update();
            },
            msToTime(s) {

                // Pad to 2 or 3 digits, default is 2
                function pad(n, z) {
                    z = z || 2;
                    return ('00' + n).slice(-z);
                }

                var ms = s % 1000;
                s = (s - ms) / 1000;
                var secs = s % 60;
                s = (s - secs) / 60;
                var mins = s % 60;
                var hrs = (s - mins) / 60;

                return pad(hrs) + ':' + pad(mins) + ':' + pad(secs) + '.' + pad(ms, 3);
            }
        },
    }
</script>

<style scoped>
    .chart{
        min-width:437px;
        height:380px;
    }
    @media screen and (max-width: 992px) {
        .chart{
            min-width: 300px;
            height:50vh;
            width:100%;
        }
    }
    @media screen and (max-width: 780px) {
        .chart{
            height:40vh;
        }
    }
</style>