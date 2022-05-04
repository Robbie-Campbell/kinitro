<template>
    <div class="information">
        <div class="align-items-center p-5 bg-dark">
            <div class="justify-content-center p-2 m-2 bg-light w-100">
                <h3 class="border-bottom p-2">Measure your participation!</h3>
                <p>Participant Name: {{this.user}}</p>
                <p>Number of times spoken: {{this.numberTimesSpoken}}</p>
            </div>
            <div v-if="this.searchingForUserOrUserHasBeenFound()">
                <PulseLoader class="pulse-loader" v-if="this.loading"/>
                <div class="charts w-100 justify-content-between">
                    <IndividualPie :participant='this.participant' />
                    <MeetingPie :participant='this.participant' />
                    <!-- <OtherParticipants :participant='this.participant' /> -->
                </div>
            </div>
            <div class="justify-content-center p-2 m-2 mt-5 bg-light w-100" v-else>
                <h1>Sorry, we couldn't find any data for this user try refreshing the page or get a new link!</h1>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios'
    import PulseLoader from 'vue-spinner/src/PulseLoader.vue'
    import IndividualPie from '@/components/DataDisplay/IndividualPie.vue'
    import MeetingPie from '@/components/DataDisplay/MeetingPie.vue'
    // import OtherParticipants from '@/components/DataDisplay/OtherParticipantsSpeakingTime.vue'
    export default {
        name: 'DisplayData',
        emits: ['nameRecieved'],
        props: ['name'],
        components: {
            'PulseLoader': PulseLoader,
            'IndividualPie': IndividualPie,
            'MeetingPie': MeetingPie,
            // 'OtherParticipants': OtherParticipants,
        },
        watch: {
            name: function() {
                this.user = this.name;
            }
        },
        mounted() {
            this.$nextTick(() => {
                var checkForUpdate = window.setInterval(() => {
                    this.checkForSuccessfulConnection(checkForUpdate);
                    this.attempts++;
                    this.updateAllData();
                }, 2500)
            });
        },
        data() {
            return {
                participant: null,
                attempts: 0,
                loading: true,
                user: null,
                numberTimesSpoken: 0
            }
        },
        methods: {
            updateData() {
                const id = this.$route.params.slug;
                return axios
                .get(`${process.env.VUE_APP_API_LINK}/api/displaydata/${id}`)
                    .then((response) => {
                        this.participant = response.data;
                    }).catch(() => {
                        this.participant = null;
                    }).finally(() => (this.loading = false))
            },
            checkForSuccessfulConnection(timeOut) {
                if (!this.participant && this.attempts == 2) {
                    clearInterval(timeOut);
                }
            },
            searchingForUserOrUserHasBeenFound() {
                return this.attempts < 2 || this.participant['timeSpoken'];
            },
            updateNumberTimesSpoken() {
                this.numberTimesSpoken = Math.round(parseInt(this.participant['numberOfTimesSpoken']));
            },
            updatePropInParent() {
                this.loading = false;
                if (this.participant['timeSpoken'])
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
        }
    }
</style>