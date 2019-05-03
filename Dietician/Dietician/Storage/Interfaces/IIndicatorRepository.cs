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
        Task<List<IndicatorEntity>> GetIndicatorsFromTable(string idUser);
        Task<IndicatorEntity> GetLastIndicatorFromTable(string idUser);
    }
}
