using RunGroopTutorial.Data.Enum;
using RunGroopTutorial.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunGroopTutorial.ViewModels
{

    /* view model is a "translated" version of the model that is tailored to the needs of the view. It typically contains only the data that is needed by the view*/
    public class CreateClubViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Adress Adress { get; set; }
        public IFormFile? Image { get; set; }
        public ClubCategory ClubCategory { get; set; }
    }
}
