using FluentValidation;
using OL_OASP_dev_H_07_23_Shared.Models;
using OL_OASP_dev_H_07_23_Shared.Models.Binding;
using OL_OASP_dev_H_07_23_WebApi.Services.Interfaces;

namespace OL_OASP_dev_H_07_23_WebApi.FluentValidation
{
    public class ActorBindingValidation : AbstractValidator<ActorBinding>
    {

        public ActorBindingValidation(IMoviesService moviesService)
        {
            RuleFor(y => y.MovieId).MustAsync(async (id, cancellation) => await moviesService.MovieExists(id)).WithMessage(ErrorCodes.NotFound);
            RuleFor(y => y.FirstName).NotEmpty().WithMessage(ErrorCodes.MissingValue);
            RuleFor(y => y.LastName).NotEmpty().WithMessage(ErrorCodes.MissingValue);

        }
    }
}
