using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace QuizITAPI.Helpers
{

    public class ChatHub : Hub
    {
        public async Task JoinRoom(string roomName,string message)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("joinedMethod",message);
        }

        public async Task SendMessage(string roomName, string message)
        {
            await Clients.Group(roomName).SendAsync("sentMethod", message);
        }
        public async Task LeaveRoom(string roomName, string message)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("leftMethod",message);
        }
    }

}
