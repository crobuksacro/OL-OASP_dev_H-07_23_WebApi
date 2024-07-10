using FluentValidation;
using OL_OASP_dev_H_07_23_Shared.Models;
using OL_OASP_dev_H_07_23_Shared.Models.Binding;
using OL_OASP_dev_H_07_23_WebApi.Services.Interfaces;

namespace OL_OASP_dev_H_07_23_WebApi.FluentValidation
{
    public class MovieUpdateBindingValidation : AbstractValidator<MovieUpdateBinding>
    {
        public MovieUpdateBindingValidation(IMoviesService moviesService)
        {

            RuleFor(y => y.Id).MustAsync(async (id, cancellation) => await moviesService.MovieExists(id)).WithMessage(ErrorCodes.NotFound);
            RuleFor(y => y.Title).NotEmpty().WithMessage(ErrorCodes.MissingValue);
            RuleFor(y => y.Genre).NotEmpty().WithMessage(ErrorCodes.MissingValue);
            RuleFor(y => y.ReleaseYear).GreaterThan(1900).WithMessage(ErrorCodes.OutOfRange);

        }
    }
}
