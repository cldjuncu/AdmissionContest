using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace AdmissionContest.WebApi.Models
{
    public class Result
    {
        public long Cnp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double AdmissionGrade { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Categories Clasification { get; set; }
    }

    public enum Categories
    {
        Budget,
        Tax,
        Rejected,
    };
}
