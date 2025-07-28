<template>
  <div class="wizard-step">
    <h1 class="step-title">Шаг 1: Информация о проекте</h1>

    <div class="form-group">
      <label>Название проекта</label>
      <input
          type="text"
          v-model="store.projectName"
          placeholder="Введите название проекта"
      />
    </div>

    <div class="form-row">
      <div class="form-group">
        <label>Дата начала</label>
        <input
            type="datetime-local"
            v-model="store.startDate"
        />
      </div>

      <div class="form-group">
        <label>Дата окончания</label>
        <input
            type="datetime-local"
            v-model="store.endDate"
        />
      </div>
    </div>

    <div class="form-group">
      <label>Приоритет проекта (1–10)</label>
      <input
          type="number"
          min="1"
          max="10"
          v-model.number="store.priority"
      />
    </div>

    <p v-if="dateError" class="error">{{ dateError }}</p>
  </div>
</template>

<script setup>
import { computed } from 'vue'
import { useWizardStore } from '../../store/wizardStore'

const store = useWizardStore()

// Валидация: дата окончания >= дата начала
const dateError = computed(() => {
  if (store.startDate && store.endDate) {
    const start = new Date(store.startDate)
    const end = new Date(store.endDate)
    if (end < start) {
      return 'Дата окончания не может быть раньше даты начала'
    }
  }
  return null
})
</script>

<style scoped>
.wizard-step {
  max-width: 500px;
  margin: 0 auto;
  padding: 24px;
  background: #fff;
  border-radius: 6px;
  border: 1px solid #ddd;
}

.step-title {
  font-size: 1.4rem;
  font-weight: 600;
  color: #222;
  margin-bottom: 24px;
}

.form-group {
  display: flex;
  flex-direction: column;
  margin-bottom: 20px;
}

label {
  font-size: 0.9rem;
  color: #555;
  margin-bottom: 6px;
}

input[type="text"],
input[type="datetime-local"],
input[type="number"] {
  padding: 8px 10px;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  outline: none;
  transition: border-color 0.2s;
}

input[type="text"]:focus,
input[type="datetime-local"]:focus,
input[type="number"]:focus {
  border-color: #3b82f6;
}

.form-row {
  display: flex;
  gap: 20px;
}

.form-row .form-group {
  flex: 1;
}

.error {
  color: #e63946;
  font-size: 0.9rem;
}
</style>
