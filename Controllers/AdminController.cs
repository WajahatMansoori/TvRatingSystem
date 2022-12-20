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
using System.Text;
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

        public ActionResult RatingDataUpdateThrough_ModalPopup()
        {
            return View();
        }

        public ActionResult PakistanNewsChannelRatings_V2(int? CompanyId,int? CategoryId)
        {
            List<RatingData> list = new List<RatingData>();
            List<Category> catlist = new List<Category>();
            List<Company> companylist = new List<Company>();
            RatingData vm = new RatingData();
            vm.catlist = new List<SelectListItem>();
            vm.CompanyList = new List<SelectListItem>();
            if (CompanyId == null)
            {
                CompanyId = 1;
            }
            DataTable dtcompany = new DataTable();
            dtcompany = GetAllCompanies();
            if(dtcompany!=null && dtcompany.Rows.Count > 0)
            {
                for (int i = 0; i < dtcompany.Rows.Count; i++)
                {
                    Company cm = new Company();
                    int Company_Id = Convert.ToInt32(dtcompany.Rows[i]["CompanyId"].ToString());
                    string CompanyName = dtcompany.Rows[i]["CompanyName"].ToString();
                    cm.CompanyId = Company_Id;
                    cm.CompanyName = CompanyName;
                    companylist.Add(cm);
                }
                foreach (var item in companylist)
                {
                    var Company = new SelectListItem()
                    {
                        Value = item.CompanyId.ToString(),
                        Text = item.CompanyName
                    };
                    vm.CompanyList.Add(Company);
                }

            }
            if (CategoryId != null)
            {
                DataTable dtratingdata = new DataTable();
                dtratingdata = GetCategoryWiseData((int)CategoryId);
                if (dtratingdata != null)
                {
                    for (int i = 0; i < dtratingdata.Rows.Count; i++)
                    {
                        RatingData r = new RatingData();
                        int RatingId = Convert.ToInt32(dtratingdata.Rows[i]["RatingId"].ToString());
                        string ChildCategoryName = dtratingdata.Rows[i]["ChildCategoryName"].ToString();
                        decimal RatingPercentage = Convert.ToDecimal(dtratingdata.Rows[i]["RatingPercentage"].ToString());
                        decimal ViewshipPer = Convert.ToDecimal(dtratingdata.Rows[i]["ViewshipPercentage"].ToString());
                        //int Category_Id = Convert.ToInt32(dtratingdata.Rows[i]["CategoryId"].ToString());
                        r.RatingId = RatingId;
                        r.ChildCategoryName = ChildCategoryName;
                        r.RatingPercentage = RatingPercentage;
                        r.ViewersPercentage = ViewshipPer;
                        //r.CategotyId = Category_Id;
                        list.Add(r);
                        ViewBag.RatingData = list;
                    }
                }
                return View(list);
            }
            else
            {
                return View(vm);
            }
        }
        
        [HttpGet]
        public ActionResult PakistanNewsChannelRatings_V3(int? CategoryId)
        {
            if (CategoryId == null)
            {
                CategoryId = 3;
            }
            DataTable dtratingdata = new DataTable();
            List<RatingData> list = new List<RatingData>();
            dtratingdata = GetCategoryWiseData((int)CategoryId);
            if (dtratingdata != null)
            {
                for (int i = 0; i < dtratingdata.Rows.Count; i++)
                {
                    RatingData r = new RatingData();
                    int RatingId = Convert.ToInt32(dtratingdata.Rows[i]["RatingId"].ToString());
                    string ChildCategoryName = dtratingdata.Rows[i]["ChildCategoryName"].ToString();
                    decimal RatingPercentage = Convert.ToDecimal(dtratingdata.Rows[i]["RatingPercentage"].ToString());
                    decimal ViewshipPer = Convert.ToDecimal(dtratingdata.Rows[i]["ViewshipPercentage"].ToString());
                    r.RatingId = RatingId;
                    r.ChildCategoryName = ChildCategoryName;
                    r.RatingPercentage = RatingPercentage;
                    r.ViewersPercentage = ViewshipPer;
                    list.Add(r);
                }
            }
            return View(list);
            
        }

        public DataTable GetCategoryWiseData(int CategoryId)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(sqlcon);
            SqlCommand cmd = new SqlCommand("Admin_SelectCategoryWiseData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Categoryid", CategoryId);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            return dt;

        }

        public DataTable GetCompanyWiseData(int CompanyId, int CategoryId)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(sqlcon);
            SqlCommand cmd = new SqlCommand("Admiun_SelectCompanyWiseData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Categoryid", CategoryId);
            cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            return dt;

        }
        //[HttpPost]
        //public ActionResult GetCategoryByCompanyId(int companyId)
        //{
        //    DataTable dtcat = new DataTable();
        //    dtcat = Select_CategoryOnCompanyChange(companyId);
        //    if(dtcat!=null && dtcat.Rows.Count > 0)
        //    {
        //        //return JsonConvert.SerializeObject(dtcat);
        //        //return Json(new { data= DataTableToJSONWithStringBuilder(dtcat) },JsonRequestBehavior.AllowGet) ;
        //        return Json(new { data= DataTableToJSONWithJSONNet(dtcat) },JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return RedirectToAction("PakistanNewsChannelRatings_V2", "Admin");
        //    }
        //}


   

        public DataTable Select_CategoryOnCompanyChange(int CompanyId)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(sqlcon);
            SqlCommand cmd = new SqlCommand("Admin_Select_CategoryOnCompanyChange", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            return dt;

        }

        public DataTable GetAllCompanies()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(sqlcon);
            SqlCommand cmd = new SqlCommand("Admin_GetAllCompanies", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            return dt;

        }

        [HttpPost]
        public ActionResult GetCatByCompanyId(int CompanyId)
        {
            var cat = Getcat().Where(x => x.CompanyId == CompanyId).ToList();
            return Json(new { data = cat }, JsonRequestBehavior.AllowGet);
        }

        public List<Category> Getcat()
        {
            List<Category> cat = new List<Category>
            {
                new Category{ CategoryId =1,CategoryName="Pakistan TV Ratings For Broadcasters",CompanyId=1},
                new Category{ CategoryId =2,CategoryName="Pakistan TV Ratings For Advertiser",CompanyId=1},
                new Category{ CategoryId =3,CategoryName="Pakistan News Channel Ratings",CompanyId=1},
                new Category{ CategoryId =4,CategoryName="Pakistan Entertainment Channel Ratings",CompanyId=1},
                new Category{ CategoryId =5,CategoryName="Pakistan Sports Channel Ratings",CompanyId=1},
                new Category{ CategoryId =6,CategoryName="Pakistan TV Ratings For Broadcasters_V2",CompanyId=2},
                new Category{ CategoryId =7,CategoryName="Pakistan TV Ratings For Advertiser_V2",CompanyId=2},
                new Category{ CategoryId =8,CategoryName="Pakistan News Channel Ratings_V2",CompanyId=2},
            };
            return cat;
        }

        public ActionResult DropdownChangeResult()
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
    }
}