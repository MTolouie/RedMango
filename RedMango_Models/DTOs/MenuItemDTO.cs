using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedMango_Models.DTOs;

public class MenuItemDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name Is Required")]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description Is Required")]
    public string Description { get; set; }
    [Required(ErrorMessage = "Tag Is Required")]
    [MaxLength(100)]
    public string SpecialTag { get; set; }
    [Required(ErrorMessage = "Category Is Required")]
    [MaxLength(100)]
    public string Category { get; set; }
    [Range(1, int.MaxValue)]
    [Required(ErrorMessage = "Price Is Required")]
    public double Price { get; set; }
    [Required(ErrorMessage = "Image Is Required")]
    [MaxLength(800)]
    public string Image{ get; set; }
}
