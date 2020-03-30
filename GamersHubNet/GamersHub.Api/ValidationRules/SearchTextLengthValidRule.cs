using System.Threading.Tasks;
using Gybs;
using Gybs.Extensions;
using Gybs.Logic.Validation;
using Gybs.Results;

namespace GamersHub.Api.ValidationRules
{
    public class SearchTextLengthValidRule : IValidationRule<string>
    {
        public const string SearchTextTooLong = "search-text-too-long";

        public async Task<IResult> ValidateAsync(string searchText)
        {
            if (searchText.Length > 500)
            {
                return Result.Failure(SearchTextTooLong);
            }

            return await Result.Success().ToCompletedTask();
        }
    }
}