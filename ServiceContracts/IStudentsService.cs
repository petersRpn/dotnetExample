using System;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ServiceContracts
{
 /// <summary>
 /// Represents business logic for manipulating Perosn entity
 /// </summary>
 public interface IStudentsService
 {
  /// <summary>
  /// Addds a new person into the list of persons
  /// </summary>
  /// <param name="studentAddRequest">Person to add</param>
  /// <returns>Returns the same person details, along with newly generated PersonID</returns>
  Task<StudentResponse> AddStudent(StudentAddRequest? studentAddRequest);


  /// <summary>
  /// Returns all persons
  /// </summary>
  /// <returns>Returns a list of objects of PersonResponse type</returns>
  Task<List<StudentResponse>> GetAllStudents();

  
 }
}
