<template>
    <div class="linkDisplay">
        <input class="form-control" type="text" v-model="this.searchQuery" />
        <div v-if="this.links">
            <div v-for="link in this.links.results" v-bind:key="link.name" v-bind="this.links">
                <Link :link="link" />
            </div>
        </div>
        <div v-if="!this.links" class="bg-light p-3"><p>No Links were found... Yet!</p></div>
        <PulseLoader v-if="this.searching" />
        <button v-on:click="this.getCurrentLinks()" class="btn btn-secondary">Search for Your URL</button>
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
    data() {
        return {
            links: {},
            searchQuery: null,
            searching: false,
        }
    },
    methods: {
        getCurrentLinks() {
            this.searching = true;
            return axios
            .get(`https://e3b5-194-80-64-241.ngrok.io/api/links/getlinks/${this.searchQuery}`)
                .then((response) => {
                    this.links['results'] = response.data;
            }).catch((e) => {
                console.log(e)
                this.links = null;
            }).finally(() => {
                this.searching = false;
            });
        },
    },
}
</script>