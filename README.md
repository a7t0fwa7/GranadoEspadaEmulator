# GranadoEspadaEmulator
Clientless Bot/Network Client Emulator for Granado Espada, written in C# for educational purposes  

You will need to provide your own packet encryption DLL (Crypt.dll) which exports the routines found in the file "Network/Encryption/Crypto.cs". While the program works just fine, there is no built-in features such as monster hunting, exploring, etc, besides player movement. The code base can be used as a 'template' for other game network bots.
