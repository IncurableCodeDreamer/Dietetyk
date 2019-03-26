namespace Dietician.Models
{
    public class Parameters
    {
        public PersonalDataSettings PresonalData { get; set; }
        public FatLevel FatLevel { get; set; }  
        public ParameterResults ParameterResults { get; set; }
        public CheckboxResult Params { get; set; }
        public bool ShowResults { get; set; }
    }
}
