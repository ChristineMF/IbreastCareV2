using IbreastCare.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
