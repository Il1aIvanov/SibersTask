<template>
  <div class="wizard-step">
    <h2 class="step-title">Шаг 5: Документы</h2>
    <div class="dropzone"
         @drop="handleDrop"
         @dragover="handleDragOver">
      Перетащите файлы сюда
    </div>

    <ul v-if="files.length" class="file-list">
      <li v-for="(file, i) in files" :key="i">{{ file.name }}</li>
    </ul>

    <p v-if="!store.projectId" class="hint">
      Сначала завершите предыдущие шаги и нажмите
      <strong>«Создать&nbsp;проект»</strong>. После этого загрузка
      документов станет доступна.
    </p>

    <button
        v-if="files.length"
        class="btn btn-primary"
        @click="upload"
        :disabled="!store.projectId">
      Загрузить файлы
    </button>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useWizardStore } from '@/store/wizardStore'
import { uploadDocuments } from '@/services/webapi'

const store = useWizardStore()
const files = ref([])

function handleDrop (e) {
  e.preventDefault()
  files.value.push(...Array.from(e.dataTransfer.files))
  store.documents = files.value
}

function handleDragOver (e) { e.preventDefault() }

async function upload () {
  if (!store.projectId) return

  const formData = new FormData()
  files.value.forEach(f => formData.append('files', f))

  try {
    await uploadDocuments(store.projectId, formData)
    alert('Файлы загружены')
  } catch (err) {
    console.error('Ошибка загрузки файлов:', err.response?.data || err.message)
    alert('Ошибка загрузки файлов')
  }
}
</script>

<style scoped>
.dropzone {
  border: 2px dashed #ccc;
  border-radius: 6px;
  padding: 20px;
  text-align: center;
  color: #888;
  cursor: pointer;
  margin-bottom: 15px;
}
.dropzone:hover {
  border-color: #3b82f6;
  color: #3b82f6;
}

.file-list { margin-bottom: 15px; text-align: left; }

.hint {
  color: #e07a00; 
  font-size: 0.9rem;
  margin-bottom: 10px;
}
</style>
