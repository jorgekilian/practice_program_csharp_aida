using NUnit.Framework;

namespace MarketingCampaign.Tests;

public class MarketingCampaignTests
{
    [TestCase(DayOfWeek.Friday, true)]
    [TestCase(DayOfWeek.Monday, false)]
    [TestCase(DayOfWeek.Tuesday, false)]
    [TestCase(DayOfWeek.Wednesday, false)]
    [TestCase(DayOfWeek.Thursday, false)]
    [TestCase(DayOfWeek.Saturday, false)]
    [TestCase(DayOfWeek.Sunday, false)]
    public void knows_which_day_is_the_crazy_day(DayOfWeek dayOfWeek, bool isCrazyDay)
    {
        var campaign = CreateMarketingCampaignOnDayOfTheWeek(dayOfWeek);

        var isCrazySalesDay = campaign.IsCrazySalesDay();

        Assert.That(isCrazySalesDay, Is.EqualTo(isCrazyDay));
    }

    [Test]
    public void campaign_is_active_when_milliseconds_of_current_date_are_even()
    {
        var campaign = CreateMarketingCampaignAt(4);

        var isActive = campaign.IsActive();

        Assert.That(isActive, Is.True);
    }

    [Test]
    public void campaign_is_not_active_when_milliseconds_of_current_date_are_odd()
    {
        var campaign = CreateMarketingCampaignAt(3);

        var isActive = campaign.IsActive();

        Assert.That(isActive, Is.False);
    }

    private static MarketingCampaignForTesting CreateMarketingCampaignAt(long milliseconds)
    {
        var anyDay = DayOfWeek.Monday;
        return new MarketingCampaignForTesting(anyDay, milliseconds);
    }

    private static MarketingCampaignForTesting CreateMarketingCampaignOnDayOfTheWeek(DayOfWeek dayOfWeek)
    {
        var notUsed = 0;
        return new MarketingCampaignForTesting(dayOfWeek, notUsed);
    }

    private class MarketingCampaignForTesting : MarketingCampaign
    {
        private readonly DayOfWeek _dayOfWeek;
        private readonly long _milliseconds;

        public MarketingCampaignForTesting(DayOfWeek dayOfWeek, long milliseconds)
        {
            _dayOfWeek = dayOfWeek;
            _milliseconds = milliseconds;
        }

        protected override long MilliSeconds()
        {
            return _milliseconds;
        }

        protected override DayOfWeek DayOfTheWeek()
        {
            return _dayOfWeek;
        }
    }
}