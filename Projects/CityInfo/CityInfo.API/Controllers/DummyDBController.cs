using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Entities;

namespace CityInfo.API.Controllers
{
    public class DummyDBController : Controller
    {
        private CityInfoContext _ctx;

        public DummyDBController(CityInfoContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/testDB")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
