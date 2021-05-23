using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Data;
using Test.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        testEntities1 _db = new testEntities1();
        public ActionResult Index()
        {
            Employ emp = new Employ();
            emp.CountryList = _db.Countries.Select(s => new CountryList()
            {
                CID = s.ID,
                Name = s.Name
            }).ToList();
            return View(emp);
        }
        [HttpPost]
        public ActionResult Index(Employ employee)
        {
            string ProfilePhotoPath = string.Empty;
            string ProfileFinalUserImagePath = string.Empty;
            string ResumePath = string.Empty;
            string resumeFinalPath = string.Empty;
            List<string> QualificationPath = new List<string>();
            List<string> QualificationFinalPath = new List<string>();

            if (ModelState.IsValid)
            {
                if (employee.ProfilePic.ContentLength > 0 && employee.ProfilePic != null)
                {
                    string DirectoryPath = Server.MapPath("~/ProfileImage");
                    if (!Directory.Exists(DirectoryPath))
                    {
                        Directory.CreateDirectory(DirectoryPath);
                    }
                    ProfilePhotoPath = DateTime.Now.ToString("Hmmssffff") + "_" + employee.ProfilePic.FileName;
                    ProfileFinalUserImagePath = Path.Combine(DirectoryPath, ProfilePhotoPath);
                }
                if (employee.Resume.ContentLength > 0 && employee.Resume != null)
                {
                    string DirectoryPath = Server.MapPath("~/Resumes");
                    if (!Directory.Exists(DirectoryPath))
                    {
                        Directory.CreateDirectory(DirectoryPath);
                    }
                    ResumePath = DateTime.Now.ToString("Hmmssffff") + "_" + employee.Resume.FileName;
                    resumeFinalPath = Path.Combine(DirectoryPath, ResumePath);
                }
                if (employee.Qupload.Count >= 0 && employee.Qupload != null)
                {
                    string DirectoryPath = Server.MapPath("~/Qualification");
                    if (!Directory.Exists(DirectoryPath))
                    {
                        Directory.CreateDirectory(DirectoryPath);
                    }
                    for (int i = 0; i < employee.Qupload.Count; i++)
                    {
                        QualificationPath.Add(DateTime.Now.ToString("Hmmssffff") + "_" + employee.Qupload[i].FileName);
                        QualificationFinalPath.Add(Path.Combine(DirectoryPath, ProfilePhotoPath));
                    }
                }
                Employee employee1 = new Employee()
                {
                    Address = employee.Address,
                    Cid = employee.CID,
                    ctid = employee.CTID,
                    Email = employee.Email,
                    Name = employee.Name,
                    profileImage = ProfilePhotoPath,
                    sid = employee.SID,
                    resume = ResumePath,
                    MobileNo = employee.Mobile
                };
                _db.Employees.Add(employee1);
                _db.SaveChanges();
                if (employee.ProfilePic.ContentLength > 0)
                {
                    employee.ProfilePic.SaveAs(ProfileFinalUserImagePath);
                }
                if (employee.Resume.ContentLength > 0)
                {
                    employee.Resume.SaveAs(resumeFinalPath);
                }
                var id = _db.Employees.Where(s => s.Name == employee.Name && s.Email == employee.Email && s.Address == employee.Address).FirstOrDefault();
                for (int i = 0; i < employee.QName.Count; i++)
                {
                    Qualification qualification = new Qualification()
                    {
                        Qname = employee.QName[i],
                        EmpID = id.ID,
                        Qcertificate = QualificationPath[i]
                    };
                    _db.Qualifications.Add(qualification);
                    _db.SaveChanges();
                }
            }
            return RedirectToAction("GetEmployees");
        }
        [HttpGet]
        public ActionResult BindState(int? id)
        {
            var state = _db.States.Where(x => x.CID == id).Select(s => new StateList
            {
                SID = s.ID,
                Name = s.Name
            }).ToList();
            return Json(state, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult BindCity(int? id)
        {
            var citys = _db.Cities.Where(x => x.SID == id).Select(s => new CityList()
            {
                CID = s.ID,
                Name = s.name
            }).ToList();
            return Json(citys, JsonRequestBehavior.AllowGet);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult GetEmployees()
        {
            var emp = from empl in _db.Employees
                      join co in _db.Countries on empl.Cid equals co.ID
                      join st in _db.States on empl.sid equals st.ID
                      join ct in _db.Cities on empl.ctid equals ct.ID
                      join qt in _db.Qualifications on empl.ID equals qt.EmpID
                      select new Employ()
                      {
                          ID = empl.ID,
                          Name = empl.Name,
                          Address = empl.Address,
                          Email = empl.Email,
                          Mobile = empl.MobileNo,
                          CountryName = co.Name,
                          StateName = st.Name,
                          CityName = ct.name,
                          ProfileImage = empl.profileImage,
                          ResumeFile = empl.resume
                      };
            
            //employ.QualificatoinCert = string.Join(",",emp
            return View(emp);
        }
    }
}