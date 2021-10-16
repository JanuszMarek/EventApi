using BusinessModels.Modules.EventParticipantModule.Models;

namespace BusinessModels.Modules.EventTicketModule.Models
{
    public class EventTicketCreateModel
    {
        public EventParticipantCreateModel EventParticipant { get; set; }
    }
}
