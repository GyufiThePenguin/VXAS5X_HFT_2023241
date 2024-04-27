using GalaSoft.MvvmLight.Command;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VXAS5X_HFT_2023241.Models;

namespace DesktopFrontend.WpfClient
{
    public class MainWindowViewModell : ObservableRecipient
    {

        public RestCollection<Actor> Actors { get; set; }

        private Actor selectedActor;

        public Actor SelectedActor
        {
            get { return selectedActor; }
            set 
            {
                if (value != null)
                {
                    selectedActor = new Actor()
                    {
                        Name = value.Name,
                        Id = value.Id,
                    };
                    OnPropertyChanged();
                    (DeleteActorCommand as RelayCommand).RaiseCanExecuteChanged();
                }


                //SetProperty(ref selectedActor, value);
                
            }
        }


        public ICommand CreateActorCommand { get; set; }
        public ICommand DeleteActorCommand { get; set; }
        public ICommand UpdateActorCommand { get; set; }
        



        public MainWindowViewModell()
        {
            

            //if ide?

            Actors = new RestCollection<Actor>("http://localhost:62255/", "actor");

            CreateActorCommand = new RelayCommand(() =>
            {
                Actors.Add(new Actor()
                {
                    Name = SelectedActor.Name
                });
            });

            DeleteActorCommand = new RelayCommand(() =>
            {
                Actors.Delete(selectedActor.Id);
            },
            () =>
            {
                return SelectedActor != null;
            });

            UpdateActorCommand = new RelayCommand(() =>
            {
                Actors.Update(selectedActor);
            });

            selectedActor = new Actor();
        }

    }
}
