using Business.Abstract;
using Business.Constants;
using Core.DataAccess.MongoDb;
using Core.Utilities.Results;
using Entities.Concrete;
using MongoDB.Bson;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Business.Concrete
{
    public class TestManager : ITestService
    {
        private readonly IMongoRepository<Test> _testRepository;
        private readonly IMongoRepository<Sound1> _sound1Repository;
        private readonly IMongoRepository<Sound2> _sound2Repository;
        private readonly IMongoRepository<TestMain> _mainRepository;
        private readonly IMongoRepository<Vibration1> _vibration1Repository;
        private readonly IMongoRepository<Vibration2> _vibration2Repository;


        public TestManager(IMongoRepository<Sound1> sound1Repository,
            IMongoRepository<Sound2> sound2Repository, IMongoRepository<Test> testRepository,
            IMongoRepository<TestMain> mainRepository, IMongoRepository<Vibration1> vibration1Repository,
            IMongoRepository<Vibration2> vibration2Repository)
        {
            _sound1Repository = sound1Repository;
            _sound2Repository = sound2Repository;
            _testRepository = testRepository;
            _mainRepository = mainRepository;
            _vibration1Repository = vibration1Repository;
            _vibration2Repository = vibration2Repository;
        }

        public IDataResult<bool> DeleteTestById(string id)
        {
            try
            {
                _testRepository.DeleteByIdAsync(id);
                _sound1Repository.DeleteManyAsync(x => x.TestID == id);
                _sound2Repository.DeleteManyAsync(x => x.TestID == id);
                _mainRepository.DeleteManyAsync(x => x.TestID == id);
                _vibration1Repository.DeleteManyAsync(x => x.TestID == id);
                _vibration2Repository.DeleteManyAsync(x => x.TestID == id);
                return new SuccessDataResult<bool>(true, Messages.TestDeleted);
            }
            catch (Exception)
            {
                return new ErrorDataResult<bool>(false, Messages.TestNotDeleted);
            }
        }

        public IDataResult<Test> GetTestById(string id)
        {
            try
            {
                var test = _testRepository.FindByIdAsync(id).Result;
                if (test == null)
                {
                    return new ErrorDataResult<Test>(null, Messages.TestNotFound);
                }
                var sound1 = _sound1Repository.AsQueryable().Where(s => s.TestID == id);
                var sound2 = _sound2Repository.AsQueryable().Where(s => s.TestID == id);
                var main = _mainRepository.AsQueryable().Where(s => s.TestID == id);
                var vib1 = _vibration1Repository.AsQueryable().Where(s => s.TestID == id);
                var vib2 = _vibration2Repository.AsQueryable().Where(s => s.TestID == id);

                test.Sound1 = sound1.ToList();
                test.Sound2 = sound2.ToList();
                test.Main = main.ToList();
                test.Vibration1 = vib1.ToList();
                test.Vibration2 = vib2.ToList();

                return new SuccessDataResult<Test>(test, Messages.TestsFetched);
            }
            catch (Exception)
            {
                return new ErrorDataResult<Test>(null, Messages.TestsNotFetched);
            }
        }

        public IDataResult<int> GetTestNumber()
        {
            int testNumber = _testRepository.AsQueryable().Count();
            return new SuccessDataResult<int>(testNumber, Messages.TestsFetched);
        }

        public IDataResult<List<Test>> GetTests()
        {
            var tests = _testRepository.AsQueryable().ToList();
            if (tests.Count == 0)
            {
                return new ErrorDataResult<List<Test>>(null, Messages.TestNotFound);
            }
            return new SuccessDataResult<List<Test>>(tests, Messages.TestsFetched);
        }

        IDataResult<Test> ITestService.AddTest(Test test)
        {
            try
            {
                var main = test.Main;
                var sound1 = test.Sound1;
                var sound2 = test.Sound2;
                var vib1 = test.Vibration1;
                var vib2 = test.Vibration2;

                test.Main = null;
                test.Sound1 = null;
                test.Sound2 = null;
                test.Vibration1 = null;
                test.Vibration2 = null;
                test.Id = null;
                test.CreatedAt = DateTime.Now;

                _testRepository.InsertOne(test);

                var testId = test.Id;
                main = main.Select(c => { c.TestID = testId; c.Id = null; return c; }).ToList();
                sound1 = sound1.Select(c => { c.TestID = testId; c.Id = null; return c; }).ToList();
                sound2 = sound2.Select(c => { c.TestID = testId; c.Id = null; return c; }).ToList();
                vib1 = vib1.Select(c => { c.TestID = testId; c.Id = null; return c; }).ToList();
                vib2 = vib2.Select(c => { c.TestID = testId; c.Id = null; return c; }).ToList();

                _sound1Repository.InsertManyAsync(sound1);
                _sound2Repository.InsertManyAsync(sound2);
                _mainRepository.InsertManyAsync(main);
                _vibration1Repository.InsertManyAsync(vib1);
                _vibration2Repository.InsertManyAsync(vib2);
                return new SuccessDataResult<Test>(test, Messages.TestsAdded);
            }
            catch (Exception)
            {
                return new ErrorDataResult<Test>(null, Messages.TestNotAdded);
            }
        }

        IDataResult<bool> ITestService.AddTests(List<Test> tests)
        {
            _testRepository.InsertManyAsync(tests);
            return new SuccessDataResult<bool>(true, Messages.TestsAdded);
        }

        public IDataResult<TestReport> GetTestReport(string id)
        {
            var test = GetTestById(id).Data;

            TestReport testReport = new TestReport();
            testReport.Description = test.Description;
            testReport.TestTime = test.Main.Count;

            GetMainReport(test, ref testReport);
            GetReport(test, ref testReport);

            return new SuccessDataResult<TestReport>(testReport, Messages.TestReported);
        }

        private void GetMainReport(Test test, ref TestReport testReport)
        {
            testReport.D_KonumXAvg = test.Main.Select(x => Double.Parse(x.D_KonumX, CultureInfo.InvariantCulture)).Average();
            testReport.D_KonumXMax = test.Main.Select(x => Double.Parse(x.D_KonumX, CultureInfo.InvariantCulture)).Max();
            testReport.D_KonumXMin = test.Main.Select(x => Double.Parse(x.D_KonumX, CultureInfo.InvariantCulture)).Min();

            testReport.D_KonumYAvg = test.Main.Select(x => Double.Parse(x.D_KonumY, CultureInfo.InvariantCulture)).Average();
            testReport.D_KonumYMax = test.Main.Select(x => Double.Parse(x.D_KonumY, CultureInfo.InvariantCulture)).Max();
            testReport.D_KonumYMin = test.Main.Select(x => Double.Parse(x.D_KonumY, CultureInfo.InvariantCulture)).Min();

            testReport.KonumXAvg = test.Main.Select(x => Double.Parse(x.KonumX, CultureInfo.InvariantCulture)).Average();
            testReport.KonumXMax = test.Main.Select(x => Double.Parse(x.KonumX, CultureInfo.InvariantCulture)).Max();
            testReport.KonumXMin = test.Main.Select(x => Double.Parse(x.KonumX, CultureInfo.InvariantCulture)).Min();

            testReport.KonumYAvg = test.Main.Select(x => Double.Parse(x.KonumY, CultureInfo.InvariantCulture)).Average();
            testReport.KonumYMax = test.Main.Select(x => Double.Parse(x.KonumY, CultureInfo.InvariantCulture)).Max();
            testReport.KonumYMin = test.Main.Select(x => Double.Parse(x.KonumY, CultureInfo.InvariantCulture)).Min();

            testReport.Motor1AkuGerilimAvg = test.Main.Select(x => Double.Parse(x.Motor1AkuGerilim, CultureInfo.InvariantCulture)).Average();
            testReport.Motor1AkuGerilimMax = test.Main.Select(x => Double.Parse(x.Motor1AkuGerilim, CultureInfo.InvariantCulture)).Max();
            testReport.Motor1AkuGerilimMin = test.Main.Select(x => Double.Parse(x.Motor1AkuGerilim, CultureInfo.InvariantCulture)).Min();

            testReport.Motor1CekimAkimiAvg = test.Main.Select(x => Double.Parse(x.Motor1CekimAkimi, CultureInfo.InvariantCulture)).Average();
            testReport.Motor1CekimAkimiMax = test.Main.Select(x => Double.Parse(x.Motor1CekimAkimi, CultureInfo.InvariantCulture)).Max();
            testReport.Motor1CekimAkimiMin = test.Main.Select(x => Double.Parse(x.Motor1CekimAkimi, CultureInfo.InvariantCulture)).Min();

            testReport.Motor1GucAvg = test.Main.Select(x => Double.Parse(x.Motor1Guc, CultureInfo.InvariantCulture)).Average();
            testReport.Motor1GucMax = test.Main.Select(x => Double.Parse(x.Motor1Guc, CultureInfo.InvariantCulture)).Max();
            testReport.Motor1GucMin = test.Main.Select(x => Double.Parse(x.Motor1Guc, CultureInfo.InvariantCulture)).Min();

            testReport.Motor1NemAvg = test.Main.Select(x => Double.Parse(x.Motor1Nem, CultureInfo.InvariantCulture)).Average();
            testReport.Motor1NemMax = test.Main.Select(x => Double.Parse(x.Motor1Nem, CultureInfo.InvariantCulture)).Max();
            testReport.Motor1NemMin = test.Main.Select(x => Double.Parse(x.Motor1Nem, CultureInfo.InvariantCulture)).Min();

            testReport.Motor1SicaklikAvg = test.Main.Select(x => Double.Parse(x.Motor1Sicaklik, CultureInfo.InvariantCulture)).Average();
            testReport.Motor1SicaklikMax = test.Main.Select(x => Double.Parse(x.Motor1Sicaklik, CultureInfo.InvariantCulture)).Max();
            testReport.Motor1SicaklikMin = test.Main.Select(x => Double.Parse(x.Motor1Sicaklik, CultureInfo.InvariantCulture)).Min();

            testReport.Motor1TekerHiziAvg = test.Main.Select(x => Double.Parse(x.Motor1TekerHizi, CultureInfo.InvariantCulture)).Average();
            testReport.Motor1TekerHiziMax = test.Main.Select(x => Double.Parse(x.Motor1TekerHizi, CultureInfo.InvariantCulture)).Max();
            testReport.Motor1TekerHiziMin = test.Main.Select(x => Double.Parse(x.Motor1TekerHizi, CultureInfo.InvariantCulture)).Min();

            testReport.Motor2AkuGerilimAvg = test.Main.Select(x => Double.Parse(x.Motor2AkuGerilim, CultureInfo.InvariantCulture)).Average();
            testReport.Motor2AkuGerilimMax = test.Main.Select(x => Double.Parse(x.Motor2AkuGerilim, CultureInfo.InvariantCulture)).Max();
            testReport.Motor2AkuGerilimMin = test.Main.Select(x => Double.Parse(x.Motor2AkuGerilim, CultureInfo.InvariantCulture)).Min();

            testReport.Motor2CekimAkimiAvg = test.Main.Select(x => Double.Parse(x.Motor2CekimAkimi, CultureInfo.InvariantCulture)).Average();
            testReport.Motor2CekimAkimiMax = test.Main.Select(x => Double.Parse(x.Motor2CekimAkimi, CultureInfo.InvariantCulture)).Max();
            testReport.Motor2CekimAkimiMin = test.Main.Select(x => Double.Parse(x.Motor2CekimAkimi, CultureInfo.InvariantCulture)).Min();

            testReport.Motor2GucAvg = test.Main.Select(x => Double.Parse(x.Motor2Guc, CultureInfo.InvariantCulture)).Average();
            testReport.Motor2GucMax = test.Main.Select(x => Double.Parse(x.Motor2Guc, CultureInfo.InvariantCulture)).Max();
            testReport.Motor2GucMin = test.Main.Select(x => Double.Parse(x.Motor2Guc, CultureInfo.InvariantCulture)).Min();

            testReport.Motor2NemAvg = test.Main.Select(x => Double.Parse(x.Motor2Nem, CultureInfo.InvariantCulture)).Average();
            testReport.Motor2NemMax = test.Main.Select(x => Double.Parse(x.Motor2Nem, CultureInfo.InvariantCulture)).Max();
            testReport.Motor2NemMin = test.Main.Select(x => Double.Parse(x.Motor2Nem, CultureInfo.InvariantCulture)).Min();

            testReport.Motor2SicaklikAvg = test.Main.Select(x => Double.Parse(x.Motor2Sicaklik, CultureInfo.InvariantCulture)).Average();
            testReport.Motor2SicaklikMax = test.Main.Select(x => Double.Parse(x.Motor2Sicaklik, CultureInfo.InvariantCulture)).Max();
            testReport.Motor2SicaklikMin = test.Main.Select(x => Double.Parse(x.Motor2Sicaklik, CultureInfo.InvariantCulture)).Min();

            testReport.Motor2TekerHiziAvg = test.Main.Select(x => Double.Parse(x.Motor2TekerHizi, CultureInfo.InvariantCulture)).Average();
            testReport.Motor2TekerHiziMax = test.Main.Select(x => Double.Parse(x.Motor2TekerHizi, CultureInfo.InvariantCulture)).Max();
            testReport.Motor2TekerHiziMin = test.Main.Select(x => Double.Parse(x.Motor2TekerHizi, CultureInfo.InvariantCulture)).Min();

            testReport.OrtamSicakligiAvg = test.Main.Select(x => Double.Parse(x.OrtamSicakligi, CultureInfo.InvariantCulture)).Average();
            testReport.OrtamSicakligiMax = test.Main.Select(x => Double.Parse(x.OrtamSicakligi, CultureInfo.InvariantCulture)).Max();
            testReport.OrtamSicakligiMin = test.Main.Select(x => Double.Parse(x.OrtamSicakligi, CultureInfo.InvariantCulture)).Min();

            testReport.PlabKonumXAvg = test.Main.Select(x => Double.Parse(x.PlabKonumX, CultureInfo.InvariantCulture)).Average();
            testReport.PlabKonumXMax = test.Main.Select(x => Double.Parse(x.PlabKonumX, CultureInfo.InvariantCulture)).Max();
            testReport.PlabKonumXMin = test.Main.Select(x => Double.Parse(x.PlabKonumX, CultureInfo.InvariantCulture)).Min();

            testReport.PlabKonumYAvg = test.Main.Select(x => Double.Parse(x.PlabKonumY, CultureInfo.InvariantCulture)).Average();
            testReport.PlabKonumYMax = test.Main.Select(x => Double.Parse(x.PlabKonumY, CultureInfo.InvariantCulture)).Max();
            testReport.PlabKonumYMin = test.Main.Select(x => Double.Parse(x.PlabKonumY, CultureInfo.InvariantCulture)).Min();

            testReport.RobotBasAcisiAvg = test.Main.Select(x => Double.Parse(x.RobotBasAcisi, CultureInfo.InvariantCulture)).Average();
            testReport.RobotBasAcisiMax = test.Main.Select(x => Double.Parse(x.RobotBasAcisi, CultureInfo.InvariantCulture)).Max();
            testReport.RobotBasAcisiMin = test.Main.Select(x => Double.Parse(x.RobotBasAcisi, CultureInfo.InvariantCulture)).Min();
        }

        private void GetReport(Test test, ref TestReport testReport)
        {
            List<double> avg = new List<double>();
            List<double> max = new List<double>();
            List<double> min = new List<double>();

            foreach (var item in test.Sound1)
            {
                item.Data = item.Data.Replace("[", "");
                item.Data = item.Data.Replace("]", "");

                var data = Array.ConvertAll(item.Data.Split(','),i=> Convert.ToDouble(i, CultureInfo.InvariantCulture));
                avg.Add(data.Average());
                max.Add(data.Max());
                min.Add(data.Min());
            }

            testReport.Sound1Avg = avg.Average();
            testReport.Sound1Max = max.Max();
            testReport.Sound1Min = min.Min();


            avg.Clear();
            max.Clear();
            min.Clear();
            foreach (var item in test.Sound2)
            {
                item.Data = item.Data.Replace("[", "");
                item.Data = item.Data.Replace("]", "");

                var data = Array.ConvertAll(item.Data.Split(','), i => Convert.ToDouble(i, CultureInfo.InvariantCulture));
                avg.Add(data.Average());
                max.Add(data.Max());
                min.Add(data.Min());
            }

            testReport.Sound2Avg = avg.Average();
            testReport.Sound2Max = max.Max();
            testReport.Sound2Min = min.Min();

            avg.Clear();
            max.Clear();
            min.Clear();
            foreach (var item in test.Vibration1.Where(x => x.Axis == 'x'))
            {
                item.Data = item.Data.Replace("[", "");
                item.Data = item.Data.Replace("]", "");

                var data = Array.ConvertAll(item.Data.Split(','), i => Convert.ToDouble(i, CultureInfo.InvariantCulture));
                avg.Add(data.Average());
                max.Add(data.Max());
                min.Add(data.Min());
            }

            testReport.Vib1XAvg = avg.Average();
            testReport.Vib1XMax = max.Max();
            testReport.Vib1XMin = min.Min();

            avg.Clear();
            max.Clear();
            min.Clear();
            foreach (var item in test.Vibration1.Where(x => x.Axis == 'y' ))
            {
                item.Data = item.Data.Replace("[", "");
                item.Data = item.Data.Replace("]", "");

                var data = Array.ConvertAll(item.Data.Split(','), i => Convert.ToDouble(i, CultureInfo.InvariantCulture));
                avg.Add(data.Average());
                max.Add(data.Max());
                min.Add(data.Min());
            }

            testReport.Vib1YAvg = avg.Average();
            testReport.Vib1YMax = max.Max();
            testReport.Vib1YMin = min.Min();

            avg.Clear();
            max.Clear();
            min.Clear();
            foreach (var item in test.Vibration1.Where(x => x.Axis == 'z' ))
            {
                item.Data = item.Data.Replace("[", "");
                item.Data = item.Data.Replace("]", "");

                var data = Array.ConvertAll(item.Data.Split(','), i => Convert.ToDouble(i, CultureInfo.InvariantCulture));
                avg.Add(data.Average());
                max.Add(data.Max());
                min.Add(data.Min());
            }

            testReport.Vib1ZAvg = avg.Average();
            testReport.Vib1ZMax = max.Max();
            testReport.Vib1ZMin = min.Min();

            avg.Clear();
            max.Clear();
            min.Clear();
            foreach (var item in test.Vibration2.Where(x => x.Axis == 'x' ))
            {
                item.Data = item.Data.Replace("[", "");
                item.Data = item.Data.Replace("]", "");

                var data = Array.ConvertAll(item.Data.Split(','), i => Convert.ToDouble(i, CultureInfo.InvariantCulture));
                avg.Add(data.Average());
                max.Add(data.Max());
                min.Add(data.Min());
            }

            testReport.Vib2XAvg = avg.Average();
            testReport.Vib2XMax = max.Max();
            testReport.Vib2XMin = min.Min();

            avg.Clear();
            max.Clear();
            min.Clear();
            foreach (var item in test.Vibration2.Where(x => x.Axis == 'y' ))
            {
                item.Data = item.Data.Replace("[", "");
                item.Data = item.Data.Replace("]", "");

                var data = Array.ConvertAll(item.Data.Split(','), i => Convert.ToDouble(i, CultureInfo.InvariantCulture));
                avg.Add(data.Average());
                max.Add(data.Max());
                min.Add(data.Min());
            }

            testReport.Vib2YAvg = avg.Average();
            testReport.Vib2YMax = max.Max();
            testReport.Vib2YMin = min.Min();

            avg.Clear();
            max.Clear();
            min.Clear();
            foreach (var item in test.Vibration2.Where(x => x.Axis == 'z' ))
            {
                item.Data = item.Data.Replace("[", "");
                item.Data = item.Data.Replace("]", "");

                var data = Array.ConvertAll(item.Data.Split(','), i => Convert.ToDouble(i, CultureInfo.InvariantCulture));
                avg.Add(data.Average());
                max.Add(data.Max());
                min.Add(data.Min());
            }

            testReport.Vib2ZAvg = avg.Average();
            testReport.Vib2ZMax = max.Max();
            testReport.Vib2ZMin = min.Min();
        }

        public IDataResult<TestStatistic> GetStatistics()
        {
            TestStatistic statistic = new TestStatistic();
            var tests = _testRepository.AsQueryable().ToList();

            if (tests.Count == 0)
            {
                statistic.TestNumber = 0;
                statistic.LastAdded = DateTime.MinValue;
                statistic.LastTestDef = "No test";
            }
            else
            {
                var lastAddedTest = tests.FirstOrDefault(x => x.CreatedAt == tests.Max(y => y.CreatedAt));

                statistic.TestNumber = tests.Count();
                statistic.LastAdded =  lastAddedTest.CreatedAt;
                statistic.LastTestDef = lastAddedTest.Description;
            }

            

            return new SuccessDataResult<TestStatistic>(statistic, Messages.TestStatistic);
        }
    }
}
