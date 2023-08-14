using System;
using System.Collections.Generic;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using Services;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Moq;
using AutoFixture;
using FluentAssertions;
using RepositoryContracts;
using System.Linq;

namespace CRUDTests
{
 public class TeachersServiceTest
 {
  private readonly ITeachersService _teachersService;
  private readonly Mock<ITeachersRepository> _teachersRepositoryMock;
  private readonly ITeachersRepository _teachersRepository;

  private readonly IFixture _fixture;

  //constructor
  public TeachersServiceTest()
  {
   _fixture = new Fixture();

   _teachersRepositoryMock = new Mock<ITeachersRepository>();
   _teachersRepository = _teachersRepositoryMock.Object;
   _teachersService = new TeachersService(_teachersRepository);
  }


  #region AddTeachers

  //When CountryAddRequest is null, it should throw ArgumentNullException
  [Fact]
  public async Task AddCountry_NullCountry_ToBeArgumentNullException()
  {
   //Arrange
   TeacherAddRequest? request = null;

   Teachers country = _fixture.Build<Teachers>()
        .With(temp => temp.Name).Create();

   _teachersRepositoryMock
    .Setup(temp => temp.AddCountry(It.IsAny<Teachers>()))
    .ReturnsAsync(country);


   //Act
   var action = async () =>
   {
    await _teachersService.AddTeacher(request);
   };

   //Assert
   await action.Should().ThrowAsync<ArgumentNullException>();
  }


  //When the CountryName is null, it should throw ArgumentException
  [Fact]
  public async Task AddCountry_CountryNameIsNull_ToBeArgumentException()
  {
   //Arrange
   TeacherAddRequest? request = _fixture.Build<TeacherAddRequest>()
    .With(temp => temp.Name, null as string)
    .Create();

   Teachers country = _fixture.Build<Teachers>()
        .With(temp => temp.Name).Create();

   _teachersRepositoryMock
    .Setup(temp => temp.AddCountry(It.IsAny<Teachers>()))
    .ReturnsAsync(country);

   //Act
   var action = async () =>
   {
    await _teachersService.AddTeacher(request);
   };

   //Assert
   await action.Should().ThrowAsync<ArgumentException>();
  }


        //When the CountryName is duplicate, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_DuplicateTeacherName_ToBeArgumentException()
        {
            //Arrange
            TeacherAddRequest first_teacher_request = _fixture.Build<TeacherAddRequest>()
                 .With(temp => temp.Name, "Test name").Create();
            TeacherAddRequest second_country_request = _fixture.Build<TeacherAddRequest>()
              .With(temp => temp.Name, "Test name").Create();

            Teachers first_country = first_teacher_request.ToTeacher();
            Teachers second_country = second_country_request.ToTeacher();

            _teachersRepositoryMock
             .Setup(temp => temp.AddCountry(It.IsAny<Teachers>()))
             .ReturnsAsync(first_country);

            //Act

        }

            //When you supply proper country name, it should insert (add) the country to the existing list of countries
            [Fact]
            public async Task AddCountry_FullCountry_ToBeSuccessful()
            {
                //Arrange
                TeacherAddRequest country_request = _fixture.Create<TeacherAddRequest>();
                Teachers country = country_request.ToTeacher();
                TeacherResponse country_response = country.ToTeacherResponse();

                _teachersRepositoryMock
                 .Setup(temp => temp.AddCountry(It.IsAny<Teachers>()))
                 .ReturnsAsync(country);

                ;


                //Act
                TeacherResponse teacher_from_add_teacher = await _teachersService.AddTeacher(country_request);

                var teacher = teacher_from_add_teacher.PersonID;

                //Assert
                teacher_from_add_teacher.PersonID.Should().NotBe(Guid.Empty);
                teacher_from_add_teacher.Should().BeEquivalentTo(teacher);
            }

            #endregion


            #region GetAllTeachers

            [Fact]
            //The list of countries should be empty by default (before adding any countries)
            public async Task GetAllCountries_ToBeEmptyList()
            {
                //Arrange
                List<Teachers> country_empty_list = new List<Teachers>();
                _teachersRepositoryMock.Setup(temp => temp.GetAllCountries()).ReturnsAsync(country_empty_list);

                //Act
                List<TeacherResponse> actual_country_response_list = await _teachersService.GetAllTeachers();

                //Assert
                actual_country_response_list.Should().BeEmpty();
            }


            [Fact]
            public async Task GetAllCountries_ShouldHaveFewCountries()
            {
                //Arrange
                List<Teachers> country_list = new List<Teachers>() {
        _fixture.Build<Teachers>()
        .With(temp => temp.Name).Create(),
        _fixture.Build<Teachers>()
        .With(temp => temp.Surname).Create()
      };

                List<TeacherResponse> country_response_list = country_list.Select(temp => temp.ToTeacherResponse()).ToList();

                _teachersRepositoryMock.Setup(temp => temp.GetAllCountries()).ReturnsAsync(country_list);

                //Act
                List<TeacherResponse> actualCountryResponseList = await _teachersService.GetAllTeachers();

                //Assert
                actualCountryResponseList.Should().BeEquivalentTo(country_response_list);
            }
            #endregion


        }
 
}
