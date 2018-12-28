using System.Collections.Generic;
using FluentValidation;

namespace ImageSearcher.Web.Api.Models.Request
{
    public class SearchImages
    {
        public SearchImages()
        {
            Tags = new List<string>();
        }

        public IEnumerable<string> Tags { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }
    }

    public class SearchImagesValidator : AbstractValidator<SearchImages>
    {
        public SearchImagesValidator()
        {
            RuleForEach(x => x.Tags).NotEmpty();

            RuleFor(x => x.Longitude).InclusiveBetween(-180, 180).When(x => x.Longitude.HasValue);

            RuleFor(x => x.Latitude).InclusiveBetween(-90, 90).When(x => x.Latitude.HasValue);

            RuleFor(x => x.PageNumber).GreaterThan(0).When(x => x.PageNumber.HasValue);

            RuleFor(x => x.PageSize).GreaterThan(0).When(x => x.PageSize.HasValue);
        }
    }
}
