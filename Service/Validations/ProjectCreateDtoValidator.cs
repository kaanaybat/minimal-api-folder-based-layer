using Core.Dtos;
using FluentValidation;

namespace MinimalApi.Service.Validations
{
    public class ProjectCreateDtoValidator:AbstractValidator<ProjectCreateDto>
    {
        public ProjectCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
        
    }
}