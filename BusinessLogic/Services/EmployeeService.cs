using BusinessLogic.DTOs;
using BusinessLogic.Services.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace BusinessLogic.Services;

internal class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
{
    public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await employeeRepository.GetAllAsync(cancellationToken);
    }
    
    public async Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await employeeRepository.GetByIdAsync(id, cancellationToken);
        if (employee == null)
            throw new NullReferenceException();

        return EmployeeDto.FromEntity(employee);
    }

    public async Task<int> CreateAsync(EmployeeCreateDto dto, CancellationToken cancellationToken = default)
    {
        var employee = new Employee()
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Patronymic = dto.Patronymic,
            Email = dto.Email
        };
        await employeeRepository.CreateAsync(employee, cancellationToken);
        return employee.Id;
    }

    public async Task UpdateAsync(EmployeeUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var employee = await employeeRepository.GetByIdAsync(dto.Id, cancellationToken);
        if (employee is null)
            throw new NullReferenceException("Employee not found");
        employee.Name = dto.Name;
        employee.Surname = dto.Surname;
        employee.Patronymic = dto.Patronymic;
        employee.Email = dto.Email;
        await employeeRepository.UpdateAsync(employee, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await employeeRepository.GetByIdAsync(id, cancellationToken);
        if (employee is null)
            throw new NullReferenceException("Employee not found");
        
        await employeeRepository.DeleteAsync(employee, cancellationToken);
    }
}