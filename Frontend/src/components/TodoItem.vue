<script setup lang="ts">
import type { TodoItem } from '../types/TodoItem'

const props = defineProps<{
  task: TodoItem
}>()

const emit = defineEmits<{
  (e: 'toggleComplete', payload: TodoItem): void
  (e: 'deleteTask', id: number): void
}>()

function handleToggle() {
  emit('toggleComplete', {
    ...props.task,
    isCompleted: !props.task.isCompleted
  })
}

function handleDelete() {
  emit('deleteTask', props.task.id)
}
</script>

<template>
  <div class="todo-item">
    <input
      type="checkbox"
      :checked="task.isCompleted"
      @change="handleToggle"
      class="todo-checkbox"
    />
    <div class="todo-content">
      <h3 class="todo-title" :class="{ completed: task.isCompleted }">
        {{ task.title }}
      </h3>
      <p v-if="task.description" class="todo-description">
        {{ task.description }}
      </p>
    </div>
    <button @click="handleDelete" class="delete-button">
      Delete
    </button>
  </div>
</template>

<style scoped>
.todo-item {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 16px;
  border: 1px solid #e0e0e0;
  border-radius: 8px;
  background-color: #fff;
  margin-bottom: 12px;
}

.todo-checkbox {
  width: 20px;
  height: 20px;
  cursor: pointer;
}

.todo-content {
  flex: 1;
}

.todo-title {
  margin: 0;
  font-size: 18px;
  font-weight: 600;
  color: #333;
}

.todo-title.completed {
  text-decoration: line-through;
  color: #999;
}

.todo-description {
  margin: 4px 0 0 0;
  font-size: 14px;
  color: #666;
}

.delete-button {
  padding: 8px 16px;
  background-color: #dc3545;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 14px;
  font-weight: 500;
}

.delete-button:hover {
  background-color: #c82333;
}
</style>
