using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
 public class ApplicationDbContext : DbContext
 {
  public ApplicationDbContext(DbContextOptions options) : base(options)
  {
  }

  public virtual DbSet<Teachers> Teacher { get; set; }
  public virtual DbSet<Student> Students { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
   base.OnModelCreating(modelBuilder);

   modelBuilder.Entity<Teachers>().ToTable("Teachers");
   modelBuilder.Entity<Student>().ToTable("Students");

   //Seed to Countries
   string teachersJson = System.IO.File.ReadAllText("teachers.json");
   List<Teachers> countries = System.Text.Json.JsonSerializer.Deserialize<List<Teachers>>(teachersJson);

   foreach (Teachers country in countries)
    modelBuilder.Entity<Teachers>().HasData(country);


   //Seed to Persons
   string studentsJson = System.IO.File.ReadAllText("students.json");
   List<Student> students = System.Text.Json.JsonSerializer.Deserialize<List<Student>>(studentsJson);

   foreach (Student student in students)
    modelBuilder.Entity<Student>().HasData(student);


   //Fluent API
   modelBuilder.Entity<Student>().Property(temp => temp.TIN)
     .HasColumnName("TaxIdentificationNumber")
     .HasColumnType("varchar(8)")
     .HasDefaultValue("ABC12345");

   //modelBuilder.Entity<Person>()
   //  .HasIndex(temp => temp.TIN).IsUnique();

   modelBuilder.Entity<Student>()
     .HasCheckConstraint("CHK_TIN", "len([TaxIdentificationNumber]) = 8");

   
  }

  public List<Student> sp_GetAllPersons()
  {
   return Persons.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]").ToList();
  }

  public int sp_InsertPerson(Student student)
  {
   SqlParameter[] parameters = new SqlParameter[] {
        new SqlParameter("@PersonID", student.PersonID),
        new SqlParameter("@Name", student.Name),
        new SqlParameter("@surname", student.Surname),
        new SqlParameter("@DateOfBirth", student.DateOfBirth),
        new SqlParameter("@NationalIDNumber", student.NationalIDNumber),
        new SqlParameter("@CountryID", student.StudentNumber),
       
      };

   return Database.ExecuteSqlRaw("EXECUTE [dbo].[InsertPerson] @PersonID, @PersonName, @Email, @DateOfBirth, @Gender, @CountryID, @Address, @ReceiveNewsLetters", parameters);
  }
 }
}
