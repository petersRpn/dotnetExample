using AutoFixture;
using Moq;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using CRUDExample.Controllers;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CRUDTests
{
 public class StudentsControllerTest
 {
  private readonly IStudentsService _personsService;
  private readonly ITeachersService _countriesService;

  private readonly Mock<ITeachersService> _countriesServiceMock;
  private readonly Mock<IStudentsService> _personsServiceMock;

  private readonly Fixture _fixture;

  public StudentsControllerTest()
  {
   _fixture = new Fixture();

   _countriesServiceMock = new Mock<ITeachersService>();
   _personsServiceMock = new Mock<IStudentsService>();

   _countriesService = _countriesServiceMock.Object;
   _personsService = _personsServiceMock.Object;
  }

  #region Index

  [Fact]
  public async Task Index_ShouldReturnIndexViewWithPersonsList()
  {
   //Arrange
   List<StudentResponse> persons_response_list = _fixture.Create<List<StudentResponse>>();

   TeachersController personsController = new TeachersController(_personService, _countriesService);

  

   //Act
   IActionResult result = await personsController.Index(_fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<string>(), _fixture.Create<SortOrderOptions>());

   //Assert
   ViewResult viewResult = Assert.IsType<ViewResult>(result);

   viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<StudentResponse>>();
   viewResult.ViewData.Model.Should().Be(persons_response_list);
  }
  #endregion


  #region Create

  [Fact]
  public async void Create_IfModelErrors_ToReturnCreateView()
  {
   //Arrange
   StudentAddRequest person_add_request = _fixture.Create<StudentAddRequest>();

   PersonResponse person_response = _fixture.Create<PersonResponse>();

   List<CountryResponse> countries = _fixture.Create<List<CountryResponse>>();

   _countriesServiceMock
    .Setup(temp => temp.GetAllCountries())
    .ReturnsAsync(countries);

   _personsServiceMock
    .Setup(temp => temp.AddPerson(It.IsAny<StudentAddRequest>()))
    .ReturnsAsync(person_response);

   TeachersController personsController = new TeachersController(_personsService, _countriesService);


   //Act
   personsController.ModelState.AddModelError("PersonName", "Person Name can't be blank");

   IActionResult result = await personsController.Create(person_add_request);

   //Assert
   ViewResult viewResult = Assert.IsType<ViewResult>(result);

   viewResult.ViewData.Model.Should().BeAssignableTo<StudentAddRequest>();

   viewResult.ViewData.Model.Should().Be(person_add_request);
  }


  [Fact]
  public async void Create_IfNoModelErrors_ToReturnRedirectToIndex()
  {
   //Arrange
   StudentAddRequest person_add_request = _fixture.Create<StudentAddRequest>();

   PersonResponse person_response = _fixture.Create<PersonResponse>();

   List<CountryResponse> countries = _fixture.Create<List<CountryResponse>>();

   _countriesServiceMock
    .Setup(temp => temp.GetAllCountries())
    .ReturnsAsync(countries);

   _personsServiceMock
    .Setup(temp => temp.AddPerson(It.IsAny<StudentAddRequest>()))
    .ReturnsAsync(person_response);

   TeachersController personsController = new TeachersController(_personsService, _countriesService);


   //Act
   IActionResult result = await personsController.Create(person_add_request);

   //Assert
   RedirectToActionResult redirectResult = Assert.IsType<RedirectToActionResult>(result);

   redirectResult.ActionName.Should().Be("Index");
  }

  #endregion
 }
}
