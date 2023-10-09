using System.Text.Json.Serialization;

namespace student_management.Model
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnrollmentClass
    {
        FUllTime = 1,
        PartTime = 2,
        Online = 3
    }
}