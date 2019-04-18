using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LibFFRNetwork
{
    public class JTMessage
    {
        public string mtext { get; set; }
        public string mtype { get; set; }
    }
    public class JTSample
    {
        public List<string> players { get; set; }
        public List<JTMessage> messages { get; set; }
        public string data { get; set; }
        public string team { get; set; }
    }
    //JTSample jtsObject = JsonConvert.DeserializeObject<JTSample>(sample);
    public class CoopHelper
    {
        private string DLL_VERSION = "0.07b";
        private string SCRIPT_VERSION = "";

        private string STATE_UNINITIALIZED = "Uninitialized";
        private string STATE_IDLE = "Idle";
        private string STATE_RECEIVING = "Receiving";
        private string STATE_HAS_DATA = "HasData";
        private string STATE_ERROR = "Error";

        private string team = "50";
        private string playername = "";
        private string state;
        private string server;
        private string Result { get; set; }
        
        private bool initialized = false;
        private List<bool> resultList;
        private List<string> itemPlayerMap;
        private FFRNetworkUI ui;
        Task uitask;
        System.Net.Http.HttpClient http;

        public static List<string> KeyItemsOrder = new List<string>()
{
            "Lute", "Crown", "Crystal", "Herb", "Key", "TNT",
            "Adamant", "Slab", "Ruby", "Rod", "Floater", "Chime",
            "Tail", "Cube", "Bottle", "Oxyale", "Ship", "Canoe",
            "Airship", "Bridge", "Canal", "SlabTranslation",
            "EarthOrb", "FireOrb", "WaterOrb", "AirOrb", "EndGame"
};

        public CoopHelper()
        {
            //if (DateTime.Now.DayOfYear > 205)
            //{
            //    state = STATE_ERROR;
            //    throw new Exception("This alpha version of FFR Coop has expired.");
            //}
            Result = "";
            server = "localhost";
            state = STATE_UNINITIALIZED;
            itemPlayerMap = new List<string>();
            http = new System.Net.Http.HttpClient();
            http.Timeout = new TimeSpan(0, 0, 30);

            uitask = new Task(() => startUI());
            uitask.Start();
        }
        public void ReportScriptVersion(string scriptVersion)
        {
            SCRIPT_VERSION = scriptVersion;
            var versionTask = new Task(() => asyncVersionCheck());
            versionTask.Start();
        }
        private async void asyncVersionCheck()
        {
            try
            {
                string res = await http.GetStringAsync($"http://{server}/version");
                var splitres = res.Split(new char[] { ';' });
                bool dllUpToDate = (splitres[0] == DLL_VERSION);
                bool scriptUpToDate = (splitres[1] == SCRIPT_VERSION);
                if (dllUpToDate && scriptUpToDate)
                {
                    ui.setStatusLine("Co-op DLL and script are up to date.");
                    ui.status = 3;
                }
                else if (!dllUpToDate)
                {
                    ui.setStatusLine("Co-op DLL is out of date.");
                    ui.status = 4;
                }
                else if (!scriptUpToDate)
                {
                    ui.setStatusLine("Co-op Lua script is out of date.");
                    ui.status = 4;
                }
            }
            catch (System.Net.Http.HttpRequestException e)
            {
                ui.setStatusLine("Error checking for updates.  Co-op server may be down.");
                ui.status = 4;
            }

        }
        private void startUI()
        {
            ui = new FFRNetworkUI();
            ui.initEvent += Ui_initEvent;
            ui.joinEvent += Ui_joinEvent;
            ui.ShowDialog();
        }
        public string testTable(Object o)
        {
            string ret = "";
            ret = ret + "toString: " + o.ToString() + "\n";
            ret = ret + "type: " + o.GetType().Name + "\n";
            ret = ret + "value type? - " + o.GetType().IsValueType.ToString();
            return ret;
        }

        public string testLuaTable(NLua.LuaTable t)
        {
            string ret = "";
            foreach (var key in t.Keys)
            {
                ret += $"Key: {key.ToString()}, Value: {t[key].ToString()}\n";
            }

            return ret;
        }

        private async void Ui_joinEvent()
        {
            playername = ui.getPlayername();
            if (playername.Length > 14)
            {
                playername = playername.Substring(0, 13);
            }
            string teamstring = ui.getTeamText();
            string res = await http.GetStringAsync($"http://{server}/join?team={teamstring}&player={playername}");
            if (false)
            {
                state = STATE_ERROR;
            }
            else
            {
                team = teamstring;
                state = STATE_IDLE;
                initialized = true;
            }
        }
        private async void Ui_initEvent()
        {
            playername = ui.getPlayername();
            string teamstring = await http.GetStringAsync($"http://{server}/init?player={playername}");
            team = teamstring;
            ui.setInitText(team);
            state = STATE_IDLE;
            initialized = true;
        }


        public int HasResult()
        {
            return Result.Length;
        }
        public string GetResult()
        {
            if (!initialized) { return ""; }
            string ret = Result;
            Result = "";
            state = STATE_IDLE;
            return ret;
        }

        public void SetServer(string server)
        {
            this.server = server;
        }
        public void SendData(string data)
        {
            if (!initialized) { return; }
            if (state == STATE_RECEIVING || state == STATE_HAS_DATA) { return; }

            if (!(state == STATE_ERROR)) state = STATE_RECEIVING;
            Task.Run(() => backgroundSend(data));
        }
        private async void backgroundSend(string data)
        {
            try
            {
                string res = await http.GetStringAsync($"http://{server}/coop?team={team}&player={playername}&data={data}");
                var splitres = res.Split(new char[] { '{' });
                Result = splitres[0];
                itemPlayerMap = splitres.Skip(1).ToList();
                resultList = new List<bool>();
                for (int i = 0; i < res.Length; i++)
                {
                    if (res[i] == '0')
                    {
                        resultList.Add(false);
                    }
                    if (res[i] == '1')
                    {
                        resultList.Add(true);
                    }
                }

                state = STATE_HAS_DATA;
            }
            catch (Exception e)
            {
                state = STATE_ERROR;
            }

            
            
            //System.Threading.Thread.Sleep(5000);
            
            
        }
        public string GetState()
        {
            return state;
        }
        public string GetUsernameForItem(string item)
        {
            if (!KeyItemsOrder.Contains(item))
            {
                return "not in item list";
            }
            int i = KeyItemsOrder.IndexOf(item);
            return itemPlayerMap[i];
        }

        public void SendDataTable(NLua.LuaTable k)
        {
            Task.Run(() => SendDataTableWorker(k));
            
        }
        private void SendDataTableWorker(NLua.LuaTable k)
        {
            if (!initialized) { return; }
            if (!(state == STATE_ERROR)) state = STATE_RECEIVING;
            string outdata = "";
            for (int i = 0; i < KeyItemsOrder.Count; i++)
            {
                bool curitem = (bool)(k[KeyItemsOrder[i]]);
                if (curitem)
                {
                    outdata += "1";
                }
                else
                {
                    outdata += "0";
                }
            }
            backgroundSend(outdata);
        }
        public NLua.LuaTable GetResultTable(NLua.LuaTable k)
        {
            if (!initialized) { return null; }
            for (int i = 0; i < KeyItemsOrder.Count; i++)
            {
                k[KeyItemsOrder[i]] = resultList[i];
            }
            Result = "";
            state = STATE_IDLE;
            return k;
            
        }
    }
}
 