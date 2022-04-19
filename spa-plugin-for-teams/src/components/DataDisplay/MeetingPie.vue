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
                data: {},
                setup: {'labels': ["Time Spent Speaking", "Time Spent In Meeting"],
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
                var percentageTimeSpeaking = this.participant['meetingTotalTimeSpoken'] - this.participant['timeSpoken']['elapsedMilliseconds'] / this.participant['meetingTotalTimeInMeeting'] - this.participant['timeInMeeting']['elapsedMilliseconds'] * 100;
                if (this.participant['meetingTotalTimeSpoken'] - this.participant['timeSpoken']['elapsedMilliseconds'] > 1)
                {
                    this.data = {
                        'values': [Math.round(percentageTimeSpeaking), 100 - Math.round(percentageTimeSpeaking)],
                        'timeValues': [this.participant['meetingTotalTimeSpoken'] - this.participant['timeSpoken']['elapsedMilliseconds'], this.participant['meetingTotalTimeInMeeting'] - this.participant['timeInMeeting']['elapsedMilliseconds']]
                    }
                }
                else {
                    this.data = {
                        'values': [1, 1],
                        'timeValues': [0, 0]

                    }
                }       
            }
        }
    }
</script>