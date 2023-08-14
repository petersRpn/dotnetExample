using System;
using System.Collections.Generic;
using Xunit;
using ServiceContracts;
using Entities;
using ServiceContracts.DTO;
using Services;
using ServiceContracts.Enums;
using Xunit.Abstractions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EntityFrameworkCoreMock;
using AutoFixture;
using FluentAssertions;
using RepositoryContracts;
using Moq;
using System.Linq.Expressions;

namespace CRUDTests
{
 public class StudentServiceTest
 {
  //private fields
  private readonly IStudentsService _studentService;

  private readonly Mock<IStudentsRepository> _studentRepositoryMock;
  private readonly IStudentsRepository _studentsRepository;

  private readonly ITestOutputHelper _testOutputHelper;
  private readonly IFixture _fixture;

  //constructor
  public StudentServiceTest(ITestOutputHelper testOutputHelper)
  {
   _fixture = new Fixture();
   _studentRepositoryMock = new Mock<IStudentsRepository>();
   _studentsRepository = _studentRepositoryMock.Object;

   _studentService = new StudentsService(_studentsRepository);

   _testOutputHelper = testOutputHelper;
  }


  #region AddStudent

  //When we supply null value as PersonAddRequest, it should throw ArgumentNullException
  [Fact]
  public async Task AddPerson_NullPerson_ToBeArgumentNullException()
  {
   //Arrange
   StudentAddRequest? studentAddRequest = null;

   //Act
   Func<Task> action = async () =>
   {
    await _studentService.AddStudent(studentAddRequest);
   };

   //Assert
   await action.Should().ThrowAsync<ArgumentNullException>();
  }


  //When we supply null value as PersonName, it should throw ArgumentException
  [Fact]
  public async Task AddPerson_PersonNameIsNull_ToBeArgumentException()
  {
   //Arrange
   StudentAddRequest? studentAddRequest = _fixture.Build<StudentAddRequest>()
    .With(temp => temp.Name, null as string)
    .Create();

   Student person = studentAddRequest.ToStudent();

   //When PersonsRepository.AddPerson is called, it has to return the same "person" object
   _studentRepositoryMock
    .Setup(temp => temp.AddPerson(It.IsAny<Student>()))
    .ReturnsAsync(person);

   //Act
   Func<Task> action = async () =>
   {
    await _studentService.AddStudent(studentAddRequest);
   };

   //Assert
   await action.Should().ThrowAsync<ArgumentException>();
  }




  //When we supply proper person details, it should insert the person into the persons list; and it should return an object of PersonResponse, which includes with the newly generated person id
  [Fact]
  public async Task AddPerson_FullPersonDetails_ToBeSuccessful()
  {
   //Arrange
   StudentAddRequest? studentAddRequest = _fixture.Build<StudentAddRequest>()
    .With(temp => temp.Name, "Ade")
    .Create();

   Student person = studentAddRequest.ToStudent();
   StudentResponse person_response_expected = person.ToPersonResponse();

   //If we supply any argument value to the AddPerson method, it should return the same return value
   _studentRepositoryMock.Setup
    (temp => temp.AddPerson(It.IsAny<Student>()))
    .ReturnsAsync(person);

   //Act
   StudentResponse person_response_from_add = await _studentService.AddStudent(studentAddRequest);

   person_response_expected.PersonID = person_response_from_add.PersonID;

   //Assert
   person_response_from_add.PersonID.Should().NotBe(Guid.Empty);
   person_response_from_add.Should().Be(person_response_expected);
  }

  #endregion


  


  #region GetAllStudents

  //The GetAllPersons() should return an empty list by default
  [Fact]
  public async Task GetAllPersons_ToBeEmptyList()
  {
   //Arrange
   var persons = new List<Student>();
   _studentRepositoryMock
    .Setup(temp => temp.GetAllPersons())
    .ReturnsAsync(persons);

   //Act
   List<StudentResponse> persons_from_get = await _studentService.GetAllStudents();

   //Assert
   persons_from_get.Should().BeEmpty();
  }


  //First, we will add few persons; and then when we call GetAllPersons(), it should return the same persons that were added
  [Fact]
  public async Task GetAllPersons_WithFewPersons_ToBeSuccessful()
  {
   //Arrange
   List<Student> persons = new List<Student>() {
    _fixture.Build<Student>()
    .With(temp => temp.Name, "Ade")
    .With(temp => temp.Surname, "john")
    .Create(),

    _fixture.Build<Student>()
    .With(temp => temp.Name, "Ade")
    .With(temp => temp.Surname, "john")
    .Create(),

    _fixture.Build<Student>()
   .With(temp => temp.Name, "Ade")
    .With(temp => temp.Surname, "john")
    .Create()
   };

   List<StudentResponse> person_response_list_expected = persons.Select(temp => temp.ToPersonResponse()).ToList();


   //print person_response_list_from_add
   _testOutputHelper.WriteLine("Expected:");
   foreach (StudentResponse person_response_from_add in person_response_list_expected)
   {
    _testOutputHelper.WriteLine(person_response_from_add.ToString());
   }

   _studentRepositoryMock.Setup(temp => temp.GetAllPersons()).ReturnsAsync(persons);

   //Act
   List<StudentResponse> persons_list_from_get = await _studentService.GetAllStudents();

   //print persons_list_from_get
   _testOutputHelper.WriteLine("Actual:");
   foreach (StudentResponse person_response_from_get in persons_list_from_get)
   {
    _testOutputHelper.WriteLine(person_response_from_get.ToString());
   }

   //Assert
   persons_list_from_get.Should().BeEquivalentTo(person_response_list_expected);
  }
  #endregion




 
 }
}
