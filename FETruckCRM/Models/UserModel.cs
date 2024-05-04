using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace FETruckCRM.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "Employee Code  is required")]
        [Display(Name = "Employee Code: ")]
        public string EmployeeCode { get; set; }

        //[Required(ErrorMessage = "Username is required")]
        [StringLength(50)]
        [Display(Name = "Username: ")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string oldPassword { get; set; }
        public int[] strFormid { get; set; }
        public string hdnSelectedSiteIds { get; set; }
        public int[] SelectedOpsManagers { get; set; }
        public string hdnSelectedOpsManagers { get; set; }

        

        //[Required(ErrorMessage = "Form Permission is required")]
        public string strFormids { get; set; }

        public List<SelectListItem> UserList { get; set; }
        public List<SelectListItem> GroupList { get; set; }
        public List<SelectListItem> TeamLeadList { get; set; }
        public List<SelectListItem> TeamManagerList { get; set; }
        public List<SelectListItem> OpsTeamManagerList { get; set; }
        
        public List<SelectListItem> StatusList { get; set; }
        public List<SelectListItem> RoleList { get; set; }
        public List<SelectListItem> EmployeeTypeList { get; set; }
        public List<SelectListItem> SiteList { get; set; }
        public int[] SelectedSiteIds { get; set; }
        public List<SelectListItem> FormList { get; set; }
        public List<SelectListItem> GenderList { get; set; }
        public Int64 RowNo { get; set; }
        public Int64 UserID { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(100)]
        public string LastName { get; set; }
        public bool IsDeletedInd { get; set; }
        public DateTime CreatedDate { get; set; }
        public string strCreatedDate { get; set; }
        public Int64 createdBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Int64 LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(200)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email ID: ")]
        public string EmailId { get; set; }
        public string RoleName { get; set; }
       // public int RoleID { get; set; }

        [Required(ErrorMessage = "Phone is required")]

        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Phone is not valid.")]
        public string Phone { get; set; }
        public Int64 OfficeID { get; set; }
        public bool Status { get; set; }
        public string Role { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public string strStatus { get; set; }
        public string strTeamLead { get; set; }
        
        public string strTeamManager { get; set; }
        public string Fax { get; set; }
        public int RoleID { get; set; }
        [Required(ErrorMessage = "User group is required")]
        public string strRoleID { get; set; }
        public Int64 GroupID { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string strGroupID { get; set; }
        public Int64 TeamLead { get; set; }
        public Int64 TeamManager { get; set; }
        public string strReportingManager { get; set; }
        public string Alias { get; set; }
        [Required(ErrorMessage = "Employee Type is required")]
        public string EmployeeType { get; set; }
       // [Required(ErrorMessage = "Site is required")]
        public string SiteID { get; set; }
        public string SiteName { get; set; }
        public Int64 CreatedByID { get; set; }
        public Int64 LastModifiedByID { get; set; }
        public bool Isdeleted { get; set; }
        public string OTP { get; set; }
        public DateTime OTPDateTime { get; set; }

    }

    public class ChangePasswordModel
    {

        //[Required(ErrorMessage = "Username is required")]
        [Required(ErrorMessage = "Email is required")]
        [StringLength(200)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email ID: ")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Old Password is required")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 6)]
        [Display(Name = "Old Password: ")]
        public string OldPassword { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 6)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        [StringLength(15, MinimumLength = 6)]
        [Display(Name = "Confirm Password: ")]
        public string ConfirmPassword { get; set; }
        public Int64 UserID { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Display(Name = "Phone")]
        [StringLength(15, MinimumLength = 6)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid phone number. Phone number should be of 10 digits")]
        public string Phone { get; set; }

    }

    public class CheckEmailModel
    {

        //[Required(ErrorMessage = "Username is required")]
        [Required(ErrorMessage = "Email is required")]
        [StringLength(200)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email ID: ")]
        public string EmailId { get; set; }



    }

    public class ResetPasswordModel
    {


        public string UserID { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 6)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        [StringLength(15, MinimumLength = 6)]
        [Display(Name = "Confirm Password: ")]
        public string ConfirmPassword { get; set; }



    }


    public class GroupFormPermissionList
    {
        public List<FormModel> FormPermissionList { get; set; }
    }
    public class GroupFormPermission
    {
        public List<SelectListItem> RoleList { get; set; }
        public List<SelectListItem> FormList { get; set; }
        public long GroupFormID { get; set; }
        public int RoleID { get; set; }
        [Required(ErrorMessage = "User group is required")]
        public string strRoleID { get; set; }
        public int FormID { get; set; }
        [Required(ErrorMessage = "Form is required")]
        public string strFormid { get; set; }
        public string FormName { get; set; }
        public string strFormids { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedByID { get; set; }
        public bool IsFormPermissions { get; set; }

    }



    public class FormModel
    {
        public int FormID { get; set; }
        public string FormName { get; set; }
        public int FormLevel { get; set; }
        public int ParentID { get; set; }
        public bool Status { get; set; }
        public bool IsFormPermissions { get; set; }

    }

    public class UserCustomerModel
    {
        public UserCustomerModel()
        {
            UserList = new List<SelectListItem>();
            UserList.Add(new SelectListItem() { Text = "Select User", Value = "0" });
            CustomerList = new List<SelectListItem>();
            CustomerList.Add(new SelectListItem() { Text = "Select Customer", Value = "0" });
        }
        public List<SelectListItem> UserList{ get; set; }

        public List<SelectListItem> CustomerList { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select user!")]
        [Display(Name = "Select User")]
        public int SelectedUserId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select customer!")]
        [Display(Name = "Select Customer")]
        public int SelectedCustomerId { get; set; }

    }
}