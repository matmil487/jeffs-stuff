using System.ComponentModel.DataAnnotations;

namespace ShoppingApi.Models;


public record ShoppingItem(Guid Id, string Name);


public record ShoppingItemCreate
{
    [Required, MaxLength(30)]
    public string Name { get; set; } = string.Empty;
}