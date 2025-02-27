﻿using RunGroopTutorial.Models;

namespace RunGroopTutorial.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAll();
        Task<Race> GetByIdAsync(int id);
        Task<Race?> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Race>> GetClubByCity(string city);
        bool Add(Race race);
        bool Update(Race race);
        bool Delete(Race race);
        bool Save();
    }
}
