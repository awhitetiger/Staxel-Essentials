using Plukit.Base;
using Staxel.Logic;
using Staxel.Server;
using Staxel.Commands;

namespace StaxelEssentials
{
    public class tpaCommand : ICommandBuilder
    {
        public string Kind => "tp2";

        public string Usage => "StaxelEssentials - /tp2 playername";

        public bool Public => true;

        public string Execute(string[] bits, Blob blob, ClientServerConnection connection, ICommandsApi api, out object[] responseParams)
        {
            responseParams = new object[0];
            string player = connection.Credentials.Username;

            if (bits.Length != 2)
                return "Usage: /tp2 player";

            EntityId playerEntityId1 = api.FindPlayerEntityId(player);
            EntityId playerEntityId2 = api.FindPlayerEntityId(bits[1]);
            Entity entity1;
            Entity entity2;
            if (!api.TryGetEntity(playerEntityId1, out entity1) || !api.TryGetEntity(playerEntityId2, out entity2))
                return "commands.playerEntityNotFound";
            StaxelEssentialsHolder.SetBack(entity1.Physics.Position);
            if (!entity1.Physics.Teleport(entity2.Physics.Position))
                return "commands.teleport.distance";
            return "commands.teleport.success";
        }
    }

    public class setHomeCommand : ICommandBuilder
    {
        public string Kind => "sethome";

        public string Usage => "StaxelEssentials - /sethome";

        public bool Public => true;

        public string Execute(string[] bits, Blob blob, ClientServerConnection connection, ICommandsApi api, out object[] responseParams)
        {
            responseParams = new object[0];
            string player = connection.Credentials.Username;
            EntityId connectionEntityId = api.FindPlayerEntityId(player);
            Entity entity;
            if (!api.TryGetEntity(connectionEntityId, out entity))
            {
                return "commands.playerEntityNotFound";
            }
            entity.PlayerEntityLogic.SetHome(entity.Physics.Position);
            return "Home set! Note, sleeping in a bed will reset this!";
        }
    }

    public class homeCommand : ICommandBuilder //this entire command is literally ripped from the game and is the same code as /returnhome this just shortens the command really
    {
        public string Kind => "home";

        public string Usage => "StaxelEssentials - /home";

        public bool Public => true;

        public string Execute(string[] bits, Blob blob, ClientServerConnection connection, ICommandsApi api, out object[] responseParams)
        {
            responseParams = new object[0];
            string player = connection.Credentials.Username;
            EntityId connectionEntityId = api.FindPlayerEntityId(player);
            Entity entity;
            if (!api.TryGetEntity(connectionEntityId, out entity))
            {
                return "commands.playerEntityNotFound";
            }
            StaxelEssentialsHolder.SetBack(entity.Physics.Position);
            if (entity.Physics.Teleport(entity.PlayerEntityLogic.GetHome()))
            {
                return "commands.teleport.success";
            }
            return "commands.teleport.distance";
        }
    }

    public class backCommand : ICommandBuilder
    {
        public string Kind => "back";

        public string Usage => "StaxelEssentials - /back";

        public bool Public => true;

        public string Execute(string[] bits, Blob blob, ClientServerConnection connection, ICommandsApi api, out object[] responseParams)
        {
            Vector3D storeLast;
            responseParams = new object[0];
            string player = connection.Credentials.Username;
            EntityId connectionEntityId = api.FindPlayerEntityId(player);
            Entity entity;
            if (!api.TryGetEntity(connectionEntityId, out entity))
            {
                return "commands.playerEntityNotFound";
            }
            if (StaxelEssentialsHolder.GetBack() == null)
            {
                return "You don't have a place to go back to!";
            }
            storeLast = entity.Physics.Position;
            if (entity.Physics.Teleport(StaxelEssentialsHolder.GetBack()))
            {
                StaxelEssentialsHolder.SetBack(storeLast);
                return "commands.teleport.success";
            }
            return "commands.teleport.distance";
        }
    }

    public class payCommand : ICommandBuilder
    {
        public string Kind => "pay";

        public string Usage => "StaxelEssentials - /pay player amount";

        public bool Public => true;

        public string Execute(string[] bits, Blob blob, ClientServerConnection connection, ICommandsApi api, out object[] responseParams)
        {
            responseParams = new object[0];
            if (bits.Length != 3)
                return "Usage: /pay player amount";
            string player = connection.Credentials.Username;
            string buyer = bits[1];
            EntityId playerEntityId1 = api.FindPlayerEntityId(player);
            EntityId playerEntityId2 = api.FindPlayerEntityId(buyer);
            Entity entity1;
            Entity entity2;
            if (!api.TryGetEntity(playerEntityId1, out entity1) || !api.TryGetEntity(playerEntityId2, out entity2))
                return "commands.playerEntityNotFound";
            int result;
            if (!int.TryParse(bits[2], out result) || entity1.Inventory.GetMoney() - result < 0 || result < 1)
                return "commands.givePetals.invalidNumber";
            entity1.Inventory.AdjustMoney(-result);
            entity2.Inventory.AdjustMoney(result);
            return "payed";
        }
    }
}
