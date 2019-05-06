using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Dietician.Storage.StorageModels
{
    public class IndicatorEntity:TableEntity
    {
        public IndicatorModel IndicatorsModelData { get; set; }

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext context)
        {
            string idUser = "";
            int weight = 0;
            int height = 0;
            DateTime date = new DateTime();
            int idIndicator = 0;

            foreach (var prop in properties)
            {
                switch (prop.Key.ToLower())
                {
                    case "idUser":
                        idUser = prop.Value.ToString();
                        break;
                    case "weight":
                        weight = (int)prop.Value.Int32Value;
                        break;
                    case "height":
                        height = (int)prop.Value.Int32Value;
                        break;
                    case "idindicator":
                        idIndicator = (int)prop.Value.Int32Value;
                        break;
                    case "changedate":
                        date = (DateTime) prop.Value.DateTime;
                        break;
                }

                IndicatorsModelData = new IndicatorModel(idUser,idIndicator, height,weight,date);
            }
        }

        public override IDictionary<string, EntityProperty> WriteEntity(OperationContext context)
        {
            var result = new Dictionary<string, EntityProperty>
            {
                {nameof(IndicatorsModelData.IdUser), new EntityProperty(IndicatorsModelData.IdUser)},
                {nameof(IndicatorsModelData.IdIndicators), new EntityProperty(IndicatorsModelData.IdIndicators)},
                {nameof(IndicatorsModelData.Height), new EntityProperty(IndicatorsModelData.Height)},
                {nameof(IndicatorsModelData.Weight), new EntityProperty(IndicatorsModelData.Weight)},
                {nameof(IndicatorsModelData.ChangeDate), new EntityProperty(IndicatorsModelData.ChangeDate)}
            };
            return result;
        }
    }
}

