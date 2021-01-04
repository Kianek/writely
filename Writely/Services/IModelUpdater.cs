namespace Writely.Services
{
    /// <summary>
    /// Updates an existing model (T1) with information from an update model (T2).
    /// </summary>
    /// <typeparam name="T1">The model to update</typeparam>
    /// <typeparam name="T2">The update model containing the new information</typeparam>
    public interface IModelUpdater<T1, T2>
    {
        (T1?, bool) Update(T1 model, T2 updateModel);
    }
}