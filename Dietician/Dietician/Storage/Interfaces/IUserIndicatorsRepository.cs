using System.Threading.Tasks;
using Dietician.Storage.StorageModels;

namespace Dietician.Storage.Interfaces
{
    public interface IUserIndicatorsRepository
    {
        Task InsertUserIndicatorsIntoTable(UserIndicatorModel model);
        Task<UserIndicatorsEntity> GetUserIndicatorFromTableFromIdUser(string idUser);
        Task<UserIndicatorsEntity> GetUserIndicatorFromTableFromIdIndicator(string idIndicator);
    }
}
