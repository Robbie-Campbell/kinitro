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
        data() {
            return {
                data: {
                    totalSpeakingtime: 1,
                },
                setup: {'labels': ["Time Spent Speaking", "Time Not Speaking"],
                    'title': 'Meeting Average for Time Spent Speaking',
                    'colors': ['#00003f', '#00008b'], 'id': "meetingPie"},
            }
        },
        props: ['participant'],
        watch: {
            participant: function() {
                if (this.participant['timeSpoken'])
                    this.updateData();
            }
        },
        methods: {
            updateData() {
                this.getTotalTimeSpokenInMeeting();
                var percentageTimeSpeaking = this.totalSpeakingtime / this.participant['meetingTotalTimeInMeeting'] * 100;
                this.data = {
                    'values': [Math.round(percentageTimeSpeaking), 100 - Math.round(percentageTimeSpeaking)],
                    'timeValues': [this.totalSpeakingtime, this.participant['meetingTotalTimeInMeeting'] - this.totalSpeakingtime]
                }     
            },
            getTotalTimeSpokenInMeeting() {
                let sumOfTimeInMeeting = 0;
                Object.values(this.participant['otherParticipantsSpeakingTime']).forEach(element => {
                    sumOfTimeInMeeting += element;
                });
                this.totalSpeakingtime = sumOfTimeInMeeting;
            }
        }
    }
</script>