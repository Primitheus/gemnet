﻿using Gemnet.Network.Header;
using Gemnet.Packets.Login;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gemnet.Network.Packets
{
    public class GetPropertyReq : HeaderPacket
    {
        public int UserID { get; set; }

        public new static GetPropertyReq Deserialize(byte[] data)
        {
            GetPropertyReq packet = new GetPropertyReq();

            int offset = 6;

            packet.Type = ToUInt16BigEndian(data, 0);
            packet.Size = ToUInt16BigEndian(data, 2);
            packet.Action = BitConverter.ToUInt16(data, 4);

            packet.UserID = BitConverter.ToInt32(data, offset);

            return packet;
        }
    }

    public class Item {

        public int ServerID { get; set; }
        public int ItemID { get; set; }
        public int ItemEnd { get; set; }
        public int StatMod {get; set;}
        public int ItemType { get; set; }

    }

    public class GetPropertyRes : HeaderPacket
    {
        public List<Item> Items { get; set; }

        private struct PropertyOffsets {

            public static readonly int NumberOfItems = 6;
            public static readonly int ServerID = 8;
            public static readonly int ItemID = 12;
            public static readonly int ItemEnd = 37;
            public static readonly int StatMod = 41;
            public static readonly int ItemType = 39;

        }


        public override byte[] Serialize()
        {
            int NumberOfItems = Items.Count();
            int bufferSize = 6 + (NumberOfItems * 49) + 12;

            byte[] buffer = new byte[bufferSize];

            Size = (ushort)buffer.Length;
            int offset = 0;

            base.Serialize().CopyTo(buffer, offset);
            var i = 0;

            foreach (var item in Items) 
            {
                if (i == 0) {
                    
                    BitConverter.GetBytes(NumberOfItems).CopyTo(buffer, PropertyOffsets.NumberOfItems);
                
                }
               
                BitConverter.GetBytes(item.ServerID).CopyTo(buffer, i + PropertyOffsets.ServerID);    
                BitConverter.GetBytes(item.ItemID).CopyTo(buffer, i + PropertyOffsets.ItemID);
                BitConverter.GetBytes(item.ItemEnd).CopyTo(buffer, i + PropertyOffsets.ItemEnd);
                BitConverter.GetBytes(item.StatMod).CopyTo(buffer, i + PropertyOffsets.StatMod);                

                i = i + 49;

            }
            return buffer;
        }

    }
}
