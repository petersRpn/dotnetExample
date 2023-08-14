using ServiceContracts.DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ServiceContracts
{
 /// <summary>
 /// Represents business logic for manipulating Country entity
 /// </summary>
 public interface ITeachersService
 {
  /// <summary>
  /// Adds a country object to the list of countries
  /// </summary>
  /// <param name="teacherAddRequest">Country object to add</param>
  /// <returns>Returns the country object after adding it (including newly generated country id)</returns>
  Task<TeacherResponse> AddTeacher(TeacherAddRequest? teacherAddRequest);

  /// <summary>
  /// Returns all countries from the list
  /// </summary>
  /// <returns>All countries from the list as List of CountryResponse</CountryResponse></returns>
  Task<List<TeacherResponse>> GetAllTeachers();


  
 }
}
