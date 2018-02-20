using System.Collections.Generic;
using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace FileSystemWatcherTest.Repository.Index
{
    public class IndexRepository : IIndexRepository
    {
        private readonly IndexWriter _writer;

        public IndexRepository(IndexWriter writer)
        {
            _writer = writer;
        }

        private List<Document> CreateDoc(File file)
        {
            var docs = new List<Document>();
            foreach (var message in file.Content)
            {
                var doc = new Document();
                doc.Add(new Field(IndexFields.Path, file.Path, Field.Store.NO, Field.Index.NO));

                doc.Add(new Field(IndexFields.Id, message.Id.ToString(), Field.Store.YES, Field.Index.NO));
                doc.Add(new Field(IndexFields.Message, message.Body, Field.Store.YES, Field.Index.ANALYZED));
                docs.Add(doc);
            }
            return docs;
        }

        public void AddFile(File file)
        {
            CreateDoc(file).ForEach(_writer.AddDocument);
        }

        //TODO it won't work
        public void RemoveFile(string path)
        {
            var indexReader = IndexReader.Open(_writer.Directory, false);
            indexReader.DeleteDocuments(new Term(IndexFields.Path, path));
        }

        public void Update(string filePath, string fieldName, string newValue)
        {
            //TODO T_T
            //var doc = _writer.ge
            //_writer.UpdateDocument()
        }
    }
}