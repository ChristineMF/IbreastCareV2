using IbreastCare.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace IbreastCare.ViewModel
{
    public class MydataAPIController : ApiController
    {
        private IbreastDBEntities Db = new IbreastDBEntities();
        // GET: api/MydataAPI
        public IEnumerable<Personal_Data> GetMydatas()
        {
            return Db.Personal_Data;
        }

        // GET: api/MydataAPI/5
        [ResponseType(typeof(Personal_Data))]
        public async Task<IHttpActionResult> GetMydata(int id)
        {
            Personal_Data mydata = await Db.Personal_Data.FindAsync(id);
            if (mydata == null)
            {
                return NotFound();
            }
            return Ok(mydata);
        }

        // POST: api/MydataAPI
        public void Post(Personal_Data mydata)
        {

            mydata.InputDate = DateTime.Now;

            List<string> Optypes = mydata.OperationType.Split(',').ToList();
            List<string> Treats = mydata.TreatPlan.Split(',').ToList();

            //using (var myOp = new MyOperation())
            //{
            foreach (var item in Optypes)
            {

                if (item !="")
                {
                    var optypeid = new MyOperation
                    {

                        MyId = mydata.MyId,
                        OpeTypeId = Convert.ToInt32(item)
                    };
                    Db.MyOperations.Add(optypeid);
                }
             
            }
            foreach (var item in Treats)
            {
                if (item !="")
                {
                    var treatid = new MyTreat //開一新的Mytreat物件
                    {
                        MyId = mydata.MyId,
                        TreatId = Convert.ToInt32(item)
                    };
                    Db.MyTreats.Add(treatid);
                }
               
            }
            Db.Personal_Data.Add(mydata);

            Db.SaveChanges();
        }

        // PUT: api/MydataAPI/5
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: api/MydataAPI/5
        public void Delete(int id)
        {
            Personal_Data personData = Db.Personal_Data.FirstOrDefault(p => p.MyId == id);
            Db.Personal_Data.Remove(personData);
            Db.SaveChanges();
        }
    }
}
