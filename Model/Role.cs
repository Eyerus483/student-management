using System.Text.Json.Serialization;

namespace student_management.Model
{
     [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Admin = 1,
        Department = 2,
        Student = 3,
        Teacher = 4

    }
}