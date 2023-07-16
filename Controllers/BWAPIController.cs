using IbreastCare.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Http;
using IbreastCare.Models;




namespace IbreastCare.Controllers
{
    public class BWAPIController : ApiController
    {
        private IbreastDBEntities Db = new IbreastDBEntities();
        // GET: api/BWAPI
        public IEnumerable<BWDTO> Get(int? id)
        {


            return this.Db.BWs
                .Where(p => p.UserId == id)
                 .Select(p => new BWDTO
                 {
                     BWId = p.BWId,
                     BW1 = p.BW1,
                     BMI = p.BMI,
                     InputDate = p.InputDate,
                     MeasureDate = p.MeasureDate

                 })
                .OrderByDescending(p => p.BWId).ToList()
            .Select(p => new BWDTO
            {
                BW1 = p.BW1,
                BMI = p.BMI,
                InputDate = p.InputDate.Date,
                MeasureDate = p.MeasureDate
            }).OrderByDescending(p => p.BWId);

            //return this.Db.BWs.ToList();
        }
        //public BWDataResult Get(int? id)
        //{
        //    var measureArray = Db.BWs.Where(r => r.MeasureDate != null).Select(r => r.MeasureDate).ToArray();
        //    var bwArray = Db.BWs.Where(r => r.BW1 != null).Select(r => r.BW1).ToArray();
        //    var bmiArray = Db.BWs.Where(r => r.BMI != null).Select(r => r.BMI).ToArray();

        //    var dataMeasure = string.Join(",", measureArray);
        //    var dataBW = string.Join(",", bwArray);
        //    var dataBMI = string.Join(",", bmiArray);

        //    var bwList = this.Db.BWs
        //        .Where(p => p.UserId == id)
        //        .Select(p => new BWDTO
        //        {
        //            BWId = p.BWId,
        //            BW1 = p.BW1,
        //            BMI = p.BMI,
        //            InputDate = p.InputDate,
        //            MeasureDate = p.MeasureDate
        //        })
        //        .OrderByDescending(p => p.BWId)
        //        .ToList()
        //        .Select(p => new BWDTO
        //        {
        //            BWId = p.BWId,
        //            BW1 = p.BW1,
        //            BMI = p.BMI,
        //            InputDate = p.InputDate.Date,
        //            MeasureDate = p.MeasureDate
        //        });

        //    var result = new BWDataResult
        //    {
        //        DataMeasure = dataMeasure,
        //        DataBW = dataBW,
        //        DataBMI= dataBMI,
        //        BWList = bwList
        //    };

        //    return result;
        //}


        //[System.Web.Http.HttpGet]
        //[System.Web.Http.Route("api/BWAPI/GetHeight")]
        //public IHttpActionResult GetHeight( int id)
        //{

        //    try
        //    {
        //        var height = Db.Personal_Data.Where(p=>p.UserId==id)
        //            .OrderByDescending(p=>p.MyId)
        //            .Select(p=>p.Height)
        //            .FirstOrDefault();
        //        if (height.HasValue)
        //        {
        //            return Ok(height);
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}
        //    // GET: api/BWAPI/5
        //    public string GetB(int id)
        //{


        //    return "value";
        //}

        // POST: api/BWAPI
        public void Post(BW myBW)
        {
            myBW.InputDate = DateTime.Now;
            if (myBW.MeasureDate.Year == 0001)
                myBW.MeasureDate = DateTime.Now;

            Db.BWs.Add(myBW);

            Db.SaveChanges();
        }

        // PUT: api/BWAPI/5
        public void Put( [FromBody] BW myBW)
        {
            var item = Db.BWs.Find( myBW.BWId);
            if (item != null)
            {
                item.InputDate = DateTime.Now;
                if (myBW.MeasureDate.Year == 0001)
                    item.MeasureDate = DateTime.Now;
                else
                    item.MeasureDate = myBW.MeasureDate;
                item.BMI = myBW.BMI;
                item.BW1 = myBW.BW1;
                item.BWId = myBW.BWId;
                item.UserId = myBW.UserId;
            }
            

            Db.SaveChanges();
        }

        // DELETE: api/BWAPI/5
        public void Delete(int id)
        {
        }
    }
}
