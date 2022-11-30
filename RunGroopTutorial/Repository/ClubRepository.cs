using Microsoft.EntityFrameworkCore;
using RunGroopTutorial.Data;
using RunGroopTutorial.Interfaces;
using RunGroopTutorial.Models;
// 2. create repository implementing interface
namespace RunGroopTutorial.Repository
{
    public class ClubRepository : IClubRepository //implementing interface for SQL injection
    {
        //bringing db (ctor)
        private readonly ApplicationDbContext _context;
        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Club club)
        {
            //EF - When calling add it is generating sql and than when you click save than tsql is sent 
            _context.Add(club);
            return Save();
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return Save();
        }

        //get all - it displays all data from clubs  
        //async is like a buzzer in kebab sth like notifes you oh hey you are gonna get meal ? "Here's your buzzer we don't know when you table comes but when it comes it will do 'task'"
        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Clubs.ToListAsync(); //returns IEnumerable list
        }

        public async Task<Club> GetByIdAsync(int id)
        {
            return await _context.Clubs.FirstOrDefaultAsync(i => i.Id == id); //returns first element that satisfies specified condition in brackets()
        }

        //return list of clubs where city adress is like city club -> adress -> city
        public async Task<IEnumerable<Club>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(c => c.Adress.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
           var saved = _context.SaveChanges(); //savechanges returns int 
           return saved > 0 ? true : false; //if saved > 0 return true, else return false 
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return Save();
        }
    }
}
