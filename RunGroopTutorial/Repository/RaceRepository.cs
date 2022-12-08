using Microsoft.EntityFrameworkCore;
using RunGroopTutorial.Data;
using RunGroopTutorial.Interfaces;
using RunGroopTutorial.Models;

namespace RunGroopTutorial.Repository
{
    public class RaceRepository : IRaceRepository
    {
        //bringing db (ctor)
        private readonly ApplicationDbContext _context;
        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Race race)
        {
            //EF - When calling add it is generating sql and than when you click save than tsql is sent 
            _context.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            _context.Remove(race);
            return Save();
        }

        //get all - it displays all data from clubs  
        //async is like a buzzer in kebab sth like notifes you oh hey you are gonna get meal ? "Here's your buzzer we don't know when you table comes but when it comes it will do 'task'"
        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync(); //returns IEnumerable list
        }

        public async Task<Race?> GetByIdAsync(int id)
        {
            return await _context.Races.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<Race?> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Races.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync();
        }


        //return list of clubs where city adress is like city club -> adress -> city
        public async Task<IEnumerable<Race>> GetClubByCity(string city)
        {
            return await _context.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges(); //savechanges returns int 
            return saved > 0 ? true : false; //if saved > 0 return true, else return false 
        }

        public bool Update(Race race)
        {
            _context.Update(race);
            return Save();
        }

     
    }
}
