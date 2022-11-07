using AutoMapper;
using Core;
using Core.Dtos;
using MinimalApi.Core.Repository;
using MinimalApi.Core.Service;

namespace MinimalApi.Service.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public ProjectService(IProjectRepository projectRepository,IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(Project model)
        {
            if(model == null)
               throw new ArgumentNullException(nameof(model));

            await _projectRepository.AddAsync(model);
        }

        public void Remove(Project model)
        {
            if(model == null)
               throw new ArgumentNullException(nameof(model));

            _projectRepository.Remove(model);
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<ProjectDto>>(await _projectRepository.GetAllAsync());
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return  await _projectRepository.GetByIdAsync(id);
            
        }

        public async Task SaveChangesAsync()
        {
            await _projectRepository.SaveChangesAsync();
        }

        public async Task<List<ProjectWithCategoryDto>> GetProjectsWithCategoryAsync()
        {
            var projectsWithCategory = await _projectRepository.GetProjectsWithCategoryAsync();

            var projectsDto = _mapper.Map<List<ProjectWithCategoryDto>>(projectsWithCategory);

            return projectsDto;
        }
    }
}