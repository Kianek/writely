using Writely.Models;

namespace Writely.Services
{
    public class JournalUpdater : IModelUpdater<Journal, JournalUpdateModel>
    {
        public (Journal?, bool) Update(Journal model, JournalUpdateModel updateModel)
        {
            throw new System.NotImplementedException();
        }
    }
}