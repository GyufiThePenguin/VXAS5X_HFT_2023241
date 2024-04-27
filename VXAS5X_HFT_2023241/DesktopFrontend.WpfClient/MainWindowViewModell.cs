using GalaSoft.MvvmLight.Command;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                        Id= value.Id,
                        Name = value.Name,
                        Gender = value.Gender,
                        Age = Convert.ToInt32(value.Age)
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

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.Equals(true);
            }
        }


        public MainWindowViewModell()
        {


            //if ide? IsInDesignMode

            Actors = new RestCollection<Actor>("http://localhost:62255/", "actor", "hub");

            CreateActorCommand = new RelayCommand(() =>
            {
                Actors.Add(new Actor()
                {
                    Name = SelectedActor.Name,
                    Gender = SelectedActor.Gender,
                    Age = Convert.ToInt32(SelectedActor.Age)
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
