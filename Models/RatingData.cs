using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaVoirAdmin.Models
{
    public class RatingData
    {
        public int RatingId { get; set; }
        public int CategotyId { get; set; }
        public decimal RatingPercentage { get; set; }
        public decimal ViewersPercentage { get; set; }
        public string ChannelName { get; set; }
        public string CategoryName { get; set; }
        public string CompanyName { get; set; }
        public List<SelectListItem> catlist { get; set; }
        public List<SelectListItem> CompanyList { get; set; }
        public string ChildCategoryName { get; set; }
    }

    public class Channel
    {
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
    }

    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }

    public enum CategoryList
    {
        PakistanTVRatingsForBroadcasters=1,
        PakistanTVRatingsForAdvertiser=2,
        PakistanNewsChannelRatings=3,
        PakistanEntertainmentChannelRatings=4,
        PakistanSportsChannelRatings=5,
        TopNewsChannelsInPakistan=6,
        TopNewsShowsInPakistan=7,
        TopNewsShowsTimeSlotWise=8,
        TopNewsBulletinsInPakistan=9,
        TopNewsHeadlinesInPakistan=10,
        TopTransmissionInPakistan=11,
        TopEntertainmentChannelsInPakistan=12,
        TopTvSerialsInPakistan=13,
        TopMorningShowsInPakistan=14,
        TopGameShowsOfPakistan=15,
        TopEntertainmentTransmissionOfPakistan=16,
        TopPakistaniKidsShows=17,
        TopSportsChannelsInPakistan=18,
        TopSportsProgramInPakistan=19
    }
}