using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
 public class TeachersRepository : ITeachersRepository
 {
  private readonly ApplicationDbContext _db;

  public TeachersRepository(ApplicationDbContext db)
  {
   _db = db;
  }

  public async Task<Teachers> AddCountry(Teachers teacher)
  {
   _db.Teachers.Add(teacher);
   await _db.SaveChangesAsync();

   return teacher;
  }

  public async Task<List<Teachers>> GetAllCountries()
  {
   return await _db.Teachers.ToListAsync();
  }

 
  }
 }
}