using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services;

internal class ProjectService(
    IProjectRepository projectRepository,
    IEmployeeRepository employeeRepository) 
    : IProjectService
{
    public async Task<IEnumerable<Project>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await projectRepository.GetAllAsync(cancellationToken);
    }

    public async Task<ProjectDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var project = await projectRepository.GetByIdAsync(id,cancellationToken);
        if (project is null) 
            throw new NullReferenceException("Project not found");
        return ProjectDto.FromEntity(project);
    }

    public async Task<int> CreateAsync(ProjectCreateDto dto, CancellationToken cancellationToken = default)
    {
        var project = new Project
        {
            ProjectName = dto.ProjectName,
            CustomerCompanyName = dto.CustomerCompanyName,
            ExecutorCompanyName = dto.ExecutorCompanyName,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Priority = dto.Priority,
            ProjectManagerId = dto.ProjectManagerId
        };
        await projectRepository.CreateAsync(project, cancellationToken);
        return project.Id;
    }

    public async Task UpdateAsync(ProjectUpdateDto dto, CancellationToken cancellationToken = default)
    {
        var project = await projectRepository.GetByIdAsync(dto.Id, cancellationToken);
        if (project is null)
            throw new NullReferenceException("Project not found");
        project.ProjectName = dto.ProjectName;
        project.CustomerCompanyName = dto.CustomerCompanyName;
        project.ExecutorCompanyName = dto.ExecutorCompanyName;
        project.StartDate = dto.StartDate;
        project.EndDate = dto.EndDate;
        project.Priority = dto.Priority;
        project.ProjectManagerId = dto.ProjectManagerId;

        await projectRepository.UpdateAsync(project, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var project = await projectRepository.GetByIdAsync(id, cancellationToken);
        if (project != null)
        {
            await projectRepository.DeleteAsync(project, cancellationToken);
        }
    }

    public async Task AddEmployeeAsync(int projectId, int employeeId, CancellationToken cancellationToken = default)
    {
        var project = await projectRepository.GetByIdAsync(projectId, cancellationToken);
        var employee = await employeeRepository.GetByIdAsync(employeeId, cancellationToken);

        if (project != null && employee != null && !project.Employees!.Contains(employee))
        {
            project.Employees!.Add(employee);
            await projectRepository.UpdateAsync(project, cancellationToken);
        }
    }

    public async Task RemoveEmployeeAsync(int projectId, int employeeId, CancellationToken cancellationToken = default)
    {
        var project = await projectRepository.GetByIdAsync(projectId, cancellationToken);
        var employee = await employeeRepository.GetByIdAsync(employeeId, cancellationToken);

        if (project != null && employee != null && project.Employees!.Contains(employee))
        {
            project.Employees!.Remove(employee);
            await projectRepository.UpdateAsync(project, cancellationToken);
        }
    }

    public async Task<IEnumerable<ProjectDto>> FilterAsync(ProjectFilterDto filterDto, CancellationToken cancellationToken = default)
    {
        var query = projectRepository.Query();

        query = ApplyFilters(query, filterDto);
        query = ApplySorting(query, filterDto);

        var projects = await query.ToListAsync(cancellationToken);
        return projects.Select(ProjectDto.FromEntity);
    }
    
    private IQueryable<Project> ApplyFilters(IQueryable<Project> query, ProjectFilterDto filter)
    {
        if (filter.StartDateFrom.HasValue)
            query = query.Where(p => p.StartDate >= filter.StartDateFrom.Value);
    
        if (filter.StartDateTo.HasValue)
            query = query.Where(p => p.StartDate <= filter.StartDateTo.Value);
    
        if (filter.MinPriority.HasValue)
            query = query.Where(p => p.Priority >= filter.MinPriority.Value);
    
        if (filter.MaxPriority.HasValue)
            query = query.Where(p => p.Priority <= filter.MaxPriority.Value);

        if (!string.IsNullOrWhiteSpace(filter.ProjectNameContains))
            query = query.Where(p => p.ProjectName!.Contains(filter.ProjectNameContains));

        return query;
    }

    private IQueryable<Project> ApplySorting(IQueryable<Project> query, ProjectFilterDto filter)
    {
        if (string.IsNullOrWhiteSpace(filter.OrderBy))
            return query;

        return filter.OrderBy switch
        {
            "StartDate" => filter.OrderDescending 
                ? query.OrderByDescending(p => p.StartDate) 
                : query.OrderBy(p => p.StartDate),

            "Priority" => filter.OrderDescending 
                ? query.OrderByDescending(p => p.Priority) 
                : query.OrderBy(p => p.Priority),

            "ProjectName" => filter.OrderDescending 
                ? query.OrderByDescending(p => p.ProjectName) 
                : query.OrderBy(p => p.ProjectName),

            _ => query // fallback без сортировки
        };
    }
}