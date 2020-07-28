using CS_ABET.Controllers.Enterprise;
using CS_ABET.Persistence;
using CS_ABET.Persistence.DTO;
using CS_ABET.Persistence.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace CS_ABET.Controllers.Api
{
    [RoutePrefix("api/Subject")]
    public class SubjectController : ApiController
    {
        [HttpGet]
        [Route("Get")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await new SubjectEC().Get());
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            return Ok(await new SubjectEC().GetById(id));
        }

        [HttpPost]
        [Route("AddOrUpdate")]
        public async Task<IHttpActionResult> AddOrUpdate([FromBody] SubjectDto Subject)
        {
            return Ok(await new SubjectEC().AddOrUpdate(Subject));
        }

        [HttpGet]
        [Route("RemoveById/{id}")]
        public async Task<IHttpActionResult> RemoveById(int id)
        {
            return Ok(await new SubjectEC().RemoveById(id));
        }
    }
}