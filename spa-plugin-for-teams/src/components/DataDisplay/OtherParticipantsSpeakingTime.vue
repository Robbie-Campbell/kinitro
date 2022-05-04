<template>
    <div>
        <PieBase :data="this.data" :setup="this.setup" />
    </div>
</template>

<script>
    import PieBase from '@/components/DataDisplay/PieBase.vue'
    export default {
        name: 'OtherParticipants',
        components: {
            'PieBase': PieBase,
        },
        data() {
            return {
                data: {
                    parsedOtherUsers: {},
                },
                setup: {'labels': this.getOtherUserDetails('label'),
                    'title': 'How others have performed',
                    'colors': this.getOtherUserDetails('color'), 'id': "otherParticipants"},
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
                    'timeValues': [this.getOtherUserDetails['timeSpoken']]
                }
            },
            parseOtherUsers() {
                let i = 0;
                this.participant['otherParticipantsSpeakingTime'].array.forEach(element => {
                    this.parsedOtherUsers[i].label = 'Participant ' + i;
                    this.parsedOtherUsers[i].timeSpoken = element;
                    this.parsedOtherUsers[i].color = '#'+(Math.random()*0xFFFFFF<<0).toString(16)
                });
            },
            getOtherUserDetails(param) {
                let returnArr = [];
                this.parsedOtherUsers.forEach(element => {
                    returnArr += element[param];
                });
                return returnArr;
            },
        }
    }
</script>