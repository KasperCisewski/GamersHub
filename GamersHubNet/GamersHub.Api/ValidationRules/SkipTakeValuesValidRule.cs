using System.Threading.Tasks;
using Gybs;
using Gybs.Extensions;
using Gybs.Logic.Validation;
using Gybs.Results;

namespace GamersHub.Api.ValidationRules
{
    public class SkipTakeValuesValidRule : IValidationRule<(int skip, int take)>
    {
        public const string SkipTakeNotValid = "skip-or-take-cannot-be-negative";

        public async Task<IResult> ValidateAsync((int skip, int take) data)
        {
            var (skip, take) = data;
            if (skip < 0 || take < 0)
            {
                return Result.Failure(SkipTakeNotValid);
            }

            return await Result.Success().ToCompletedTask();
        }
    }
}
