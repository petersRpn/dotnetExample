using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
  /// <summary>
  /// Person domain model class
  /// </summary>
  public class Student
  {
    [Key]
    public Guid PersonID { get; set; }

    [StringLength(40)] //nvarchar(40)
    //[Required]
    public string? Name { get; set; }

    [StringLength(40)] //nvarchar(40)
    public string? Surname { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [StringLength(10)] //nvarchar(100)
    public int? StudentNumber { get; set; }

    //uniqueidentifier
    public int? NationalIDNumber { get; set; }
    public object TIN { get; internal set; }
    }
}
