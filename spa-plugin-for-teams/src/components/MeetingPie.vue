<template>
    <div>
        <PieBase :data="this.data" :setup="this.setup" />
    </div>
</template>

<script>
    import PieBase from '@/components/PieBase.vue'
    export default {
        name: 'MeetingPie',
        components: {
            'PieBase': PieBase,
        },
        data() {
            return {
                data: {},
                setup: {'labels': ["Time Spent Speaking", "Time Spent In Meeting"],
                    'title': 'Average Meeting Participant Speaking Time',
                    'colors': ['#00003f', '#00008b'], 'id': "meetingPie"},
            }
        },
        props: ['participant'],
        watch: {
            participant: function() {
                this.updateData();
                console.log(this.participant);
            }
        },
        methods: {
            updateData() {
                var percentageTimeSpeaking = this.participant['meetingTotalTimeSpoken'] / this.participant['meetingTotalTimeInMeeting'] * 100;
                this.data = {
                    'values': [percentageTimeSpeaking, 100 - percentageTimeSpeaking]
                }
            }
        }
    }
</script>