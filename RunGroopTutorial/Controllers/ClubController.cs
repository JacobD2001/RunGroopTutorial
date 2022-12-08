using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunGroopTutorial.Data;
using RunGroopTutorial.Interfaces;
using RunGroopTutorial.Models;
using RunGroopTutorial.ViewModels;
using System.Net;

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
        private readonly IPhotoService _photoService;

        public ClubController(IClubRepository clubRepository, IPhotoService photoService)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
        }

        
        public async Task<IActionResult> Index() //CCCCCCCCC - CONTROLER 
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll(); //data from table clubs and tolist //MMMMMMMMMM - MODEL
            return View(clubs); //VVVVVVVVV - VIEW
        }

        public async Task<IActionResult> Detail(int id)
        {
            //when using include is a equal to a join so it is very expensive for a db, thanks to include we can get to address
            //cuz address is a navigation prop, relations, lazy loading ef
            // Club club = _context.Clubs.Include(a => a.Adress).FirstOrDefault(c => c.Id == id); //returns first element that satisfies that condition or default is condition is not met
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }

        //just a create page so not neccesairly async
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
                if (ModelState.IsValid)
                {
                    var result = await _photoService.AddPhotoAsync(clubVM.Image);

                    var club = new Club
                    {
                        Title = clubVM.Title,
                        Description = clubVM.Description,
                        Image = result.Url.ToString(),
                        Adress = new Adress
                        {
                            Street = clubVM.Adress.Street,
                            City = clubVM.Adress.City,
                            State = clubVM.Adress.State,
                        }
                    };
                    _clubRepository.Add(club);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Photo upload failed");
                }


            return View(clubVM);
        }
        //edit button so selects data by id  
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            if(club == null) return View("Error");

            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = club.Adress,
                URL = club.Image,
                ClubCategory = club.ClubCategory
            };
            return View(clubVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", clubVM);
            }

            var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);

            if (userClub == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(clubVM);
            }

            if (!string.IsNullOrEmpty(userClub.Image))
            {
                _ = _photoService.DeletePhotoAsync(userClub.Image);
            }

            var club = new Club
            {
                Id = id,
                Title = clubVM.Title,
                Description = clubVM.Description,
                Image = photoResult.Url.ToString(),
                AddressId = clubVM.AddressId,
                Adress = clubVM.Address,
            };

            _clubRepository.Update(club);

            return RedirectToAction("Index");
        }







    }
}
