



using System.ComponentModel.DataAnnotations;

namespace testGameStore.Server.Models;

public class Game
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public required string Name  { get; set; }

    [Required]
    [StringLength(30)]
    public required string Genre  { get; set; }

    [Range(1,100)]
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
}