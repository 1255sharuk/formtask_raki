using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace formtask2.Controllers
{
    public class DBController : Controller
    {
        // GET: DB
        public ActionResult Index()
        {
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
    }
}