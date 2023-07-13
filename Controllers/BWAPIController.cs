using IbreastCare.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Http;


namespace IbreastCare.Controllers
{
    public class BWAPIController : ApiController
    {
        private IbreastDBEntities Db = new IbreastDBEntities();
        // GET: api/BWAPI
        public IEnumerable<BW> GetBW()
        {
            return this.Db.BWs.ToList();
        }
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/BWAPI/GetHeight/{id}")]
        public IHttpActionResult GetHeight(int id)
        {
           
            try
            {
                var height = Db.Personal_Data.Where(p=>p.UserId==id)
                    .OrderByDescending(p=>p.InputDate)
                    .Select(p=>p.Height)
                    .FirstOrDefault();
                if (height.HasValue)
                {
                    return Ok(height);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
            // GET: api/BWAPI/5
            public string Get(int id)
        {
            return "value";
        }

        // POST: api/BWAPI
        public void Post(BW myBW)
        {
            myBW.InputDate = DateTime.Now;
            if (myBW.MeasureDate == null)
                myBW.MeasureDate = DateTime.Now;
           
            Db.BWs.Add(myBW);

            Db.SaveChanges();
        }

        // PUT: api/BWAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/BWAPI/5
        public void Delete(int id)
        {
        }
    }
}
