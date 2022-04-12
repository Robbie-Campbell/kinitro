<template>
  <div class="information align-items-center">
    <div>
        <div class=" justify-content-center p-2 m-2 bg-light w-100">
            <h3 class="border-bottom p-2">Measure your participation!</h3>
            <p>Participant Name: {{this.user}}</p>
            <p>Number of times spoken: {{this.numberTimesSpoken}}</p>
        </div>
        <PulseLoader class="pulse-loader" v-if="this.loading"/>
        <div class="charts w-100 justify-content-between">
            <IndividualPie :participant='this.participant' />
            <MeetingPie :participant='this.participant' />
        </div>
    </div>
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
        props: ['name'],
        components: {
            'PulseLoader': PulseLoader,
            'IndividualPie': IndividualPie,
            'MeetingPie': MeetingPie,
        },
        watch: {
            name: function() {
                this.user = this.name;
            }
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
                user: null,
                numberTimesSpoken: 0
            }
        },
        methods: {
            updateData() {
                const id = this.$route.params.slug;
                return axios
                .get(`https://ed53-194-80-64-241.ngrok.io/api/displaydata/${id}`)
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
    .charts{
        display: flex;
    }
    @media screen and (max-width: 992px) {
        .charts{
            flex-direction: column;
            margin:auto;
        }
    }
</style>