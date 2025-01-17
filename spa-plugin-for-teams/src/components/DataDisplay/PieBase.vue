<template>
    <div class="m-2 p-2 bg-light chart">
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

        /**
         * pie: The chart.js component
         * id: The id given to the chart component.
         */
        data() {
            return {
                pie: null,
                id: this.setup.id
            }
        },

        /**
         * Updates the pie chart with data.
         */
        mounted() {
            this.applyDataToPie();
        },
        methods: {

            /**
             * Sets the pie chart with all necessary data.
             */
            applyDataToPie() {
                const ctx = document.getElementById(this.setup.id);
                this.pie = new Chart(ctx, {
                    type: 'pie',
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
                        },
                        responsive: true,
                        maintainAspectRatio: true
                    }
                })
            },

            /**
             * Updates all pie chart data.
             */
            updatePieData() {
                if (this.data.values) {
                    console.log(this.data);
                    this.pie.data.datasets[0].data = this.data.values;
                    this.updatePieSetup();
                }
                this.pie.update();
            },

            /**
             * Updates the setup of the pie chart.
             */
            updatePieSetup() {
                this.pie.data.labels = this.setup.labels;
                this.pie.data.datasets[0].backgroundColor = this.setup.colors;
            },

            /**
             * Converts the milliseconds into readable time values.
             */
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
        width:500px;
        height:380px;
    }
    @media screen and (max-width: 992px) {
        .chart{
            height:50vh;
            width:100%;
            max-width:600px;
        }
    }
    @media screen and (max-width: 780px) {
        .chart{
            height:40vh;
            max-width:500px;
        }
    }
</style>