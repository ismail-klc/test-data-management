using Core.Utilities.Results;
using Entities.Concrete;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ITestService
    {
        IDataResult<Test> AddTest(Test test);
        IDataResult<bool> AddTests(List<Test> tests);
        IDataResult<List<Test>> GetTests();
        IDataResult<Test> GetTestById(string id);
        IDataResult<bool> DeleteTestById(string id);
        IDataResult<int> GetTestNumber();
        IDataResult<TestReport> GetTestReport(string id);
        IDataResult<TestStatistic> GetStatistics();
    }
}
