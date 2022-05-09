<template>
    <div>
        <PieBase :data="this.data" :setup="this.setup" />
    </div>
</template>

<script>
    import PieBase from '@/components/DataDisplay/PieBase.vue'
    export default {
        name: 'MeetingPie',
        components: {
            'PieBase': PieBase,
        },

        /**
         * data: The data to be passed into the piebase component
         * setup: The setup information for the pie component.
         */
        data() {
            return {
                data: {
                    totalSpeakingtime: 1,
                },
                setup: {'labels': ["Time Speaking", "Time Not Speaking"],
                    'title': 'Meeting Average for Speaking Time',
                    'colors': ['#93003a', '#ff005e'], 'id': "meetingPie"},
            }
        },

        /**
         * The prop data passed from the parent component.
         */
        props: ['participant'],

        /**
         * Checks to see if the participant exists and then updates data.
         */
        watch: {
            participant: function() {
                if (this.participant['timeSpoken'])
                    this.updateData();
            }
        },
        methods: {

            /**
             * Updates the piebase with the newly requested data.
             */
            updateData() {
                this.getTotalTimeSpokenInMeeting();
                var percentageTimeSpeaking = this.totalSpeakingtime / this.participant['meetingTotalTimeInMeeting'] * 100;
                this.data = {
                    'values': [Math.round(percentageTimeSpeaking), 100 - Math.round(percentageTimeSpeaking)],
                    'timeValues': [this.totalSpeakingtime, this.participant['meetingTotalTimeInMeeting'] - this.totalSpeakingtime]
                }     
            },

            /**
             * Gets the meeting total of all participants speaking time.
             */
            getTotalTimeSpokenInMeeting() {
                let sumOfTimeSpentSpeaking = 0;
                Object.values(this.participant['otherParticipantsSpeakingTime']).forEach(element => {
                    sumOfTimeSpentSpeaking += element;
                });
                this.totalSpeakingtime = sumOfTimeSpentSpeaking;
            },
        }
    }
</script>