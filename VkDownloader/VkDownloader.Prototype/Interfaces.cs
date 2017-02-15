using System;
using System.Collections.Generic;
using System.IO;


namespace VkDownloader.Prototype
{
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

    public interface IAudioEntryRepository
    {
        List<AudioEntry> List();
        //void Clear();
        //void Delete();
    }

    public interface IVkAudioService
    {
        void ListAll();
        void ListUnloaded();
        //void Download();
    }

    public interface IVkAccountService
    {
        //void Login();
    }

    public class Context
    {
        private static Context _context;

        public static Context Current
        {
            get
            {
                if (_context == null)
                {
                    _context = new Context();
                }
                return _context;
            }
        }

        public string AccountId { get; set; }
        public ScanningArea ScanningArea { get; set; }
        public string DefaultDownloadPath { get; set; }
    }

    
}
