using System;
using Entities;

namespace ServiceContracts.DTO
{
  /// <summary>
  /// DTO class that is used as return type for most of CountriesService methods
  /// </summary>
  public class TeacherResponse
  {
    public Guid PersonID { get; set; }

    public string? Title { get; set; }
    public string? Name { get; set; }

    public string? Surname { get; set; }
    public DateTime? DateOfBirth{ get; set; }

    public int? TeacherNumber { get; set; }
    public int? NationalIDNumber { get; set; }
    public decimal? Salary { get; set; }

        //It compares the current object to another object of CountryResponse type and returns true, if both values are same; otherwise returns false
        public override bool Equals(object? obj)
    {
      if (obj == null)
      {
        return false;
      }

      if (obj.GetType() != typeof(TeacherResponse))
      {
        return false;
      }
      TeacherResponse country_to_compare = (TeacherResponse)obj;

      return PersonID == country_to_compare.PersonID && Name == country_to_compare.Name;
    }

    //returns an unique key for the current object
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
  }

  public static class CountryExtensions
  {
    //Converts from Country object to CountryResponse object
    public static TeacherResponse ToTeacherResponse(this Teachers teacher)
    {
      return new TeacherResponse() {  PersonID = teacher.PersonID,Title = teacher.Title, Name = teacher.Name, Surname = teacher.Surname, DateOfBirth = teacher.DateOfBirth, TeacherNumber = teacher.TeacherNumber, NationalIDNumber = teacher.NationalIDNumber, Salary = teacher.Salary};
    }
  }
}
