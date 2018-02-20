using System;
using System.Collections.Generic;

namespace FileSystemWatcherTest.Managers
{
    public class MessageManager : IBaseManager<Message>
    {
        public void Save(Message message)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Message> List()
        {
            throw new NotImplementedException();
        }
    }
}