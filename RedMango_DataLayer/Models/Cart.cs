
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RedMango_DataLayer.Models;

public class Cart
{
    [Key]
    public int CartId { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    public double CartSum { get; set; }

    public bool IsFinally { get; set; } = false;

    [Required]
    public DateTime CreateDate { get; set; }


    #region relations

    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }

    public List<CartDetail> CartDetails { get; set; }
    #endregion
    [NotMapped]
    public string StripePaymentIntentId { get; set; }
    [NotMapped]
    public string ClientSecret { get; set; }
}
