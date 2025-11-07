using System.ComponentModel.DataAnnotations;

namespace Firmezaa.Web.Models.Entities;

public abstract class Person
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string? FullName { get; set; }

    [MaxLength(50)]
    public string? DocumentNumber { get; set; }

    [MaxLength(200)]
    public string? Address { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [Range(0, 150, ErrorMessage = "La edad debe estar entre 0 y 150 años.")]
    public int Age { get; set; }
}