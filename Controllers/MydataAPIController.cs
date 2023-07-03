using IbreastCare.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IbreastCare.Controllers
{
    public class MydataAPIController : ApiController
    {
        private IbreastDBEntities Db = new IbreastDBEntities();
        // GET: api/MydataAPI
        public IEnumerable<Personal_Data> Get()
        {
            return this.Db.Personal_Data.ToList();
        }

        // GET: api/MydataAPI/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MydataAPI
        public void Post(Personal_Data mydata)
        {
            Db.Personal_Data.Add(mydata);

            Db.SaveChanges();
        }

        // PUT: api/MydataAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MydataAPI/5
        public void Delete(int id)
        {
        }
    }
}
