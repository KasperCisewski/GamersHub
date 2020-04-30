using System;
using System.Collections.Generic;
using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.Tests
{
    public class InMemoryFixture
    {
        public static DataContext Context => InMemoryContext();

        private static DataContext InMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);

            SeedData(context);
            return context;
        }

        private static void SeedData(DataContext context)
        {
            context.Users.AddRange(new List<GamersHubUser>
            {
                new GamersHubUser { Id = Guid.Parse("852cd5f9-083e-464e-86e1-f2631eb88573")},
                new GamersHubUser { Id = Guid.Parse("475d30dc-6e0f-4369-bf15-37f4f8873215")}
            });

            var friendOneId = Guid.Parse("44889f11-b43e-476f-8cba-e9bbbf5d2b86");
            var friendTwoId = Guid.Parse("c47d5746-461f-461a-9f99-88e5dd725c1a");
            var friendThreeId = Guid.Parse("475d30dc-6e0f-4369-bf15-37f4f8873215");

            context.Friendships.AddRange(new List<Friendship>
            {
                new Friendship{ CurrentUserId = friendTwoId, FriendId = friendOneId },
                new Friendship{ CurrentUserId = friendOneId, FriendId = friendTwoId },

                new Friendship { CurrentUserId = friendOneId, FriendId = friendThreeId },
                new Friendship { CurrentUserId = friendThreeId, FriendId = friendOneId }
            });

            context.Games.AddRange(new List<Game>
            {
                new Game
                {
                    Id = Guid.Parse("3d948385-a94b-40ea-8ea2-4a87de24f113"),
                    Description = "Lorem ipsum",
                    CoverGameImage = new GameImage {Data = new byte[1]},
                    Name = "Lorem",
                    ReleaseDate = DateTime.Parse("01/01/2020"),
                    VideoUrl = "https://www.youtube.com/watch?v=cG6dyEjgtIM"
                }
            });

            context.SaveChanges();
        }
    }
}
