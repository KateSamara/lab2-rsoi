using RatingSystem.Domain.Models;

namespace RatingSystem.Domain.Interfaces.Repositories;

public interface IRatingRepository
{
    public Task<Rating> GetRatingByUsernameAsync(string username);
    
    public Task<int> GetRatingCountAsync();
    
    public Task AddRatingAsync(Rating rating);
}