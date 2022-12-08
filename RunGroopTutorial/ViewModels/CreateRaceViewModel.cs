using RunGroopTutorial.Data.Enum;
using RunGroopTutorial.Models;

namespace RunGroopTutorial.ViewModels
{
    public class CreateRaceViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Adress Adress { get; set; }
        public IFormFile? Image { get; set; }
        public RacesCategory RaceCategory { get; set; }
    }
}
