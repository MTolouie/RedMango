using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedMango_DataLayer.Models;

public class MenuItem
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string SpecialTag { get; set; }
    public string Category { get; set; }
    [Range(1, int.MaxValue)]
    public double Price { get; set; }
    public string Image { get; set; }
}
