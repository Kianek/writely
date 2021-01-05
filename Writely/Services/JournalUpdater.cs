using Writely.Models;

namespace Writely.Services
{
    public class JournalUpdater : IModelUpdater<Journal, JournalUpdateModel>
    {
        public (Journal?, bool) Update(Journal model, JournalUpdateModel updateModel)
        {
            var didUpdate = false;
            if (updateModel?.Title is not null && model.Title != updateModel.Title)
            {
                model.Title = updateModel.Title;
                didUpdate = true;
            }
            
            return (model, didUpdate);
        }
    }
}