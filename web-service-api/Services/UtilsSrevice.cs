namespace web_service_api.Services
{

    public class UtilsSrevice : IUtilsService
    {
        private readonly WebServiceContext _context;
        private readonly IContactService _contactService;
        public UtilsSrevice(WebServiceContext context, IContactService contactService)
        {

            _context = context;
            _contactService = contactService;

        }
        public Task Invitations(string from, string to, string server)
        {
            throw new NotImplementedException();
        }

        public Task Transfer(string from, string to, string content)
        {
            throw new NotImplementedException();
        }
    }
}
