using System;
using System.Collections.Generic;
using FileSystemWatcherTest.Managers;

namespace FileSystemWatcherTest
{
    internal class MessageTagger
    {
        private readonly TagManager _tagManager;

        public MessageTagger(TagManager tagManager)
        {
            _tagManager = tagManager;
        }

        public List<Tag> GetTags(string message)
        {
            throw new NotImplementedException();    
        }
    }
}