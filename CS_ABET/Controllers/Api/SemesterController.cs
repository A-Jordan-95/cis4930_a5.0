using CS_ABET.Controllers.Enterprise;
using CS_ABET.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CS_ABET.Controllers.Api
{
    [RoutePrefix("api/Semester")]
    public class SemesterController : ApiController
    {
        [HttpGet]
        [Route("Get")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(new SemesterEC().Get());
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            return Ok(new SemesterEC().GetById(id));
        }

        [HttpPost]
        [Route("AddOrUpdate")]
        public async Task<IHttpActionResult> AddOrUpdate([FromBody] Semester semester)
        {
            if(semester != null)
            {
                DbMock.Semesters.Add(new SemesterEC().AddOrUpdate(semester));
            }

            return Ok(semester);
        }
    }
}