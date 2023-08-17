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
        public IEnumerable<Personal_Datas> GetMydatas()
        {
            return Db.Personal_Datas;
        }

        // GET: api/MydataAPI/5
        [ResponseType(typeof(Personal_Datas))]
        public async Task<IHttpActionResult> GetMydata(int id)
        {
            Personal_Datas mydata = await Db.Personal_Datas.FindAsync(id);
            if (mydata == null)
            {
                return NotFound();
            }
            return Ok(mydata);
        }

        // POST: api/MydataAPI
        public void Post(Personal_Datas mydata)
        {

            mydata.InputDate = DateTime.Now;

            if(mydata.OperationType!=null)
            {
                List<string> Optypes = mydata.OperationType.Split(',').ToList();
                foreach (var item in Optypes)
                {

                    if (item != "")
                    {
                        var optypeid = new MyOperation
                        {

                            MyId = mydata.MyId,
                            OpeTypeId = Convert.ToInt32(item)
                        };
                        Db.MyOperations.Add(optypeid);
                    }

                }
            }


            if (mydata.TreatPlan != null)
            {
                List<string> Treats = mydata.TreatPlan.Split(',').ToList();
                foreach (var item in Treats)
                {
                    if (item != "")
                    {
                        var treatid = new MyTreat //開一新的Mytreat物件
                        {
                            MyId = mydata.MyId,
                            TreatId = Convert.ToInt32(item)
                        };
                        Db.MyTreats.Add(treatid);
                    }

                }
            }
            
            //using (var myOp = new MyOperation())
            //{
           
           
            Db.Personal_Datas.Add(mydata);

            Db.SaveChanges();
        }

        // PUT: api/MydataAPI/5
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: api/MydataAPI/5
        public void Delete(int id)
        {
            Personal_Datas personData = Db.Personal_Datas.FirstOrDefault(p => p.MyId == id);
            Db.Personal_Datas.Remove(personData);
            Db.SaveChanges();
        }
    }
}
