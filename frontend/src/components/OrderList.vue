<script setup lang="ts">
import { ref } from 'vue'

interface MenuItem {
  id: number
  name: string
  price: number
}

const items = ref<MenuItem[]>([
  { id: 1, name: 'Pizza Margherita', price: 8.5 },
  { id: 2, name: 'Cheeseburger', price: 5.75 },
  { id: 3, name: 'Sushi', price: 12.0 }
])

function order(item: MenuItem) {
  alert(`Ordered ${item.name}`)
}
</script>

<template>
  <ul>
    <li v-for="item in items" :key="item.id">
      {{ item.name }} - €{{ item.price.toFixed(2) }}
      <button @click="order(item)">Order</button>
    </li>
  </ul>
</template>

<style scoped>
ul {
  list-style: none;
  padding: 0;
}
li {
  margin: 0.5rem 0;
}
button {
  margin-left: 1rem;
}
</style>
