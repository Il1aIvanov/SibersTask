<template>
  <div class="wizard-step">
    <h2 class="step-title">Шаг 3: Руководитель проекта</h2>

    <input
        type="text"
        v-model="searchQuery"
        placeholder="Введите имя или фамилию"
        class="form-group"
    />

    <div v-if="filteredEmployees.length" class="dropdown">
      <div
          v-for="emp in filteredEmployees"
          :key="emp.id"
          class="dropdown-item"
          @click="selectManager(emp.id)"
          :class="{ selected: emp.id === store.projectManagerId }"
      >
        {{ emp.surname }} {{ emp.name }}
      </div>
    </div>

    <p v-if="store.projectManagerId">
      Выбран руководитель: {{ selectedManagerName }}
    </p>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useWizardStore } from '@/store/wizardStore'
import { getAllEmployees } from '@/services/webapi'

const store       = useWizardStore()
const employees   = ref([])
const searchQuery = ref('')

onMounted(async () => {
  const { data } = await getAllEmployees()
  employees.value = data
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

function selectManager (id) { store.projectManagerId = id }

const selectedManagerName = computed(() => {
  const emp = employees.value.find(e => e.id === store.projectManagerId)
  return emp ? `${emp.surname} ${emp.name}` : ''
})
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
.dropdown-item.selected {
  background: #d0ebff;
  border-color: #74c0fc;
}
</style>
