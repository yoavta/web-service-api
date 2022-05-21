using Microsoft.AspNetCore.SignalR;

namespace web_service_api.Hubs
{
    public class MyHub : Hub
    {
        public async Task Changed(string userName)
        {
            if (Clients?.Group(userName) != null)
            //if (Clients != null)
            {
                await Clients.Group(userName).SendAsync("RenderPage");
                //await Clients.All.SendAsync("RenderPage");

            }
        }

        public async Task ChangedAll(string userName)
        {
            //if (Clients?.Group(userName)!= null)
            if (Clients != null)
            {
                //await Clients.Group(userName).SendAsync("RenderPage");

                await Clients.All.SendAsync("RenderPage", userName);

            }
        }
    }
}
