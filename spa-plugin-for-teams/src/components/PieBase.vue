<template>
  <div style="width:400px;height:400px">
    <canvas :id="this.setup.id">
    </canvas>
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
            console.log(this.setup.id)
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
            genRandomId() {
                var S4 = () => {
                    return (((1+Math.random())*0x10000)|0).toString(16).substring(1);
                };
                return (S4()+S4()+"-"+S4()+"-"+S4()+"-"+S4()+"-"+S4()+S4()+S4());
            },
            updatePieData() {
                if (this.data.values) {
                    this.pie.data.datasets[0].data = this.data.values;
                }
                this.pie.update();
            },
        },
    }
</script>