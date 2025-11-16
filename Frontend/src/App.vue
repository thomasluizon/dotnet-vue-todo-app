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

html,
body {
  font-family: system-ui, -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  background-attachment: fixed;
  color: #333;
  line-height: 1.6;
  min-height: 100vh;
}

#app-container {
  max-width: 700px;
  margin: 2rem auto;
  padding: 0 1rem;
}

@media (max-width: 768px) {
  #app-container {
    margin: 1rem auto;
    padding: 0 0.5rem;
  }
}
</style>
