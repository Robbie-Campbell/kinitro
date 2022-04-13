<template>
    <div class="linkDisplay">
        <div v-if="this.links">
            <div v-for="link in this.links" :key="link.name" v-bind="this.links">
                <Link :link="link" />
            </div>
        </div>
        <PulseLoader v-if="this.searching" />
        <div v-else class="p-2 bg-light">
            <h1>Could not find any active links...</h1>
        </div>
        <button v-on:click="this.getCurrentLinks" class="btn btn-secondary">Refresh List</button>
    </div>
</template>

<script>
import axios from 'axios'
import PulseLoader from 'vue-spinner/src/PulseLoader.vue'
import Link from '@/components/GetLinks/Link.vue'
export default {
    name: 'LinkList',
    components: {
        Link,
        PulseLoader
    },
    mounted() {
        this.getCurrentLinks();
    },
    data() {
        return {
            links: {},
            searching: false,
        }
    },
    methods: {
        getCurrentLinks() {
            this.searching = true;
            return axios
            .get(`https://ed53-194-80-64-241.ngrok.io/api/getLinks`)
                .then((response) => {
                    this.links = response.data;
            }).catch((e) => {
                console.log(e)
                this.links = null;
            }).finally(() => {
                this.searching = false;
            });
        },
    }
}
</script>