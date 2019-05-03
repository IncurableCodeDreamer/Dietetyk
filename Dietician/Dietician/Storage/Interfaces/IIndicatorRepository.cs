using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dietician.Storage.StorageModels;

namespace Dietician.Storage.Interfaces
{
    public interface IIndicatorRepository
    {
        Task InsertIndicatorsIntoTable(IndicatorModel model);
        Task<IndicatorEntity> GetIndicatorFromTable(string idIndicator);
    }
}
