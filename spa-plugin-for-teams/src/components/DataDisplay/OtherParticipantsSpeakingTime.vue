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
                data: {},
                setup: {'labels': ['Loading...'],
                    'title': 'The Time Spoken by Each Participant',
                    'colors': ['#FFFFFF', '#000000'], 'id': "otherParticipants", watch: true},
                parsedOtherUsers: {},
                hasNotBeenParsed: true,
            }
        },
        props: ['participant'],
        watch: {
            participant: function() {
                if (this.participant['timeSpoken'])
                {
                    if (this.hasNotBeenParsed)
                    {
                        this.parseOtherUsers();
                        this.hasNotBeenParsed = false;
                    }
                    this.updateData();
                }
            }
        },
        methods: {
            updateData() {
                this.updateSetup();
                this.updateTimeSpoken();
                this.data = {
                    'values': this.getOtherUserDetails('percentage'),
                    'timeValues': this.getOtherUserDetails('timeSpoken')
                }
            },
            parseOtherUsers() {
                let i = 1;
                Object.values(this.participant['otherParticipantsSpeakingTime']).forEach(element => {
                    if (element > 100){
                        this.parsedOtherUsers[i] = {'label': 'Participant ' + i,
                                                    'timeSpoken': element, 
                                                    'color': '#'+(Math.random()*0xFFFFFF<<0).toString(16),
                                                    'percentage': Math.round(element / this.sumOfTimeSpentSpeaking() * 100)
                        };
                    }
                    i++;
                });
            },
            getOtherUserDetails(param) {
                let returnArr = [];
                Object.values(this.parsedOtherUsers).forEach(element => {
                    returnArr.push(element[param]);
                });
                return returnArr;
            },
            updateTimeSpoken(){
let             i = 1;
                Object.values(this.participant['otherParticipantsSpeakingTime']).forEach(element => {
                    if (this.parsedOtherUsers[i]) {
                        this.parsedOtherUsers[i]['timeSpoken'] = element;
                        this.parsedOtherUsers[i]['percentage'] = Math.round(element / this.sumOfTimeSpentSpeaking() * 100);                   
                    }
                    i++;
                });            
            },
            updateSetup() {
                this.setup ={'labels': this.getOtherUserDetails('label'),
                    'title': 'How much time others have spoken',
                    'colors': this.getOtherUserDetails('color'), 'id': "otherParticipants"}
            },
            sumOfTimeSpentSpeaking() {
                let sumOfTimeSpentSpeaking = 0;
                Object.values(this.participant['otherParticipantsSpeakingTime']).forEach(element => {
                    sumOfTimeSpentSpeaking += element;
                });
                console.log(sumOfTimeSpentSpeaking);
                return sumOfTimeSpentSpeaking;
            },
        }
    }
</script>