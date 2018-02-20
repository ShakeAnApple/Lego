using System;
using System.Data.Linq;
using System.Data.SqlServerCe;
using System.Linq;
using IO = System.IO;

namespace FileSystemWatcherTest.Repository.Database
{
    //class ConstructionTimeMappingSource : MappingSource
    //{
    //    class Model : MetaModel
    //    {
    //        private readonly MetaModel _defaultMapping;
    //        private readonly Type[] _excludeModelTypes;

    //        public Model(MetaModel defaultMapping, Type[] excludeModelTypes)
    //        {
    //            _defaultMapping = defaultMapping;
    //            _excludeModelTypes = excludeModelTypes;
    //        }

    //        public override MetaTable GetTable(Type rowType)
    //        {
    //            return _defaultMapping.GetTable(rowType);
    //        }

    //        public override MetaFunction GetFunction(MethodInfo method)
    //        {
    //            return _defaultMapping.GetFunction(method);
    //        }

    //        public override IEnumerable<MetaTable> GetTables()
    //        {
    //            return _defaultMapping.GetTables().Where(t => !_excludeModelTypes.Contains(t.RowType.Type)).ToArray();
    //        }

    //        public override IEnumerable<MetaFunction> GetFunctions()
    //        {
    //            return _defaultMapping.GetFunctions();
    //        }

    //        public override MetaType GetMetaType(Type type)
    //        {
    //            return _defaultMapping.GetMetaType(type);
    //        }

    //        public override MappingSource MappingSource { get { return _defaultMapping.MappingSource; } }
    //        public override Type ContextType { get { return _defaultMapping.ContextType; } }
    //        public override string DatabaseName { get { return _defaultMapping.DatabaseName; } }
    //        public override Type ProviderType { get { return _defaultMapping.ProviderType; } }
    //    }

    //    private readonly Type[] _excludeModelTypes;

    //    public ConstructionTimeMappingSource(params Type[] excludeModelTypes)
    //    {
    //        _excludeModelTypes = excludeModelTypes;
    //    }

    //    protected override MetaModel CreateModel(Type dataContextType)
    //    {
    //        var defaultMappingSource = new AttributeMappingSource();
    //        var mapping = defaultMappingSource.GetModel(dataContextType);
    //        return new Model(mapping, _excludeModelTypes);
    //    }
    //}

    //public class GraphDataPlainContext : DataContext
    //{
    //    public Table<Message> Messages { get { return this.GetTable<Message>(); } }
    //    public Table<TagType> TagTypes;
    //    public Table<Tag> Tags;
    //    public Table<MessageTag> MessageTags;
    //    public Table<FileWithMessages> FilesWithMessages;

    //    public GraphDataPlainContext(string connString) //, bool constructionTime = false)
    //        : base(new SqlCeConnection(connString)) //, MakeMappingSource(constructionTime))
    //    {
    //    }
    //}

    public class GraphDataContext : DataContext //: GraphDataPlainContext
    {
        public Table<File> Files { get { return this.GetTable<File>(); } }
        public Table<Message> Messages { get { return this.GetTable<Message>(); } }
        public Table<TagType> TagTypes { get { return this.GetTable<TagType>(); } }
        public Table<Tag> Tags { get { return this.GetTable<Tag>(); } }
        public Table<MessageTag> MessageTags { get { return this.GetTable<MessageTag>(); } }

        public GraphDataContext(string connString) //, bool constructionTime = false)
                : base(new SqlCeConnection(connString)) //, MakeMappingSource(constructionTime))
        {
        }
    


    //private static MappingSource MakeMappingSource(bool constructionTime)
    //{
    //    return !constructionTime ? (MappingSource)new AttributeMappingSource()
    //                             : new ConstructionTimeMappingSource(typeof(File));
    //}

    public static void Test()
        {
            var fname = "MyData.sdf";
            while (IO.File.Exists(fname))
                IO.File.Delete(fname);

            var cnnString = "Data Source=" + fname + ";Persist Security Info=False;";

            using (var cnn = new GraphDataContext(cnnString))
            {
                cnn.CreateDatabase();
            }
            using (var cnn = new GraphDataContext(cnnString))
            { 
                //var s = cnn.Messages.Join(cnn.MessageTags, m => m.Id, k => k.MessageId, (m, k) => new { m, k });
                ////cnn.ExecuteQuery<Message>("select * from messages");

                //foreach (var item in s)
                //{

                //}
                var fileId = Guid.NewGuid();
                var file = new File() { FullName = "kpk", Id = fileId, Messages = new EntitySet<Message>() };
                cnn.Files.InsertOnSubmit(file);
                for (int i = 0; i < 10; i++)
                {
                    var messId = Guid.NewGuid();
                    cnn.Messages.InsertOnSubmit(new Message { Id = messId, Body = "olol_" + i, FileId = fileId });
                }

                cnn.SubmitChanges();

                var dbFileMes = cnn.Files.First(f => f.Id == fileId);
                foreach (var message in dbFileMes.Messages)
                {
                    Console.WriteLine(message.Id + " " + message.Body);
                }

                var dbfile = cnn.Files.First(f => f.Id == fileId);
                Console.WriteLine(dbfile);
            }
        }
    }
}