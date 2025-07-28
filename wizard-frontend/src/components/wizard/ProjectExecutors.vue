<template>
  <div class="wizard-step">
    <h2 class="step-title">Шаг 4: Исполнители</h2>

    <div class="form-group">
      <label>Поиск сотрудников</label>
      <input type="text" v-model="searchQuery" placeholder="Начните вводить..." />
    </div>

    <div v-if="filteredEmployees.length" class="dropdown">
      <div
          v-for="emp in filteredEmployees"
          :key="emp.id"
          class="dropdown-item"
          @click="toggleExecutor(emp)"
      >
        <span>{{ emp.surname }} {{ emp.name }}</span>
        <span v-if="executors.find(e => e.id === emp.id)">✓</span>
      </div>
    </div>

    <div v-if="executors.length" class="selected-list">
      <p>Выбранные исполнители:</p>
      <ul>
        <li v-for="emp in executors" :key="emp.id">
          {{ emp.surname }} {{ emp.name }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useWizardStore } from '@/store/wizardStore'
import { getAllEmployees } from '@/services/webapi'

const store       = useWizardStore()
const employees   = ref([])
const searchQuery = ref('')
const executors   = ref([])

onMounted(async () => {
  const { data } = await getAllEmployees()
  employees.value  = data
  executors.value  = data.filter(e => store.executors.includes(e.id))
})

const filteredEmployees = computed(() => {
  if (!searchQuery.value.trim()) return employees.value
  const q = searchQuery.value.trim().toLowerCase()
  return employees.value.filter(
      e =>
          e.name.toLowerCase().includes(q) ||
          e.surname.toLowerCase().includes(q)
  )
})

function toggleExecutor (emp) {
  const idx = executors.value.findIndex(e => e.id === emp.id)
  if (idx !== -1) {
    executors.value.splice(idx, 1)
  } else {
    executors.value.push(emp)
  }
  store.executors = executors.value.map(e => e.id)
}
</script>

<style scoped>
.dropdown {
  border: 1px solid #ccc;
  border-radius: 4px;
  max-height: 150px;
  overflow-y: auto;
  margin-top: 6px;
}
.dropdown-item {
  padding: 6px 8px;
  cursor: pointer;
}
.dropdown-item:hover {
  background: #f0f0f0;
}
.selected-list {
  margin-top: 10px;
}
</style>