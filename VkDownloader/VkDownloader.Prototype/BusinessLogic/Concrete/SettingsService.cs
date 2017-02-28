using System;
using VkDownloader.Prototype.BusinessLogic.Abstract;
using VkDownloader.Prototype.BusinessLogic.Model;

namespace VkDownloader.Prototype.BusinessLogic.Concrete
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly IApplicationConfigurationRepository _appConfigRepository;

        public SettingsService()
        {
            // TODO
            _settingsRepository = new SettingsRepositoryMock();
            _appConfigRepository = new ApplicationConfigurationRepository();
        }

        public void InitializeContext()
        {   
            var defaultSettingsPath = _appConfigRepository.GetDefaultSettingsPath();
            var settings = _settingsRepository.Get(defaultSettingsPath);
            if (settings == null)
            {
                // TODO implement custom exception?
                throw new ApplicationException("Settings file was not found or was corrupted");
            }
            Context.Initialize(settings);
        }

        public void Load(string path)
        {
            var settings = _settingsRepository.Get(path);
            if (settings == null)
            {
                // TODO implement custom exception?
                throw new ApplicationException("Settings file was not found or was corrupted");
            }
            Context.Current.Update(settings);
        }

        public void Save(Settings settings, string path)
        {
            _settingsRepository.Save(settings, path);
            Context.Current.Update(settings);
        }
    }
}
