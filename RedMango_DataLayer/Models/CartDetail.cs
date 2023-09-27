
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RedMango_DataLayer.Models;

public class CartDetail
{
    [Key]
    public int DetailId { get; set; }

    [Required]
    public int CartId { get; set; }

    [Required]
    public int MenuItemId { get; set; }
    
    [Required]
    public int Quantity { get; set; }

    [Required]
    public double Price { get; set; }

    #region relations

    [ForeignKey("CartId")]
    public Cart Cart { get; set; }

    [ForeignKey("MenuItemId")]
    public MenuItem MenuItem{ get; set; }
    #endregion
}
