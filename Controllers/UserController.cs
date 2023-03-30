using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using formtask2.Models;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;

namespace formtask2.Controllers
{
    public class UserController : Controller
    {
        Models.Users dbop = new Models.Users();

        [HttpGet]
        public ActionResult Register()
        {

            ViewBag.UserTypeList = new List<SelectListItem>

            { new SelectListItem { Text = "User", Value = "User" },
               new SelectListItem { Text = "Admin", Value = "Admin" }};



            CompanyDBEntities sd = new CompanyDBEntities();
            ViewBag.countryList = new SelectList(GetCountries(), "cid", "cname");
            return View();
        }
        public List<country> GetCountries()
        {
            CompanyDBEntities sd = new CompanyDBEntities();
            List<country> countries = sd.countries.ToList();
            return countries;
        }
        public ActionResult GetStates(int cid)
        {
            CompanyDBEntities sd = new CompanyDBEntities();
            List<state> selectList = sd.states.Where(x => x.cid == cid).ToList();
            ViewBag.Slist = new SelectList(selectList, "sid", "sname");
            return PartialView("DisplayStates");
        }
        public ActionResult GetCities(int sid)
        {
            CompanyDBEntities sd = new CompanyDBEntities();
            List<city> selectList = sd.cities.Where(x => x.sid == sid).ToList(); ;
            ViewBag.citylist = new SelectList(selectList, "cityid", "cityname");
            return PartialView("DisplayCities");
        }
        public ActionResult Index(string captchaInput)
        {
            string captcha = (string)Session["Captcha"] ?? string.Empty;
            if (captchaInput == captcha)
            {
                // Captcha input is correct
            }
            else
            {
                // Captcha input is incorrect
            }

            return View();
        }

        // GET: Captcha
       

        
        public string Textforcaptcha()
        {
            Random generate = new Random();
            string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string captchaString = "";
            for (int i = 0; i < 5; i++)
            {
                captchaString += chars[generate.Next(chars.Length)];
            }
            return captchaString;
        }
        public ActionResult Imageforcaptcha()
        {
            var captchaText = Textforcaptcha();
            var image = new Bitmap(125, 50);
            var graphics = Graphics.FromImage(image);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Set the background color to light grey
            graphics.Clear(Color.LightGray);

            var font = new Font("Times New Roman", 25, FontStyle.Italic, GraphicsUnit.Pixel);
            var brush = new SolidBrush(Color.Black);
            graphics.DrawString(captchaText, font, brush, new PointF(30, 20));

            // Add random black dots
            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                int x = random.Next(125);
                int y = random.Next(50);
                image.SetPixel(x, y, Color.Black);
            }


           
        // Save the image to a memory stream and return the byte array
        var stream = new MemoryStream();
            image.Save(stream, ImageFormat.Png);
            Session["CaptchaText"] = captchaText;
            return File(stream.ToArray(), "image/png");
        }
    }
            }









