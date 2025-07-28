<template>
  <div class="wizard-step">
    <h2 class="step-title">Шаг 2: Компании</h2>

    <div class="form-group">
      <label>Наименование компании-заказчика</label>
      <input
          type="text"
          v-model="store.customerCompanyName"
          placeholder="Введите название компании-заказчика"
      />
    </div>

    <div class="form-group">
      <label>Наименование компании-исполнителя</label>
      <input
          type="text"
          v-model="store.executorCompanyName"
          placeholder="Введите название компании-исполнителя"
      />
    </div>
  </div>
</template>

<script setup>
import { useWizardStore } from '@/store/wizardStore'
const store = useWizardStore()
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

input[type="text"] {
  padding: 8px 10px;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  outline: none;
  transition: border-color 0.2s;
}

input[type="text"]:focus {
  border-color: #3b82f6;
}
</style>