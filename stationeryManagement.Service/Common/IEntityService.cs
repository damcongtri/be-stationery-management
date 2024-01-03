namespace stationeryManagement.Service.Common;

public interface IEntityService<T>
{
    /// <summary>
    /// Get record by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T?> GetByIdAsync<TDataType>(TDataType id);

    /// <summary>
    /// Create an entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T?> CreateAsync(T entity);

    /// <summary>
    ///delete entity by id
    /// </summary>
    /// <param name="id"></param>
    /// <typeparam name="TDataType"></typeparam>
    /// <returns></returns>
    Task<bool> DeleteById<TDataType>(TDataType id);

    /// <summary>
    /// delete entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(T entity);

    /// <summary>
    /// get all entity
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// update entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(T entity);
}