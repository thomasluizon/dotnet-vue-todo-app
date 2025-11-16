<script setup lang="ts">
import TodoListView from './views/TodoListView.vue'
</script>

<template>
  <div id="app-container">
    <TodoListView />
  </div>
</template>

<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
  background-color: #f5f5f5;
  color: #333;
  line-height: 1.6;
}

#app-container {
  min-height: 100vh;
  padding: 20px;
}
</style>
