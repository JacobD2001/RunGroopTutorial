using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopTutorial.Data;
using RunGroopTutorial.Interfaces;
using RunGroopTutorial.Models;

namespace RunGroopTutorial.Controllers
{
    //naming of controler is model + Controller so e.g. ClubController
    public class ClubController : Controller
    {
     /*   private readonly ApplicationDbContext _context;

        //this is calling our db - version b4 sql injection

        public ClubController(ApplicationDbContext context)
        {
            _context = context; //context from another part of our program 'injecting'
        }*/

        //4. adding clubrepository to ctor for sql injection

        private readonly IClubRepository _clubRepository;
        public ClubController(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }

        //B4
        public IActionResult Index() //CCCCCCCCC - CONTROLER 
        {
            List<Club> clubs = _context.Clubs.ToList(); //data from table clubs and tolist //MMMMMMMMMM - MODEL
            return View(clubs); //VVVVVVVVV - VIEW
        }

        public IActionResult Detail(int id)
        {
            //when using include is a equal to a join so it is very expensive for a db, thanks to include we can get to address
            //cuz address is a navigation prop, relations, lazy loading ef
            Club club = _context.Clubs.Include(a => a.Adress).FirstOrDefault(c => c.Id == id); //returns first element that satisfies that condition or default is condition is not met
            return View(club);
        }

    }
}
