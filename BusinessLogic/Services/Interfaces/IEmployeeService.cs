using BusinessLogic.DTOs;
using DataAccess.Models;

namespace BusinessLogic.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> CreateAsync(EmployeeCreateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(EmployeeUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}