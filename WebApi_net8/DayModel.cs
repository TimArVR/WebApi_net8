namespace WebApi_net8
{
    public class DayModel
    {
        public string GetCurrentDay() 
        {
            return DateTime.Today.DayOfWeek.ToString();
        }
    }
}
