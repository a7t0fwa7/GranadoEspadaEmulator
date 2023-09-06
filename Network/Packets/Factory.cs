using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//RULES: 
//PACKETCOUNT AND CHECKSUM MUST BE CORRECT OR THE PACKET WILL D/C!
namespace GranadoEspadaHeadless.Network.Packets
{
    public static class Factory
    {
        public static int packetCount = 0; //increments by 1 each time a packet is sent and resets on each new server

        //04 00 00 00 00 00 2F 57 00 00 2C 03 63 61 74 32 30 32 32 00 00 00 00 00 00 00 00 00 00 DC C1 76 DC D4 19 00 01 00 00 00 5E 08 5F 00 90 22 0E 02 44 DB C1 76 5E 08 5F 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 68 E9 6C 92 00 00 00 00 74 D1 19 00 C5 D9 C1 76 90 22 0E 02 00 00 00 00 0D 00 00 00 0B 00 00 00 DC D4 19 00 0D 00 00 00 10 60 00 00 7C C8 AB 77 A0 D1 19 00 72 A0 C6 76 90 22 0E 02 00 00 00 00 0D 00 00 00 0B 00 00 00 DC D4 19 00 00 00 00 00 5E 08 5F 00 0D 00 00 00 00 81 AF 77 CC D1 19 00 7B 13 C4 76 5E 08 5F 00 0D 00 00 00 0B 00 00 00 DC D4 19 00 5E 08 5F 00 CD AB BA DC 02 00 00 00 9C 3C C3 76 80 3C C3 76 60 00 E1 0A DC 39 49 BA 59 AB BE 56 E0 57 F2 0F 88 3E 00 00 00 65 00 00 00 44 45 55 00 07 9D 73 F4 00 7F 04 00 00 00 00 44 50 7F 01 00 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 1B E9 04 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 24 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 70 00 00 00 FF FF FF FF FF FF FF FF 4E 81 C3 76 7F 85 C3 76 00 00 00 00 00 00 00 00 C8 11 BE 01 00 00 00 00 10 60 00 00 00 00 00 00 00 00 00 00 48 13 BE 01 00 00 00 00 00 00 00 00 00 00 00 00 00 81 AF 77 00 00 00 00 01 00 00 00 0A 00 00 00 5E 08 5F 00 10 60 00 00 00 00 00 00 00 00 00 00 28 D4 19 00 20 33 C4 76 BC 90 BC E4 FE FF FF FF E8 D2 19 00 07 7A C3 76 00 81 AF 77 00 00 00 00 0D 00 00 00 0B 00 00 00 DC D4 19 00 00 00 00 00 00 00 00 00 0D 00 00 00 0B 00 00 00 DC D4 19 00 00 81 AF 77 00 00 00 00 00 D3 19 00 DB 44 C2 76 0D 00 00 00 10 60 00 00 7C C8 AB 77 01 00 00 00 28 D3 19 00 23 D4 89 00 00 81 AF 77 5E 08 5F 00 00 08 00 00 00 00 00 00 C4 00 30 50 C4 00 30 50 00 00 00 00 A0 D3 19 00 F3 57 C3 76 00 E0 FF FF 00 00 00 00 60 00 00 00 00 00 00 20 D4 EB 6C 92 00 00 00 00 44 50 7F 04 44 50 7F 04 68 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 C4 D3 19 00 90 22 0E 02 DA FC FF FF 6A 00 00 00 A4 D3 19 00 04 89 C3 76 D0 EB 6C 92 04 51 7F 04 5E 08 5F 00 44 50 7F 04 D4 EB 6C 92 00 00 00 00 74 D3 19 00 00 D6 19 00 00 D6 19 00 20 33 C4 76 7C 91 BC E4 FE FF FF FF 04 D4 19 00 82 12 9D 73 5E 08 5F 00 00 00 00 00 44 50 7F 04 A9 12 9D 73 00 00 00 00 5E 08 5F 00 3C 00 00 00 DA FC FF FF 6A 00 00 00 32 FF FF FF 5E 02 00 00 DA FC FF FF 6A 00 00 00 21 FF FF FF 4D 02 00 00 00 00 00 00 44 50 7F 04 20 50 7F 04 00 00 00 00 5E 08 5F 00 F0 D4 19 00 24 0F 9D 73 F0 D4 19 00 59 0F 9D 73 D0 54 7F 04 20 50 74 00 
        public static PacketWriter Login(string account)
        {
            packetCount = 0;

            var p = new PacketWriter(Opcodes.Outbound.Login);
                    
            p.WriteInt(packetCount);        
            p.WriteUInt(0); //CHECKSUM
           
            p.WriteShort(0x032C); //stays CONSTANT
            p.WriteString(account);
            p.WriteZero(16 - account.Length);

            p.WriteUInt(0x0F75DBB5); //likely a hash of the password

            p.WriteZero(3);

            p.WriteUInt(0x7E6E01E1);  //behold the horror: very large packet with gibberish-looking contents
            p.WriteUInt(0x282);

            p.WriteUInt(0x80006010);
            p.WriteUInt(0x77E2C87C);
            p.WriteZero(12);

            p.WriteUInt(0x20CC780);
            p.WriteByte(9);
            p.WriteUInt(0x77E2DD04);
            p.WriteZero(8);
            p.WriteUInt(0x0019D148);
            p.WriteUInt(0x75DBB386);
            p.WriteUInt(0x0F);
            p.WriteUInt(0x7E6E01E1);
            p.WriteUInt(0x19D174);
            p.WriteUInt(0x77C7137B);
            p.WriteUShort(0x0604);
            p.WriteUShort(0x0010);
            p.WriteUInt(0x282);
            p.WriteUInt(0x0F);
            p.WriteUInt(0x7E6E01E1);
            p.WriteUShort(0x0604);
            p.WriteUShort(0x0010);

            p.WriteUInt(0xDCBAABCD); //line 30
            p.WriteUInt(0x75DBb370);

            p.WriteUInt(0x282);
            p.WriteUShort(0x0604);
            p.WriteUShort(0x0010);
            p.WriteUInt(0x19D258);

            p.WriteUInt(0x77C6833A);
            p.WriteUInt(0x3E57B397);

            p.WriteUShort(0x0604);
            p.WriteUShort(0x0010);
            p.WriteUInt(0x19D258); //line 40

            p.WriteUInt(0x77C6857F);
            p.WriteUInt(0x77C6837C);
            p.WriteUInt(0x6E13C1A7);

            p.WriteUInt(0x40000000);
            p.WriteUInt(0xC0002308);

            p.WriteUInt(0x282);
            p.WriteZero(46); //47
            p.WriteUInt(0x39DC0AE1);
            p.WriteUInt(0xAB59BA49);
            p.WriteUInt(0x57E056BE); //line 50
            p.WriteUInt(0x3E880FF2);

            p.WriteZero(3);
            p.WriteUInt(0x65);
            p.WriteString("DEU"); //CHECK # of ZEROS HERE!

            p.WriteZero(4);

            p.WriteUShort(0x00FF);
            p.WriteUInt(0xFFFFFFFF);
            p.WriteUShort(0xFFFF);

            p.WriteUInt(0x01C6814E);

            p.WriteZero(2); //line 60
            p.WriteInt(2);
            p.WriteZero(6);
            p.WriteUInt(0x01B811C8);
            p.WriteZero(4);
            p.WriteUInt(0x80006010);
            p.WriteZero(6);
            p.WriteUInt(0x0004E91B);
            p.WriteUInt(0x000001B8);
            p.WriteZero(10);

            p.WriteUInt(0x75DBb370); //line 70
            p.WriteInt(0);
            p.WriteInt(1);
            p.WriteInt(0);
            p.WriteUShort(0x0604);
            p.WriteUShort(0x0010);
            p.WriteUInt(0x80006010);
            p.WriteInt(0);
            p.WriteUInt(0x19D2AC);
            p.WriteUInt(0x19D2AC);
            p.WriteUInt(0x77C73320); //80
            p.WriteUInt(0x19C6BB37);

            p.WriteUInt(0xFFFFFFFE);
            p.WriteUInt(0x19D2BC);
            p.WriteUInt(0x77C67F2A);

            p.WriteUInt(0x75DBB370);
            p.WriteUInt(0);
            p.WriteUInt(0x282);
            p.WriteUInt(0x0F);

            p.WriteUInt(0x7E6E01E1);
            p.WriteUInt(0x0031E000); //90
            p.WriteInt(1);
            p.WriteUInt(0x6E13C143);
            p.WriteUInt(0x282);
            p.WriteUInt(0x77E68240);
            p.WriteUInt(0);
            p.WriteUInt(0x77CC1BA8);
            p.WriteShort(0);
            p.WriteInt(4);
            p.WriteZero(6);
            p.WriteUShort(0x0604); //100
            p.WriteUShort(0x0010);
            p.WriteInt(0);
            p.WriteUInt(0x19D27C);
            p.WriteInt(0);
            p.WriteUInt(0x19D308);
            p.WriteUInt(0x77C73320);
            p.WriteUInt(0x19C6BB57);
            p.WriteUInt(0xFFFFFFFE);
            p.WriteUInt(0x0019D2F8);
            p.WriteUInt(0x77C6BAFF); //110

            p.WriteUInt(0x020CC780);
            p.WriteUInt(0x77E52b2C);
            p.WriteUInt(0x77C6BB17);
            p.WriteUInt(0x0019D2E0);
            p.WriteInt(0x18);
            p.WriteInt(0);
            p.WriteUInt(0x77C72DA0);

            p.WriteZero(24);
            p.WriteUInt(0x19D39c);
            p.WriteUInt(0x77E54FBD); //120
            p.WriteUInt(0x19D314);
            p.WriteUInt(0x20);
            p.WriteUInt(0x19D540);
            p.WriteUInt(0x77E54EF0);
            p.WriteUInt(0x19D338);
            p.WriteUInt(0x020CC780);
            p.WriteInt(0);
            p.WriteUInt(0x282);
            p.WriteUInt(0x0F);
            p.WriteUInt(0x7E6E01E1); //130
            p.WriteUInt(0x75DBb370);
            p.WriteUInt(0x77E68240);
            p.WriteUInt(0);
            p.WriteUInt(0x7770107C);
            p.WriteUInt(0x77C6788B);
            p.WriteUShort(0x0604); 
            p.WriteUShort(0x0010);
            p.WriteUInt(0x282);
            p.WriteUInt(0x0F);

            p.WriteUInt(0x7E6E01E1); //140
            p.WriteInt(0);
            p.WriteUInt(0x02B1);
            p.WriteInt(0);

            p.WriteUInt(0x01376658);
            p.WriteUInt(0x01376680);

            p.WriteUInt(0x0F);
            p.WriteUInt(0x020BB330);
            p.WriteInt(0);
            p.WriteUInt(0x0F);

            p.WriteUShort(0x0604); //150
            p.WriteUShort(0x0010);
            p.WriteUInt(0x2860);
            p.WriteZero(6);

            p.WriteShort(0x01BE);
            p.WriteUInt(0x1002);
            p.WriteUInt(0x7770107C);
            p.WriteInt(0);
            p.WriteUInt(0x75DBB370);
            p.WriteUInt(0x40000000);
            p.WriteUInt(0xC0002308); //160

            p.WriteUInt(0x0F);
            p.WriteUInt(0x19D3C4);
            p.WriteUInt(0x77C6A181);
            p.WriteUInt(0x020CC780);

            p.WriteInt(0);
            p.WriteUInt(0x7E6E01E1);
            p.WriteZero(8);
            p.WriteUInt(0x020CC4B0);
            p.WriteUInt(0x01376658);
            p.WriteUInt(0x282); //170
            p.WriteUInt(0x19D3F4);
            p.WriteUInt(0x77C6AA25);
            p.WriteUInt(0x01376658);
            p.WriteUInt(0);
            p.WriteUInt(0x7E6E01E1);
            p.WriteUInt(0);
            p.WriteUInt(0x282);
            p.WriteUInt(0x020CC4B0);
            p.WriteUInt(0x0F);
            p.WriteUInt(0x0F); //180

            p.WriteUInt(0x779647CB);
            p.WriteUInt(0x282);
            p.WriteUInt(0x19D420);
            p.WriteUInt(0x77C69AE2);
            p.WriteUInt(0x01376658);
            p.WriteUInt(0);
            p.WriteUInt(0x7E6E01E1);
            p.WriteUInt(0x282);
            p.WriteUInt(0x80006010);
            p.WriteUInt(0x0074C87C);

            packetCount += 1;
            return p;
        }

        //04 00 00 00 00 00 00 00 00 00 2E 03 37 36 35 36 31 31 39 39 32 34 36 32 34 35 37 32 30 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 65 00 00 00 53 54 45 41 4D 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 CB 64 05 00 14 00 00 00 32 E7 E5 1D DE 26 12 4A 58 7F A6 4C 01 00 10 01 94 E2 E6 62 18 00 00 00 01 00 00 00 02 00 00 00 E3 53 D8 5E 6A B7 A5 C5 CA A4 71 22 36 00 00 00 B2 00 00 00 32 00 00 00 04 00 00 00 58 7F A6 4C 01 00 10 01 32 1E 0A 00 D3 EB AF 6C 0E 01 A8 C0 00 00 00 00 F8 13 DE 62 78 C3 F9 62 01 00 18 DF 02 00 00 00 00 00 02 40 9F 9D DC 76 AD 76 20 0D 33 0A E7 2F B2 E3 ED 92 F2 8C 6D B6 4A 84 05 2F 55 E2 2D C3 96 AC E9 0B 03 19 E4 68 80 21 5E 95 72 01 5B FE 34 9B 4A 72 E1 BB 99 3C D4 8B 30 86 35 ED 53 F8 4A 4B 37 1D DF 30 36 71 6E 4E 5C 54 1C 24 9B F9 2A 41 9F 39 47 56 8B 38 31 F6 FD B7 F5 B7 07 69 2D C8 08 10 88 27 43 22 33 60 80 85 6A DA 3D BC 0B 3C 23 40 63 72 A0 6F 3E 13 C8 B4 16 66 7B 98 B2 E5 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 EA 00 00 00
        public static PacketWriter SteamLogin(string token)
        {
            packetCount = 0;

            var p = new PacketWriter(Opcodes.Outbound.Login);
            p.WriteInt(packetCount);
            p.WriteInt(0);
            p.WriteUShort(0x032E);
            p.WriteString(token);
            p.WriteZero(239 - token.Length);
            p.WriteShort(1);
            p.WriteInt(0x65);
            p.WriteString("STEAM");
            p.WriteZero(13);
            p.WriteShort(1);
            p.WriteByte(0);
            p.WriteInt(2);
            p.WriteZero(24);
            p.WriteUInt(0x000564CB);
            p.WriteInt(0x14);

            byte[] loginBytes = { 0x32, 0xE7, 0xE5, 0x1D, 0xDE, 0x26, 0x12, 0x4A, 0x58, 0x7F, 0xA6, 0x4C, 0x01, 0x00, 0x10, 0x01, 0x94, 0xE2, 0xE6, 0x62, 0x18, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0xE3, 0x53, 0xD8, 0x5E, 0x6A, 0xB7, 0xA5, 0xC5, 0xCA, 0xA4, 0x71, 0x22, 0x36, 0x00, 0x00, 0x00, 0xB2, 0x00, 0x00, 0x00, 0x32, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x58, 0x7F, 0xA6, 0x4C, 0x01, 0x00, 0x10, 0x01, 0x32, 0x1E, 0x0A, 0x00, 0xD3, 0xEB, 0xAF, 0x6C, 0x0E, 0x01, 0xA8, 0xC0, 0x00, 0x00, 0x00, 0x00, 0xF8, 0x13, 0xDE, 0x62, 0x78, 0xC3, 0xF9, 0x62, 0x01, 0x00, 0x18, 0xDF, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x40, 0x9F, 0x9D, 0xDC, 0x76, 0xAD, 0x76, 0x20, 0x0D, 0x33, 0x0A, 0xE7, 0x2F, 0xB2, 0xE3, 0xED, 0x92, 0xF2, 0x8C, 0x6D, 0xB6, 0x4A, 0x84, 0x05, 0x2F, 0x55, 0xE2, 0x2D, 0xC3, 0x96, 0xAC, 0xE9, 0x0B, 0x03, 0x19, 0xE4, 0x68, 0x80, 0x21, 0x5E, 0x95, 0x72, 0x01, 0x5B, 0xFE, 0x34, 0x9B, 0x4A, 0x72, 0xE1, 0xBB, 0x99, 0x3C, 0xD4, 0x8B, 0x30, 0x86, 0x35, 0xED, 0x53, 0xF8, 0x4A, 0x4B, 0x37, 0x1D, 0xDF, 0x30, 0x36, 0x71, 0x6E, 0x4E, 0x5C, 0x54, 0x1C, 0x24, 0x9B, 0xF9, 0x2A, 0x41, 0x9F, 0x39, 0x47, 0x56, 0x8B, 0x38, 0x31, 0xF6, 0xFD, 0xB7, 0xF5, 0xB7, 0x07, 0x69, 0x2D, 0xC8, 0x08, 0x10, 0x88, 0x27, 0x43, 0x22, 0x33, 0x60, 0x80, 0x85, 0x6A, 0xDA, 0x3D, 0xBC, 0x0B, 0x3C, 0x23, 0x40, 0x63, 0x72, 0xA0, 0x6F, 0x3E, 0x13, 0xC8, 0xB4, 0x16, 0x66, 0x7B, 0x98, 0xB2, 0xE5, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xEA, 0x00, 0x00, 0x00 };
            p.WriteBytes(loginBytes);

            return p;
        }

        //09 00 01 00 00 00 08 00 00 00 00 
        //10 00 85 99 1E 0C 54 4C AD 2C C0 58 34 18 55 74 DC D6
        public static PacketWriter AfterLogin()
        {
            var p = new PacketWriter(Opcodes.Outbound.EnterBarracks);
            p.WriteInt(packetCount);
            p.WriteInt(0);
            p.WriteZero(1);
            packetCount += 1;
            return p;
        }

        //0F 00 02 00 00 00 0D 00 00 00 
        //10 00 C3 88 69 4E 80 1C 44 78 1C 0D 4E 24 0E 58 EA 3F
        public static PacketWriter RequestCharacterList()
        {
            var p = new PacketWriter(Opcodes.Outbound.RequestCharacterList);
            p.WriteInt(packetCount);
            p.WriteInt(0);
            packetCount += 1;
            return p;
        }

        //16 00 03 00 00 00 F7 00 00 00 00 00 00 00 02 00 01 00 1C E9 04 00 
        public static PacketWriter MigrateSavePoint(uint TeamID)
        {
            var p = new PacketWriter(Opcodes.Outbound.MigrateSavePoint);
            p.WriteZero(8);
            p.WriteInt(0);
            p.WriteShort(2);
            p.WriteShort(1);
            p.WriteUInt(TeamID);

            packetCount = 0; //reset on new server
            return p;
        }

        //17 00 03 00 00 00 F0 00 00 00 00 00 00 00 02 00 01 00 1C E9 04 00 
        public static PacketWriter MigrateLastPoint(uint TeamID)
        {
            var p = new PacketWriter(Opcodes.Outbound.MigrateLastPoint);
            p.WriteInt(3);
            p.WriteInt(0); //checksum
            p.WriteInt(0);
            p.WriteShort(2);
            p.WriteShort(1);
            p.WriteUInt(TeamID); //teamID must be correct or we cant enter game

            packetCount = 0; //reset on new server
            return p;
        }

        //GAME SERVER PACKETS BELOW
        //06 0E 00 00 00 00 E7 03 00 00 D4 1A 00 00 04 00 00 00 63 61 74 32 30 32 32 00 77 6E 10 8B B3 00 78 D6 6D BF 00 00 00 00 
        // ** CharacterSetHash is 4 bytes UINT and represents which 3 characters in team you are choosing for in-game. This must be updated correctly or you cannot log in past first GameServer packet! **
        public static PacketWriter EnterGameServer(uint sessionId, uint accountId, string accountName,  ushort zoneID, uint CharacterSetHash)
        {
            var p = new PacketWriter(Opcodes.Outbound.EnterGameServer);
            p.WriteInt(0); //is always first packet sent, counter can be 0
            p.WriteUInt(0); //probably is checksum..
            p.WriteUInt(sessionId);
            p.WriteUInt(accountId);
            p.WriteString(accountName);
            p.WriteZero(11 - accountName.Length);
            p.WriteUShort(zoneID);
            p.WriteZero(1);
            p.WriteUInt(CharacterSetHash);
            p.WriteInt(0);
            packetCount += 1;
            return p;
        }

        public static PacketWriter EnterGameServer2(uint unk1, ushort unk3)
        {
            var p = new PacketWriter(Opcodes.Outbound.EnterGameServer2);
            p.WriteInt(packetCount);
            p.WriteUInt(0);
            p.WriteByte(1);
            p.WriteUShort(unk3);
            packetCount += 1;
            return p;
        }

        //6B 0D 02 00 00 00 7A 00 00 00 
        public static PacketWriter EnterGameServer3(uint unk1)
        {
            var p = new PacketWriter(Opcodes.Outbound.EnterGameServer3);
            p.WriteInt(packetCount);
            p.WriteUInt(0);
            p.WriteZero(3);
            packetCount += 1;
            return p;
        }

        //6D 0D 03 00 00 00 79 00 00 00 
        public static PacketWriter EnterGameServer4(uint unk1)
        {
            var p = new PacketWriter(Opcodes.Outbound.EnterGameServer4);
            p.WriteInt(packetCount);
            p.WriteUInt(0);
            p.WriteZero(3);
            packetCount += 1;
            return p;
        }

        //1B 0F 04 00 00 00 2E 00 00 00 
        public static PacketWriter EnterGameServer5( )
        {
            var p = new PacketWriter(Opcodes.Outbound.EnterGameServer5);
            p.WriteInt(packetCount);
            p.WriteInt(0);       
            packetCount += 1;
            return p;
        }

        public static PacketWriter EnterGameServer6(int unk1)
        {
            var p = new PacketWriter(Opcodes.Outbound.EnterGameServer6);
            p.WriteInt(packetCount);
            p.WriteInt(0);
            p.WriteInt(0x10);
            p.WriteShort(0);
            packetCount += 1;
            return p;
        }

        public static PacketWriter EnterGameServer7(int unk1)
        {
            var p = new PacketWriter(Opcodes.Outbound.EnterGameServer7);
            p.WriteInt(packetCount);
            p.WriteInt(0);
            p.WriteInt(0);
            packetCount += 1;
            return p;
        }

        public static PacketWriter EnterGameServer8(int unk1)
        {
            var p = new PacketWriter(Opcodes.Outbound.EnterGameServer8);
            p.WriteInt(packetCount);
            p.WriteInt(0);
            p.WriteInt(0);
            packetCount += 1;
            return p;
        }

        public static PacketWriter EnterGameServer9(int unk1)
        {
            var p = new PacketWriter(Opcodes.Outbound.EnterGameServer9);
            p.WriteInt(packetCount);
            p.WriteInt(0);
            p.WriteInt(0);
            packetCount += 1;
            return p;
        }

        //30 0F 0A 00 00 00 83 01 00 00 1F 00 00 00 00 00 00 22 4C 75 63 61 6E 6F 20 48 45 4C 4C 4F 21 00
        //30 0F 22 00 00 00 D5 01 00 00 23 00 00 00 00 00 00 22 61 47 45 42 4F 54 20 73 64 66 73 64 66 73 64 66 00 
        public static PacketWriter Speak(string message)
        { 
            int dataLength = message.Length + 18; //check this...

            var p = new PacketWriter(Opcodes.Outbound.Speak);
            p.WriteInt(packetCount);
            p.WriteInt(0); // checksum
            p.WriteInt(dataLength);
            p.WriteZero(3);
            p.WriteString(message);
            p.WriteZero(1);
            packetCount += 1;
            return p;
        }

        //0E 0E 57 00 00 00 36 03 00 00 02 BA 01 00 00 79 FF FF FF BA 00 00 00 
        public static PacketWriter MoveCharacter(byte characterIndex, int unk1, int x, int y, int z)
        {
            var p = new PacketWriter(Opcodes.Outbound.MoveCharacter);
            p.WriteInt(packetCount);
            p.WriteInt(0);
            p.WriteByte(characterIndex);
            p.WriteInt(x);
            p.WriteInt(y);
            p.WriteInt(z);
            packetCount += 1;
            return p;
        }

        //83 0D 74 00 00 00 5E 01 00 00 6A CE 02 00 
        public static PacketWriter RetrieveCashGift(uint unk1, uint index)
        {
            var p = new PacketWriter(Opcodes.Outbound.RetrieveCashItem);
            p.WriteInt(packetCount);
            p.WriteUInt(0);
            p.WriteUInt(index);
            packetCount += 1;
            return p;
        }
    }
}
