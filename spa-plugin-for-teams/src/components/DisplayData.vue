<template>
  <div class="information">
    <h1>{{this.numberTimesSpoken}}</h1>
    <IndividualPie :participant='this.participant' />
    <PulseLoader class="pulse-loader" v-if="this.loading"/>
    <MeetingPie :participant='this.participant' />
  </div>
</template>

<script>
    import axios from 'axios'
    import PulseLoader from 'vue-spinner/src/PulseLoader.vue'
    import IndividualPie from '@/components/IndividualPie.vue'
    import MeetingPie from '@/components/MeetingPie.vue'
    export default {
        name: 'DisplayData',
        emits: ['nameRecieved'],
        components: {
            'PulseLoader': PulseLoader,
            'IndividualPie': IndividualPie,
            'MeetingPie': MeetingPie,
        },
        mounted() {
            this.$nextTick(() => {
                    window.setInterval(() => {
                    console.log(this.participant);
                    this.updateAllData()
                }, 5000)
            });
        },
        data() {
            return {
                participant: null,
                loading: true,
                data: {},
                numberTimesSpoken: 0
            }
        },
        methods: {
            updateData() {
                const id = this.$route.params.slug;
                return axios
                .get(`https://e33e-194-80-64-241.ngrok.io/api/displaydata/${id}`)
                    .then((response) => {
                        this.participant = response.data;
                    }).finally(() => (this.loading = false))
            },
            updateNumberTimesSpoken() {
                this.numberTimesSpoken = Math.round(parseInt(this.participant['numberOfTimesSpoken']) / 30);
            },
            updatePropInParent() {
                this.loading = false;
                this.$emit("nameRecieved", this.participant['participantName']);
            },
            updateAllData() {
                this.updateData();
                if (this.participant['timeSpoken']) {
                    this.updatePropInParent()
                    this.updateNumberTimesSpoken();
                }
            }
        }
    }
</script>

<style>
    .pulse-loader{
        padding: 20px;
    }
</style>