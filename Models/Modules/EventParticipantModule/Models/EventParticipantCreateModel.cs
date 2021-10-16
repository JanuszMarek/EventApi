using Entities.Constants;
using Entities.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BusinessModels.Modules.EventParticipantModule.DTOs
{
    public class EventParticipantCreateModel : IEventParticipant
    {
        [Required]
        [MaxLength(EventParticipantConstants.FIRST_NAME_MAX_LENGTH)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(EventParticipantConstants.LAST_NAME_MAX_LENGTH)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(EventParticipantConstants.EMAIL_MAX_LENGTH)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
