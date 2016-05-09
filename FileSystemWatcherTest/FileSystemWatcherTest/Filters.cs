using System.Text.RegularExpressions;

namespace FileSystemWatcherTest
{
    public enum FilesFilterType
    {
        Exclude,
        WildCard,
        NamePattern,
        // whatever
    }

    public interface IFilesFilter
    {
        FilesFilterType Type { get; }

        string[] Apply(string rootPath, bool includeSubdirs);
    }

    public class ExcludeFilter : IFilesFilter
    {
        public FilesFilterType Type
        {
            get
            {
                return FilesFilterType.Exclude;
            }
        }

        public ExcludeFilter(string[] options)
        {
            this.Options = options;
        }

        public string[] Options { get; private set; }

        public string[] Apply(string rootPath, bool includeSubdirs)
        {
            throw new System.NotImplementedException();
        }
    }

    public class WhildCardFilter : IFilesFilter
    {
        public FilesFilterType Type
        {
            get
            {
                return FilesFilterType.WildCard;
            }
        }

        public WhildCardFilter(string[] options)
        {
            this.Options = options;
        }

        public string[] Options { get; private set; }

        public string[] Apply(string rootPath, bool includeSubdirs)
        {
            throw new System.NotImplementedException();
        }
    }

    public class NamePatternFilter : IFilesFilter
    {
        private Regex _regex;

        public FilesFilterType Type
        {
            get
            {
                return FilesFilterType.NamePattern;
            }
        }

        public NamePatternFilter(string pattern)
        {
            this.Pattern = pattern;
            _regex = new Regex(pattern);
        }

        public string Pattern { get; private set; }

        public string[] Apply(string rootPath, bool includeSubdirs)
        {
            throw new System.NotImplementedException();
        }
    }
}