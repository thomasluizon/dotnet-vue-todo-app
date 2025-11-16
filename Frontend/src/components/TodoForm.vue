<script setup lang="ts">
import { ref } from 'vue'

const title = ref<string>('')
const description = ref<string>('')

const emit = defineEmits<{
  (e: 'submitTask', payload: { title: string; description: string }): void
}>()

function handleSubmit() {
  if (!title.value.trim()) {
    return
  }

  emit('submitTask', {
    title: title.value,
    description: description.value
  })

  title.value = ''
  description.value = ''
}
</script>

<template>
  <form @submit.prevent="handleSubmit" class="todo-form">
    <div class="form-group">
      <label for="title">Title</label>
      <input
        id="title"
        v-model="title"
        type="text"
        placeholder="Enter task title"
        required
        class="form-input"
      />
    </div>
    <div class="form-group">
      <label for="description">Description (optional)</label>
      <textarea
        id="description"
        v-model="description"
        placeholder="Enter task description"
        rows="3"
        class="form-input"
      />
    </div>
    <button type="submit" class="submit-button">
      Add Task
    </button>
  </form>
</template>

<style scoped>
.todo-form {
  background-color: #f8f9fa;
  padding: 20px;
  border-radius: 8px;
  margin-bottom: 24px;
}

.form-group {
  margin-bottom: 16px;
}

.form-group label {
  display: block;
  margin-bottom: 6px;
  font-weight: 600;
  color: #333;
  font-size: 14px;
}

.form-input {
  width: 100%;
  padding: 10px 12px;
  border: 1px solid #ced4da;
  border-radius: 4px;
  font-size: 14px;
  font-family: inherit;
  box-sizing: border-box;
}

.form-input:focus {
  outline: none;
  border-color: #007bff;
  box-shadow: 0 0 0 3px rgba(0, 123, 255, 0.1);
}

textarea.form-input {
  resize: vertical;
}

.submit-button {
  width: 100%;
  padding: 12px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
  font-weight: 600;
}

.submit-button:hover {
  background-color: #0056b3;
}
</style>
