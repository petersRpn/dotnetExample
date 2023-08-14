using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Entities;
using RepositoryContracts;
using System.Linq.Expressions;

namespace Repositories
{
 public class StudentRepository : IStudentsRepository
 {
  private readonly ApplicationDbContext _db;

  public StudentRepository(ApplicationDbContext db)
  {
   _db = db;
  }

  public async Task<Student> AddPerson(Student student)
  {
   _db.Students.Add(student);
   await _db.SaveChangesAsync();

   return student;
  }

  

  public async Task<List<Student>> GetAllPersons()
  {
   return await _db.Students.Include("Student").ToListAsync();
  }

 
}
