using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Kimo.Models;


namespace Kimo.Controllers
{
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class CustomerController : ApiController
    {
        private KIMODBEntities db = new KIMODBEntities();

        // GET: api/Customer
        public IQueryable<Customer> GetCustomers()
        {
            return db.Customers;
        }

        // GET: api/Customer/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customer/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Customer_ID)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customer
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.Customer_ID }, customer);
        }

        // DELETE: api/Customer/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.Customer_ID == id) > 0;
        }

        //see if username exist
        [Route("api/Customer/doesUserExist/{usrName}")]
        [HttpGet]
        public string doesUserExist(string usrName)
        {
            KIMODBEntities db = new KIMODBEntities();
            db.Configuration.ProxyCreationEnabled = false;

            foreach(User usr in db.Users)
            {
                if(usr.UserName == usrName)
                {
                    return "true"; 
                }
            }
            return "false";
        }

        //function to determine if username already exists
        private bool UserExists(string usrName)
        {
            KIMODBEntities db = new KIMODBEntities();
            db.Configuration.ProxyCreationEnabled = false;

            foreach(User usr in db.Users)
            {
                if (usr.UserName == usrName)
                {
                    return true;
                }
            }
            return false;
        }

        [System.Web.Http.Route("api/Customer/registerNewUser")]
        [System.Web.Mvc.HttpPost]
        public HttpResponseMessage RegisterNewUser([FromBody] User user)
        {
            bool uExist = UserExists(user.UserName);
            db.Configuration.ProxyCreationEnabled = false;

            User cUser = new User();
            cUser.UserRoleID = 1;
            var hash = GenerateHash(user.UserPassword);
            cUser.UserPassword = hash;
            cUser.UserName = user.UserName;
            cUser.UserPasswordChangeRequest = false;

            if(uExist == false)
            {
                try
                {
                    db.Users.Add(cUser);
                    db.SaveChanges();
                }
                catch(Exception e)
                {

                }
                var response = Request.CreateResponse(HttpStatusCode.OK, cUser);
                return response;
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, "user Exists");
                return response;
            }
        }

        [System.Web.Http.Route("api/Customer/registerNewCustomerAddress")]
        [System.Web.Mvc.HttpPost]
        public HttpResponseMessage registerNewCustomerAddress([FromBody] Delivery_Address dAddress)
        {
            db.Configuration.ProxyCreationEnabled = false;
            Delivery_Address delAdd = new Delivery_Address();
            delAdd.StreetNumber = dAddress.StreetNumber;
            delAdd.StreetName = dAddress.StreetName;
            delAdd.Postal_Code = dAddress.Postal_Code;
            delAdd.Province = dAddress.Province;
            try
            {
                db.Delivery_Address.Add(delAdd);
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }
            var response = Request.CreateResponse(HttpStatusCode.OK, dAddress);
            return response;
       
        }

        [System.Web.Http.Route("api/Customer/registerNewCustomer")]
        [System.Web.Mvc.HttpPost]
        public bool registerNewCustomer(Customer customer)
        {
           


                db.Configuration.ProxyCreationEnabled = false;

                Customer cus = new Customer();
                cus.First_Name = customer.First_Name;
                cus.Last_Name = customer.Last_Name;
                cus.Email_Address = customer.Email_Address;
                cus.Contact_Number = customer.Contact_Number;


                cus.verified = false;

                Random random = new Random();
                cus.OTP = random.Next(100000, 999999).ToString();

                MailMessage mail = new MailMessage();
                var senderemail = new MailAddress("Kimosystem1@gmail.com");
                var password = "Kimo1234";
                mail.From = new MailAddress("Kimosystem1@gmail.com");
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
              
                //mail.To.Add("shalangt@gmail.com");
                mail.To.Add(customer.Email_Address);
                mail.IsBodyHtml = true;
                mail.Subject = "Please verify your account for KimoSource";
                mail.Body = "Welcome to KimoSource. Your account has successfully been registered with us. Please verify your account using the OTP in the ACCOUNT section on KimoSource to enable checkout on your account. Your OTP is: " + cus.OTP;
            
                 
                SmtpServer.Port = 587;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential(senderemail.Address, password);
            //SmtpServer.Credentials = new NetworkCredential("u17345180@tuks.co.za", "Pringles98");
                SmtpServer.EnableSsl = true;
            

                SmtpServer.Send(mail);
          
            int tempID = 1;

            foreach (User usr in db.Users)
            {
                if (usr.UserID > tempID)
                {
                    tempID = usr.UserID;
                }
            }
            cus.UserID = tempID++;


            int tempID2 = 1;
            foreach (Delivery_Address del in db.Delivery_Address)
            {
                if (del.Address_ID > tempID2)
                {
                    tempID2 = del.Address_ID;
                }
            }
            cus.Address_ID = tempID2;


            try
            {
                db.Customers.Add(cus);
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }



            return true;
        }
        
            //Hashing
            public static string ApplySomeSalt(string input)
            {
                return input + "tepszgjoowiwccuvckqinnimxxjbbmukxompumnmyugjnwehrnldjsdjygjo";
            }

            public static string GenerateHash(string inputString)
            {
            //SHA256 sha256 = SHA256Managed.Create();
            //byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            //byte[] hash = sha256.ComputeHash(bytes);
            //return GetStringFromHash(hash);

                using (SHA256 shaHash = SHA256.Create())
                {
                    byte[] builder = shaHash.ComputeHash(Encoding.UTF8.GetBytes(inputString));
                    StringBuilder myBuilder = new StringBuilder();
                    for(int counter = 0; counter < builder.Length; counter++)
                    {
                        myBuilder.Append(builder[counter].ToString("x2"));
                    }

                    myBuilder.ToString();

                return myBuilder.ToString();
            }
            }

            public static string RandomString(int length)
            {
                Random randoms = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[randoms.Next(s.Length)]).ToArray());
            }

            private static string GetStringFromHash(byte[] hash)
            {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    result.Append(hash[i].ToString("X2"));
                }
                return result.ToString();
            }

        //Customer Login
        [Route("api/Customer/CustomerLogin")]
        [HttpPost]
        public dynamic CustomerLogin([FromBody] User usr)
        {
            //check if user exists
            User checkUserExist = db.Users.Where(usrw => usrw.UserName == usr.UserName).FirstOrDefault();
            if (checkUserExist == null)
            {
                dynamic retEmptyUser = new ExpandoObject();
                retEmptyUser.Message = "Invalid User!";
                return retEmptyUser;
            }

            var hash = GenerateHash(usr.UserPassword);
            User usrr = db.Users.Where(usrw => usrw.UserName == usr.UserName && usrw.UserPassword == hash)
                             .Include(zz => zz.User_Role)
                             .Include(zz => zz.Customers)
                             .FirstOrDefault();
            if (usrr != null)
            {
                Customer customerDetails = db.Customers.Where(zz => zz.UserID == usrr.UserID).FirstOrDefault();

                dynamic user = new ExpandoObject();
                user.Customer_ID = customerDetails.Customer_ID;
                user.UserID = customerDetails.UserID;
                user.Address_ID = customerDetails.Address_ID;
                user.First_Name = customerDetails.First_Name;
                user.Last_Name = customerDetails.Last_Name;
                user.Contact_Number = customerDetails.Contact_Number;
                user.Email_Address = customerDetails.Email_Address;
                //add new columns for verification
                user.verified = customerDetails.verified;

                return user;
            }
            else
            {
                dynamic user = new ExpandoObject();
                user.Message = "Invalid Password!";
                return user;
            }

        }

        [Route("api/Customer/getMD5Hash")]
        [HttpPost]
        public string getMD5Hash(dynamic stringX)
        {
            string newStr = stringX.hashString;
            newStr = newStr.Replace('$', '&');
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(newStr);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }



        private bool setOTP(string usrn, string OTP)
        {
            try
            {
                KIMODBEntities db10 = new KIMODBEntities();
                User usr = db10.Users.Where(y => y.UserName == usrn).FirstOrDefault();
                usr.ResetOTP = OTP;
                usr.UserPasswordChangeRequest = true;
                db10.Entry(usr).State = EntityState.Modified;
                db10.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        //Verify OTP

        [Route("api/Customer/VerifyResetOTP")]
        [HttpPost]
        public bool VerifyResetOTP(dynamic dataX)
        {
            try
            {
                string usr = dataX.usrn;
                string entOTP = dataX.otp;
                User user1 = db.Users.Where(k => k.UserName == usr).FirstOrDefault();
                if (user1.ResetOTP == entOTP)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }


        [Route("api/Customer/SetNewPass")]
        [HttpPost]
        public bool SetNewPass(dynamic pss)
        {
            try
            {
                KIMODBEntities db11 = new KIMODBEntities();
                string usr = pss.usrn;
                User user = db11.Users.Where(k => k.UserName == usr).FirstOrDefault();
                if (user != null)
                {
                    var hash = GenerateHash(pss.pssw.Value); // Do you have to use the word value
                    string newP = hash;
                    user.UserPassword = newP;
                    user.UserPasswordChangeRequest = false;
                    //user.UserPassword = pss.pssw;

                    db11.Entry(user).State = EntityState.Modified;
                    db11.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }

        //Resetpassword
        [Route("api/Customer/RequestPasswordReset/{usrn}")]
        [HttpPost]
        public bool RequestPasswordReset(string usrn)
        {
            try
            {
                User usr = db.Users.Where(x => x.UserName == usrn).FirstOrDefault();
                if (usr != null)
                {
                    string otpString = "";
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("u17345180@tuks.co.za");
                    Customer getEmail = db.Customers.Where(z => z.UserID == usr.UserID).FirstOrDefault();
                    string addressTo = getEmail.Email_Address;
                    mail.To.Add(addressTo);
                    mail.Subject = "Your OTP for you Password Reset Request";
                    if (usr.UserPasswordChangeRequest == false)
                    {
                        Random random = new Random();
                        otpString = random.Next(100000, 999999).ToString();
                        setOTP(usrn, otpString);
                    }
                    else
                    {
                        otpString = usr.ResetOTP;
                    }
                    User usr2 = db.Users.Where(x => x.UserName == usrn).FirstOrDefault();
                    string bod = "Your OTP is: " + otpString;
                    mail.Body = bod;

                    SmtpServer.Port = 587;
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("u17345180@tuks.co.za", "Pringles98");
                    SmtpServer.EnableSsl = false;

                    SmtpServer.Send(mail);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }


    }





    }



