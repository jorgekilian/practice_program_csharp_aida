namespace MarketingCampaign;

public class MarketingCampaign
{
    public bool IsActive()
    {
        return MilliSeconds() % 2 == 0;
    }

    public bool IsCrazySalesDay()
    {
        return DayOfTheWeek().Equals(DayOfWeek.Friday);
    }

    protected virtual long MilliSeconds()
    {
        return (long)DateTime.Now.TimeOfDay.TotalMilliseconds;
    }

    protected virtual DayOfWeek DayOfTheWeek()
    {
        return DateTime.Now.DayOfWeek;
    }
}