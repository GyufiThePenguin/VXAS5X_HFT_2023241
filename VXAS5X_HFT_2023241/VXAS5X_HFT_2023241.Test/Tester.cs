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

        [Test] //5 TODO: Check it again
        public void ReadAllTest()
        {
            var res = dramaturgLogic.ReadAll().ToArray();
            Assert.That(res[1].Name, Is.EqualTo("Réka Varga"));

        }

        [Test] //6
        public void DramaturgCount()
        {
            var res = dramaturgLogic.ReadAll().Count();
            Assert.That(res, Is.EqualTo(3));

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
            Assert.That(res == 1);

        }

    }
}
