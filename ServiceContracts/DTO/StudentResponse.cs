using System;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO
{
  /// <summary>
  /// Represents DTO class that is used as return type of most methods of Persons Service
  /// </summary>
  public class StudentResponse
  {
    public Guid PersonID { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public int? StudentNumber { get; set; }
    public int? NationalIDNumber { get; set; }
   
    

    /// <summary>
    /// Compares the current object data with the parameter object
    /// </summary>
    /// <param name="obj">The PersonResponse Object to compare</param>
    /// <returns>True or false, indicating whether all person details are matched with the specified parameter object</returns>
    public override bool Equals(object? obj)
    {
      if (obj == null) return false;

      if (obj.GetType() != typeof(StudentResponse)) return false;

      StudentResponse student = (StudentResponse)obj;
      return PersonID == student.PersonID && Name == student.Name && Surname == student.Surname && DateOfBirth == student.DateOfBirth && StudentNumber == student.StudentNumber && NationalIDNumber == student.NationalIDNumber ;
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return $"Person ID: {PersonID}, Person Name: {Name}, Email: {Surname}, Date of Birth: {DateOfBirth?.ToString("dd MMM yyyy")},Student Number:{StudentNumber}, National ID Number: {NationalIDNumber}";
    }

    
  }


  public static class StudentExtensions
  {
    /// <summary>
    /// An extension method to convert an object of Person class into PersonResponse class
    /// </summary>
    /// <param name="person">The Person object to convert</param>
    /// /// <returns>Returns the converted PersonResponse object</returns>
    public static StudentResponse ToPersonResponse(this Student student)
    {
      //person => convert => PersonResponse
      return new StudentResponse() { PersonID = student.PersonID, Name = student.Name, Surname = student.Surname, DateOfBirth = student.DateOfBirth, StudentNumber = student.StudentNumber, NationalIDNumber = student.NationalIDNumber

        };
    }
  }
}
