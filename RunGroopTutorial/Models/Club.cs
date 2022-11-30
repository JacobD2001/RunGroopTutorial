using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;
using RunGroopTutorial.Data.Enum;

namespace RunGroopTutorial.Models
{
    public class Club
    {
        [Key] //defining primary key but if 'Id' - entity framework will know its primary key
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        [ForeignKey("Adress")]
        public int? AddressId { get; set; }
        public Adress? Adress { get; set; }
        public ClubCategory ClubCategory { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
