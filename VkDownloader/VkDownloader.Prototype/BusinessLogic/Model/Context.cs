namespace VkDownloader.Prototype.BusinessLogic.Model
{
    public class Context : IContext
    {
        private static Context _context;

        private Context() { }
        private Context(Settings settings)
        {
            Settings = settings;
        }

        public static IContext Current
        {
            get
            {
                // TODO
                // if context hasn't been initialized with settings create new context
                // or throw exception is better?
                if (_context == null)
                {
                    _context = new Context();
                }
                return _context;
            }
        }

        public Settings Settings { get; private set; }
        public string TemporarySettingsPath { get; set; }

        public void Update(Settings settings)
        {
            Settings = settings;
        }

        public static void Initialize(Settings settings)
        {
            if (_context == null)
            {
                _context = new Context(settings);
            } 
        }
    }
}
