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
            var settings = GetDefaultInternal();

            Context.Initialize(settings);
        }

        public void UpdateContextWithTempSettings(string tmpSettingsFilePath)
        {
            CheckPath(tmpSettingsFilePath);

            var settings = _settingsRepository.Get(tmpSettingsFilePath);
            CheckSettings(settings);

            Context.Current.Update(settings);
        }

        public void Save(Settings settings, string path)
        {
            CheckPath(path);

            _settingsRepository.Save(settings, path);
            Context.Current.Update(settings);
        }

        public Settings GetCurrent()
        {
            var defaultSettingsPath = _appConfigRepository.GetDefaultSettingsPath();
            var path = Context.Current.TemporarySettingsPath ?? defaultSettingsPath;
            CheckPath(path);

            var settings = _settingsRepository.Get(path);
            CheckSettings(settings);

            return settings;
        }

        public Settings GetDefault()
        {
            return GetDefaultInternal();
        }

        private Settings GetDefaultInternal()
        {
            var defaultSettingsPath = _appConfigRepository.GetDefaultSettingsPath();
            CheckPath(defaultSettingsPath);

            var settings = _settingsRepository.Get(defaultSettingsPath);
            CheckSettings(settings);

            return settings;
        }

        public ConfigurationSettings GetConfigurationSettings()
        {
            return new ConfigurationSettings
            {
                DefaultSettingsFilePath = _appConfigRepository.GetDefaultSettingsPath(),
                TempSettingsFilePath = Context.Current.TemporarySettingsPath
            };
        }

        public void SaveConfigurationSettings(ConfigurationSettings settings)
        {
            _appConfigRepository.UpdateDefaultSettingsPath(settings.DefaultSettingsFilePath);
            Context.Current.TemporarySettingsPath = settings.TempSettingsFilePath;
        }

        public Settings Get(string path)
        {
            CheckPath(path);

            var settings = _settingsRepository.Get(path);
            CheckSettings(settings);

            return settings;
        }

        private void CheckPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("Path cannot be empty");
            }
        }

        private void CheckSettings(Settings settings)
        {
            if (settings == null)
            {
                // TODO implement custom exception?
                throw new ApplicationException("Settings file was not found or was corrupted");
            }
        }
    }
}
