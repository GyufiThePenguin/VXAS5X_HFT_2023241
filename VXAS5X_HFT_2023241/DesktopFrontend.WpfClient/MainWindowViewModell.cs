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
        public RestCollection<Dramaturg> Dramaturgs { get; set; }
        public RestCollection<StagePlay> StagePlays { get; set; }

        //
        // ========
        //

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
                
            }

        }


        private Dramaturg selectedDramaturg;
        public Dramaturg SelectedDramaturg
        {
            get { return selectedDramaturg; }
            set 
            {
                if (value != null)
                {
                    selectedDramaturg = new Dramaturg()
                    {
                        Id= value.Id,
                        Name = value.Name,
                        Gender = value.Gender,
                        Age = Convert.ToInt32(value.Age)
                    };
                    OnPropertyChanged();
                    (DeleteDramaturgCommand as RelayCommand).RaiseCanExecuteChanged();
                }  
                
            }

        }


        private StagePlay selectedStagePlay;
        public StagePlay SelectedStagePlay
        {
            get { return selectedStagePlay; }
            set
            {
                if (value != null)
                {
                    selectedStagePlay = new StagePlay()
                    {
                        Id = value.Id,
                        Title = value.Title,
                        Premier = Convert.ToInt32(value.Premier),
                        Profit = Convert.ToInt32(value.Profit),
                        Rating = value.Rating,
                        Dramaturg = value.Dramaturg,
                        DramaturgId = value.DramaturgId,
                    };
                    OnPropertyChanged();
                    (DeleteStagePlayCommand as RelayCommand).RaiseCanExecuteChanged();
                }



            }
        }

        //
        // ========
        //

        public ICommand CreateActorCommand { get; set; }
        public ICommand DeleteActorCommand { get; set; }
        public ICommand UpdateActorCommand { get; set; }

        public ICommand CreateDramaturgCommand { get; set; }
        public ICommand DeleteDramaturgCommand { get; set; }
        public ICommand UpdateDramaturgCommand { get; set; }

        public ICommand CreateStagePlayCommand { get; set; }
        public ICommand DeleteStagePlayCommand { get; set; }
        public ICommand UpdateStagePlayCommand { get; set; }


        //
        // ========
        //

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

            Actors = new RestCollection<Actor>("http://localhost:62255/", "actor", "hub");
            Dramaturgs = new RestCollection<Dramaturg>("http://localhost:62255/", "dramaturg", "hub");
            StagePlays = new RestCollection<StagePlay>("http://localhost:62255/", "stageplay", "hub");

            //
            // CREATE COMMANDS
            //

            CreateActorCommand = new RelayCommand(() =>
            {
                Actors.Add(new Actor()
                {
                    Name = SelectedActor.Name,
                    Gender = SelectedActor.Gender,
                    Age = Convert.ToInt32(SelectedActor.Age)
                });
            });
            
            CreateDramaturgCommand = new RelayCommand(() =>
            {
                Dramaturgs.Add(new Dramaturg()
                {
                    Name = SelectedDramaturg.Name,
                    Gender = SelectedDramaturg.Gender,
                    Age = Convert.ToInt32(SelectedDramaturg.Age)
                });
            });
            
            CreateStagePlayCommand = new RelayCommand(() =>
            {
                
                StagePlays.Add(new StagePlay()
                {
                    Id = selectedStagePlay.Id,
                    Title = selectedStagePlay.Title,
                    Premier = Convert.ToInt32(selectedStagePlay.Premier),
                    Profit = Convert.ToInt32(selectedStagePlay.Profit),
                    Rating = selectedStagePlay.Rating,
                    DramaturgId = selectedStagePlay.DramaturgId
                });
            });


            //
            // DELETE COMMANDS
            //

            DeleteActorCommand = new RelayCommand(() =>
            {
                Actors.Delete(selectedActor.Id);
            },
            () =>
            {
                return SelectedActor != null;
            });

            DeleteDramaturgCommand = new RelayCommand(() =>
            {
                Dramaturgs.Delete(selectedDramaturg.Id);
            },
            () =>
            {
                return selectedDramaturg != null;
            });

            DeleteStagePlayCommand = new RelayCommand(() =>
            {
                StagePlays.Delete(selectedStagePlay.Id);
            },
            () =>
            {
                return selectedDramaturg != null;
            });


            //
            // UPDATE COMMANDS
            //

            UpdateActorCommand = new RelayCommand(() =>
            {
                Actors.Update(selectedActor);
            });
            
            UpdateDramaturgCommand = new RelayCommand(() =>
            {
                Dramaturgs.Update(selectedDramaturg);
            });

            UpdateStagePlayCommand = new RelayCommand(() =>
            {
                StagePlays.Update(selectedStagePlay);
            });

            selectedActor = new Actor();
            selectedDramaturg = new Dramaturg();
            selectedStagePlay = new StagePlay();
        }

    }
}
