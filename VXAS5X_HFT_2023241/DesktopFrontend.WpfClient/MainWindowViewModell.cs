using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Models;

namespace DesktopFrontend.WpfClient
{
    public class MainWindowViewModell
    {

        public RestCollection<Actor> Actors { get; set; }
        public MainWindowViewModell()
        {
            Actors = new RestCollection<Actor>("http://localhost:62255/", "actor");
        }

    }
}
