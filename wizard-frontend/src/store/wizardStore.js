import { defineStore } from 'pinia'

export const useWizardStore = defineStore('wizard', {
    state: () => ({
        projectName: '',
        startDate: '',
        endDate: '',
        priority: 1,
        customerCompanyName: '',
        executorCompanyName: '',
        projectManagerId: null,
        executors: [],
        documents: [],
        projectId: null
    }),
    actions: {
        setField(field, value) {
            this[field] = value
        },
        setProjectId(id) {
            this.projectId = id
        }
    }
})