using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VXAS5X_HFT_2023241.Logic;
using VXAS5X_HFT_2023241.Models;
using VXAS5X_HFT_2023241.Repository;

namespace VXAS5X_HFT_2023241.Test
{
    [TestFixture]
    public class Tester
    {

        public ActorLogic actorLogic;
        public StagePlayLogic stagePlayLogic;
        public DramaturgLogic dramaturgLogic;

        public Tester()
        {
            var mockActorRepo = new Mock<IActorRepo>();
            actorLogic = new ActorLogic(mockActorRepo.Object);

            var mockStagePlayRepo = new Mock<IStagePlayRepo>();
            stagePlayLogic = new StagePlayLogic(mockStagePlayRepo.Object);

            var mockDramaturgRepo = new Mock<IDramaturgRepo>();
            dramaturgLogic = new DramaturgLogic(mockDramaturgRepo.Object);


            //Dramaturgs
            Dramaturg dBelaBalla = new Dramaturg() { Name = "Béla Balla" };
            var dramaturgs = new List<Dramaturg>()
            {
                new Dramaturg()
                {
                    Id = 1,
                    Name = "Róbert Alföldi",
                    Age = 56,
                    Gender = "man"
                },
                new Dramaturg()
                {
                    Id=2,
                    Name="Réka Varga",
                    Gender="woman",
                    Age=68
                },
                new Dramaturg()
                {
                    Id=3,
                    Name= "János Mohácsi",
                    Gender="man",
                    Age=68,
                },
            };

            //Plays
            var plays = new List<StagePlay>()
            {
                new StagePlay()
                {
                    Id=1,
                    Title="StagePlayTitle1",
                    Profit=1357481,
                    Rating="good",
                    Premier = 2018,
                    Dramaturg = dBelaBalla
                },
                new StagePlay()
                {
                    Id=2,
                    Title="StagePlayTitle2",
                    Rating="exceptional",
                    Premier=2008,
                    Profit=15612,

                },
                new StagePlay()
                {
                    Id=3,
                    Title="StagePlayTitle3",
                    Profit=18513216,
                    Premier=2020
                },
                new StagePlay()
                {
                    Id = 4,
                    Title = "Tükör a tóban - A menyasszony visszavár",
                    Rating = "okay",
                    Profit = 9999999
                },
                new StagePlay()
                {
                    Id = 5,
                    Title = "StagePlayTitle5",
                    Rating = "horrible",
                    Profit=2458764,
                }
            };

            //Actors
            var actors = new List<Actor>()
            {
                new Actor()
                {
                    Id = 1,
                    Name = "TotallyReal Actor1",
                    Age = 48,
                    Gender = "man"
                },
                new Actor()
                {
                    Id=2,
                    Name="TotallyReal Actor2",
                    Gender="woman",
                    Age=50
                },
                new Actor()
                {
                    Id=3,
                    Name="TotallyReal Actor3",
                    Gender="man",
                    Age=53
                },
            };



            // Additional Dramaturgs
            dramaturgs.AddRange(new List<Dramaturg>
            {
                new Dramaturg { Id = 4, Name = "Dramaturg 4", Age = 45, Gender = "woman" },
                new Dramaturg { Id = 5, Name = "Dramaturg 5", Age = 50, Gender = "man" }
            });

            // Additional Plays
            plays.AddRange(new List<StagePlay>
            {
                new StagePlay { Id = 6, Title = "StagePlayTitle6", Profit = 500000, Rating = "good", Premier = 2019, Dramaturg = dramaturgs[3] },
                new StagePlay { Id = 7, Title = "StagePlayTitle7", Profit = 750000, Rating = "exceptional", Premier = 2020, Dramaturg = dramaturgs[4] }
            });

            // Additional Actors
            actors.AddRange(new List<Actor>
            {
                new Actor { Id = 4, Name = "Actor 4", Age = 40, Gender = "woman" },
                new Actor { Id = 5, Name = "Actor 5", Age = 55, Gender = "man" }
            });

            // Associate actors with plays
            plays[0].Actors = new List<Actor> { actors[0], actors[1] };
            plays[1].Actors = new List<Actor> { actors[1], actors[2] };
            plays[2].Actors = new List<Actor> { actors[2], actors[3] };
            plays[3].Actors = new List<Actor> { actors[3], actors[4] };
            plays[4].Actors = new List<Actor> { actors[4], actors[0] };



            mockActorRepo.Setup((t) => t.ReadAll()).Returns(actors.AsQueryable());
            mockDramaturgRepo.Setup((t) => t.ReadAll()).Returns(dramaturgs.AsQueryable());
            mockStagePlayRepo.Setup((t) => t.ReadAll()).Returns(plays.AsQueryable());

            }




        [Test] //1
        public void ActorCreateTest()
        {
            Assert.That(() => stagePlayLogic.Create(new StagePlay()
            {
                Title = null
            }), Throws.Exception);

        }

        [Test] //2
        public void DirectorDeleteTest()
        {
            Assert.That(() => dramaturgLogic.Delete(29), Throws.Exception);

        }

        [Test]//3
        public void ActorDeleteExceptionTest()
        {
            Assert.That(() => actorLogic.Delete(666), Throws.TypeOf<IndexOutOfRangeException>());

        }

        [TestCase(-1)] //4
        public void StagePlayUpdateTest(int id)
        {
            StagePlay stagePlay = new StagePlay()
            {
                Id = id,
                Title = "OriginalTitle1",
                Rating = "horrible",
                Profit = 6666666,
                Premier = 2018,
                DramaturgId = 1
            };

            Assert.That(() => stagePlayLogic.Update(stagePlay), Throws.TypeOf<IndexOutOfRangeException>());

        }

        [Test] //5
        public void ReadAllTest()
        {
            var res = dramaturgLogic.ReadAll().ToArray();
            Assert.That(res[1].Name, Is.EqualTo("Réka Varga"));

        }

        [Test] //6
        public void DramaturgCount()
        {
            var res = dramaturgLogic.ReadAll().Count();
            Assert.That(res, Is.EqualTo(5));

        }

        [Test] //7 
        public void ExceptionalStagePlaysTest()
        {
            var res = stagePlayLogic.ExceptionalStagePlays().ToArray();
            Assert.That(res[0].Title, Is.EqualTo("StagePlayTitle2"));

        }

        [Test] //8
        public void TwentyOneCenturyMoviesTest()
        {
            var res = stagePlayLogic.PremieredAfter2010().ToArray();
            Assert.That(res[0].Title, Is.EqualTo("StagePlayTitle1"));

        }

        [Test] //9
        public void HorribleSuccessStagePlays()
        {
            var res = stagePlayLogic.HorribleSuccessStagePlays().ToArray();
            Assert.That(res[0].Title, Is.EqualTo("StagePlayTitle5"));

        }

        [Test] //10
        public void NotHorribleTuktukStagePlaysTest()
        {
            var res = stagePlayLogic.NotHorribleTuktukStagePlays().ToArray();
            Assert.That(res[0].Title, Is.EqualTo("Tükör a tóban - A menyasszony visszavár"));
        }

        [Test] //11
        public void GoodPlaysTest()
        {
            var res = stagePlayLogic.GoodPlays();
            Assert.That(res,Is.EqualTo(2));

        }

        // Multi table non-crud

        [Test] //12
        public void ActorsInExceptionalPlaysTest()
        {      
            var result = stagePlayLogic.ActorsInExceptionalPlays().ToArray();
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Length, Is.EqualTo(2));
        }

        [Test] //13
        public void AverageAgeOfActorsInSuccessfulPlaysTest()
        {
            var averageAge = stagePlayLogic.AverageAgeOfActorsInSuccessfulPlays();
            Assert.That(averageAge, Is.GreaterThan(0));
        }

        [Test] //14
        public void MostProfitableDramaturgsTest()
        {
            var profitableDramaturgs = stagePlayLogic.MostProfitableDramaturgs().ToArray();
            Assert.That(profitableDramaturgs, Is.Not.Empty);
            Assert.That(profitableDramaturgs.Length, Is.EqualTo(4));
        }

        [Test] //15
        public void PlaysWithSeniorActorsTest()
        {
            var playsWithSeniorActors = stagePlayLogic.PlaysWithSeniorActors(50).ToArray();
            Assert.That(playsWithSeniorActors, Is.Not.Empty);
            Assert.That(playsWithSeniorActors, Has.Some.Matches<StagePlay>(play => play.Actors.Any(actor => actor.Age > 50)));
        }


        [Test] //16
        public void DramaturgsWhoWorkedWithSpecificActorTest()
        {
            string actorName = "TotallyReal Actor1";
            var result = stagePlayLogic.DramaturgsWhoWorkedWithActor(actorName).ToArray();
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }

        [Test] //17
        public void DramaturgsWithMostPlaysTest()
        {
            var topDramaturgs = stagePlayLogic.DramaturgsWithMostPlays().ToArray();
            Assert.That(topDramaturgs.Length, Is.EqualTo(3));
        }


        [Test] //18
        public void PlaysWithMaxActorAgeAboveTest()
        {
            int ageThreshold = 45;
            var result = stagePlayLogic.PlaysWithMaxActorAgeAbove(ageThreshold).ToArray();
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }









    }
}
