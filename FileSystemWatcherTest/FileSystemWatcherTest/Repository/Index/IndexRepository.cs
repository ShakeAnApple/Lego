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
                doc.Add(new Field(IndexFields.Id, message.Id.ToString(), Field.Store.YES, Field.Index.NO));
                doc.Add(new Field(IndexFields.Path, file.Path, Field.Store.YES, Field.Index.NO));
                //TODO format this doc field as datetime
                doc.Add(new Field(IndexFields.Date, message.Timestamp.ToString(), Field.Store.YES, Field.Index.NO));
                doc.Add(new Field(IndexFields.Status, message.Severity.ToString(), Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field(IndexFields.Message, message.Body, Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field(IndexFields.Thread, message.Thread.ToString(), Field.Store.YES, Field.Index.NO));
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