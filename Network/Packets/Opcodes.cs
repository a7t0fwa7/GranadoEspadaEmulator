using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranadoEspadaHeadless.Network.Packets
{
    class Opcodes
    {
        public class Outbound
        {
            public const short Login = 0x0004;

            public const short EnterBarracks = 0x0009;
            public const short RequestCharacterList = 0x000F;
            public const short MigrateSavePoint = 0x0016;
            public const short MigrateLastPoint = 0x0017;

            public const short EnterGameServer = 0x0E06;  //06 0E 00 00 00 00 2C 04 00 00 FC 6B 00 00 E1 41 00 00 63 61 74 32 30 32 32 00 3D 54 10 8B B3 00 C0 6B 95 58 00 00 00 00 
            public const short EnterGameServer2 = 0x0E08; //08 0E 01 00 00 00 D8 00 00 00 01 1B E9 
            public const short EnterGameServer3 = 0x0D6B; //6B 0D 02 00 00 00 7A 00 00 00 
            public const short EnterGameServer4 = 0x0D6D; //6D 0D 03 00 00 00 79 00 00 00 
            public const short EnterGameServer5 = 0x0F1b; //1B 0F 04 00 00 00 2E 00 00 00 
            public const short EnterGameServer6 = 0x0C08; //08 0C 06 00 00 00 02 00 00 00 10 00 00 00 00 00 
            public const short EnterGameServer7 = 0x0D7E; //7E 0D 08 00 00 00 83 00 00 00 00 00 00 00 
            public const short EnterGameServer8 = 0x0E68; //68 0E 0C 00 00 00 7A 00 00 00 
            public const short EnterGameServer9 = 0x0C4E; //4E 0C 0B 00 00 00 51 00 00 00 
       
            public const short UnequipCashItem_Barracks = 0x32;

            public const short Speak = 0x0F30;

            public const short MoveCharacter = 0x0E0E; //
            public const short OpenCashItemWindow = 0x0;
            public const short RetrieveCashItem = 0x0DF1; //F1 0D 28 00 00 00 06 01 00 00 44 75 01 00
            public const short Logout = 0x0B0E;
        }
    
        public class Inbound
        {
            public const short JunkData = 0;

            //BARRACK
            public const short PacketFailed = 0x0003;
            public const short ForceDisconnected = 0x0C22;
            public const short OnDisconnect = 0x0EC8;

            public const short AfterLoginResponse = 0x00B1;
            public const short StringInfo = 0x0042;
            public const  short LoginResponse = 0x0008;
            public const short CharacterList = 0x0010;
            public const short CharacterList2 = 0x0011; //11 00 4F 00 00 00 00 00 00 00 00 00 07 00 00 00 01 00 00 00 00 00 00 00 00 01 00 00 00 00 01 00 00 00 04 00 00 00 00 01 00 00 00 05 00 00 00 00 01 00 00 00 01 00 00 00 00 02 00 00 00 04 00 00 00 00 02 00 00 00 05 00 00 00 00 02 00 00 00 23 00 0B 00 01 01 01 12 00 00 [01] ...

            public const short ZoneDisconnected = 0x0eC8;

            public const short ServerIP = 0x0019;

            //ZONE/GAME
            public const short ZoneCharactersData = 0x0E07;
            public const short Zone_Unk1 = 0x0F1F;

            public const short GuildMembersOnline = 0x0D9A;

            public const short UI_Alert1 = 0x180D;
            public const short UI_Alert2 = 0x0D72;
            public const short ServerChatMessage = 0x0F2E;
            public const short ServerChatMessage_2 = 0x0CC7;

            public const short ColonyWarInfo = 0x0E05;

            public const short Movement = 0x0DDC;

            public const short PlayerChat = 0x0DE0;
            public const short AdminWhisper = 0x0D77;
            public const short Whisper = 0x0F2F;
            public const short BroadcastMessage = 0x0F2E;
        }
    }
}
