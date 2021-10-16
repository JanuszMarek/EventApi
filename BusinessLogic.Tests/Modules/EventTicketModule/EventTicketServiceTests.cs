using AutoMapper;
using BusinessLogic.Modules.EventParticipantModule.Profiles;
using BusinessLogic.Modules.EventTicketModule.Services;
using BusinessModels.Modules.EventModule.DTOs;
using BusinessModels.Modules.EventParticipantModule.Models;
using BusinessModels.Modules.EventTicketModule.Models;
using Entities.Models;
using Infrastructure.Interfaces.IRepositories;
using Infrastructure.Interfaces.IServices;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogic.Tests.Modules.EventTicketModule
{
    public class EventTicketServiceTests
    {
        private readonly EventTicketService eventTicketService;
        private readonly Mock<IEventTicketRepository> repositoryMock;
        private readonly Mock<IEventService> eventServiceMock;
        private readonly Mock<IEventParticipantService> eventParticipantServiceMock;

        public EventTicketServiceTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<EventParticipantProfile>());
            var mapper = config.CreateMapper();

            this.repositoryMock = new Mock<IEventTicketRepository>();
            this.eventServiceMock = new Mock<IEventService>();
            this.eventParticipantServiceMock = new Mock<IEventParticipantService>();
            this.eventTicketService = new EventTicketService(
                repositoryMock.Object,
                mapper,
                eventServiceMock.Object,
                eventParticipantServiceMock.Object);
        }

        [Fact]
        public async Task BuyTicketAsync_TicketPoolReached_Failure()
        {
            // Arrange
            var remainingDto = new EventRemainingTicketDto() { Count = 0 };
            eventServiceMock.Setup(x => x.GetTicketRemainingCountAsync(It.IsAny<int>())).ReturnsAsync(remainingDto);

            // Act
            var result = await eventTicketService.BuyTicketAsync(1, new EventTicketCreateModel());

            // Assert
            Assert.False(result);
            repositoryMock.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task BuyTicketAsync_TicketPoolFreeExistingParticipant_Success()
        {
            // Arrange
            var eventId = 1;
            var eventCreateModel = new EventTicketCreateModel()
            {
                EventParticipant = new EventParticipantCreateModel()
                {
                    Email = "john.doe@gmail.com",
                    FirstName = "John",
                    LastName = "Doe"
                }
            };
            var participantId = 1;

            var expectedEventTicket = new EventTicket()
            {
                EventId = eventId,
                IsDeleted = false,
                EventParticipantId = 1
            };

            var remainingDto = new EventRemainingTicketDto() { Count = 1 };
            eventServiceMock.Setup(x => x.GetTicketRemainingCountAsync(It.IsAny<int>())).ReturnsAsync(remainingDto);
            eventParticipantServiceMock.Setup(x => x.GetExistingParticipantId(It.IsAny<EventParticipantCreateModel>())).ReturnsAsync(participantId);

            // Act
            var result = await eventTicketService.BuyTicketAsync(eventId, eventCreateModel);

            // Assert
            Assert.True(result);
            repositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
            repositoryMock.Verify(x => x.AddAsync(
                It.Is<EventTicket>(
                    x => x.Equals(expectedEventTicket))),
                Times.Once);
        }

        [Fact]
        public async Task BuyTicketAsync_TicketPoolFreeNewParticipant_SuccessNewParticipantCreated()
        {
            // Arrange
            var eventId = 1;
            var eventCreateModel = new EventTicketCreateModel()
            {
                EventParticipant = new EventParticipantCreateModel()
                {
                    Email = "john.doe@gmail.com",
                    FirstName = "John",
                    LastName = "Doe"
                }
            };
            var participantId = 0; // Not exist

            var expectedEventTicket = new EventTicket()
            {
                EventId = eventId,
                IsDeleted = false,
                EventParticipantId = 0,
                EventParticipant = new EventParticipant()
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@gmail.com"
                }
            };

            var remainingDto = new EventRemainingTicketDto() { Count = 1 };
            eventServiceMock.Setup(x => x.GetTicketRemainingCountAsync(It.IsAny<int>())).ReturnsAsync(remainingDto);
            eventParticipantServiceMock.Setup(x => x.GetExistingParticipantId(It.IsAny<EventParticipantCreateModel>())).ReturnsAsync(participantId);

            // Act
            var result = await eventTicketService.BuyTicketAsync(eventId, eventCreateModel);

            // Assert
            Assert.True(result);
            repositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
            repositoryMock.Verify(x => x.AddAsync(
                It.Is<EventTicket>(
                    x => x.Equals(expectedEventTicket) && 
                    x.EventParticipant.Equals(expectedEventTicket.EventParticipant))),
                Times.Once);
        }
    }
}
