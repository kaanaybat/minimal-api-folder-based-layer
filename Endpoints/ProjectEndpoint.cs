using AutoMapper;
using Core;
using Core.Dto.Contracts;
using Core.Dtos;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Core.Service;
using MinimalApi.Service.Extensions;

namespace MinimalApi.Endpoints
{
    public static class ProjectEndpoint {

        public static void RegisterProjectEndpoint(this WebApplication app)
        {
            app.MapGet("api/project/all", async (IProjectService repo, IMapper mapper) => {
                var projects = await repo.GetAllAsync();
                return Results.Ok(CustomResponseDto<IEnumerable<ProjectDto>>.Success(projects));
            });

            app.MapGet("api/project/category", async (IProjectService repo, IMapper mapper) => {
                var projects = await repo.GetProjectsWithCategoryAsync();
                return Results.Ok(CustomResponseDto<List<ProjectWithCategoryDto>>.Success(projects));
            });

            app.MapGet("api/project/{id}", async (
                IConfiguration configuration,
                IProjectService repo, 
                IMapper mapper,
                int id,
                [FromHeader(Name = "x-api-key")] string? key) => {   

                if(configuration.GetSection("ApiKey").Value != key)
                    return Results.Unauthorized();
                 
                var project = await repo.GetByIdAsync(id);
                if (project != null)
                {
                    return Results.Ok(CustomResponseDto<ProjectDto>.Success(mapper.Map<ProjectDto>(project)));
                }
                return Results.NotFound();
            });

            app.MapPost("api/project", async (IValidator<ProjectCreateDto> validator,IProjectService repo, IMapper mapper, ProjectCreateDto projectCreateDto) => {
                
                ValidationResult validationResult = await validator.ValidateAsync(projectCreateDto);

                if (!validationResult.IsValid) 
                {
                    return Results.BadRequest(CustomResponseDto<ProjectDto>.Fail(validationResult.ToList()));
                }

                var model = mapper.Map<Project>(projectCreateDto);

                await repo.AddAsync(model);
                await repo.SaveChangesAsync();

                var projectDto = mapper.Map<ProjectDto>(model);

                // return Results.Created($"api/project/{projectDto.Id}", projectDto);
                return Results.Created($"api/project/{projectDto.Id}",CustomResponseDto<ProjectDto>.Success(projectDto));

            });

            app.MapPut("api/project/{id}", async (IProjectService repo, IMapper mapper, int id, ProjectUpdateDto projectUpdateDto) => {
                
                var project = await repo.GetByIdAsync(id);

                if (project == null)
                {
                    return Results.NotFound();
                }
                
                mapper.Map(projectUpdateDto, project);

                await repo.SaveChangesAsync();

                return Results.NoContent();

            });

            app.MapDelete("api/project/{id}", async (IProjectService repo, IMapper mapper, int id) => {
                var project = await repo.GetByIdAsync(id);
                if (project == null)
                {
                    return Results.NotFound();
                }

                repo.Remove(project);

                await repo.SaveChangesAsync();

                return Results.NoContent();

            });

        }

    }

}