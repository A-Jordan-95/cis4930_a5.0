using CS_ABET.Controllers.Enterprise;
using CS_ABET.Persistence;
using CS_ABET.Persistence.DTO;
using CS_ABET.Persistence.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace CS_ABET.Controllers.Api
{
    [RoutePrefix("api/Class")]
    public class ClassController : ApiController
    {
        [HttpGet]
        [Route("Get/{semesterId}")]
        public async Task<IHttpActionResult> GetById(int semesterId)
        {
            return Ok(await new ClassEC().Get(semesterId));
        }

        [HttpPost]
        [Route("AddOrUpdate")]
        public async Task<IHttpActionResult> AddOrUpdate([FromBody] ClassDto classDto)
        {
            return Ok(await new ClassEC().AddOrUpdate(classDto));
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<IHttpActionResult> Remove([FromBody] ClassDto classDto)
        {
            return Ok(await new ClassEC().Remove(classDto));
        }

        [HttpGet]
        [Route("Remove/{id}")]
        public async Task<IHttpActionResult> Remove(int id)
        {
            return Ok(await new ClassEC().Remove(id));
        }
    }
}