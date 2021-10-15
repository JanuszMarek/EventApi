using Entities.Constants;
using System.ComponentModel.DataAnnotations;

namespace BusinessModels.Modules.EventModule.Models
{
    public class EventEditModel
    {
        [MaxLength(EventConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [Range(0, EventConstants.TICKET_POOL_MAX_VALUE)]
        public int TicketPool { get; set; }
    }
}
