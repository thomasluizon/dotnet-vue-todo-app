import { ref } from 'vue'
import type { TodoItem } from '../types/TodoItem'

export function useTodoApi() {
  const baseUrl = import.meta.env.VITE_API_BASE_URL + '/api/tasks'

  const tasks = ref<TodoItem[]>([])
  const loading = ref(false)

  async function fetchTasks() {
    try {
      loading.value = true
      const response = await fetch(baseUrl)
      if (!response.ok) {
        throw new Error('Failed to fetch tasks')
      }
      tasks.value = await response.json()
    } catch (error) {
      console.error('Error fetching tasks:', error)
    } finally {
      loading.value = false
    }
  }

  async function createTask(payload: { title: string; description?: string }) {
    try {
      const response = await fetch(baseUrl, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(payload)
      })
      if (!response.ok) {
        throw new Error('Failed to create task')
      }
      await fetchTasks()
    } catch (error) {
      console.error('Error creating task:', error)
    }
  }

  async function updateTask(task: TodoItem) {
    try {
      const response = await fetch(`${baseUrl}/${task.id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(task)
      })
      if (!response.ok) {
        throw new Error('Failed to update task')
      }
      await fetchTasks()
    } catch (error) {
      console.error('Error updating task:', error)
    }
  }

  async function deleteTask(id: number) {
    try {
      const response = await fetch(`${baseUrl}/${id}`, {
        method: 'DELETE'
      })
      if (!response.ok) {
        throw new Error('Failed to delete task')
      }
      await fetchTasks()
    } catch (error) {
      console.error('Error deleting task:', error)
    }
  }

  return {
    tasks,
    loading,
    fetchTasks,
    createTask,
    updateTask,
    deleteTask
  }
}
