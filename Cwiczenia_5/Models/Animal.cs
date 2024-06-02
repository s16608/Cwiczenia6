using System.ComponentModel.DataAnnotations;

namespace Cwiczenia_5.Models;

public class Animal
{
    public int IdAnimal { get; set; }
    [Required]
    [StringLength(200)]
    public string Name { get; set; }
    
    [StringLength(200)]
    public string Description { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Category { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Area { get; set; }
}