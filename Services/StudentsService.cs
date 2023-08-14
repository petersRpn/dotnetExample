using System;
using Entities;
using ServiceContracts.DTO;
using ServiceContracts;
using Services.Helpers;
using RepositoryContracts;

namespace Services
{
    public class StudentsService : IStudentsService
    {
        //private field
        private readonly IStudentsRepository _studentRepository;

        //constructor
        public StudentsService(IStudentsRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        public async Task<StudentResponse> AddStudent(StudentAddRequest? studentAddRequest)
        {
            //check if PersonAddRequest is not null
            if (studentAddRequest == null)
            {
                throw new ArgumentNullException(nameof(studentAddRequest));
            }

            //Model validation
            ValidationHelper.ModelValidation(studentAddRequest);

            //convert personAddRequest into Person type
            Student person = studentAddRequest.ToStudent();

            //generate PersonID
            person.PersonID = Guid.NewGuid();

            //add person object to persons list
            await _studentRepository.AddPerson(person);

            //convert the Person object into PersonResponse type
            return person.ToPersonResponse();
        }


        public async Task<List<StudentResponse>> GetAllStudents()
        {
            var persons = await _studentRepository.GetAllPersons();

            return persons
              .Select(temp => temp.ToPersonResponse()).ToList();
        }

       
    }





}
