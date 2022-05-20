using Microsoft.AspNetCore.SignalR;

namespace web_service_api.Hubs
{
    public class MyHub : Hub
    {
        public async Task render(string userName)
        {
            await Clients.Group(userName).SendAsync("RenderPage");
        }

    }
}
