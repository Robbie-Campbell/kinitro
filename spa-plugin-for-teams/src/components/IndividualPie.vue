<template>
    <div>
        <PieBase :data="this.data" :setup="this.setup" />
    </div>
</template>

<script>
    import PieBase from '@/components/PieBase.vue'
    export default {
        name: 'IndividualPie',
        components: {
            'PieBase': PieBase,
        },
        data() {
            return {
                data: {},
                setup: {'labels': ["Percentage Time Spent Speaking", "Percentage Time Not Speaking"],
                    'title': 'Total Time Speaking Breakdown',
                    'colors': ['#ab500f', '#6e4115'], 'id': "individualPie"},
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
                var percentageTimeSpeaking = this.participant['timeSpoken']['elapsedMilliseconds'] / this.participant['timeInMeeting']['elapsedMilliseconds'] * 100;
                this.data = {
                    'values': [Math.round(percentageTimeSpeaking), 100 - Math.round(percentageTimeSpeaking)],
                    'timeValues': [this.participant['timeSpoken']['elapsedMilliseconds'], this.participant['timeInMeeting']['elapsedMilliseconds']]
                }
            },
        }
    }
</script>