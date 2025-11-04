namespace eWorldCup.Core.Models.API;

public class PlayerScheduleApiModel
{
    /// <summary>
    /// Gets or sets the matches for the player, where the key is the round number and the value is the match details.
    /// </summary>
    public Dictionary<long, MatchApiModel> Matches { get; set; } = new();
}