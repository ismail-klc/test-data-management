using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestsController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestsController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        public ActionResult GetTests()
        {
            var testsResult = _testService.GetTests();
            if (testsResult.Data == null)
            {
                return Ok(testsResult);
            }
            
            return Ok(testsResult);
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult GetTestById(string id)
        {
            var testResult = _testService.GetTestById(id);
            if (testResult.Data == null)
            {
                return Ok(testResult.Message);
            }

            return Ok(testResult);
        }

        [HttpPost("addTest")]
        public ActionResult AddTest(Test test)
        {
            var testAdded = _testService.AddTest(test);
            return Ok(testAdded);
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult DeleteTest(string id)
        {
            var testDeleted = _testService.DeleteTestById(id);
            if (testDeleted.Success)
            {
                return Ok(testDeleted);
            }
            return BadRequest(testDeleted);
        }

        [HttpGet("testNumber")]
        public ActionResult GetTestNumber()
        {
            int testNumber = _testService.GetTestNumber().Data;
            return Ok(testNumber);
        }

        [HttpGet("report/{id:length(24)}")]
        public ActionResult GetReport(string id)
        {
            var testReport = _testService.GetTestReport(id);
            return Ok(testReport);
        }

        [HttpGet("statistic")]
        public ActionResult GetStatistics()
        {
            var statistics = _testService.GetStatistics();
            return Ok(statistics);
        }
    }
}
