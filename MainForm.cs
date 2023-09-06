using GranadoEspadaHeadless.Game;
using GranadoEspadaHeadless.Helpers;
using GranadoEspadaHeadless.Network;
using GranadoEspadaHeadless.Network.Packets;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Text;

namespace GranadoEspadaHeadless
{
    public partial class MainForm : Form
    {
        public Client Client { get; private set; }

        public string CurrentUsername { get; set; }
     
        public Thread t_RecvLog { get; private set; }
        public Thread t_SendLog { get; private set; }

        public Thread SendPlayerMessagesThread { get; private set; }
        public Thread SendNoticeFromDbThread { get; private set; }

        public MainForm()
        {
            InitializeComponent();
            Client = new Client();
        }

        private void ProcessPacketQueues()
        {
            bool recving = true;

            Console.WriteLine("Started processing packets...");

            while(recving)
            {
                try
                {
                    if (Client.RecvPacketQueue.Count > 0)
                    {
                        PacketReader packet = Client.RecvPacketQueue.Dequeue();
                        
                        //packet.Position = 0;

                        if (packet == null)
                        {
                            // Dispose();
                            continue;
                        }

                        string s_length = Convert.ToString(packet.Length);

                        if (listView_RawView.InvokeRequired)
                        {
                            listView_RawView.Invoke((MethodInvoker)delegate ()
                            {
                                if (packet.Length >= 2)
                                {
                                    packet.m_index = 0;
                                    byte[] bytes = packet.ReadBytes(packet.Length);
                                    
                                    if(bytes != null)
                                    {
                                        string hexString = BitConverter.ToString(bytes);
                                        string hexOpcode = BitConverter.ToString(bytes, 0, 2);
                                        hexString = hexString.Replace("-", " ");
                                        hexOpcode = hexOpcode.Replace("-", " ");

                                        string[] row = { "In", s_length, hexOpcode, hexString };
                                        var listViewItem = new ListViewItem(row);
                                        listView_RawView.Items.Add(listViewItem);
                                    }

                                }
                                else
                                {
                                    byte[] bytes = packet.ReadBytes(packet.Length);
                                    
                                    if(bytes != null)
                                    {
                                        string hexOpcode = BitConverter.ToString(bytes);
                                        hexOpcode = hexOpcode.Replace("-", " ");

                                        string[] row = { "In", s_length, hexOpcode, " " };
                                        var listViewItem = new ListViewItem(row);
                                        listView_RawView.Items.Add(listViewItem);
                                    }
                                }
                            });

                        }
                    }

                    if (Client.SendPacketQueue.Count > 0)
                    {
                        PacketReader packet = Client.SendPacketQueue.Dequeue();

                        if (packet == null)
                        {
                            // Dispose();
                            continue;
                        }

                        if (listView_RawView.InvokeRequired)
                        {
                            listView_RawView.Invoke((MethodInvoker)delegate ()
                            {
                                if (packet.Length >= 2)
                                {
                                    if (packet.m_index == packet.Length)
                                        packet.m_index = 0;

                                    byte[] bytes = packet.ReadBytes(packet.Length);
                                    
                                    if(bytes != null)
                                    {
                                        string hexString = BitConverter.ToString(bytes);
                                        string hexOpcode = BitConverter.ToString(bytes, 0, 2);
                                        hexString = hexString.Replace("-", " ");
                                        hexOpcode = hexOpcode.Replace("-", " ");

                                        string[] row = { "Out", Convert.ToString(packet.Length), hexOpcode, hexString };
                                        var listViewItem = new ListViewItem(row);
                                        listView_RawView.Items.Add(listViewItem);
                                    }
                                }
                                else
                                {
                                    byte[] bytes = packet.ReadBytes(packet.Length);

                                    if (bytes != null)
                                    {

                                        string hexOpcode = BitConverter.ToString(bytes);
                                        hexOpcode = hexOpcode.Replace("-", " ");

                                        string[] row = { "In", Convert.ToString(packet.Length), hexOpcode, " " };
                                        var listViewItem = new ListViewItem(row);
                                        listView_RawView.Items.Add(listViewItem);
                                    }
                                }
                            });
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to de-queue packet: " + e.Message);
                    continue;
                }          
            }
            
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Client.account = new Account();

            Client.account.Username = textBox_AcctName.Text;
            Client.account.Password = textBox_Pass.Text;
            Client.account.CharacterName = textBox_TeamHash.Text;
            Client.account.PIN = textBox_PIN.Text;
            Client.account.TeamId = Convert.ToUInt32(textBox_TeamID.Text); //todo: wrap in try, catch

            if (Client.account.Username != CurrentUsername)
            {
                Client.AcceptedAccountIndex = 0; //reset login tricks
                Client.AccountIndex = 4;
            }

            this.CurrentUsername = textBox_AcctName.Text;

            try
            {
                Client.account.SelectedTeamHash = Convert.ToUInt32(textBox_TeamHash.Text);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Could not parse team hash: " + ex.Message);
            }

            string IP = textBox_IP.Text;
            int port = (int)numericUpDown_Port.Value;

            if(IP.Length > 0 && IP.Contains("."))
            {
                Client.ConnectedIp = IP;

                if (Client.Port > 0)
                    Client.Port = port;
                else
                    Client.Port = 7001;
            }

            Client.LoginServerIP = IP;
            Client.LoginPort = port;

            ConnectToLoginServer();
        }

        private void ConnectToLoginServer()
        {
            Client.RecvPacketQueue = new Queue<PacketReader>();
            Client.SendPacketQueue = new Queue<PacketReader>();

            //create recv GUI threads
            if (t_RecvLog != null)
            {
                if (t_RecvLog.ThreadState == ThreadState.Running)
                {
                    t_RecvLog.Abort();
                }
            }

            Client.m_DB = new Network.Database.Database(Network.Database.Database.DefaultConnectionString);

            t_RecvLog = new Thread(new ThreadStart(ProcessPacketQueues));
            t_RecvLog.Start();

            if(!ControlInvokeRequired(this.textBox_LogArea, () => LogAppend("Connecting to: " + Client.ConnectedIp + "\r\n")))
            {
                textBox_LogArea.AppendText("Connecting to: " + Client.ConnectedIp + "\r\n");
            }

            Client.Migrate(Client.ConnectedIp, Client.Port, true);
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                Client.m_session.Disconnect();

                textBox_LogArea.AppendText("Disconnected! \r\n");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception! Already disconnected/Could not disconnect: " + ex.Message);
            }
        }

        private void button_SendPacket_Click(object sender, EventArgs e)
        {
            //todo this
            string sendPacketText = textBox_SendPacket.Text.Trim();

            byte[] packetBytes = HexText.ToBytes(sendPacketText);

            var p = new PacketWriter();
            p.WriteFullPacket(packetBytes);

            Client.m_session.SendPacket(p);
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by alsch092 (https://github.com/AlSch092)");
        }

        private void CopyPacket()
        {
            ListViewItem selectedItem;

            if (listView_RawView.InvokeRequired)
            {
                listView_RawView.Invoke((MethodInvoker)delegate ()
                {
                    for (int i = 0; i < listView_RawView.Items.Count; i++)
                    {
                        selectedItem = listView_RawView.Items[i];

                        if (selectedItem.Selected)
                        {
                            String packetString = selectedItem.SubItems[3].Text;
                            Clipboard.SetDataObject(packetString, true);
                        }
                    }
                });
            }            
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread  t = new Thread(new ThreadStart(CopyPacket));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.listView_RawView.Items.Clear();
        }

        private void checkBox_SendDBNotices_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_SendDBNotices.Checked)
            {
                // start thread          
                SendNoticeFromDbThread = new Thread(new ThreadStart(SendNoticeFromDbThreadProc));
                SendNoticeFromDbThread.Start();
            }
            else
            {
                // stop the thread
                SendNoticeFromDbThread.Abort();
            }
        }

        private void LogAppend(string text) //for cross-thread methods
        {
            textBox_LogArea.AppendText(text); 
        }


        public bool ControlInvokeRequired(Control c, Action a) //for cross-thread methods
        {
            if (c.InvokeRequired) c.Invoke(new MethodInvoker(delegate { a(); }));
            else return false;

            return true;
        }

        /// <summary>
        /// Thread function for sending notices from DB
        /// </summary>
        private void SendNoticeFromDbThreadProc()
        {
            // check connection
            if (Client.account == null || Client.m_session.Connected == false)
            {
                ControlInvokeRequired(this.textBox_LogArea, () => LogAppend("ERROR: Cann't start sending notice. Connect to the login server first."));

                this.checkBox_SendDBNotices.BeginInvoke(new MethodInvoker(() => { this.checkBox_SendDBNotices.Checked = false; }));
                return;
            }

            while (true)
            {
                if (Client.m_session.Connected == false) // if there is no connection - try to reconnect
                {
                    if(!ControlInvokeRequired(this.textBox_LogArea, () => LogAppend("Error: connection is lost. Try to re-connect...")))
                    {
                        textBox_LogArea.AppendText("Error: connection is lost. Try to re-connect...");
                    }
                    
                    Thread T = new Thread(new ThreadStart(ConnectToLoginServer)); //probably needs another new thread
                    T.Start();
                }
                else
                {
                    if(Client.ReadyInGame == true)
                    {
                        // getting notice from DB and its ID
                        int id = 0;
                        string notice = Client.m_DB.GetGlobalNotice(out id);

                        Console.WriteLine("getting DB notice!");

                        // if we got notice
                        if (!string.IsNullOrEmpty(notice))
                        {
                            // try to send packet with this notice
                            if (Client.m_session.SendPacket(Factory.Speak("//toall " + notice)))
                            {
                                // if sending is successful - update DB record
                                Client.m_DB.UpdateGlobalNotice(id);
                                string msg = String.Format("Notice #{0} \"{1}\" has been sent from db", id, notice);
                                Console.WriteLine(msg);

                                if (!ControlInvokeRequired(this.textBox_LogArea, () => LogAppend(msg)))
                                {
                                    textBox_LogArea.AppendText(msg);
                                }
                            }
                            else
                            {
                                string msg = String.Format("Error while sending notice #{0} \"{1}\" from db", id, notice);
                              
                                if(!ControlInvokeRequired(this.textBox_LogArea, () => LogAppend(msg)))
                                {
                                    textBox_LogArea.AppendText(msg);
                                }
                            }
                        }
                    } 
                }
                 
                Thread.Sleep(1000); // sleep for 1 second
            }
        }

        private void SendWhispersFromDbThreadProc()
        {
            // check connection
            if (Client.account == null || Client.m_session.Connected == false)
            {
                if(!ControlInvokeRequired(this.textBox_LogArea, () => LogAppend("ERROR: Cann't start sending notice. Connect to the login server first.")))
                {
                    textBox_LogArea.AppendText("ERROR: Cann't start sending notice. Connect to the login server first.");                 
                }
                             
                checkBox_AutoSendMsgs.BeginInvoke(new MethodInvoker(() => { checkBox_AutoSendMsgs.Checked = false; }));
                return;
            }

            while (true)
            {
                if (Client.m_session.Connected == false) // if there is no connection - try to reconnect
                {
                    ControlInvokeRequired(this.textBox_LogArea, () => LogAppend("Error: connection is lost. Trying to reconnect..."));
                    
                    Thread T = new Thread(new ThreadStart(ConnectToLoginServer));
                    T.Start();
                }
                else
                {
                    if(Client.ReadyInGame)
                    {
                         //getting notice from DB
                        int id = 0;

                        string[] name_notice = Client.m_DB.GetPlayerNotice(out id);

                        if(name_notice != null)
                        {
                            // if we got notice
                            if (!string.IsNullOrEmpty(name_notice[0]))
                            {
                                // try to send packet with this notice
                                if (Client.m_session.SendPacket(Factory.Speak(("\"" + name_notice[0]) + " " + name_notice[1])))
                                {
                                    // if sending is successful - update DB record
                                    Client.m_DB.UpdatePlayerNotice(id);
                                    string msg = String.Format("Notice #{0} \"{1} : {2}\" has been sent from db", id, name_notice[0], name_notice[1]);
                                    Console.WriteLine(msg);
                                    ControlInvokeRequired(this.textBox_LogArea, () => LogAppend(msg));
                                }
                                else
                                {
                                    string msg = String.Format("Error while sending notice #{0} \"{1} : {2}\" from db", id, name_notice[0], name_notice[1]);
                                    Console.WriteLine(msg);
                                    ControlInvokeRequired(this.textBox_LogArea, () => LogAppend(msg));
                                }
                            }
                        }
                                 
                    }
                } // eo if-else Connected

                Thread.Sleep(1000); // sleep for 1 second
            }
        }

        private void checkBox_AutoSendMsgs_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_AutoSendMsgs.Checked)
            {
                // start thread
                SendPlayerMessagesThread = new Thread(new ThreadStart(SendWhispersFromDbThreadProc));
                SendPlayerMessagesThread.Start();
            }
            else
            {
                SendPlayerMessagesThread.Abort();
            }
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void checkBox_AutoRelog_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_AutoRelog.Checked)
            {
                Client.AutoReconnecting = true;
            }
            else
            {
                Client.AutoReconnecting = false;
            }
        }

        private void checkBox_SendAfterLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_SendAfterLogin.Checked)
            {
                string sendPacketText = textBox_PacketAfterLogin.Text.Trim();
                byte[] packetBytes = HexText.ToBytes(sendPacketText);

                if(packetBytes.Length > 0)
                {
                    Client.PacketAfterLoginBytes = HexText.ToBytes(sendPacketText);
                    Client.SendingPacketAfterLogin = true;
                }
                else
                {
                    Console.WriteLine("[ERROR] Please enter a packet into the field below the checkbox before ticking the checkbox (Send packet after login).");
                    Client.SendingPacketAfterLogin = false;
                }

            }
            else
            {
                Client.SendingPacketAfterLogin = false;
            }
        }
    }
}
