﻿using CS_ABET.Controllers.Enterprise;
using CS_ABET.Persistence;
using CS_ABET.Persistence.DTO;
using CS_ABET.Persistence.Models;
using System.Threading.Tasks;
using System.Web.Http;
namespace CS_ABET.Controllers.Api
{
    [RoutePrefix("api/Review")]
    public class ReviewController : ApiController
    {
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            return Ok(await new ReviewEC().Get(id));
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await new ReviewEC().Get());
        }
    }
}