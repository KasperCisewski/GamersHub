using System.Threading.Tasks;
using Gybs;
using Gybs.Extensions;
using Gybs.Logic.Validation;
using Gybs.Results;

namespace GamersHub.Api.ValidationRules
{
    public class ImageNotEmptyRule : IValidationRule<byte[]>
    {
        public const string ImageEmpty = "image-is-empty";

        public async Task<IResult> ValidateAsync(byte[] image)
        {
            if (image == null || image.Length == 0)
            {
                return Result.Failure(ImageEmpty);
            }

            return await Result.Success().ToCompletedTask();
        }
    }
}