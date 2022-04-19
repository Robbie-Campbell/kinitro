<template>
    <div class="linkDisplay">
        <input class="form-control" type="text" v-model="this.searchQuery" />
        <div v-if="this.links">
            <div v-for="link in this.links" :key="link.name" v-bind="this.links">
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
            links: null,
            searchQuery: null,
            searching: false,
        }
    },
    methods: {
        getCurrentLinks() {
            this.searching = true;
            return axios
            .get(`https://a7f2-194-80-64-241.ngrok.io/api/links/getlinks/${this.searchQuery}`)
                .then((response) => {
                    if (response)
                        this.links = response.data;
                    else
                        this.links = null;
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