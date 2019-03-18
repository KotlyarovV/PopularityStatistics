using System.Linq;
using System.Threading.Tasks;
using Autofac;
using PopularityStatistics.Data;
using PopularityStatistics.Infrastucture;
using PopularityStatistics.Models;
using TagsCloudVisualization;
using VKWebApi;

namespace PopularityStatistics
{
    public class TaskRepository
    {
        private TaskPool<Report> _taskPool;
        private IFileRepository _fileRepository;
        private IReportRepository _reportRepository;

        public TaskRepository(
            IFileRepository fileRepository, 
            IReportRepository reportRepository, 
            TaskPool<Report> taskPool
            )
        {
            _taskPool = taskPool;
            _fileRepository = fileRepository;
            _reportRepository = reportRepository;
        }

        private async Task<Report> PerformTask(ParametersModel param)
        {
            var report = new Report(param);
            var dataCollector = new DataCollector();
            var dataTask =  dataCollector.GetUsersBfs(param.User);

            await Task.WhenAny(dataTask);
            if (dataTask.IsFaulted)
            {
                report.Error = ErrorEnum.GetDataError;
                return report;
            }

            var data = dataTask.Result;
            var filter = new UserFilter(param);
            var filteredData = data
                .Where(filter.IsSuitable)
                .Select(filter.FilteredField)
                .ToList();

            if (filteredData.Count == 0)
            {
                report.Error = ErrorEnum.EmptyDataError;
                return report;
            }

            var cloud = CloudContainer().Resolve<ICloudPainter>();
            var cloudTask = cloud.GetBitmapAsync(
                filteredData, 
                param.Colors, 
                param.Width, 
                param.Height, 
                param.MinFont, 
                param.MaxFont, 
                param.FontName
                );

            await Task.WhenAny(cloudTask);
            if (cloudTask.IsFaulted)
            {
                report.Error = ErrorEnum.CloudError;
                return report;
            }
            var cloudBitmap = cloudTask.Result;
            var saveTask = _fileRepository.SaveImageAsync(cloudBitmap);
            await Task.WhenAny(saveTask);
            if (saveTask.IsFaulted)
            {
                report.Error = ErrorEnum.SaveError;
                return report;
            }

            report.SetWayToFileWithSuccess(saveTask.Result);
            return report;
        }

        private async Task<Report> PerformTaskAndMakeReport(ParametersModel parameters)
        {
            var report = await PerformTask(parameters);
            await _reportRepository.SaveReport(report);
            return report;
        }
        
        public bool StartTask(ParametersModel parameters)
        {
            return _taskPool.TryAddTask(PerformTaskAndMakeReport(parameters));
        }
        
        
        private IContainer CloudContainer()
        {
            var builder = new ContainerBuilder();
            builder
                .RegisterType<ArchimedeanSpiral>()
                .As<ISpiral>();

            builder
                .RegisterType<Analysator>()
                .As<IAnalysator>();

            builder
                .RegisterType<Filter>()
                .As<IFilter>();

            builder
                .RegisterType<WordExtractor>()
                .As<IWordExtractor>();

            builder
                .RegisterType<Formatter>()
                .As<IFormatter>();

            builder
                .RegisterType<CircularCloudLayouter>()
                .As<ICloudLayouter>();

            builder
                .RegisterType<TextVisualisator>()
                .As<ITextVisualisator>();

            builder.RegisterType<CloudPainter>().As<ICloudPainter>();
            return builder.Build();
        }
    }
}
