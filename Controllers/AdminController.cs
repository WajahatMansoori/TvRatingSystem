using MediaVoirAdmin.DAL;
using MediaVoirAdmin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MediaVoirAdmin.Controllers
{
    public class AdminController : Controller
    {
        string sqlcon = ConfigurationManager.ConnectionStrings["MediaViorLiveCon"].ConnectionString;
        private MediaVoirContext db = new MediaVoirContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AdminUsers user, string ReturnUrl)
        {
            SqlConnection con = new SqlConnection(sqlcon);
            string Query = "select *from AdminUsers where AdminName=@User and Password=@Pass";
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@User", user.AdminName);
            cmd.Parameters.AddWithValue("@Pass", user.Password);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows == true)
            {
                Session["User"] = user.AdminName;
                return RedirectToAction("Index", "Admin");
            }

            else
            {
                TempData["AdminfailLogin"] = "<script>alert('Incorrect UserName or Password')</script>";

            }
            con.Close();
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult PakistanTVRatingsForBroadcasters()
        {
            DataTable dt = new DataTable();
            List<RatingData> list = new List<RatingData>();
            SqlConnection con = new SqlConnection(sqlcon);
            string Query = "select rd.RatingId,ch.ChannelName,rd.RatingPercentage,rd.ViewersPerCentage from RatingData rd (NOLOCK) INNER JOIN OG_ChannelList ch (NOLOCK) on ch.ChannelId=rd.ChannelId where CategoryId=@CatId and ch.IsActive=1 and rd.IsActive=1";
            con.Open();
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@CatId", (int)CategoryList.PakistanEntertainmentChannelRatings);
            SqlDataReader sdr = cmd.ExecuteReader();
            
            if (sdr.HasRows == true)
            {
                while (sdr.Read())
                {
                    RatingData r = new RatingData();
                    int RatingId = Convert.ToInt32(sdr["RatingId"].ToString());
                    string ChannelName = sdr["ChannelName"].ToString();
                    decimal RatingPercentage = Convert.ToDecimal(sdr["RatingPercentage"].ToString());
                    decimal ViewersPercentage = Convert.ToDecimal(sdr["ViewersPerCentage"].ToString());
                    r.RatingId = RatingId;
                    r.ChannelName = ChannelName;
                    r.RatingPercentage = RatingPercentage;
                    r.ViewersPercentage = ViewersPercentage;
                    list.Add(r);
                }
                con.Close();
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

        public ActionResult PakistanTVRatingsForAdvertiser()
        {
            DataTable dt = new DataTable();
            List<RatingData> list = new List<RatingData>();
            SqlConnection con = new SqlConnection(sqlcon);
            string Query = "select rd.RatingId,ch.ChannelName,rd.RatingPercentage,rd.ViewersPerCentage from RatingData rd (NOLOCK) INNER JOIN OG_ChannelList ch (NOLOCK) on ch.ChannelId=rd.ChannelId where CategoryId=@CatId and ch.IsActive=1 and rd.IsActive=1";
            con.Open();
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@CatId", (int)CategoryList.PakistanTVRatingsForAdvertiser);
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows == true)
            {
                while (sdr.Read())
                {
                    RatingData r = new RatingData();
                    int RatingId = Convert.ToInt32(sdr["RatingId"].ToString());
                    string ChannelName = sdr["ChannelName"].ToString();
                    decimal RatingPercentage = Convert.ToDecimal(sdr["RatingPercentage"].ToString());
                    decimal ViewersPercentage = Convert.ToDecimal(sdr["ViewersPerCentage"].ToString());
                    r.RatingId = RatingId;
                    r.ChannelName = ChannelName;
                    r.RatingPercentage = RatingPercentage;
                    r.ViewersPercentage = ViewersPercentage;
                    list.Add(r);
                }
                con.Close();
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

        public ActionResult PakistanNewsChannelRatings()
        {
            DataTable dt = new DataTable();
            List<RatingData> list = new List<RatingData>();
            SqlConnection con = new SqlConnection(sqlcon);
            string Query = "select rd.RatingId,ch.ChannelName,rd.RatingPercentage,rd.ViewersPerCentage,cat.CategoryId from RatingData rd (NOLOCK) INNER JOIN OG_ChannelList ch (NOLOCK) on ch.ChannelId=rd.ChannelId left join Og_Category cat (NOLOCK) on cat.CategoryId=rd.CategoryId where rd.CategoryId=@CategoryId and ch.IsActive=1 and rd.IsActive=1 or ParentId=@ParentId";
            con.Open();
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@CategoryId", (int)CategoryList.PakistanNewsChannelRatings);
            cmd.Parameters.AddWithValue("@ParentId", (int)CategoryList.PakistanNewsChannelRatings);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows == true)
            {
                while (sdr.Read())
                {
                    RatingData r = new RatingData();
                    int RatingId = Convert.ToInt32(sdr["RatingId"].ToString());
                    int CategoryId = Convert.ToInt32(sdr["CategoryId"].ToString());
                    string ChannelName = sdr["ChannelName"].ToString();
                    decimal RatingPercentage = Convert.ToDecimal(sdr["RatingPercentage"].ToString());
                    decimal ViewersPercentage = Convert.ToDecimal(sdr["ViewersPerCentage"].ToString());
                    r.RatingId = RatingId;
                    r.CategotyId = CategoryId;
                    r.ChannelName = ChannelName;
                    r.RatingPercentage = RatingPercentage;
                    r.ViewersPercentage = ViewersPercentage;
                    list.Add(r);
                }
                con.Close();
                ViewBag.RatingData = list;
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        public ActionResult PakistanEntertainmentChannelRatings()
        {
            DataTable dt = new DataTable();
            List<RatingData> list = new List<RatingData>();
            SqlConnection con = new SqlConnection(sqlcon);
            string Query = "select rd.RatingId,ch.ChannelName,rd.RatingPercentage,rd.ViewersPerCentage,cat.CategoryId from RatingData rd (NOLOCK) INNER JOIN OG_ChannelList ch (NOLOCK) on ch.ChannelId=rd.ChannelId left join Og_Category cat (NOLOCK) on cat.CategoryId=rd.CategoryId where rd.CategoryId=@CategoryId and ch.IsActive=1 and rd.IsActive=1 or ParentId=@ParentId";
            con.Open();
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@CategoryId", (int)CategoryList.PakistanEntertainmentChannelRatings);
            cmd.Parameters.AddWithValue("@ParentId", (int)CategoryList.PakistanEntertainmentChannelRatings);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows == true)
            {
                while (sdr.Read())
                {
                    RatingData r = new RatingData();
                    int RatingId = Convert.ToInt32(sdr["RatingId"].ToString());
                    int CategoryId = Convert.ToInt32(sdr["CategoryId"].ToString());
                    string ChannelName = sdr["ChannelName"].ToString();
                    decimal RatingPercentage = Convert.ToDecimal(sdr["RatingPercentage"].ToString());
                    decimal ViewersPercentage = Convert.ToDecimal(sdr["ViewersPerCentage"].ToString());
                    r.RatingId = RatingId;
                    r.CategotyId = CategoryId;
                    r.ChannelName = ChannelName;
                    r.RatingPercentage = RatingPercentage;
                    r.ViewersPercentage = ViewersPercentage;
                    list.Add(r);
                }
                con.Close();
                ViewBag.RatingData = list;
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }

        }

        public ActionResult PakistanSportsChannelRatings()
        {
            DataTable dt = new DataTable();
            List<RatingData> list = new List<RatingData>();
            SqlConnection con = new SqlConnection(sqlcon);
            string Query = "select rd.RatingId,ch.ChannelName,rd.RatingPercentage,rd.ViewersPerCentage,cat.CategoryId from RatingData rd (NOLOCK) INNER JOIN OG_ChannelList ch (NOLOCK) on ch.ChannelId=rd.ChannelId left join Og_Category cat (NOLOCK) on cat.CategoryId=rd.CategoryId where rd.CategoryId=@CategoryId and ch.IsActive=1 and rd.IsActive=1 or ParentId=@ParentId";
            con.Open();
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@CategoryId", (int)CategoryList.PakistanSportsChannelRatings);
            cmd.Parameters.AddWithValue("@ParentId", (int)CategoryList.PakistanSportsChannelRatings);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows == true)
            {
                while (sdr.Read())
                {
                    RatingData r = new RatingData();
                    int RatingId = Convert.ToInt32(sdr["RatingId"].ToString());
                    int CategoryId = Convert.ToInt32(sdr["CategoryId"].ToString());
                    string ChannelName = sdr["ChannelName"].ToString();
                    decimal RatingPercentage = Convert.ToDecimal(sdr["RatingPercentage"].ToString());
                    decimal ViewersPercentage = Convert.ToDecimal(sdr["ViewersPerCentage"].ToString());
                    r.RatingId = RatingId;
                    r.CategotyId = CategoryId;
                    r.ChannelName = ChannelName;
                    r.RatingPercentage = RatingPercentage;
                    r.ViewersPercentage = ViewersPercentage;
                    list.Add(r);
                }
                con.Close();
                ViewBag.RatingData = list;
                return View(list);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

        public ActionResult EditPakistanTVRatingsForBroadcasters(int id)
        {
            SqlConnection con = new SqlConnection(sqlcon);
            string Query = "select RatingPercentage,ViewersPerCentage from RatingData (NOLOCK) where RatingId=@RatingId";
            con.Open();
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@RatingId", id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows == true)
            {
               while (sdr.Read())
                {
                  RatingData r = new RatingData();
                    ViewBag.RatingId = id;
                    ViewBag.RatingPercentage = Convert.ToDecimal(sdr["RatingPercentage"].ToString());
                    ViewBag.ViewersPerCentage = Convert.ToDecimal(sdr["ViewersPerCentage"].ToString());

                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

        [HttpPost]
        public ActionResult EditPakistanTVRatingsForBroadcasters(RatingData data)
        {
            int result;
            SqlConnection con = new SqlConnection(sqlcon);
            SqlCommand cmd = new SqlCommand("Admin_UpdateRatingData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RatingId", data.RatingId);
            cmd.Parameters.AddWithValue("@RatingPercentage", data.RatingPercentage);
            cmd.Parameters.AddWithValue("@ViewersPercentage", data.ViewersPercentage);
            con.Open();
            result = cmd.ExecuteNonQuery();
            if (result>0)
            {
                return RedirectToAction("PakistanTVRatingsForBroadcasters", "Admin");
            }
            else
            {
                return RedirectToAction("EditPakistanTVRatingsForBroadcasters", "Admin");
            }
        }

        public ActionResult EditPakistanSportsChannelRatings(int id)
        {
            SqlConnection con = new SqlConnection(sqlcon);
            string Query = "select RatingPercentage,ViewersPerCentage from RatingData (NOLOCK) where RatingId=@RatingId";
            con.Open();
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@RatingId", id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows == true)
            {
                while (sdr.Read())
                {
                    RatingData r = new RatingData();
                    ViewBag.RatingId = id;
                    ViewBag.RatingPercentage = Convert.ToDecimal(sdr["RatingPercentage"].ToString());
                    ViewBag.ViewersPerCentage = Convert.ToDecimal(sdr["ViewersPerCentage"].ToString());

                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }
        [HttpPost]
        public ActionResult EditPakistanSportsChannelRatings(RatingData data)
        {
            int result;
            SqlConnection con = new SqlConnection(sqlcon);
            SqlCommand cmd = new SqlCommand("Admin_UpdateRatingData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RatingId", data.RatingId);
            cmd.Parameters.AddWithValue("@RatingPercentage", data.RatingPercentage);
            cmd.Parameters.AddWithValue("@ViewersPercentage", data.ViewersPercentage);
            con.Open();
            result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                return RedirectToAction("PakistanSportsChannelRatings", "Admin");
            }
            else
            {
                return RedirectToAction("EditPakistanSportsChannelRatings", "Admin");
            }
        }

        public ActionResult EditTopSportsChannelsInPakistan(int id)
        {
            SqlConnection con = new SqlConnection(sqlcon);
            string Query = "select RatingPercentage,ViewersPerCentage from RatingData (NOLOCK) where RatingId=@RatingId";
            con.Open();
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@RatingId", id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows == true)
            {
                while (sdr.Read())
                {
                    RatingData r = new RatingData();
                    ViewBag.RatingId = id;
                    ViewBag.RatingPercentage = Convert.ToDecimal(sdr["RatingPercentage"].ToString());
                    ViewBag.ViewersPerCentage = Convert.ToDecimal(sdr["ViewersPerCentage"].ToString());

                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }
        [HttpPost]
        public ActionResult EditTopSportsChannelsInPakistan(RatingData data)
        {
            int result;
            SqlConnection con = new SqlConnection(sqlcon);
            SqlCommand cmd = new SqlCommand("Admin_UpdateRatingData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RatingId", data.RatingId);
            cmd.Parameters.AddWithValue("@RatingPercentage", data.RatingPercentage);
            cmd.Parameters.AddWithValue("@ViewersPercentage", data.ViewersPercentage);
            con.Open();
            result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                return RedirectToAction("PakistanSportsChannelRatings", "Admin");
            }
            else
            {
                return RedirectToAction("EditPakistanSportsChannelRatings", "Admin");
            }
        }

        public List<DataPoint> GetCategoryWiseGraph(int CategoryId)
        {
            List<DataPoint> dataPoint = new List<DataPoint>();
            List<RatingData> list = new List<RatingData>();
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(sqlcon);
            string Query = "select r.ViewersPerCentage,ch.ChannelName from RatingData r (NOLOCK) INNER JOIN OG_ChannelList ch (NOLOCK) on ch.ChannelId=r.ChannelId where CategoryId=@CatId";
            con.Open();
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@CatId", (int)CategoryList.PakistanTVRatingsForAdvertiser);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    RatingData r = new RatingData();
                    //r.RatingPercentage = Convert.ToDecimal(sdr["RatingPercentage"].ToString());
                    r.ChannelName = sdr["ChannelName"].ToString();
                    r.ViewersPercentage = Convert.ToDecimal(sdr["ViewersPerCentage"].ToString());
                    list.Add(r);
                }
            }
            con.Close();
            foreach (var item in list)
            {
                dataPoint.Add(new DataPoint(item.ChannelName, item.ViewersPercentage));
            }
            return dataPoint;
            //ViewBag.RatingData = JsonConvert.SerializeObject(dataPoint);
        }

        public ActionResult Rating_PakistanTVRatingsForBroadcasters()
        {
            //List<DataPoint> dataPoint = new List<DataPoint>();
            //List<RatingData> list = new List<RatingData>();
            //DataTable dt = new DataTable();
            //SqlConnection con = new SqlConnection(sqlcon);
            //string Query = "select r.ViewersPerCentage,ch.ChannelName from RatingData r (NOLOCK) INNER JOIN OG_ChannelList ch (NOLOCK) on ch.ChannelId=r.ChannelId where CategoryId=@CatId";
            //con.Open();
            //SqlCommand cmd = new SqlCommand(Query, con);
            //cmd.Parameters.AddWithValue("@CatId", (int)CategoryList.PakistanTVRatingsForBroadcasters);
            //SqlDataReader sdr = cmd.ExecuteReader();
            //if (sdr.HasRows)
            //{
            //    while (sdr.Read())
            //    {
            //        RatingData r = new RatingData();
            //        //r.RatingPercentage = Convert.ToDecimal(sdr["RatingPercentage"].ToString());
            //        r.ChannelName = sdr["ChannelName"].ToString();
            //        r.ViewersPercentage = Convert.ToDecimal(sdr["ViewersPerCentage"].ToString());
            //        list.Add(r);
            //    }
            //}
            //con.Close();
            //foreach (var item in list)
            //{
            //    dataPoint.Add(new DataPoint(item.ChannelName, item.ViewersPercentage));
            //}
            List<DataPoint> data = GetCategoryWiseGraph((int)CategoryList.PakistanTVRatingsForBroadcasters);
            ViewBag.RatingData= JsonConvert.SerializeObject(data);
            return View();
        }
    }
}