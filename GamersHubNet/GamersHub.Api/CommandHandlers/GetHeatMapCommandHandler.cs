using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GamersHub.Api.Commands;
using GamersHub.Api.Data;
using GamersHub.Api.Domain;
using GamersHub.Api.Extensions;
using GamersHub.Api.PythonScripts;
using Gybs;
using Gybs.Logic.Cqrs;
using Gybs.Logic.Validation;
using Gybs.Results;
using Microsoft.EntityFrameworkCore;

namespace GamersHub.Api.CommandHandlers
{
    internal class GetHeatMapCommandHandler : ICommandHandler<GetHeatMapCommand, IReadOnlyCollection<byte>>
    {
        private readonly IValidator _validator;
        private readonly DataContext _dataContext;

        public GetHeatMapCommandHandler(
            IValidator validator,
            DataContext dataContext)
        {
            _validator = validator;
            _dataContext = dataContext;
        }

        public async Task<IResult<IReadOnlyCollection<byte>>> HandleAsync(GetHeatMapCommand command)
        {
            var validationResult = await IsValidAsync(command);

            if (validationResult.HasFailed())
            {
                return validationResult.Map<IReadOnlyCollection<byte>>();
            }

            var userId = command.UserId ?? command.CurrentUserId;

            var existingActualHeatMap = await _dataContext.GeneratedHeatMaps
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.GeneratedAt > DateTime.Now.AddDays(-1));

            if (existingActualHeatMap != null)
            {
                return existingActualHeatMap.HeatMap.ToList().ToSuccessfulResult();
            }

            PythonScriptRunner.RunScript("PythonScripts/heatmap.py", userId.ToString());

            var fileInfo = new FileInfo("heatplot.png");

            var data = new byte[fileInfo.Length];

            await using (var fs = fileInfo.OpenRead())
            {
                fs.Read(data, 0, data.Length);
            }

            fileInfo.Delete();

            _dataContext.GeneratedHeatMaps.Add(new GeneratedHeatmap
            {
                HeatMap = data,
                UserId = userId,
                GeneratedAt = DateTime.Now
            });

            await _dataContext.SaveChangesAsync();

            return data.ToList().ToSuccessfulResult();
        }

        private Task<IResult> IsValidAsync(GetHeatMapCommand query)
        {
            _validator.ValidateUserIds(query.CurrentUserId, query.UserId);

            return _validator.ValidateAsync();
        }
    }
}
