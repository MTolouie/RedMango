
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RedMango_DataLayer.Models;

public class ApplicationUser : IdentityUser
{
    [Required(ErrorMessage = "Name Is Required")]
    [MaxLength(150)]
    public string Name { get; set; }
}
