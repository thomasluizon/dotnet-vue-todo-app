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
    <div class="form-row">
      <div class="form-group">
        <label for="title">Title</label>
        <input
          id="title"
          v-model="title"
          type="text"
          placeholder="What needs to be done?"
          required
          class="form-input"
        />
      </div>
      <div class="form-group">
        <label for="description">Description</label>
        <textarea
          id="description"
          v-model="description"
          placeholder="Add more details (optional)"
          rows="3"
          class="form-input"
        />
      </div>
    </div>
    <button type="submit" class="submit-button">
      + Add Task
    </button>
  </form>
</template>

<style scoped>
.todo-form {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  padding: 1.5rem;
  border-radius: 8px;
  margin-bottom: 2rem;
  box-shadow: 0 4px 15px rgba(102, 126, 234, 0.4);
}

.form-row {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  margin-bottom: 1rem;
}

.form-group {
  display: flex;
  flex-direction: column;
}

.form-group label {
  margin-bottom: 0.5rem;
  font-weight: 600;
  color: #fff;
  font-size: 14px;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

.form-input {
  width: 100%;
  padding: 0.75rem;
  border: 2px solid transparent;
  border-radius: 6px;
  font-size: 15px;
  font-family: inherit;
  box-sizing: border-box;
  background: #fff;
  transition: all 0.3s ease;
}

.form-input:focus {
  outline: none;
  border-color: #667eea;
  box-shadow: 0 0 0 3px rgba(102, 126, 234, 0.2);
  transform: translateY(-1px);
}

.form-input::placeholder {
  color: #999;
}

textarea.form-input {
  resize: vertical;
  min-height: 80px;
}

.submit-button {
  width: 100%;
  padding: 0.875rem;
  background: #fff;
  color: #667eea;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-size: 16px;
  font-weight: 700;
  text-transform: uppercase;
  letter-spacing: 0.5px;
  transition: all 0.3s ease;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

.submit-button:hover {
  transform: translateY(-2px);
  box-shadow: 0 6px 20px rgba(0, 0, 0, 0.15);
}

.submit-button:active {
  transform: translateY(0);
}
</style>
