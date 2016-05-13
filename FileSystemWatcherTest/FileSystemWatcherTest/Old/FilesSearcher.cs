using System;
using System.Collections.Generic;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;

namespace FileSystemWatcherTest.Old
{
    public class Match
    {
        public string Path { get; private set; }
        public string String { get; private set; }
        public int Score { get; private set; }

        public Match(string path, string match, int score)
        {
            this.Path = path;
            this.String = match;
            this.Score = score;
        }
    }

    public class FilesSearcher : IDisposable
    {
        private readonly IndexSearcher _indexSearcher;
        public FilesSearcher(Directory directory)
        {
            _indexSearcher = new IndexSearcher(directory);
        }

        public List<Match> Search(string text)
        {
            var parser = new QueryParser(Version.LUCENE_30, IndexFields.Message, new StandardAnalyzer(Version.LUCENE_30));
            var hits = _indexSearcher.Search(parser.Parse(text),int.MaxValue);
            var result = new List<Match>();

            foreach (var doc in hits.ScoreDocs)
            {
                var matchDoc = _indexSearcher.Doc(doc.Doc);
                result.Add(new Match(matchDoc.Get(IndexFields.Path), matchDoc.Get(IndexFields.Message), 1));
            }
            return result;
        }

        public void Close()
        {
            _indexSearcher.Dispose();
        }

        public void Dispose()
        {
            this.Close();
        }
    }
}