using System;
using ServiceContracts.Enums;
using Entities;
using System.ComponentModel.DataAnnotations;
using Example.Services.Helpers;

namespace ServiceContracts.DTO
{
  /// <summary>
  /// Acts as a DTO for inserting a new student
  /// </summary>
  public class StudentAddRequest
  {
    [Required(ErrorMessage = "Person Name can't be blank")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Person Surname can't be blank")]
    public string? Surname { get; set; }

    [DataType(DataType.Date)]
    [MaximumAgeValidatorAttribute(2001)]
    public DateTime? DateOfBirth { get; set; }

    [Required(ErrorMessage = "Please Student number is required")]
    public int? StudentNumber { get; set; }

    [Required(ErrorMessage = "Please National Identification number is required")]
    public int? NationalIDNumber { get; set; }


    /// <summary>
    /// Converts the current object of PersonAddRequest into a new object of Person type
    /// </summary>
    /// <returns></returns>
    public Student ToStudent()
    {
      return new Student() { Name = Name, Surname = Surname, DateOfBirth = DateOfBirth, StudentNumber = StudentNumber, NationalIDNumber = NationalIDNumber };
    }
  }
}
