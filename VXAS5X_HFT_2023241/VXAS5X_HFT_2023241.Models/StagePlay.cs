using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VXAS5X_HFT_2023241.Models
{
    public class StagePlay
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public int Premier { get; set; }
        public int Profit { get; set; }
        public string Rating { get; set; }

        [ForeignKey(nameof(Dramaturg))]
        public int DramaturgId { get; set; }

        [NotMapped]
        public virtual Dramaturg Dramaturg { get; set; }

        [NotMapped]
        public virtual ICollection<Actor> Actors { get; set; }

        public StagePlay()
        {
            Actors = new HashSet<Actor>();
        }

    }
}
