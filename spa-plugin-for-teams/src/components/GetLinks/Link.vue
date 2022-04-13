<template>
    <div class="singleLink p-2 bg-light">
        <h1>{{this.link.name}}: {{this.link.link}}</h1>
    </div>
</template>

<script>
export default {
    name: 'Link',
    props: ['link'],
}
</script>