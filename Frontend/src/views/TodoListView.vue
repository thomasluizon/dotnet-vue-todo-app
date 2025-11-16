<script setup lang="ts">
import { onMounted } from 'vue'
import { useTodoApi } from '../composables/useTodoApi'
import TodoForm from '../components/TodoForm.vue'
import TodoItem from '../components/TodoItem.vue'
import type { TodoItem as TodoItemType } from '../types/TodoItem'

const { tasks, loading, fetchTasks, createTask, updateTask, deleteTask } = useTodoApi()

onMounted(() => {
  fetchTasks()
})

function handleSubmitTask(payload: { title: string; description: string }) {
  createTask(payload)
}

function handleToggleComplete(task: TodoItemType) {
  updateTask(task)
}

function handleDeleteTask(id: number) {
  deleteTask(id)
}

function handleSaveTask(task: TodoItemType) {
  updateTask(task)
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
        @saveTask="handleSaveTask"
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
  color: #fff;
  margin: 0;
  text-align: center;
}

.loading,
.empty-state {
  text-align: center;
  padding: 40px;
  font-size: 18px;
  color: #fff;
  background: rgba(255, 255, 255, 0.1);
  border-radius: 8px;
}

.todo-list {
  margin-top: 24px;
}
</style>
