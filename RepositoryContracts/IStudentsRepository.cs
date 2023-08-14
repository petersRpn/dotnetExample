﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryContracts
{
 /// <summary>
 /// Represents data access logic for managing Person entity
 /// </summary>
 public interface IStudentsRepository
 {
  /// <summary>
  /// Adds a person object to the data store
  /// </summary>
  /// <param name="person">Person object to add</param>
  /// <returns>Returns the person object after adding it to the table</returns>
  Task<Student> AddPerson(Student person);


  /// <summary>
  /// Returns all persons in the data store
  /// </summary>
  /// <returns>List of person objects from table</returns>
  Task<List<Student>> GetAllPersons();

 }
}
