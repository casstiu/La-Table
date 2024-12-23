using La_Table.Models;
using LaTable.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace La_Table.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Signup()
        {
            return View();
        }
        public ActionResult EmailOTP()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult NoAccountHome()
        {
            return View();
        }
        public ActionResult NoAccountAbout()
        {
            return View();
        }
        public ActionResult NoAccountPromos()
        {
            return View();
        }
        public ActionResult CustomerHome()
        {
            return View();
        }
        public ActionResult CustomerPromos()
        {
            return View();
        }
        public ActionResult CustomerAbout()
        {
            return View();
        }
        public ActionResult CustomerBookings()
        {
            return View();
        }
        public ActionResult AdminDashboard()
        {
            try
            {
                // Check if the session variable exists, otherwise set it to a default value
                if (Session["FirstName"] != null)
                {
                    ViewBag.FirstName = Session["FirstName"].ToString();
                }
                else
                {
                    // If the session is empty, set a default value and store it in the session
                    Session["FirstName"] = "Guest";
                    ViewBag.FirstName = "Guest";
                }
            }
            catch (Exception ex)
            {
                // Handle any errors and display a default value in case of an exception
                Console.WriteLine("Error: " + ex.Message);
                ViewBag.FirstName = "Guest";
            }

            return View();
        }

        public ActionResult AdminAccount()
        {
            return View();
        }
        public ActionResult AdminLogs()
        {
            return View();
        }
        public ActionResult AdminPromos()
        {
            return View();
        }

        private LaTableContext db = new LaTableContext();

        // CHECK EMAIL
        [HttpGet]
        public JsonResult CheckEmailExists(string email)
        {
            var user = db.tblaccounts.FirstOrDefault(a => a.email == email);
            if (user != null)
            {
                return Json(new { exists = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { exists = false }, JsonRequestBehavior.AllowGet);
            }
        }

        // SIGNUP
        [HttpPost]
        public JsonResult RegisterUser(tblAccountsModel user)
        {
            if (ModelState.IsValid)
            {
                db.tblaccounts.Add(user);
                db.SaveChanges();
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        // LOGIN
        [HttpPost]
        public JsonResult Login(string email, string password)
        {
            // CHECK IF EMAIL EXISTS
            var user = db.tblaccounts.SingleOrDefault(u => u.email == email);

            // EMAIL DOESN'T EXIST
            if (user == null)
            {
                return Json(new { success = false, message = "Invalid email." });
            }

            // DOES NOT MATCH
            if (user.password != password)
            {
                return Json(new { success = false, message = "Email and Password does not match." });
            }

            // INACTIVE ACCOUNT
            if (user.StatusID == 2)
            {
                return Json(new { success = false, message = "Your account is inactive. Please verify your email." });
            }

            // ADD TO LOGS "LOGGED IN"
            var log = new tblLogsModel
            {
                AccountID = user.AccountID,
                action = "Logged IN",
                timestamp = DateTime.Now
            };
            db.tbllogs.Add(log);
            db.SaveChanges();

            // STORE SESSION
            Session["AccountID"] = user.AccountID;
            Session["RoleID"] = user.RoleID;
            Session["FirstName"] = user.firstName;
            Session["LastName"] = user.lastName;
            Session["Phone_Num"] = user.Phone_Num;
            Session["Email"] = user.email;
            Session["StatusID"] = user.StatusID;
            Session["CreateAt"] = user.createAt;
            Session["UpdateAt"] = user.updateAt;

            // REDIRECT BASED ON ROLE
            string redirectUrl = "";
            switch (user.RoleID)
            {
                case 1: // ADMIN
                    redirectUrl = "AdminDashboard";
                    break;
                case 2: // EMPLOYEE
                    redirectUrl = "EmployeeDashboard";
                    break;
                case 3: // CUSTOMER
                    redirectUrl = "CustomerHome";
                    break;
            }

            return Json(new { success = true, message = "Login successful!", redirectUrl = redirectUrl });
        }



        // LIST ACCOUNTS
        [HttpGet]
        public JsonResult GetUsers()
        {
            try
            {
                using (var db = new LaTableContext())
                {
                    var accounts = db.tblaccounts.Select(a => new
                    {
                        accountID = a.AccountID,
                        a.firstName,
                        a.lastName,
                        a.Phone_Num,
                        a.email,
                        RoleName = db.tblroles.FirstOrDefault(r => r.RoleID == a.RoleID).roleName,
                        StatusName = db.tblstatus.FirstOrDefault(s => s.StatusID == a.StatusID).statusName
                    }).ToList();

                    return Json(new { success = true, data = accounts }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        // EDIT - UPDATE
        [HttpPost]
        public JsonResult UpdateUser(tblAccountsModel updateData)
        {
            try
            {
                using (var db = new LaTableContext())
                {
                    var account = db.tblaccounts.FirstOrDefault(a => a.AccountID == updateData.AccountID);  // Use AccountID
                    if (account != null)
                    {
                        account.firstName = updateData.firstName;  // Use firstName
                        account.lastName = updateData.lastName;    // Use lastName
                        account.email = updateData.email;          // Use email
                        account.updateAt = DateTime.Now;           // Update timestamp


                        db.SaveChanges();
                        return Json(new { success = true, message = "Account updated successfully!" });
                    }
                    else
                    {
                        return Json(new { success = false, message = $"Account not found for AccountID {updateData.AccountID}" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateLogEntry(tblLogsModel logData)
        {
            try
            {
                using (var db = new LaTableContext())
                {
                    var logEntry = new tblLogsModel
                    {
                        AccountID = logData.AccountID,  // Use the provided AccountID
                        action = logData.action,         // Log action ("Updated" in this case)
                        timestamp = logData.timestamp    // Use the provided timestamp
                    };

                    // Add the log entry to the logs table
                    db.tbllogs.Add(logEntry);
                    db.SaveChanges();

                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        // LIST LOGS
        [HttpGet]
        public JsonResult GetLogs()
        {
            try
            {
                using (var db = new LaTableContext())
                { // ACCOUNT ID = FIRST NAME, LAST NAME
                    var logs = (from log in db.tbllogs
                                join user in db.tblaccounts on log.AccountID equals user.AccountID
                                select new
                                {
                                    log.LogsID,
                                    log.AccountID,
                                    UserFullName = user.firstName + " " + user.lastName,
                                    log.action,
                                    log.timestamp
                                }).ToList();

                    return Json(new { success = true, data = logs }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



    }

}