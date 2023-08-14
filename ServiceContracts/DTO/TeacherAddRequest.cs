using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities;
using Example.Services.Helpers;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO
{
  /// <summary>
  /// DTO class for adding a new country
  /// </summary>
  public class TeacherAddRequest
  {

    [Required(ErrorMessage = "Person Name can't be blank")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Person Surname can't be blank")]
    public string? Surname { get; set; }

    [DataType(DataType.Date)]
    [MaximumAgeValidatorAttribute(2001)]
    public DateTime? DateOfBirth { get; set; }

    [Required(ErrorMessage = "Please select a title ")]
    public TitleOptions? Title { get; set; }
    [Required(ErrorMessage = "Please Student number is required")]
    public int? TeacherNumber { get; set; }

    [Required(ErrorMessage = "Please National Identification number is required")]
    public int? NationalIDNumber { get; set; }

    public decimal? Salary { get; set; }

    public Teachers ToTeacher()
{
    return new Teachers() {  Title = Title.ToString(), Name = Name, Surname = Surname, DateOfBirth = DateOfBirth, TeacherNumber = teacher.TeacherNumber, NationalIDNumber = teacher.NationalIDNumber, Salary = teacher.Salary };
}
  }
}

