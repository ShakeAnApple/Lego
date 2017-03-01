using VkDownloader.Prototype.BusinessLogic.Model;

namespace VkDownloader.Prototype.BusinessLogic.Abstract
{
    //BL

    // setting are stored in file. 
    // 1. possible to load settings from default file location or from another place in FS. 
    // 1.1 if the user loads settings from another place, all changes to settings are applyed to the chosen file
    // 1.1.1 The user is able to make loaded settings default by pressing the corresponding button (somewhere I don't know yet)
    //      This will rewrite settings in default location.
    // 1.1.2 if the user doesn't make loaded settings default then on the next start application will use settings from default settings location.
    // 1.2 The user can change default settings location
    public interface ISettingsRepository
    {
        Settings Get(string path);
        void Save(Settings settings, string path);
    }
}
