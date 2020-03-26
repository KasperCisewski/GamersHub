using System;
using System.Threading.Tasks;
using GamersHub.Api.Data;
using Gybs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.ValidationRules
{
    public class UserExistsRule : IValidationRule<Guid>
    {
        public const string UserDoesNotExist = "no-user-with-given-id";
        private readonly DataContext _dataContext;

        public UserExistsRule(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IResult> ValidateAsync(Guid userId)
        {
            if (await _dataContext.Users.AnyAsync(x => x.Id == userId))
            {
                return Result.Success();
            }

            return Result.Failure(UserDoesNotExist);
        }
    }
}