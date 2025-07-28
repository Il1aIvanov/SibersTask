<template>
  <div>
    <h1>Создание проекта</h1>
    <component :is="steps[currentStep]" :key="currentStep" />

    <div>
      <button class="btn btn-outline" @click="prevStep" :disabled="currentStep === 0">Назад</button>
      <button v-if="currentStep < steps.length - 1" class="btn" @click="nextStep">Далее</button>
      <button v-else class="btn" @click="submit">Создать проект</button>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import ProjectInfo      from '../components/wizard/ProjectInfo.vue'
import ProjectCompanies from '../components/wizard/ProjectCompanies.vue'
import ProjectManager   from '../components/wizard/ProjectManager.vue'
import ProjectExecutors from '../components/wizard/ProjectExecutors.vue'
import ProjectDocuments from '../components/wizard/ProjectDocuments.vue'
import { useWizardStore } from '@/store/wizardStore'
import { createProject, addEmployee } from '@/services/webapi'

const steps = [
  ProjectInfo,
  ProjectCompanies,
  ProjectManager,
  ProjectExecutors,
  ProjectDocuments
]

const currentStep = ref(0)
const store       = useWizardStore()

function nextStep()  { currentStep.value++ }
function prevStep()  { currentStep.value-- }

function mapStoreToDto() {
  return {
    projectName:           store.projectName,
    customerCompanyName:   store.customerCompanyName,
    executorCompanyName:   store.executorCompanyName,
    startDate:  store.startDate,
    endDate:    store.endDate,
    priority:   Number(store.priority),
    projectManagerId: Number(store.projectManagerId)
  }
}

async function submit() {
  if (!store.projectName || !store.startDate || !store.endDate) {
    alert('Пожалуйста, заполните все обязательные поля')
    return
  }

  const payload = mapStoreToDto()

  try {
    const { data } = await createProject(payload)
    store.setProjectId(data.id)

    await Promise.all(
        store.executors.map(id => addEmployee(store.projectId, id))
    )

    alert('Проект и исполнители успешно сохранены!')
  } catch (error) {
    console.error('Ошибка при создании проекта:', error.response?.data || error.message)
    alert('Ошибка создания проекта')
  }
}
</script>