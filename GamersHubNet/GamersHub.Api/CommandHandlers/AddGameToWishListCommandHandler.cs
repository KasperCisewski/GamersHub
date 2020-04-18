﻿using System.Threading.Tasks;
using GamersHub.Api.Commands;
using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Api.Extensions;
using GamersHub.Api.ValidationRules;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.CommandHandlers
{
    internal class AddGameToWishListCommandHandler : ICommandHandler<AddGameToWishListCommand>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public AddGameToWishListCommandHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult> HandleAsync(AddGameToWishListCommand command)
        {
            var validationResult = await IsValidAsync(command);

            if (validationResult.HasFailed())
            {
                return validationResult;
            }

            var game = await _dataContext.Games.FindAsync(command.GameId);
            var user = await _dataContext.Users.Include(x => x.WishList).FirstAsync(x => x.Id == command.UserId);

            var wishListEntry = new WishListEntry()
            {
                Game = game,
                User = user,
            };

            user.WishList.Add(wishListEntry);

            await _dataContext.SaveChangesAsync();

            return Result.Success();
        }

        private Task<IResult> IsValidAsync(AddGameToWishListCommand query)
        {
            return _validator
                .Require<UserExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.UserId)
                .Require<GameExistsRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData(query.GameId)
                .Require<GameNotOnWishListAlreadyRule>()
                    .WithOptions(x => x.StopIfFailed())
                    .WithData((query.GameId, query.UserId))
                .ValidateAsync();
        }
    }
}
