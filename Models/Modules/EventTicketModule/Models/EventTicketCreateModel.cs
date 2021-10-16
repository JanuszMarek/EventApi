using BusinessModels.Modules.EventParticipantModule.DTOs;

namespace BusinessModels.Modules.EventTicketModule.Models
{
    public class EventTicketCreateModel
    {
        public EventParticipantCreateModel EventParticipant { get; set; }
    }
}
