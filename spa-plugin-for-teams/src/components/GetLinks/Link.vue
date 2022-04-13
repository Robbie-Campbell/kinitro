<template>
    <div class="singleLink p-2 bg-light">
        <p>{{this.link.name}}: <a :href="this.link.link">Click here!</a></p>
    </div>
</template>

<script>
export default {
    name: 'Link',
    props: ['link'],
}
</script>