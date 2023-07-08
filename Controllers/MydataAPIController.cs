using IbreastCare.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IbreastCare.ViewModel
{
    public class MydataAPIController : ApiController
    {
        private IbreastDBEntities Db = new IbreastDBEntities();
        // GET: api/MydataAPI
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MydataAPI/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MydataAPI
        public void Post(Personal_Data mydata)
        {
            //mydata.UserId = Session["UserId"];
            mydata.InputDate = DateTime.Now;

           

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
