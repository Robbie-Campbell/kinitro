<template>
    <div class="main container">
        <div class="bg-light p-3 mb-3">
            <h3>Find your link</h3>
            <p>You should be able to find your link below, if not please press the refresh button.</p>
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