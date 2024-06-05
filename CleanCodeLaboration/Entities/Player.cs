using CleanCodeLaboration.Interfaces;

namespace CleanCodeLaboration.Entities;

public class Player : IPlayer
{
    public string PlayerId { get; set; } = string.Empty;
}
