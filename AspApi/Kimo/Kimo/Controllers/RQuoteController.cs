using Kimo.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Kimo.Controllers
{
    public class RQuoteController : ApiController
    {
        KIMODBEntities db = new KIMODBEntities();
        [System.Web.Http.Route("api/Basket/AddItemsToQuote")]
        [System.Web.Mvc.HttpPost]
        public HttpResponseMessage AddItemsToQuote([FromBody] Quotation_Request_Line ql)
        {
            try
            {
                /*Quotation_Request_Line checkBI = db.Basket_Line.Where(g => g.BasketID == bl.BasketID).FirstOrDefault();
                Basket_Line checkPI = db.Basket_Line.Where(g => g.ProductID == bl.ProductID).FirstOrDefault();
                if (checkPI != null && checkBI != null)
                {
                    var badResponse = Request.CreateResponse(HttpStatusCode.BadRequest, "already in basket");
                    return badResponse;
                }*/
                /*Basket upB = db.Baskets.Where(y => y.BasketID == bl.BasketID).FirstOrDefault();
                upB.BasketStatusID = 2;
                db.Entry(upB).State = EntityState.Modified;
                
                db.SaveChanges();*/

                Quotation_Request_Line reqLine = db.Quotation_Request_Line.SingleOrDefault(g => (g.Request_Line_ID == ql.Request_Line_ID) && (g.Design_ID == ql.Design_ID)) ;
            
                if (reqLine == null)
                {
                  

                    db.Quotation_Request_Line.Add(ql);
                    db.SaveChanges();

                    var response = Request.CreateResponse(HttpStatusCode.OK, ql);
                    response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
                    response.Headers.Add("Access-Control-Allow-Headers", "Origin, Content-Type, X-Auth-Token");
                    return response;
                }
                else
                {
                    var badResponse = Request.CreateResponse(HttpStatusCode.BadRequest, "already in basket");
                    return badResponse;
                }

                /*updateBasketStatus(bl);

                db.Basket_Line.Add(bl);
                db.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.OK, bl);
                response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
                response.Headers.Add("Access-Control-Allow-Headers", "Origin, Content-Type, X-Auth-Token");
                return response;*/
            }
            catch
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, "operation failed!");
                return response;
            }
            /* List<Product> prodList = new List<Product>();
            List<Basket> basket = new List<Basket>();
            Basket_Line newBL = new Basket_Line();
            newBL.BasketID = bl.BasketID;
            newBL.ProductID = bl.ProductID;
            newBL.Quantity = bl.Quantity;
            newBL.Cost = bl.Cost;

            Basket_Line checkBI = db.Basket_Line.Where(g => g.BasketID == newBL.BasketID).FirstOrDefault();
            Basket_Line checkPI = db.Basket_Line.Where(g => g.ProductID == newBL.ProductID).FirstOrDefault();

            if(checkPI!=null && checkBI!=null)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, "already in basket");
                return response;
            } */

            /*Product upP = db.Products.Where(x => x.ProductID == newBL.ProductID).FirstOrDefault();
            upP.QuantityOnHand = (upP.QuantityOnHand - newBL.Quantity);*/

            /* Basket upB = db.Baskets.Where(y => y.BasketID == newBL.BasketID).FirstOrDefault();
            upB.BasketStatusID = 2;

            try
            {
                //db.Entry(upP).State = EntityState.Modified;
                db.Entry(upB).State = EntityState.Modified;
                db.Basket_Line.Add(newBL);
                db.SaveChanges();
                var response = Request.CreateResponse(HttpStatusCode.OK, newBL);
                return response;
            }
            catch(Exception e)
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, "operation failed!");
                return response;
            } */

        }
        [Route("api/Basket/GetQuoteItemDetails/{id}")]
        [HttpGet]
        public List<dynamic> GetQuoteItemData(int id)
        {
            List<dynamic> quoteData = new List<dynamic>();
            List<Quotation_Request_Line> qLine = db.Quotation_Request_Line.Where(zz => zz.Request_Line_ID == id).ToList();
            List<Design> designs = new List<Design>();
            foreach (Quotation_Request_Line quotation_Request_Line in qLine)
            {
                Design des = db.Designs.Where(zz => zz.Design_ID == quotation_Request_Line.Design_ID).FirstOrDefault();
                designs.Add(des);

                dynamic toReturn = new ExpandoObject();
                
              /*  try
                {
                    string filePath = HttpContext.Current.Server.MapPath("~/Images/" + prod.ImageAssetString);
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            fileStream.CopyTo(memoryStream);
                            Bitmap image = new Bitmap(1, 1);
                            image.Save(memoryStream, ImageFormat.Png);

                            byte[] byteImage = memoryStream.ToArray();
                            string base64String = Convert.ToBase64String(byteImage);
                            toReturn.ProductImage = "data:image/png;base64," + base64String;
                        }
                    }
                }
                catch
                {
                    toReturn.ProductImage = "assets/no.png";
                }*/

                toReturn.Design_ID = des.Design_ID;
                
                toReturn.Colour = quotation_Request_Line.Colour;
                toReturn.Web_Link_ID = quotation_Request_Line.Web_Link_ID;
                toReturn.Quantity = quotation_Request_Line.Quantity;
                toReturn.Product_Code = quotation_Request_Line.Product_Code;

                //added the product's QOH for the basket view
                /*toReturn.QuantityOnHand = prod.QuantityOnHand;
                if (toReturn.Quantity > toReturn.QuantityOnHand)
                {
                    toReturn.Quantity = toReturn.QuantityOnHand;
                    Basket_Line upBL = new Basket_Line();
                    upBL.BasketID = id;
                    upBL.ProductID = toReturn.ProductID;
                    upBL.Quantity = toReturn.QuantityOnHand;

                    var newCost = toReturn.Cost / basket_Line.Quantity;
                    newCost *= toReturn.Quantity;

                    toReturn.Cost = newCost;

                    upBL.Cost = newCost;
                    PrivateUpdateQuantity(upBL);
                }*/

                quoteData.Add(toReturn);
            }



            return quoteData;
        }
        [Route("api/Basket/RemoveItem/{id}/{bId}")]
        [HttpGet]
        public IHttpActionResult RemoveItem(int id, int bId)
        {
            try
            {
                Quotation_Request_Line delLine = db.Quotation_Request_Line.Where(s => (s.Design_ID == id) && (s.Request_Line_ID == bId)).FirstOrDefault();
                db.Entry(delLine).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest("Operation Failed");
            }

        }
        /*[System.Web.Http.Route("api/Basket/UpdateQuantity")]
        [System.Web.Mvc.HttpPost]
        public HttpResponseMessage UpdateQuantity(Quotation_Request_Line bLine)
        {
            try
            {
                Quotation_Request_Line tLine = db.Quotation_Request_Line.Where(zz => zz.Request_Line_ID == bLine.Request_Line_ID && zz.Design_ID == bLine.Design_ID).FirstOrDefault();
                var cost = tLine.Cost;
                cost /= tLine.Quantity;
                var newCost = cost * bLine.Quantity;

                tLine.Quantity = bLine.Quantity;
                tLine.Cost = newCost;
                db.Entry(tLine).State = EntityState.Modified;
                db.SaveChanges();

                var response = Request.CreateResponse(HttpStatusCode.OK, "operation succeded!");
                return response;
            }
            catch
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, "operation failed!");
                return response;
            }
        }*/

    }
}
