using VkDownloader.Prototype.BusinessLogic.Model;

namespace VkDownloader.Prototype.BusinessLogic.Abstract
{
    // BL

    //я наеврное с областями поиска сооружу
    //и типа оно сканит
    //если находит сразу - выдает, если нет, то спрашивает,где поискать
    //когда находит - обновляет путь
    //и типа чтоб можно было так путь для всех песенок ненайденных задавать
    //либо для каждой в отдельности
    public interface IAudioFileRepository
    {
        void Save(AudioFile file);
        //void Get();    
    }
}
