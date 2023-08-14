using Entities;

namespace RepositoryContracts
{
 /// <summary>
 /// Represents data access logic for managing Person entity
 /// </summary>
 public interface ITeachersRepository
 {
  /// <summary>
  /// Adds a new country object to the data store
  /// </summary>
  /// <param name="country">Country object to add</param>
  /// <returns>Returns the country object after adding it to the data store</returns>
  Task<Teachers> AddCountry(Teachers country);


  /// <summary>
  /// Returns all countries in the data store
  /// </summary>
  /// <returns>All countries from the table</returns>
  Task<List<Teachers>> GetAllCountries();


  
 }
}
