namespace eWorldCup.Core.Interfaces.Repositories;

public interface ICrudRepository<TObject, in TIdentifier>
{
    /// <summary>
    /// Create
    /// </summary>
    /// <param name="value"></param>
    TObject Add(TObject value);
    
    /// <summary>
    /// Read
    /// </summary>
    /// <param name="id"></param>
    TObject Get(TIdentifier id);
    
    /// <summary>
    /// Read all
    /// </summary>
    /// <param name="id"></param>
    IEnumerable<TObject> GetAll();
    
    /// <summary>
    /// Update
    /// </summary>
    /// <param name="value"></param>
    TObject Update(TObject value);

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id"></param>
    bool Delete(TIdentifier id);
}