<template>
    <div class="main container">
        <div class="bg-light p-3 mb-3">
            <h3>Find your link</h3>
            <p>Please type in your name to search for your link</p>
        </div>
        <LinkList />
    </div>
</template>

<script>
    import LinkList from '@/components/GetLinks/LinkList.vue'
    export default {
        name: 'Base',
        components: {
            LinkList,
        },
    }
</script>