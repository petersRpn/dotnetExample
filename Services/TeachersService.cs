using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
 public class TeachersService : ITeachersService
 {
  //private field
  private readonly ITeachersRepository _teacherRepository;

  //constructor
  public TeachersService(ITeachersRepository countriesRepository)
  {
   _teacherRepository = countriesRepository;
  }

  public async Task<TeacherResponse> AddTeacher(TeacherAddRequest? teacherAddRequest)
  {
   //Validation: countryAddRequest parameter can't be null
   if (teacherAddRequest == null)
   {
    throw new ArgumentNullException(nameof(teacherAddRequest));
   }

   //Validation: CountryName can't be null
   if (teacherAddRequest.Name == null)
   {
    throw new ArgumentException(nameof(teacherAddRequest.Name));
   }

   

   //Convert object from CountryAddRequest to Country type
   Teachers teacher = teacherAddRequest.ToTeacher();

   //generate CountryID
   teacher.PersonID = Guid.NewGuid();

   //Add country object into _countries
   await _teacherRepository.AddCountry(teacher);

   return teacher.ToTeacherResponse();
  }

  public async Task<List<TeacherResponse>> GetAllTeachers()
  {
   List<Teachers> countries = await _teacherRepository.GetAllCountries();
   return countries
     .Select(country => country.ToTeacherResponse()).ToList();
  }

  

  
 }
}

