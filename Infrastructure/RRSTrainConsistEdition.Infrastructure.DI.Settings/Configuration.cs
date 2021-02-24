using RRSTrainConsistEdition.BL.Services.Consist;
using RRSTrainConsistEdition.BL.Services.Serialize;
using RRSTrainConsistEdition.BL.Services.Settings;
using RRSTrainConsistEdition.Core.Models;
using SimpleInjector;

namespace RRSTrainConsistEdition.Infrastructure.DI.Settings
{
    public class Configuration
    {
        public Container Container { get; set; }

		public Configuration()
		{
			Container = new Container();

			Setup();
		}

		public virtual void Setup()
		{
			Container.Register<IConsist, Consist>(Lifestyle.Transient);

			Container.Register<ISerializeModel, SerializeModel>(Lifestyle.Singleton);
			Container.Register<ISerializeService, SerializeService>(Lifestyle.Transient);

			Container.Register<ICreateConsistService, CreateConsistServise>(Lifestyle.Transient);
			Container.Register<ISettingService, SettingService>(Lifestyle.Singleton);

			Container.Verify();

		}

	}
}
