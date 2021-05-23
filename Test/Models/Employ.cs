using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Employ
    {
        public int ID { get; set; }
        [Display(Name = "Employe Name")]
        [Required(ErrorMessage = "{0} is required.")]
        public string Name { get; set; }
        [Display(Name = "Email ID")]
        [Required(ErrorMessage = "{0} is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Mobile No")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
        [Display(Name = "Country Name")]
        [Required(ErrorMessage = "{0} is required.")]
        public int CID { get; set; }
        public string CountryName { get; set; }
        public List<CountryList> CountryList { get; set; }
        [Display(Name = "State Name")]
        [Required(ErrorMessage ="{0} is required.")]
        public int SID { get; set; }
        public string StateName { get; set; }
        public List<StateList> stateList { get; set; }
        [Display(Name ="City Name")]
        [Required(ErrorMessage ="{0} is required.")]
        public int CTID { get; set; }
        public string CityName { get; set; }
        public List<CityList> CityList { get; set; }
        [Display(Name ="Address")]
        [Required(ErrorMessage ="{0} is required.")]
        public string Address { get; set; }
        public HttpPostedFileBase ProfilePic { get; set; }
        public HttpPostedFileBase Resume { get; set; }
        public List<string> QName { get; set; }
        public List<HttpPostedFileBase> Qupload { get; set; }
        [Display(Name ="Profile image")]
        public string ProfileImage { get; set; }
        [Display(Name ="Resume")]
        public string ResumeFile { get; set; }
        public string QualificatoinCert { get; set; }
    }
    public class CountryList
    {
        public int CID { get; set; }
        public string Name { get; set; }
    }
    public class StateList
    {
        public int SID { get; set; }
        public int CID { get; set; }
        public string Name { get; set; }
    }
    public class CityList
    {
        public int CID { get; set; }
        public int SID { get; set; }
        public string Name { get; set; }
    }
}