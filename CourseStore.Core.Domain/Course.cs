using Newtonsoft.Json;

namespace CourseStore.Core.Domain
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public LicensingModel LicensingModel { get; set; }
    }
}
