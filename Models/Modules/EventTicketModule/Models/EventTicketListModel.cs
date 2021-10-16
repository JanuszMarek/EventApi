using BusinessModels.Modules.EventParticipantModule.DTOs;

namespace BusinessModels.Modules.EventTicketModule.Models
{
    public class EventTicketListModel
    {
        public long Id { get; set; }
        public EventParticipantModel EventParticipant { get; set; }
    }
}
