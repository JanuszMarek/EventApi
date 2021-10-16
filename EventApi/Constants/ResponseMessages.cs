namespace EventApi.Constants
{
    public static class ResponseMessages
    {
        public const string ID_WRONG_FORMAT = "Wrong Id format";
        public const string ID_NOT_FOUND = "Id could not be found in parameters";
        public const string DATA_NOT_EXIST = "{0} with id {1} does not exist or is deleted.";

        public const string TICKET_BUY_SUCCESS = "Ticket bought successfully";
        public const string TICKET_BUY_FAILURE = "Ticket could not be bought cause limit reached";
    }
}
