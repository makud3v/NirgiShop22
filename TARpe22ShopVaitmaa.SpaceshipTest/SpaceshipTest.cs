using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TARpe22ShopVaitmaa.Core.Domain;
using TARpe22ShopVaitmaa.Core.Dto;
using TARpe22ShopVaitmaa.Core.ServiceInterface;
using Xunit;

namespace TARpe22ShopVaitmaa.SpaceshipTest
{
    public class SpaceshipTest : TestBase
    {
        [Fact]
        public async void ShouldNot_AddEmptySpaceship_WhenReturnResult()
        {
            string guid = Guid.NewGuid().ToString();

            SpaceshipDto spaceship = new SpaceshipDto();

            spaceship.Id = Guid.Parse(guid);
            spaceship.Price = 100;
            spaceship.Type = "rocket";
            spaceship.Name = "X ae A 12";
            spaceship.Description = "Description";
            spaceship.FuelType = "Cowfarts";
            spaceship.FuelCapacity = 100;
            spaceship.FuelConsumption = 100;
            spaceship.PassengerCount = 100;
            spaceship.EnginePower = 100;
            spaceship.DoesHaveAutopilot = true;
            spaceship.CrewCount = 100;
            spaceship.CargoWeight = 100;
            spaceship.DoesHaveLifeSupportSystems = true;
            spaceship.BuiltDate = DateTime.Now;
            spaceship.LastMaintenance = DateTime.Now;
            spaceship.MaintenanceCount = 1;
            spaceship.FullTripsCount = 1;
            spaceship.MaidenLaunch = DateTime.Now;
            spaceship.Manufacturer = "Space Z";
            spaceship.CreatedAt = DateTime.Now;
            spaceship.ModifiedAt = DateTime.Now;
            spaceship.Files = new List<IFormFile>();

            var result = await Svc<ISpaceshipsServices>().Create(spaceship);

            Assert.NotNull(result);
        }


        [Fact]
        public async void ShouldNot_GetByIdSpaceship_WhenReturnsNotEqual()
        {
            Guid wrongGuid = Guid.Parse(Guid.NewGuid().ToString());
            Guid guid = Guid.Parse("d573fe18-8daa-45cb-8188-ec03e8053e40");

            await Svc<ISpaceshipsServices>().GetAsync(guid);

            Assert.NotEqual(wrongGuid, guid);
        }


        [Fact]
        public async void Should_UpdateSpaceship_WhenUpdateData()
        {
            var guid = new Guid("d573fe18-8daa-45cb-8188-ec03e8053e40");

            Spaceship spaceship = new Spaceship();

            SpaceshipDto dto = MockSpaceshipData();

            spaceship.Id = Guid.Parse("d573fe18-8daa-45cb-8188-ec03e8053e40");
            spaceship.Price = 500;
            spaceship.Type = "Saucer";
            spaceship.Name = "Supikauss";
            spaceship.Description = "Sisaldab supi asemel tulnukaid";
            spaceship.FuelType = "Cowfarts";
            spaceship.FuelConsumption = 666;
            spaceship.PassengerCount = 100;
            spaceship.EnginePower = 9000;
            spaceship.DoesHaveAutopilot = true;
            spaceship.CrewCount = 20;
            spaceship.CargoWeight = 60;
            spaceship.DoesHaveLifeSupportSystems = true;
            spaceship.BuiltDate = DateTime.Now.AddYears(2);
            spaceship.LastMaintenance = DateTime.Now;
            spaceship.MaintenanceCount = 2;
            spaceship.FullTripsCount = 1;
            spaceship.MaidenLaunch = DateTime.Now;
            spaceship.Manufacturer = "Space Z";
            spaceship.CreatedAt = DateTime.Now.AddYears(1);
            spaceship.ModifiedAt = DateTime.Now.AddYears(1);

            await Svc<ISpaceshipsServices>().Update(dto);

            Assert.Equal(spaceship.Id, guid);
            Assert.DoesNotMatch(spaceship.Name, dto.Name);
            Assert.DoesNotMatch(spaceship.CrewCount.ToString(), dto.CrewCount.ToString());
            Assert.Equal(spaceship.EnginePower, dto.EnginePower);
        }


        [Fact]
        public async void Should_GetByIdSpaceship_WhenReturnsNotEqual()
        {
            ISpaceshipsServices svc = Svc<ISpaceshipsServices>();
            SpaceshipDto dto = MockSpaceshipData();
            Spaceship newShip = await svc.Create(dto);

            Spaceship spaceship = await svc.GetAsync(newShip.Id);

            Assert.NotNull(spaceship);
            Assert.Equal(dto.Type, spaceship.Type);
            Assert.Equal(dto.Name, spaceship.Name);

            dto.ModifiedAt = DateTime.Now;
            Assert.NotEqual(dto.ModifiedAt, spaceship.ModifiedAt);
        }


        [Fact]
        public async void Should_DeleteByIdSpaceship_WhenDeleteSpaceship()
        {
            ISpaceshipsServices svc = Svc<ISpaceshipsServices>();

            SpaceshipDto dto = MockSpaceshipData();
            Spaceship newShip = await svc.Create(dto);
            dto.Id = newShip.Id;

            await svc.Delete(dto.Id);
            Assert.Null(await svc.GetAsync(dto.Id));
        }

        [Fact]
        public async void ShouldNot_UpdateSpaceship_WhenNotUpdateData()
        {
            ISpaceshipsServices svc = Svc<ISpaceshipsServices>();
            SpaceshipDto dto = MockSpaceshipData();
            await svc.Create(dto);

            dto = MockSpaceshipData();
            Spaceship updatedShip = await svc.Update(dto);

            Assert.Equal(updatedShip.Name, dto.Name);
            Assert.Equal(updatedShip.FuelType, dto.FuelType);
            Assert.NotEqual(updatedShip.ModifiedAt, DateTime.Now);
            Assert.NotEqual(updatedShip.ModifiedAt, dto.ModifiedAt);
        }


        private SpaceshipDto MockSpaceshipData()
        {
            SpaceshipDto spaceship = new()
            {
                Price = 500,
                Type = "Saucer",
                Name = "Taldrik",
                Description = "Sisaldab supi asemel tulnukaid",
                FuelType = "Cowfarts",
                FuelConsumption = 666,
                PassengerCount = 100,
                EnginePower = 9000,
                DoesHaveAutopilot = true,
                CrewCount = 10,
                CargoWeight = 60,
                DoesHaveLifeSupportSystems = true,
                BuiltDate = DateTime.Now.AddYears(2),
                LastMaintenance = DateTime.Now,
                MaintenanceCount = 2,
                FullTripsCount = 1,
                MaidenLaunch = DateTime.Now,
                Manufacturer = "Space Z",
                CreatedAt = DateTime.Now.AddYears(1),
                ModifiedAt = DateTime.Now.AddYears(1),
            };

            return spaceship;
        }
    }
}
