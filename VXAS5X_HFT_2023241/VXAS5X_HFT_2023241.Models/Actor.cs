using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VXAS5X_HFT_2023241.Models
{
    public class Actor
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }


        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<StagePlay> Plays { get; set; }


        public Actor()
        {
            Plays = new HashSet<StagePlay>() { }
            ;

        }

    }
}
