using System.ComponentModel.DataAnnotations;

namespace Entities
{
  /// <summary>
  /// Domain Model for Country
  /// </summary>
  public class Teachers
  {
    [Key]
    public Guid PersonID { get; set; }

    [Required]
    public string? Title { get; set; }
  
    [StringLength(40)] //nvarchar(40)
                        //[Required]
    public string? Name { get; set; }

    [StringLength(40)] //nvarchar(40)
    public string? Surname { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [StringLength(10)] //nvarchar(100)
    public int? TeacherNumber { get; set; }

    //uniqueidentifier
    public int? NationalIDNumber { get; set; }

    public decimal? Salary { get; set; }
    }
}
