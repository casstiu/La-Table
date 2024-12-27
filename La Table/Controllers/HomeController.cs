using La_Table.Models;
using LaTable.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace La_Table.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Signup()
        {
            return View();
        }
        public ActionResult VerifyOTP()
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
            try
            {
                if (Session["AccountID"] != null)
                {
                    ViewBag.AccountID = Session["AccountID"].ToString();
                    ViewBag.FirstName = Session["FirstName"];
                    ViewBag.LastName = Session["LastName"];
                    ViewBag.Phone_Num = Session["Phone_Num"];
                    ViewBag.Email = Session["Email"];
                }
                else
                {
                    Session["AccountID"] = "Guest";
                    ViewBag.FirstName = "Guest";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                ViewBag.AccountID = "Guest";
            }

            return View();
        }
        public ActionResult CustomerPromos()
        {
            using (var db = new LaTableContext())
            {
                var promos = db.tblpromo.ToList();
                ViewBag.Promos = promos;
            }
            try
            {
                if (Session["AccountID"] != null)
                {
                    ViewBag.AccountID = Session["AccountID"].ToString();
                    ViewBag.FirstName = Session["FirstName"];
                    ViewBag.LastName = Session["LastName"];
                    ViewBag.Phone_Num = Session["Phone_Num"];
                    ViewBag.Email = Session["Email"];
                }
                else
                {
                    Session["AccountID"] = "Guest";
                    ViewBag.FirstName = "Guest";
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                ViewBag.AccountID = "Guest";
            }

            return View();
        }

        public ActionResult CustomerAbout()
        {
            try
            {
                if (Session["AccountID"] != null)
                {
                    ViewBag.AccountID = Session["AccountID"].ToString();
                    ViewBag.FirstName = Session["FirstName"];
                    ViewBag.LastName = Session["LastName"];
                    ViewBag.Phone_Num = Session["Phone_Num"];
                    ViewBag.Email = Session["Email"];
                }
                else
                {
                    Session["AccountID"] = "Guest";
                    ViewBag.FirstName = "Guest";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                ViewBag.AccountID = "Guest";
            }

            return View();
        }
        public ActionResult CustomerBookings()
        {
            try
            {
                if (Session["AccountID"] != null)
                {
                    var userId = (int)Session["AccountID"];

                    var currentBookings = db.tblreservations
                        .Where(r => r.AccountID == userId && r.ReservationDate >= DateTime.Now)
                        .OrderBy(r => r.ReservationDate)
                        .ToList();

                    var pastBookings = db.tblreservations
                        .Where(r => r.AccountID == userId && r.ReservationDate < DateTime.Now)
                        .OrderByDescending(r => r.ReservationDate)
                        .ToList();

                    ViewBag.CurrentBookings = currentBookings;
                    ViewBag.PastBookings = pastBookings;

                    ViewBag.AccountID = Session["AccountID"].ToString();
                    ViewBag.FirstName = Session["FirstName"];
                    ViewBag.LastName = Session["LastName"];
                    ViewBag.Phone_Num = Session["Phone_Num"];
                    ViewBag.Email = Session["Email"];
                }
                else
                {
                    Session["AccountID"] = "Guest";
                    ViewBag.FirstName = "Guest";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                ViewBag.AccountID = "Guest";
            }

            return View();
        }


        public ActionResult AdminDashboard()
        {
            try
            {
                if (Session["FirstName"] != null)
                {
                    ViewBag.FirstName = Session["FirstName"].ToString();
                    ViewBag.AccountID = Session["AccountID"].ToString();
                    ViewBag.RoleID = Session["RoleID"];
                    ViewBag.LastName = Session["LastName"];
                    ViewBag.Phone_Num = Session["Phone_Num"];
                    ViewBag.Email = Session["Email"];

                    int roleId = Convert.ToInt32(Session["RoleID"]);
                    using (var db = new LaTableContext())
                    {
                        var roleName = db.tblroles
                            .Where(r => r.RoleID == roleId)
                            .Select(r => r.roleName)
                            .FirstOrDefault();

                        ViewBag.RoleName = roleName ?? "Unknown Role";

                    }
                }
                else
                {
                    Session["FirstName"] = "Guest";
                    ViewBag.FirstName = "Guest";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                ViewBag.FirstName = "Guest";
            }

            return View();
        }

        public ActionResult AdminAccount()
        {
            try
            {
                if (Session["AccountID"] != null)
                {
                    ViewBag.AccountID = Session["AccountID"].ToString();
                    ViewBag.RoleID = Session["RoleID"];
                    ViewBag.FirstName = Session["FirstName"];
                    ViewBag.LastName = Session["LastName"];
                    ViewBag.Phone_Num = Session["Phone_Num"];
                    ViewBag.Email = Session["Email"];

                    int roleId = Convert.ToInt32(Session["RoleID"]);
                    using (var db = new LaTableContext())
                    {
                        var roleName = db.tblroles
                            .Where(r => r.RoleID == roleId)
                            .Select(r => r.roleName)
                            .FirstOrDefault();

                        ViewBag.RoleName = roleName ?? "Unknown Role";

                    }
                }
                else
                {
                    Session["AccountID"] = "Guest";
                    ViewBag.FirstName = "Guest";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                ViewBag.AccountID = "Guest";
            }

            return View();
        }
        public ActionResult AdminLogs()
        {
            try
            {
                if (Session["AccountID"] != null)
                {
                    ViewBag.AccountID = Session["AccountID"].ToString();
                    ViewBag.RoleID = Session["RoleID"];
                    ViewBag.FirstName = Session["FirstName"];
                    ViewBag.LastName = Session["LastName"];
                    ViewBag.Phone_Num = Session["Phone_Num"];
                    ViewBag.Email = Session["Email"];

                    int roleId = Convert.ToInt32(Session["RoleID"]);
                    using (var db = new LaTableContext())
                    {
                        var roleName = db.tblroles
                            .Where(r => r.RoleID == roleId)
                            .Select(r => r.roleName)
                            .FirstOrDefault();

                        ViewBag.RoleName = roleName ?? "Unknown Role";

                    }
                }
                else
                {
                    Session["AccountID"] = "Guest";
                    ViewBag.FirstName = "Guest";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                ViewBag.AccountID = "Guest";
            }

            return View();
        }
        public ActionResult AdminPromos()
        {
            try
            {
                if (Session["AccountID"] != null)
                {
                    ViewBag.AccountID = Session["AccountID"].ToString();
                    ViewBag.RoleID = Session["RoleID"];
                    ViewBag.FirstName = Session["FirstName"];
                    ViewBag.LastName = Session["LastName"];
                    ViewBag.Phone_Num = Session["Phone_Num"];
                    ViewBag.Email = Session["Email"];

                    int roleId = Convert.ToInt32(Session["RoleID"]);
                    using (var db = new LaTableContext())
                    {
                        var roleName = db.tblroles
                            .Where(r => r.RoleID == roleId)
                            .Select(r => r.roleName)
                            .FirstOrDefault();

                        ViewBag.RoleName = roleName ?? "Unknown Role";

                    }
                }
                else
                {
                    Session["AccountID"] = "Guest";
                    ViewBag.FirstName = "Guest";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                ViewBag.AccountID = "Guest";
            }

            return View();
        }
        public ActionResult AdminBookings()
        {
            try
            {
                if (Session["AccountID"] != null)
                {
                    ViewBag.AccountID = Session["AccountID"].ToString();
                    ViewBag.RoleID = Session["RoleID"];
                    ViewBag.FirstName = Session["FirstName"];
                    ViewBag.LastName = Session["LastName"];
                    ViewBag.Phone_Num = Session["Phone_Num"];
                    ViewBag.Email = Session["Email"];

                    int roleId = Convert.ToInt32(Session["RoleID"]);
                    using (var db = new LaTableContext())
                    {
                        var roleName = db.tblroles
                            .Where(r => r.RoleID == roleId)
                            .Select(r => r.roleName)
                            .FirstOrDefault();

                        ViewBag.RoleName = roleName ?? "Unknown Role";

                    }
                }
                else
                {
                    Session["AccountID"] = "Guest";
                    ViewBag.FirstName = "Guest";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                ViewBag.AccountID = "Guest";
            }

            return View();
        }
        public ActionResult AdminTables()
        {
            try
            {
                if (Session["AccountID"] != null)
                {
                    ViewBag.AccountID = Session["AccountID"].ToString();
                    ViewBag.RoleID = Session["RoleID"];
                    ViewBag.FirstName = Session["FirstName"];
                    ViewBag.LastName = Session["LastName"];
                    ViewBag.Phone_Num = Session["Phone_Num"];
                    ViewBag.Email = Session["Email"];

                    int roleId = Convert.ToInt32(Session["RoleID"]);
                    using (var db = new LaTableContext())
                    {
                        var roleName = db.tblroles
                            .Where(r => r.RoleID == roleId)
                            .Select(r => r.roleName)
                            .FirstOrDefault();

                        ViewBag.RoleName = roleName ?? "Unknown Role";

                    }
                }
                else
                {
                    Session["AccountID"] = "Guest";
                    ViewBag.FirstName = "Guest";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                ViewBag.AccountID = "Guest";
            }

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

            // ADD TO LOGS = LOGGED IN
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
            return Json(new { success = true, message = "Login successful!", redirectUrl });
        }

        // LIST ACCOUNTS
        [HttpGet]
        public JsonResult GetUsers()
        {
            try
            {
                using (var db = new LaTableContext())
                { // ROLE ID = ROLE NAME, STATUS ID = STATUS NAME
                    var accounts = db.tblaccounts.Where(t => t.StatusID != 5).Select(a => new
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

        // ADD ACCOUNT
        [HttpPost]
        public JsonResult AddAccount(tblAccountsModel user)
        {
            try
            {
                var currentUserID = Session["AccountID"];
                if (currentUserID == null)
                {
                    return Json(new { success = false, message = "You must be logged in to add an account." });
                }

                if (ModelState.IsValid)
                {
                    var existingUser = db.tblaccounts.FirstOrDefault(a => a.email == user.email);
                    if (existingUser != null)
                    {
                        return Json(new { success = false, message = "Email already registered!" });
                    }

                    // ADD THE ACCOUNT
                    db.tblaccounts.Add(user);
                    db.SaveChanges();

                    var newUserId = user.AccountID;

                    // ADD TO LOGS - CREATED ACCOUNT
                    var log = new tblLogsModel
                    {
                        AccountID = (int)currentUserID,
                        action = $"Created Account ID {newUserId}",
                        timestamp = DateTime.Now
                    };
                    db.tbllogs.Add(log);
                    db.SaveChanges();

                    return Json(new { success = true, message = "Account Added Successfully!" });
                }
                return Json(new { success = false, message = "Invalid data." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        // EDIT ACCOUNT
        public void updateAcc(int editID, String editFName, String editLName, String editPhone, String editEmail)
        {
            var currentUserID = Session["AccountID"];
            using (var db = new LaTableContext())
            {
                var exists = db.tblaccounts.Where(x => x.AccountID == editID).FirstOrDefault();
                if (exists != null)
                {
                    exists.firstName = editFName;
                    exists.lastName = editLName;
                    exists.Phone_Num = editPhone;
                    exists.email = editEmail;
                    exists.updateAt = DateTime.Now;
                    db.SaveChanges();
                }
                // ADD TO LOGS = EDIT ACCOUNT
                var log = new tblLogsModel
                {
                    AccountID = (int)currentUserID,
                    action = $"Edited Account ID {editID}",
                    timestamp = DateTime.Now
                };
                db.tbllogs.Add(log);
                db.SaveChanges();
            }
        }

        // DELETE ACCOUNT
        [HttpPost]
        public JsonResult DeleteUser(int AccountID)
        {
            try
            {
                var currentUserID = Session["AccountID"];
                if (currentUserID == null)
                {
                    return Json(new { success = false, message = "You must be logged in to delete a user." });
                }

                using (var db = new LaTableContext())
                {
                    var account = db.tblaccounts.FirstOrDefault(u => u.AccountID == AccountID);
                    if (account != null)
                    {
                        account.StatusID = 5;
                        db.SaveChanges();

                        // ADD TO LOGS = DELETED USER
                        var log = new tblLogsModel
                        {
                            AccountID = (int)currentUserID,
                            action = $"Deleted Account ID {AccountID}",
                            timestamp = DateTime.Now
                        };
                        db.tbllogs.Add(log);
                        db.SaveChanges();

                        return Json(new { success = true, message = "User has been deleted successfully!" });
                    }
                    else
                    {
                        return Json(new { success = false, message = $"User with ID {AccountID} not found!" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
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

        // LIST PROMOS
        [HttpGet]
        public JsonResult GetPromos()
        {
            try
            {
                using (var db = new LaTableContext())
                {
                    var promo = db.tblpromo.Where(t => t.StatusID != 5).Select(p => new
                    {
                        PromoID = p.PromoID,
                        p.promoImage,
                        p.promoName,
                        p.description,
                        p.start_date,
                        p.end_date,
                    }).ToList();

                    return Json(new { success = true, data = promo }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // ADD PROMO
        [HttpPost]
        public JsonResult AddPromo(tblPromoModel promo, HttpPostedFileBase promoImageFile)
        {
            try
            {
                var currentUserID = Session["AccountID"];
                if (currentUserID == null)
                {
                    return Json(new { success = false, message = "You must be logged in to add a promo." });
                }

                try
                {
                    if (promoImageFile == null || promoImageFile.ContentLength == 0)
                    {
                        return Json(new { success = false, message = "Promo Image is required." });
                    }

                    string uploadsFolder = Server.MapPath("~/Images");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string fileName = Path.GetFileName(promoImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    promoImageFile.SaveAs(filePath);

                    promo.promoImage = "~/Images/" + fileName;
                }
                catch (Exception imageEx)
                {
                    return Json(new { success = false, message = "Error saving the promo image: " + imageEx.Message });
                }


                // ADD PROMO
                if (ModelState.IsValid)
                {
                    promo.createAt = DateTime.Now;
                    promo.updateAt = DateTime.Now;
                    db.tblpromo.Add(promo);
                    db.SaveChanges();

                    var newPromoID = promo.PromoID;

                    // ADD TO LOGS = CREATED PROMO
                    var log = new tblLogsModel
                    {
                        AccountID = (int)currentUserID,
                        action = $"Created Promo ID {newPromoID}",
                        timestamp = DateTime.Now
                    };
                    db.tbllogs.Add(log);
                    db.SaveChanges();

                    return Json(new { success = true, message = "Promo Added Successfully!" });
                }
                return Json(new { success = false, message = "Invalid data." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        // EDIT PROMO
        [HttpPost]
        public JsonResult UpdatePromo(int promoID, string promoName, string description, DateTime startDate, DateTime endDate)
        {
            try
            {
                var currentUserID = Session["AccountID"];
                if (currentUserID == null)
                {
                    return Json(new { success = false, message = "You must be logged in to update a promo." });
                }

                using (var db = new LaTableContext())
                {
                    var promo = db.tblpromo.FirstOrDefault(x => x.PromoID == promoID);
                    if (promo == null)
                    {
                        return Json(new { success = false, message = "Promo not found." });
                    }

                    promo.promoName = promoName;
                    promo.description = description;
                    promo.start_date = startDate;
                    promo.end_date = endDate;
                    promo.updateAt = DateTime.Now;
                    db.SaveChanges();

                    // Add to logs
                    var log = new tblLogsModel
                    {
                        AccountID = (int)currentUserID,
                        action = $"Edited Promo ID {promoID}",
                        timestamp = DateTime.Now
                    };
                    db.tbllogs.Add(log);
                    db.SaveChanges();

                    return Json(new { success = true, message = "Promo updated successfully!" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        // DELETE PROMO
        [HttpPost]
        public JsonResult DeletePromo(int PromoID)
        {
            try
            {
                var currentUserID = Session["AccountID"];
                if (currentUserID == null)
                {
                    return Json(new { success = false, message = "You must be logged in to delete a promo." });
                }

                using (var db = new LaTableContext())
                {
                    var promo = db.tblpromo.FirstOrDefault(u => u.PromoID == PromoID);
                    if (promo != null)
                    {
                        promo.StatusID = 5;  // 2 = DELETED
                        db.SaveChanges();

                        // ADD TO LOGS = DELETED PROMO
                        var log = new tblLogsModel
                        {
                            AccountID = (int)currentUserID,
                            action = $"Deleted Promo ID {PromoID}",
                            timestamp = DateTime.Now
                        };
                        db.tbllogs.Add(log);
                        db.SaveChanges();

                        return Json(new { success = true, message = "User has been deactivated successfully!" });
                    }
                    else
                    {
                        return Json(new { success = false, message = $"User with ID {PromoID} not found!" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        // LIST BOOKINGS
        [HttpGet]
        public JsonResult GetBookings()
        {
            try
            {
                using (var db = new LaTableContext())
                {
                    var rawBookings = (from booking in db.tblreservations
                                       join user in db.tblaccounts on booking.AccountID equals user.AccountID
                                       join status in db.tblstatus on booking.StatusID equals status.StatusID
                                       select new
                                       {
                                           booking.ReservationID,
                                           UserFullName = user.firstName + " " + user.lastName,
                                           booking.TableID,
                                           booking.ReservationDate,
                                           booking.ReservationTime,
                                           booking.NumofGuests,
                                           StatusName = status.statusName
                                       }).ToList();

                    var bookings = rawBookings.Select(booking => new
                    {
                        booking.ReservationID,
                        booking.UserFullName,
                        booking.TableID,
                        ReservationDate = booking.ReservationDate.ToString("yyyy-MM-dd"),
                        ReservationTime = booking.ReservationTime.ToString("hh:mm tt"),
                        booking.NumofGuests,
                        booking.StatusName
                    }).ToList();

                    return Json(new { success = true, data = bookings }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // LIST TABLES
        [HttpGet]
        public JsonResult GetTables()
        {
            try
            {
                using (var db = new LaTableContext())
                {
                    var table = db.tbltable.Where(t => t.StatusID != 5).Select(t => new
                    {
                        TableID = t.TableID,
                        t.SeatingCap,
                    }).ToList();

                    return Json(new { success = true, data = table }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // ADD TABLE
        [HttpPost]
        public JsonResult AddTable(tblTableModel table)
        {
            try
            {
                var currentUserID = Session["AccountID"];
                if (currentUserID == null)
                {
                    return Json(new { success = false, message = "You must be logged in to add a table." });
                }

                if (ModelState.IsValid)
                {
                    // ADD THE TABLE
                    db.tbltable.Add(table);
                    db.SaveChanges();

                    var newTableID = table.TableID;

                    // ADD TO LOGS = CREATED TABLE
                    var log = new tblLogsModel
                    {
                        AccountID = (int)currentUserID,
                        action = $"Created Table ID {newTableID}",
                        timestamp = DateTime.Now
                    };
                    db.tbllogs.Add(log);
                    db.SaveChanges();

                    return Json(new { success = true, message = "Table Added Successfully!" });
                }
                return Json(new { success = false, message = "Invalid data." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        // EDIT TABLE
        public void updateTable(int editID, int editSeatcap)
        {
            var currentUserID = Session["AccountID"];
            using (var db = new LaTableContext())
            {
                var exists = db.tbltable.Where(x => x.TableID == editID).FirstOrDefault();
                if (exists != null)
                {
                    exists.SeatingCap = editSeatcap;
                    exists.updateAt = DateTime.Now;
                    db.SaveChanges();
                }
                // ADD TO LOGS = EDIT TABLE
                var log = new tblLogsModel
                {
                    AccountID = (int)currentUserID,
                    action = $"Edited Table ID {editID}",
                    timestamp = DateTime.Now
                };
                db.tbllogs.Add(log);
                db.SaveChanges();
            }
        }

        // DELETE TABLE
        [HttpPost]
        public JsonResult DeleteTable(int TableID)
        {
            try
            {
                var currentUserID = Session["AccountID"];
                if (currentUserID == null)
                {
                    return Json(new { success = false, message = "You must be logged in to delete a table." });
                }

                using (var db = new LaTableContext())
                {
                    var table = db.tbltable.FirstOrDefault(u => u.TableID == TableID);
                    if (table != null)
                    {
                        table.StatusID = 5;  // 2 = DELETED
                        db.SaveChanges();

                        // ADD TO LOGS = DELETED TABLE
                        var log = new tblLogsModel
                        {
                            AccountID = (int)currentUserID,
                            action = $"Deleted Table ID {TableID}",
                            timestamp = DateTime.Now
                        };
                        db.tbllogs.Add(log);
                        db.SaveChanges();

                        return Json(new { success = true, message = "User has been deactivated successfully!" });
                    }
                    else
                    {
                        return Json(new { success = false, message = $"User with ID {TableID} not found!" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        // MAIN BOOKING TRIAL

        [HttpPost]
        public JsonResult ReserveTable(tblReservationsModel reservation)
        {
            var user = Session["AccountID"];
            if (user == null)
            {
                return Json(new { success = false, message = "User is not logged in." });
            }

            reservation.AccountID = (int)user; 
            reservation.StatusID = 6;
            reservation.createAt = DateTime.Now;
            reservation.updateAt = DateTime.Now;

            var log = new tblLogsModel
            {
                AccountID = (int)user,
                action = $"Reserved a Booking",
                timestamp = DateTime.Now
            };
            db.tbllogs.Add(log);
            db.SaveChanges();

            if (reservation.NumofGuests < 1 || reservation.NumofGuests > 9)
            {
                return Json(new { success = false, message = "Number of guests must be between 1 and 9." });
            }

            if (reservation.ReservationDate < DateTime.Now)
            {
                return Json(new { success = false, message = "Reservation date must be in the future." });
            }

            db.tblreservations.Add(reservation);
            db.SaveChanges();

            return Json(new { success = true, message = "Reservation successfully made and is pending." });
        }



    }
}