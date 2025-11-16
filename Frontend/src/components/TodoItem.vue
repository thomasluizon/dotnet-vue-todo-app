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
  <div class="todo-item" :class="{ editing: isEditing }">
    <div class="todo-main">
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
    </div>

    <div class="todo-actions">
      <div v-if="!isEditing" class="button-group">
        <button @click="startEdit" class="btn btn-edit">
          ✏️ Edit
        </button>
        <button @click="handleDelete" class="btn btn-delete">
          🗑️ Delete
        </button>
      </div>

      <div v-else class="button-group button-group-editing">
        <button @click="handleSave" class="btn btn-save">
          ✓ Save
        </button>
        <button @click="cancelEdit" class="btn btn-cancel">
          ✕ Cancel
        </button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.todo-item {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 1rem;
  background: #fff;
  border-radius: 8px;
  margin-bottom: 0.75rem;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  transition: all 0.3s ease;
}

.todo-item:hover {
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
  transform: translateY(-1px);
}

.todo-item.editing {
  background: #f8f9ff;
  box-shadow: 0 4px 16px rgba(102, 126, 234, 0.2);
}

.todo-main {
  display: flex;
  align-items: flex-start;
  gap: 1rem;
  flex: 1;
}

.todo-checkbox {
  width: 22px;
  height: 22px;
  cursor: pointer;
  flex-shrink: 0;
  margin-top: 2px;
  accent-color: #667eea;
}

.todo-content {
  flex: 1;
}

.todo-title {
  margin: 0 0 0.25rem 0;
  font-size: 17px;
  font-weight: 600;
  color: #2d3748;
  line-height: 1.4;
}

.todo-title.completed {
  text-decoration: line-through;
  color: #a0aec0;
  opacity: 0.7;
}

.todo-description {
  margin: 0;
  font-size: 14px;
  color: #718096;
  line-height: 1.5;
}

.edit-form {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
}

.edit-group {
  display: flex;
  flex-direction: column;
}

.edit-input {
  width: 100%;
  padding: 0.625rem;
  border: 2px solid #e2e8f0;
  border-radius: 6px;
  font-size: 14px;
  font-family: inherit;
  transition: all 0.2s ease;
}

.edit-input:focus {
  outline: none;
  border-color: #667eea;
  box-shadow: 0 0 0 3px rgba(102, 126, 234, 0.1);
}

textarea.edit-input {
  resize: none;
}

.todo-actions {
  flex-shrink: 0;
}

.button-group {
  display: flex;
  gap: 0.5rem;
  align-items: center;
}

.button-group-editing {
  flex-direction: column;
}

.btn {
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  font-size: 13px;
  font-weight: 600;
  transition: all 0.2s ease;
  white-space: nowrap;
}

.btn-edit {
  background: #f7fafc;
  color: #4a5568;
  border: 1px solid #e2e8f0;
}

.btn-edit:hover {
  background: #edf2f7;
  transform: translateY(-1px);
}

.btn-delete {
  background: #fff5f5;
  color: #e53e3e;
  border: 1px solid #feb2b2;
}

.btn-delete:hover {
  background: #fed7d7;
  transform: translateY(-1px);
}

.btn-save {
  background: linear-gradient(135deg, #48bb78 0%, #38a169 100%);
  color: white;
}

.btn-save:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(72, 187, 120, 0.4);
}

.btn-cancel {
  background: #edf2f7;
  color: #4a5568;
}

.btn-cancel:hover {
  background: #e2e8f0;
  transform: translateY(-1px);
}

@media (max-width: 640px) {
  .todo-item {
    flex-direction: column;
    align-items: flex-start;
    gap: 1rem;
  }

  .todo-actions {
    width: 100%;
  }

  .button-group {
    width: 100%;
    justify-content: flex-end;
  }
}
</style>
