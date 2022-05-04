<template>
    <div>
        <PieBase :data="this.data" :setup="this.setup" />
    </div>
</template>

<script>
    import PieBase from '@/components/DataDisplay/PieBase.vue'
    export default {
        name: 'IndividualPie',
        components: {
            'PieBase': PieBase,
        },
        data() {
            return {
                data: {},
                setup: {'labels': ["Time Speaking", "Time Not Speaking"],
                    'title': 'Your Personal Time Spent Speaking',
                    'colors': ['#00429d', '#5cc6b2'], 'id': "individualPie"},
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
                var percentageTimeSpeaking = this.participant['timeSpoken'] / this.participant['timeInMeeting']['elapsedMilliseconds'] * 100;
                this.data = {
                    'values': [Math.round(percentageTimeSpeaking), 100 - Math.round(percentageTimeSpeaking)],
                    'timeValues': [this.participant['timeSpoken'], this.participant['timeInMeeting']['elapsedMilliseconds'] - this.participant['timeSpoken']]
                }
            },
        }
    }
</script>