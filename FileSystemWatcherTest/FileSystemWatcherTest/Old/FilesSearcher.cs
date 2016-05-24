using System;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;
using Lucene.Net.Search.Highlight;
using Directory = Lucene.Net.Store.Directory;
using Version = Lucene.Net.Util.Version;

namespace FileSystemWatcherTest.Old
{
    public class Match
    {
        public string Path { get; private set; }
        public string String { get; private set; }

        public Match(string path, string match)
        {
            this.Path = path;
            this.String = match;
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
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            var parser = new QueryParser(Version.LUCENE_30, IndexFields.Message, analyzer);
            var query = parser.Parse(text);
            var hits = _indexSearcher.Search(query, int.MaxValue);
            var result = new List<Match>();
            var allMatches = new List<Tuple<int, string, float>>();

            foreach (var doc in hits.ScoreDocs)
            {
                var matchDoc = _indexSearcher.Doc(doc.Doc);
                var fieldText = matchDoc.Get(IndexFields.Message);
                var stream = analyzer.TokenStream("", new StringReader(fieldText));
                var fragments = this.GetFieldTextFragments(stream, fieldText, query).ToArray();
                allMatches.AddRange(fragments);
                foreach (var textFragment in fragments)
                {
                    var match = textFragment.Item2; 
                    result.Add(new Match(matchDoc.Get(IndexFields.Path), match));
                }
            }
            return result;
        }

        private IEnumerable<Tuple<int, string, float>> GetFieldTextFragments(TokenStream tokenStream, string text, Query query)
        {
            var termAttribute = tokenStream.AddAttribute<ITermAttribute>();
            var offsetAttribute = tokenStream.AddAttribute<IOffsetAttribute>();

            tokenStream.AddAttribute<IPositionIncrementAttribute>();
            tokenStream.Reset();

            var spanTermExtractor = new WeightedSpanTermExtractor();
            spanTermExtractor.ExpandMultiTermQuery = true; // ? 
            spanTermExtractor.SetWrapIfNotCachingTokenFilter(true); // ?
            var fieldWeightedSpanTerms = spanTermExtractor.GetWeightedSpanTerms(query, tokenStream, null);
            if (spanTermExtractor.IsCachedTokenStream)
                tokenStream = spanTermExtractor.TokenStream;

            for (bool hasToken = tokenStream.IncrementToken(); hasToken && offsetAttribute.StartOffset < text.Length; hasToken = tokenStream.IncrementToken())
            {
                Console.WriteLine(termAttribute.Term);
                WeightedSpanTerm term;
                if (fieldWeightedSpanTerms.TryGetValue(termAttribute.Term, out term))
                {
                    yield return Tuple.Create(offsetAttribute.StartOffset, term.Term, term.Weight);
                }
            }
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