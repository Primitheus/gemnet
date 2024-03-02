﻿using Gemnet.Network.Packets;
using Gemnet.Persistence;
using Gemnet.Persistence.Models;
using GemnetCS.Network.Packets;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static Program;

namespace Gemnet.PacketProcessors
{
    internal class Inventory
    {
        public static void GetCash(ushort type, ushort action, byte[] body, NetworkStream stream)
        {
            // UUID is req

            action++;

            Server.clientUserID.TryGetValue(stream, out int UserID);

            GetCashRes response = new GetCashRes();

            response.Type = type;
            response.Action = action;

            response.UserID = UserID;
            response.Astros = 1000000;
            response.Medals = 0;

            Console.WriteLine($"Get Cash: Astros={response.Astros}, Medals={response.Medals}");

            byte[] packet = response.Serialize();

            _ = ServerHolder.ServerInstance.SendPacket(packet, stream);
        }

        public static void Unknown6(ushort type, ushort action, byte[] body, NetworkStream stream)
        {
            // wtf
            action++;
            byte[] data = {0x00, 0x31, 0x02, 0x08, 0x8b, 0x01, 0x11, 0x27, 0x46, 0x41, 0x49, 0x4c, 0x28, 0x75, 0x6e, 0x6b, 0x6e, 0x6f, 0x77, 0x6e, 0x29, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3c, 0xf4, 0x84, 0x0a, 0x3c, 0xf4, 0x84, 0x0a, 0x82, 0xa2, 0x25, 0x74, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x48, 0xa1, 0x24, 0x1a, 0x06, 0x00, 0x00, 0x00, 0xff, 0xff, 0xff, 0xff, 0xf0, 0xf4, 0x84, 0x0a, 0x3a, 0xb1, 0x50, 0x03, 0x40, 0xaa, 0x73, 0x03, 0x01, 0x00, 0x00, 0x00, 0x48, 0xa1, 0x24, 0x1a, 0xc2, 0x6a, 0x4f, 0x03, 0x98, 0x86, 0x97, 0x03, 0xac, 0x8b, 0xb1, 0x1c, 0x48, 0xa1, 0x24, 0x1a, 0xd2, 0x6b, 0x4f, 0x03, 0xfc, 0xf4, 0x84, 0x0a, 0x25, 0xdf, 0x4f, 0x03, 0x11, 0x27, 0x00, 0x00, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x64, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1a, 0x4f, 0x00, 0xff, 0xff, 0xff, 0xff, 0x01, 0x1a, 0x4f, 0x00, 0xff, 0xff, 0xff, 0xff, 0x02, 0x1a, 0x4f, 0x00, 0x04, 0x00, 0x00, 0x00, 0x1a, 0xef, 0x4e, 0x00, 0xe3, 0x7c, 0x03, 0x00, 0xff, 0xff, 0xff, 0xff, 0x00, 0x00, 0x00, 0x00, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xf1, 0x4a, 0x40, 0x00, 0xf0, 0xed, 0x18, 0x00, 0x8a, 0x1e, 0x00, 0x00, 0xac, 0x8b, 0xb1, 0x1c, 0xa0, 0xf6, 0x93, 0x03, 0x3e, 0x45, 0x00, 0x00, 0x78, 0xaf, 0x24, 0x03, 0x15, 0x4e, 0x40, 0x00, 0x18, 0xf5, 0x84, 0x0a, 0x00, 0x09, 0x00, 0x00, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xd0, 0x88, 0x97, 0x03, 0xd8, 0x88, 0x97, 0x03, 0x48, 0xa1, 0x24, 0x1a, 0xd8, 0x88, 0x97, 0x03, 0x98, 0x86, 0x97, 0x03, 0xd8, 0x88, 0x97, 0x03, 0x48, 0xa1, 0x24, 0x1a, 0x98, 0x86, 0x97, 0x03, 0x11, 0x27};
            _ = ServerHolder.ServerInstance.SendPacket(data, stream);

        }


        public static void BuyItem(ushort type, ushort action, byte[] body, NetworkStream stream) 
        {
            action++;

            BuyItemReq request = BuyItemReq.Deserialize(body);

            Console.WriteLine($"Buying ItemID={request.ItemID}");

            BuyItemRes response = new BuyItemRes();


            var CashQueryCarats = ServerHolder.DatabaseInstance.SelectFirst<ModelAccount>(ModelAccount.QueryCashCarats, new
            {
                ID = 1,

            });

            if (CashQueryCarats != null)
            {
                if (CashQueryCarats.Carats > 0)
                {
                    response.Type = type;
                    response.Action = action;

                    ServerHolder.DatabaseInstance.Execute(ModelInventory.InsertItem, new
                    {
                        OID = 1,
                        ID = request.ItemID
                    });

                    var ServerID = ServerHolder.DatabaseInstance.SelectFirst<ModelInventory>(ModelInventory.GetServerID, new
                    {
                        OID = 1,

                    });

                    response.ServerID = ServerID.ServerID;
                    response.Carats = 5000000;

                    _ = ServerHolder.ServerInstance.SendPacket(response.Serialize(), stream);

                }
            }

        }

        public static void OpenBox(ushort type, ushort action, byte[] body, NetworkStream stream)
        {
            action++;

            OpenBoxReq request = OpenBoxReq.Deserialize(body);
            Console.WriteLine($"Opening Box with ServerID={request.ServerID}");

            ServerHolder.DatabaseInstance.Execute(ModelInventory.DeleteItem, new
            {
                SID = request.ServerID
            });

            OpenBoxRes response = new OpenBoxRes();

            int itemid = 2070006;
            int itemend = 3069;

            ServerHolder.DatabaseInstance.Execute(ModelInventory.InsertItem, new
            {
                OID = 1,
                ID = itemid
            });

            var ServerID = ServerHolder.DatabaseInstance.SelectFirst<ModelInventory>(ModelInventory.GetServerID, new
            {
                OID = 1,

            });

            response.Type = type;
            response.Action = action;

            response.ServerID = ServerID.ServerID;
            response.ItemID = itemid;
            response.ItemEnd = itemend;

            _ = ServerHolder.ServerInstance.SendPacket(response.Serialize(), stream);

        }


        public static void Enchant(ushort type, ushort action, byte[] body, NetworkStream stream)
        {
            action++;
            EnchantReq request = EnchantReq.Deserialize(body);
            EnchantRes response = new EnchantRes();

            response.Action = action;
            response.Type = type;
            
            response.EnchantGemID = request.EnchantGemID;
            response.StatMod = 1;

            _ = ServerHolder.ServerInstance.SendPacket(response.Serialize(), stream);

        }

    }
}
