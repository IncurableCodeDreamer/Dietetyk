using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.StorageModels
{
    public class UserIndicatorsEntity: TableEntity
    {
        public UserIndicatorModel UserIndicatorsModelData { get; set; }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext context)
        {

            int idUser = 0;
            int idIndicator = 0;
            foreach (var prop in properties)
            {
                switch (prop.Key.ToLower())
                {
                    case "iduser":
                        idUser = (int) prop.Value.Int32Value;
                        break;
                    case "idindicator":
                        idIndicator = (int)prop.Value.Int32Value;
                        break;
                }

                UserIndicatorsModelData = new UserIndicatorModel(idUser, idIndicator);
            }
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext context)
       {
            var result = new Dictionary<string, EntityProperty>
            {
   {nameof(UserIndicatorsModelData.IdUser), new EntityProperty(UserIndicatorsModelData.IdUser)},
   {nameof(UserIndicatorsModelData.IdIndicators), new EntityProperty(UserIndicatorsModelData.IdIndicators)} };
          return result;
        }
    }
}

