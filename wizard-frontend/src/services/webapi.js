import axios from 'axios'

const webapi = axios.create({
    baseURL: 'http://localhost:5128'
})

export const fetchEmployees = (query) =>
    webapi.get('/Employees', { params: { search: query } })

export const uploadDocuments = (projectId, formData) =>
    webapi.post(`/Projects/${projectId}/documents`, formData, {
        headers: { 'Content-Type': 'multipart/form-data' }
    })

export const createProject = (data) => webapi.post('/Projects', data)
export const addEmployee   = (projectId, employeeId) =>
    webapi.post(`/Projects/${projectId}/add-employee/${employeeId}`)

export const getAllEmployees = () => webapi.get('/Employees')

export default webapi