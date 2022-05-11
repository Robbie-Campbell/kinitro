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

        /**
         * data: The data to be passed into the piebase component
         * setup: The setup information for the pie component.
         * parsedOtherUsers: The data which has been processed for display.
         * hasNotBeenParsed: set to true if the data has been parsed.
         */
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

            /**
             * Updates the piebase with the newly requested data.
             */
            updateData() {
                this.updateSetup();
                this.updateTimeSpoken();
                this.data = {
                    'values': this.getOtherUserDetails('percentage'),
                    'timeValues': this.getOtherUserDetails('timeSpoken')
                }
            },

            /**
             * Parses the user information into a format readable by the piebase component.
             */
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

            /**
             * Collates all of the other user data in the meeting.
             */
            getOtherUserDetails(param) {
                let returnArr = [];
                Object.values(this.parsedOtherUsers).forEach(element => {
                    returnArr.push(element[param]);
                });
                return returnArr;
            },

            /**
             * Updates the time spoken data for other participants.
             */
            updateTimeSpoken(){
                let i = 1;
                Object.values(this.participant['otherParticipantsSpeakingTime']).forEach(element => {
                    if (this.parsedOtherUsers[i]) {
                        this.parsedOtherUsers[i]['timeSpoken'] = element;
                        this.parsedOtherUsers[i]['percentage'] = Math.round(element / this.sumOfTimeSpentSpeaking() * 100);                   
                    }
                    i++;
                });            
            },

            /**
             * Update the setup prop with new values.
             */
            updateSetup() {
                this.setup ={'labels': this.getOtherUserDetails('label'),
                    'title': 'How much time others have spoken',
                    'colors': this.getOtherUserDetails('color'), 'id': "otherParticipants"}
            },

            /**
             * Get the total amount of time spent in the meeting.
             */
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