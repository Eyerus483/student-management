namespace student_management.Helpers.Common
{
    public class CommonMethods
    {
        public static DateTime GetCurrentEasternDateTime()
        {
            var dateTimeUtc = DateTime.UtcNow;
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var easternDateTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc, easternZone);
            return easternDateTime;

        }
    }
}