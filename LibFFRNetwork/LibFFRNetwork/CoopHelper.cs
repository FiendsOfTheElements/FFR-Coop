using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

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
        private string DLL_VERSION = "0.11";
        private string SCRIPT_VERSION = "";

        private string STATE_UNINITIALIZED = "Uninitialized";
        private string STATE_IDLE = "Idle";
        private string STATE_RECEIVING = "Receiving";
        private string STATE_HAS_DATA = "HasData";
        private string STATE_HAS_MESSAGE = "HasMessage";
        private string STATE_ERROR = "Error";

        private string team = "50";
        private string playername = "";
        private string state;
        private string server;
        private string Result { get; set; }
        
        private bool initialized = false;
        private List<bool> resultList;
        private List<string> itemPlayerMap;
        private List<string> messages;
        private FFRNetworkUI ui;
        Task uitask;
        System.Net.Http.HttpClient http;

        private string logFile = "";
        private string prevLogMsg = "";
        private int prevLogRepetitions = 0;


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
            InitLogging();

            Result = "";
            server = "localhost";
            state = STATE_UNINITIALIZED;
            itemPlayerMap = new List<string>();
            messages = new List<string>();
            http = new System.Net.Http.HttpClient();
            http.Timeout = new TimeSpan(0, 0, 5);

            uitask = new Task(() => startUI());
            Log("Starting UI task");
            uitask.Start();
        }

        private void InitLogging()
        {
            string logpath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "coop_logs");
            System.IO.Directory.CreateDirectory(logpath);
            logFile = Path.Combine(logpath, $"{DateTime.UtcNow.ToString("yyyyMMdd_HHmmss")}.log");
            File.AppendAllText(logFile, "Begin co-op session log (all times in UTC)");
        }
        private void Log(string message,
                        [CallerMemberName] string caller = null)
        {
            //string s = $"\n{DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss")} [state: {state}][{caller}()] {message}";
            string s = $"[state: {state}][{caller}()] {message}";
            if (prevLogMsg == s)
            {
                prevLogRepetitions++;
            }
            else
            {
                lock(logFile)
                {
                    if (prevLogRepetitions > 0)
                    {
                        File.AppendAllText(logFile, $" (Message repeated {prevLogRepetitions}x)");
                    }
                    File.AppendAllText(logFile, $"\n{DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss")} {s}");
                    prevLogMsg = s;
                    prevLogRepetitions = 0;
                } 
            }
        }

        public void ReportScriptVersion(string scriptVersion)
        {
            SCRIPT_VERSION = scriptVersion;
            var versionTask = new Task(() => asyncVersionCheck());
            Log($"Got script version: {scriptVersion}");
            versionTask.Start();
        }
        private async void asyncVersionCheck()
        {
            try
            {
                string versionURI = $"http://{server}/version";
                Log($"Accessing URI: {versionURI}");
                string res = await http.GetStringAsync(versionURI);
                Log($"Got response: {res}");
                var splitres = res.Split(new char[] { ';' });
                bool dllUpToDate = (splitres[0] == DLL_VERSION);
                bool scriptUpToDate = (splitres[1] == SCRIPT_VERSION);
                if (dllUpToDate && scriptUpToDate)
                {
                    Log("Co-op DLL and script are up to date.");
                    ui.setStatusLine("Co-op DLL and script are up to date.");
                    ui.status = 3;
                }
                else if (!dllUpToDate)
                {
                    Log("Co-op DLL is out of date.");
                    ui.setStatusLine("Co-op DLL is out of date.");
                    ui.status = 4;
                    ui.showDownloadLink();
                }
                else if (!scriptUpToDate)
                {
                    Log("Co-op Lua script is out of date.");
                    ui.setStatusLine("Co-op Lua script is out of date.");
                    ui.status = 4;
                    ui.showDownloadLink();
                }
            }
            catch (System.Net.Http.HttpRequestException e)
            {
                Log($"Exception: {e.Message}\n{e.StackTrace}");
                Log("Error checking for updates.  Co-op server may be down.");
                ui.setStatusLine("Error checking for updates.  Co-op server may be down.");
                ui.status = 4;
            }

        }
        private void startUI()
        {
            Log("Creating UI");
            ui = new FFRNetworkUI();
            Log("Associating UI events");
            ui.initEvent += Ui_initEvent;
            ui.joinEvent += Ui_joinEvent;
            Log("Showing UI");
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
            Log("Join Team event triggered");
            playername = ui.getPlayername();
            Log($"Player name: {playername}");
            if (playername.Length > 14)
            {
                playername = playername.Substring(0, 13);
                Log($"Truncated long player name to {playername}");
            }
            string teamstring = ui.getTeamText();
            Log($"Using team number {teamstring}");
            string joinURI = $"http://{server}/join?team={teamstring}&player={playername}";
            Log($"Accessing URI: {joinURI}");
            string res = await http.GetStringAsync(joinURI);
            if (res.Contains("Error"))
            {
                ui.joinFailed();
                ui.setStatusLine(res);
                state = STATE_UNINITIALIZED;
            }
            else
            {
                team = teamstring;
                state = STATE_IDLE;
                initialized = true;
                Log($"Successfully joined team {team}.");
                ui.setStatusLine($"Successfully joined team {team}.");
                ui.setInitText(team);
            }
        }
        private async void Ui_initEvent()
        {
            Log("Initialize Team event triggered");
            playername = ui.getPlayername();
            string limit = ui.getPlayerLimit().ToString();
            Log($"Using player name: {playername}");
            string initURI = $"http://{server}/init?player={playername}&limit={limit}";
            Log($"Accessing URI: {initURI}");
            string teamstring = await http.GetStringAsync(initURI);
            Log($"Got team number {teamstring}");
            team = teamstring;
            ui.setInitText(team);
            state = STATE_IDLE;
            initialized = true;
            Log($"Successfully created team {team}.");
            ui.setStatusLine($"Successfully created team {team}.");
        }


        public int HasResult()
        {
            Log($"HasResult called from lua.  Length {Result.Length}");
            return Result.Length;
        }
        public string GetResult()
        {
            Log($"GetResult called from lua.  Initialized: {initialized}");
            if (!initialized) { return ""; }
            Log($"Has result: {Result}");
            string ret = Result;
            Log($"Resetting state and handing result to lua");
            Result = "";
            state = STATE_IDLE;
            return ret;
        }

        public void SetServer(string server)
        {
            Log($"Setting server to {server}");
            this.server = server;
        }
        public void SendData(string data)
        {
            Log($"SendData called to send: {data}");
            if (!initialized)
            {
                Log("Not initialized. Returning.");
                return;
            }
            if (state == STATE_RECEIVING || state == STATE_HAS_DATA)
            {
                Log($"State is currently '{state}'. Returning");
                return;
            }

            Log("Setting state to RECEIVING");
            if (!(state == STATE_ERROR)) state = STATE_RECEIVING;
            Log("Transfering to background send task");
            Task.Run(() => backgroundSend(data));
        }
        private async void backgroundSend(string data)
        {
            try
            {
                string sendURI = $"http://{server}/coop?team={team}&player={playername}&data={data}";
                Log($"Accessing URI: {sendURI}");
                string res = await http.GetStringAsync(sendURI);
                Log($"Got response: {res}");

                CoopResponse resObj = JsonConvert.DeserializeObject<CoopResponse>(res);
                itemPlayerMap = resObj.playeritems;
                Result = resObj.data;

                //var splitres = res.Split(new char[] { '{' });
                //Result = splitres[0];
                //itemPlayerMap = splitres.Skip(1).ToList();
                resultList = new List<bool>();
                for (int i = 0; i < Result.Length; i++)
                {
                    if (Result[i] == '0')
                    {
                        resultList.Add(false);
                    }
                    if (Result[i] == '1')
                    {
                        resultList.Add(true);
                    }
                }
                foreach (string message in resObj.messages)
                {
                    messages.Add(message);
                }
                Log("Setting state to HAS_DATA");
                state = STATE_HAS_DATA;
            }
            catch (Exception e)
            {
                Log($"Exception: {e.Message}\n{e.StackTrace}");
                Log("Setting state to ERROR");
                state = STATE_ERROR;
            }

            
            
            //System.Threading.Thread.Sleep(5000);
            
            
        }
        public string GetState()
        {
            Log($"Lua requested state.");
            return state;
        }
        public string GetUsernameForItem(string item)
        {
            if (!KeyItemsOrder.Contains(item))
            {
                Log($"Lua requested who obtained {item}. {item} not in item-user list");
                return "not in item list";
            }
            int i = KeyItemsOrder.IndexOf(item);
            Log($"Lua requested who obtained {item}.  {itemPlayerMap[i]} obtained {item}");
            return itemPlayerMap[i];
        }

        public void SendDataString(string s)
        {
            if (!initialized)
            {
                Log("Not initialized.  Returning.");
                return;
            }
            if (!(state == STATE_ERROR))
            {
                Log("Setting state to RECEIVING");
                state = STATE_RECEIVING;
            }
            else
            {
                Log("THIS SHOULD NOT BE HAPPENING");
            }

            Log("Starting send data string task");
            Task.Run(() => SendDataStringWorker(s));

        }
        private void SendDataStringWorker(string s)
        {
            Log($"Calling backgroundSend with data: {s}");
            backgroundSend(s);
        }
        public string GetResultString()
        {
            Log("Lua requested result string");
            if (!initialized)
            {
                Log("Not initialized.  Returning.");
                return null;
            }
            Log("Resetting state and returning result table.");
            string outResult = Result;
            Result = "";
            if (messages.Count > 0)
            {
                state = STATE_HAS_MESSAGE;
            }
            else
            {
                state = STATE_IDLE;
            }

            return outResult;

        }
        public string GetMessage()
        {
            string retmsg = messages.First();
            messages.Remove(retmsg);
            if (messages.Count > 0)
            {
                state = STATE_HAS_MESSAGE;
            }
            else
            {
                state = STATE_IDLE;
            }
            return retmsg;
        }
        public void ReportRomName(string romname)
        {
            Log($"Rom name: {romname}");
        }
    }
}
 