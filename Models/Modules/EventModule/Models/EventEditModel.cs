using Entities.Constants;
using System.ComponentModel.DataAnnotations;

namespace BusinessModels.Modules.EventModule.Models
{
    public class EventEditModel
    {
        [MaxLength(EventConstants.NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [Range(EventConstants.TICKET_POOL_MIN_VALUE, EventConstants.TICKET_POOL_MAX_VALUE)]
        public int TicketPool { get; set; }
    }
}
