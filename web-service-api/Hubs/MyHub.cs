using Microsoft.AspNetCore.SignalR;

namespace web_service_api
{
    public class MyHub : Hub
    {
        public async Task render(string userName)
        {
            if (Clients?.Group(userName)!= null)
            {
                await Clients.Group(userName).SendAsync("RenderPage");

            }
        }

    }
}
