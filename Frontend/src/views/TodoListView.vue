<script setup lang="ts">
import { onMounted } from 'vue'
import { useTodoApi } from '../composables/useTodoApi'
import TodoForm from '../components/TodoForm.vue'
import TodoItem from '../components/TodoItem.vue'

const { tasks, loading, fetchTasks, createTask, updateTask, deleteTask } = useTodoApi()

onMounted(() => {
  fetchTasks()
})

function handleSubmitTask(payload: { title: string; description: string }) {
  createTask(payload)
}

function handleToggleComplete(task: any) {
  updateTask(task)
}

function handleDeleteTask(id: number) {
  deleteTask(id)
}
</script>

<template>
  <div class="todo-list-view">
    <header class="header">
      <h1>My Todo List</h1>
    </header>

    <TodoForm @submitTask="handleSubmitTask" />

    <div v-if="loading" class="loading">
      Loading...
    </div>

    <div v-else-if="tasks.length === 0" class="empty-state">
      No tasks found.
    </div>

    <div v-else class="todo-list">
      <TodoItem
        v-for="task in tasks"
        :key="task.id"
        :task="task"
        @toggleComplete="handleToggleComplete"
        @deleteTask="handleDeleteTask"
      />
    </div>
  </div>
</template>

<style scoped>
.todo-list-view {
  max-width: 800px;
  margin: 0 auto;
  padding: 40px 20px;
}

.header {
  margin-bottom: 32px;
}

.header h1 {
  font-size: 32px;
  font-weight: 700;
  color: #333;
  margin: 0;
}

.loading,
.empty-state {
  text-align: center;
  padding: 40px;
  font-size: 18px;
  color: #666;
}

.todo-list {
  margin-top: 24px;
}
</style>
