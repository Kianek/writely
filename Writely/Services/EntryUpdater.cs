using System.Threading.Tasks;
using Writely.Models;

namespace Writely.Services
{
    public class EntryUpdater : IModelUpdater<Entry, EntryUpdateModel>
    {
        public (Entry?, bool) Update(Entry model, EntryUpdateModel updateModel)
        {
            bool didUpdate = false;
            if (updateModel?.Title is not null && model.Title != updateModel.Title)
            {
                model.Title = updateModel.Title;
                didUpdate = true;
            }

            if (updateModel?.Tags is not null && model.Tags != updateModel.Tags)
            {
                model.Tags = updateModel.Tags;
                didUpdate = true;
            }

            if (updateModel?.Body is not null && model.Body != updateModel.Body)
            {
                model.Body = updateModel.Body;
                didUpdate = true;
            }

            return (model, didUpdate);
        }
    }
}