using System.Collections.Generic;
using System.Threading.Tasks;
using Writely.Models;

namespace Writely.Services
{
    public interface IEntryService
    {
        Entry? GetById(Journal journal, long entryId);
        Entry Add(Journal journal, NewEntry model);
        Entry Update(Journal journal, long entryId, EntryUpdate updateModel);
        Entry Remove(Journal journal, long entryId);
    }
}