import { createRouter, createWebHistory } from 'vue-router'
import WizardPage from '../pages/WizardPage.vue'

const routes = [
    { path: '/wizard', name: 'Wizard', component: WizardPage },
    { path: '/', redirect: '/wizard' }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router
