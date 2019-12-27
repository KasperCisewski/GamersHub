﻿using CsvHelper;
using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GamersHub.DbSeeder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var dbContext = SetUpDbConnection();

            await SeedGames(dbContext);

            await dbContext.SaveChangesAsync();
        }

        private static async Task<DataContext> SeedGames(DataContext dbContext)
        {
            var games = ReadGamesFromCsv("Final.csv");

            foreach (var game in games)
            {
                SeedCoverPhoto(game);
                SeedScreenshots(game);
            }

            if (!await dbContext.Games.AnyAsync())
            {
                dbContext.Games.AddRange(games);
            }

            return dbContext;
        }

        private static List<Game> ReadGamesFromCsv(string fileName)
        {
            using var reader = new StreamReader(fileName);
            using var csv = new CsvReader(reader);
            csv.Configuration.RegisterClassMap<GameMap>();
            return csv.GetRecords<Game>().ToList();
        }

        private static DataContext SetUpDbConnection()
        {
            var connectionstring = "Server=(localdb)\\mssqllocaldb;Database=aspnet-GamersHub;Trusted_Connection=True;MultipleActiveResultSets=true";

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(connectionstring);
            var dbContext = new DataContext(optionsBuilder.Options);
            return dbContext;
        }

        private static void SeedScreenshots(Game game)
        {
            string[] filePaths;
            try
            {
                filePaths = Directory.GetFiles(
                        $"Screenshots\\images\\{game.Name.ToLower().Replace(':', '_').Replace(' ', '_')}_screenshot", "*.jpg");
            }
            catch (Exception)
            {
                return;
            }
            game.GameImages = new List<GameImage>();

            foreach (var path in filePaths)
            {
                var fileInfo = new FileInfo(path);
                byte[] data = new byte[fileInfo.Length];

                using (FileStream fs = fileInfo.OpenRead())
                {
                    fs.Read(data, 0, data.Length);
                }

                var image = new GameImage
                {
                    FileName = fileInfo.Name,
                    Data = data,
                    Length = fileInfo.Length,
                    ContentType = "image/jpeg",
                };

                game.GameImages.Add(image);
            }
        }

        private static void SeedCoverPhoto(Game game)
        {
            var fileInfo = new FileInfo($"CoverPhotos\\Photos\\{game.Name.Replace(":"," ")}.jpg");
            byte[] data = new byte[fileInfo.Length];

            using (FileStream fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
            }

            game.CoverGameImage = new GameImage
            {
                FileName = fileInfo.Name,
                Data = data,
                Length = fileInfo.Length,
                ContentType = "image/jpeg"
            };
        }
    }
}
