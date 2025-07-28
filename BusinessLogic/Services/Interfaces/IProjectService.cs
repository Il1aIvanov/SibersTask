using BusinessLogic.DTOs;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProjectDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> CreateAsync(ProjectCreateDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(ProjectUpdateDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task AddEmployeeAsync(int projectId, int employeeId, CancellationToken cancellationToken = default);
    Task RemoveEmployeeAsync(int projectId, int employeeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProjectDto>> FilterAsync(ProjectFilterDto filterDto, CancellationToken cancellationToken = default);
    Task UploadDocumentsAsync(int projectId, List<IFormFile> files, CancellationToken cancellationToken = default);
}