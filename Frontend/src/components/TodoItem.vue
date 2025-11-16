<script setup lang="ts">
import { ref } from 'vue'
import type { TodoItem } from '../types/TodoItem'

const props = defineProps<{
  task: TodoItem
}>()

const emit = defineEmits<{
  (e: 'toggleComplete', payload: TodoItem): void
  (e: 'deleteTask', id: number): void
  (e: 'saveTask', payload: TodoItem): void
}>()

const isEditing = ref(false)
const editTitle = ref(props.task.title)
const editDescription = ref(props.task.description || '')

function handleToggle() {
  emit('toggleComplete', {
    ...props.task,
    isCompleted: !props.task.isCompleted
  })
}

function handleDelete() {
  emit('deleteTask', props.task.id)
}

function startEdit() {
  editTitle.value = props.task.title
  editDescription.value = props.task.description || ''
  isEditing.value = true
}

function handleSave() {
  if (!editTitle.value.trim()) {
    return
  }

  emit('saveTask', {
    ...props.task,
    title: editTitle.value,
    description: editDescription.value
  })

  isEditing.value = false
}

function cancelEdit() {
  isEditing.value = false
}
</script>

<template>
  <div class="todo-item">
    <input
      v-if="!isEditing"
      type="checkbox"
      :checked="task.isCompleted"
      @change="handleToggle"
      class="todo-checkbox"
    />

    <div v-if="!isEditing" class="todo-content">
      <h3 class="todo-title" :class="{ completed: task.isCompleted }">
        {{ task.title }}
      </h3>
      <p v-if="task.description" class="todo-description">
        {{ task.description }}
      </p>
    </div>

    <div v-else class="edit-form">
      <div class="edit-group">
        <input
          v-model="editTitle"
          type="text"
          placeholder="Task title"
          class="edit-input"
        />
      </div>
      <div class="edit-group">
        <textarea
          v-model="editDescription"
          placeholder="Task description (optional)"
          rows="2"
          class="edit-input"
        />
      </div>
    </div>

    <div v-if="!isEditing" class="button-group">
      <button @click="startEdit" class="edit-button">
        Edit
      </button>
      <button @click="handleDelete" class="delete-button">
        Delete
      </button>
    </div>

    <div v-else class="button-group">
      <button @click="handleSave" class="save-button">
        Save
      </button>
      <button @click="cancelEdit" class="cancel-button">
        Cancel
      </button>
    </div>
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
  flex-shrink: 0;
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

.edit-form {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.edit-group {
  display: flex;
  flex-direction: column;
}

.edit-input {
  width: 100%;
  padding: 8px 10px;
  border: 1px solid #ced4da;
  border-radius: 4px;
  font-size: 14px;
  font-family: inherit;
}

.edit-input:focus {
  outline: none;
  border-color: #007bff;
  box-shadow: 0 0 0 2px rgba(0, 123, 255, 0.1);
}

textarea.edit-input {
  resize: vertical;
}

.button-group {
  display: flex;
  gap: 8px;
  flex-shrink: 0;
}

.edit-button,
.save-button,
.cancel-button,
.delete-button {
  padding: 8px 16px;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 14px;
  font-weight: 500;
}

.edit-button {
  background-color: #6c757d;
}

.edit-button:hover {
  background-color: #5a6268;
}

.save-button {
  background-color: #28a745;
}

.save-button:hover {
  background-color: #218838;
}

.cancel-button {
  background-color: #6c757d;
}

.cancel-button:hover {
  background-color: #5a6268;
}

.delete-button {
  background-color: #dc3545;
}

.delete-button:hover {
  background-color: #c82333;
}
</style>
