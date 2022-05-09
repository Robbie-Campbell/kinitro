<template>
    <div class="linkDisplay">
        <input class="form-control" type="text" v-model="this.searchQuery" />
        <div v-if="this.links">
            <div v-for="link in this.links.results" v-bind:key="link.name" v-bind="this.links">
                <Link :link="link" />
            </div>
        </div>
        <div v-if="!this.links || !this.links['results'] == ''" class="bg-light p-3"><p>No Links were found... Yet!</p></div>
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

    /**
     * links: the list of links
     * searchQuery: The query param for the API.
     * searching: Whether the app is currently searching for links.
     */
    data() {
        return {
            links: {},
            searchQuery: null,
            searching: false,
        }
    },
    methods: {

        /**
         * Gets all of the current links containing the searcj parameter.
         */
        getCurrentLinks() {
            this.searching = true;
            return axios
            .get(`${process.env.VUE_APP_API_LINK}/api/links/getlinks/${this.searchQuery}`)
                .then((response) => {
                this.links['results'] = response.data;
            }).catch(() => {
                this.links = null;
            }).finally(() => {
                this.searching = false;
            });
        },
    },
}
</script>