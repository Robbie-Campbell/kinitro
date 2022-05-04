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
                    'title': 'How others have performed',
                    'colors': ['#FFFFFF', '#000000'], 'id': "otherParticipants", watch: true},
                parsedOtherUsers: {},
                hasNotBeenParsed: true
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
                    'values': this.getOtherUserDetails('timeSpoken'),
                    'timeValues': this.getOtherUserDetails('timeSpoken')
                }
            },
            parseOtherUsers() {
                let i = 1;
                Object.values(this.participant['otherParticipantsSpeakingTime']).forEach(element => {
                    this.parsedOtherUsers[i] = {'label': 'Participant ' + i,
                                                'timeSpoken': element, 
                                                'color': '#'+(Math.random()*0xFFFFFF<<0).toString(16)};
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
                    this.parsedOtherUsers[i]['timeSpoken'] = element;                    
                    i++;
                });            
            },
            updateSetup() {
                this.setup ={'labels': this.getOtherUserDetails('label'),
                    'title': 'How much time others have spoken',
                    'colors': this.getOtherUserDetails('color'), 'id': "otherParticipants"}
            }
        }
    }
</script>