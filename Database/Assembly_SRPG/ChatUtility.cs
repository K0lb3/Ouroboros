namespace SRPG
{
    using GR;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using UnityEngine;

    public class ChatUtility
    {
        private static readonly string CHAT_INSPECTION_MASTER_PATH;
        private static readonly string CHAT_CHANNEL_MASTER_PATH;

        static ChatUtility()
        {
            CHAT_INSPECTION_MASTER_PATH = "Data/ChatWord";
            CHAT_CHANNEL_MASTER_PATH = "Data/Channel";
            return;
        }

        public ChatUtility()
        {
            base..ctor();
            return;
        }

        private static Json_ChatTemplateData CreateNewTemplateMessagePrefsData()
        {
            Json_ChatTemplateData data;
            List<string> list;
            data = new Json_ChatTemplateData();
            list = GetDefaultTemplateMessageList();
            data.messages = list.ToArray();
            SaveTemplateMessage(data);
            return data;
        }

        private static unsafe List<string> GetDefaultTemplateMessageList()
        {
            List<string> list;
            string str;
            bool flag;
            int num;
            list = new List<string>();
            str = string.Empty;
            flag = 0;
            num = 0;
            goto Label_0045;
        Label_0015:
            str = LocalizedText.Get("sys.CHAT_DEFAULT_TEMPLATE_MESSAGE_" + ((int) (num + 1)), &flag);
            if (flag != null)
            {
                goto Label_003A;
            }
            goto Label_004A;
        Label_003A:
            list.Add(str);
            num += 1;
        Label_0045:
            goto Label_0015;
        Label_004A:
            return list;
        }

        private static string[] GetIllegalWord(string _text)
        {
            List<string> list;
            Regex regex;
            MatchCollection matchs;
            Match match;
            IEnumerator enumerator;
            Regex regex2;
            MatchCollection matchs2;
            Match match2;
            IEnumerator enumerator2;
            IDisposable disposable;
            IDisposable disposable2;
            list = new List<string>();
            regex = new Regex(@"\d{1,4}(-|ー|‐|－)\d{1,4}(-|ー|‐|－)\d{4}|\d{10,11}");
            matchs = regex.Matches(_text);
            if (matchs == null)
            {
                goto Label_0078;
            }
            if (matchs.Count <= 0)
            {
                goto Label_0078;
            }
            enumerator = matchs.GetEnumerator();
        Label_0033:
            try
            {
                goto Label_0051;
            Label_0038:
                match = (Match) enumerator.Current;
                list.Add(match.Value);
            Label_0051:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0038;
                }
                goto Label_0078;
            }
            finally
            {
            Label_0062:
                disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    goto Label_0070;
                }
            Label_0070:
                disposable.Dispose();
            }
        Label_0078:
            regex2 = new Regex(@"(.)+@(.)+(\.|どっと|ドット|dot)(.*)");
            matchs2 = regex2.Matches(_text);
            if (matchs2 == null)
            {
                goto Label_00F2;
            }
            if (matchs2.Count <= 0)
            {
                goto Label_00F2;
            }
            enumerator2 = matchs2.GetEnumerator();
        Label_00AB:
            try
            {
                goto Label_00CB;
            Label_00B0:
                match2 = (Match) enumerator2.Current;
                list.Add(match2.Value);
            Label_00CB:
                if (enumerator2.MoveNext() != null)
                {
                    goto Label_00B0;
                }
                goto Label_00F2;
            }
            finally
            {
            Label_00DC:
                disposable2 = enumerator2 as IDisposable;
                if (disposable2 != null)
                {
                    goto Label_00EA;
                }
            Label_00EA:
                disposable2.Dispose();
            }
        Label_00F2:
            return list.ToArray();
        }

        public static bool IsMultiQuestNow()
        {
            if ((SceneBattle.Instance != null) == null)
            {
                goto Label_0049;
            }
            if (SceneBattle.Instance.Battle == null)
            {
                goto Label_0049;
            }
            if (SceneBattle.Instance.Battle.IsMultiPlay == null)
            {
                goto Label_0049;
            }
            if (SceneBattle.Instance.Battle.IsMultiVersus != null)
            {
                goto Label_0049;
            }
            return 1;
        Label_0049:
            return 0;
        }

        public static Json_ChatTemplateData LoadChatTemplateMessage()
        {
            Json_ChatTemplateData data;
            string str;
            Exception exception;
            List<string> list;
            List<string> list2;
            int num;
            if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.CHAT_TEMPLATE_MESSAGE) != null)
            {
                goto Label_0015;
            }
            return CreateNewTemplateMessagePrefsData();
        Label_0015:
            data = null;
        Label_0017:
            try
            {
                if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.CHAT_TEMPLATE_MESSAGE) == null)
                {
                    goto Label_003D;
                }
                data = JsonUtility.FromJson<Json_ChatTemplateData>(PlayerPrefsUtility.GetString(PlayerPrefsUtility.CHAT_TEMPLATE_MESSAGE, string.Empty));
            Label_003D:
                goto Label_004E;
            }
            catch (Exception exception1)
            {
            Label_0042:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_004E;
            }
        Label_004E:
            if (data != null)
            {
                goto Label_005A;
            }
            data = CreateNewTemplateMessagePrefsData();
        Label_005A:
            list = GetDefaultTemplateMessageList();
            if (((int) data.messages.Length) >= list.Count)
            {
                goto Label_00C4;
            }
            list2 = new List<string>(data.messages);
            num = (int) data.messages.Length;
            goto Label_00A4;
        Label_008F:
            list2.Add(list[num]);
            num += 1;
        Label_00A4:
            if (num < list.Count)
            {
                goto Label_008F;
            }
            data.messages = list2.ToArray();
            SaveTemplateMessage(data);
        Label_00C4:
            return data;
        }

        public static unsafe List<ChatInspectionMaster> LoadInspectionMaster(ref bool is_success)
        {
            List<ChatInspectionMaster> list;
            string str;
            JSON_ChatInspectionMaster[] masterArray;
            JSON_ChatInspectionMaster master;
            JSON_ChatInspectionMaster[] masterArray2;
            int num;
            ChatInspectionMaster master2;
            Exception exception;
            List<ChatInspectionMaster> list2;
            list = new List<ChatInspectionMaster>();
            *((sbyte*) is_success) = 0;
            str = AssetManager.LoadTextData(CHAT_INSPECTION_MASTER_PATH);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_003A;
            }
            DebugUtility.LogError("ChatWindow Error:[" + CHAT_INSPECTION_MASTER_PATH + "] is Not Found or Not Data!");
            return list;
        Label_003A:
            try
            {
                masterArray = JSONParser.parseJSONArray<JSON_ChatInspectionMaster>(str);
                if (masterArray != null)
                {
                    goto Label_004D;
                }
                throw new InvalidJSONException();
            Label_004D:
                masterArray2 = masterArray;
                num = 0;
                goto Label_0080;
            Label_0058:
                master = masterArray2[num];
                master2 = new ChatInspectionMaster();
                if (master2.Deserialize(master) == null)
                {
                    goto Label_007A;
                }
                list.Add(master2);
            Label_007A:
                num += 1;
            Label_0080:
                if (num < ((int) masterArray2.Length))
                {
                    goto Label_0058;
                }
                goto Label_00B5;
            }
            catch (Exception exception1)
            {
            Label_0090:
                exception = exception1;
                DebugUtility.LogWarning("ChatWindow/SetupInspectionMaster parse error! e=" + exception.ToString());
                list2 = list;
                goto Label_00BA;
            }
        Label_00B5:
            *((sbyte*) is_success) = 1;
            return list;
        Label_00BA:
            return list2;
        }

        public static string ReplaceNGWord(string _text, List<ChatInspectionMaster> _inspection_datas, string _okikae)
        {
            string[] strArray;
            int num;
            string str;
            string str2;
            int num2;
            string str3;
            strArray = GetIllegalWord(_text);
            if (((int) strArray.Length) <= 0)
            {
                goto Label_004A;
            }
            num = 0;
            goto Label_0041;
        Label_0017:
            str = strArray[num];
            str2 = new StringBuilder().Insert(0, _okikae, str.Length).ToString();
            _text = _text.Replace(str, str2);
            num += 1;
        Label_0041:
            if (num < ((int) strArray.Length))
            {
                goto Label_0017;
            }
        Label_004A:
            if (_inspection_datas == null)
            {
                goto Label_00E2;
            }
            if (_inspection_datas.Count <= 0)
            {
                goto Label_00E2;
            }
            num2 = 0;
            goto Label_00D5;
        Label_0064:
            if (string.IsNullOrEmpty(_inspection_datas[num2].word) != null)
            {
                goto Label_00CF;
            }
            if (_text.Contains(_inspection_datas[num2].word) == null)
            {
                goto Label_00CF;
            }
            str3 = new StringBuilder().Insert(0, _okikae, _inspection_datas[num2].word.Length).ToString();
            _text = _text.Replace(_inspection_datas[num2].word, str3);
        Label_00CF:
            num2 += 1;
        Label_00D5:
            if (num2 < _inspection_datas.Count)
            {
                goto Label_0064;
            }
        Label_00E2:
            return _text;
        }

        public static void SaveTemplateMessage(Json_ChatTemplateData new_prefs_data)
        {
            Exception exception;
        Label_0000:
            try
            {
                PlayerPrefsUtility.SetString(PlayerPrefsUtility.CHAT_TEMPLATE_MESSAGE, JsonUtility.ToJson(new_prefs_data), 0);
                if ((MonoSingleton<ChatWindow>.Instance != null) == null)
                {
                    goto Label_002C;
                }
                MonoSingleton<ChatWindow>.Instance.LoadTemplateMessage();
            Label_002C:
                goto Label_003D;
            }
            catch (Exception exception1)
            {
            Label_0031:
                exception = exception1;
                DebugUtility.LogException(exception);
                goto Label_003D;
            }
        Label_003D:
            return;
        }

        public static bool SetupChatChannelMaster()
        {
            string str;
            bool flag;
            Json_ChatChannelMasterParam[] paramArray;
            JSON_ChatChannelMaster master;
            Exception exception;
            if ((MonoSingleton<GameManager>.Instance == null) == null)
            {
                goto Label_001C;
            }
            DebugUtility.LogError("ChatWindow Error:gm is NotInstance!");
            return 0;
        Label_001C:
            if (MonoSingleton<GameManager>.Instance.GetChatChannelMaster() == null)
            {
                goto Label_003F;
            }
            if (((int) MonoSingleton<GameManager>.Instance.GetChatChannelMaster().Length) <= 0)
            {
                goto Label_003F;
            }
            return 1;
        Label_003F:
            str = AssetManager.LoadTextData(CHAT_CHANNEL_MASTER_PATH);
            if (string.IsNullOrEmpty(str) == null)
            {
                goto Label_0070;
            }
            DebugUtility.LogError("ChatWindow Error:[" + CHAT_CHANNEL_MASTER_PATH + "] is Not Found or Not Data!");
            return 0;
        Label_0070:
            flag = 0;
        Label_0072:
            try
            {
                paramArray = JSONParser.parseJSONArray<Json_ChatChannelMasterParam>(str);
                if (paramArray == null)
                {
                    goto Label_009E;
                }
                master = new JSON_ChatChannelMaster();
                master.channels = paramArray;
                if (MonoSingleton<GameManager>.Instance.Deserialize(master) == null)
                {
                    goto Label_009E;
                }
                flag = 1;
            Label_009E:
                goto Label_00B8;
            }
            catch (Exception exception1)
            {
            Label_00A3:
                exception = exception1;
                DebugUtility.LogError(exception.ToString());
                flag = 0;
                goto Label_00B8;
            }
        Label_00B8:
            return flag;
        }

        public class ChatInspectionMaster
        {
            public int id;
            public string word;
            public bool reflection;

            public ChatInspectionMaster()
            {
                this.reflection = 1;
                base..ctor();
                return;
            }

            public bool Deserialize(ChatUtility.JSON_ChatInspectionMaster json)
            {
                if ((json != null) && (json.fields != null))
                {
                    goto Label_0013;
                }
                return 0;
            Label_0013:
                this.id = json.fields.id;
                this.word = json.fields.ngword;
                this.reflection = (json.fields.reflection != 1) ? 0 : 1;
                return 1;
            }
        }

        public class Json_ChatInspectionMaster
        {
            public ChatUtility.JSON_ChatInspectionMaster[] chatinspections;

            public Json_ChatInspectionMaster()
            {
                base..ctor();
                return;
            }
        }

        public class JSON_ChatInspectionMaster
        {
            public int pk;
            public Fields fields;

            public JSON_ChatInspectionMaster()
            {
                base..ctor();
                return;
            }

            public class Fields
            {
                public int id;
                public string ngword;
                public int reflection;

                public Fields()
                {
                    base..ctor();
                    return;
                }
            }
        }

        public class Json_ChatTemplateData
        {
            public string[] messages;

            public Json_ChatTemplateData()
            {
                base..ctor();
                return;
            }
        }

        public class RoomInfo
        {
            private ChatWindow chat_window;
            private bool is_active;
            private SRPG.QuestParam quest_param;

            public RoomInfo()
            {
                base..ctor();
                return;
            }

            public void ExitRoom()
            {
                this.is_active = 0;
                return;
            }

            public void Init(ChatWindow _chat_window)
            {
                this.chat_window = _chat_window;
                return;
            }

            private void Refresh()
            {
                MyPhoton.MyRoom room;
                JSON_MyPhotonRoomParam param;
                if (this.is_active != null)
                {
                    goto Label_0051;
                }
                if (PunMonoSingleton<MyPhoton>.Instance.IsConnectedInRoom() == null)
                {
                    goto Label_0051;
                }
                param = JSON_MyPhotonRoomParam.Parse(PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom().json);
                this.SetParam(param.iname);
                this.is_active = 1;
                this.chat_window.ChangeChatTypeTab(2);
                return;
            Label_0051:
                if (ChatUtility.IsMultiQuestNow() == null)
                {
                    goto Label_0063;
                }
                this.is_active = 1;
                return;
            Label_0063:
                if (this.is_active == null)
                {
                    goto Label_0090;
                }
                if (PunMonoSingleton<MyPhoton>.Instance.IsConnectedInRoom() != null)
                {
                    goto Label_0090;
                }
                this.chat_window.ExitRoomSelf();
                this.is_active = 0;
                return;
            Label_0090:
                this.is_active = PunMonoSingleton<MyPhoton>.Instance.IsConnectedInRoom();
                return;
            }

            public void Run()
            {
                this.Refresh();
                return;
            }

            private void SetParam(string _iname)
            {
                this.quest_param = MonoSingleton<GameManager>.Instance.FindQuest(_iname);
                return;
            }

            public bool IsActive
            {
                get
                {
                    return this.is_active;
                }
            }

            public SRPG.QuestParam QuestParam
            {
                get
                {
                    return this.quest_param;
                }
            }
        }

        public class RoomMember
        {
            private int photon_player_id;
            private int player_id;
            private string uid;
            private string name;

            public RoomMember()
            {
                base..ctor();
                return;
            }

            public void SetParam(int _photon_player_id, int _player_id, string _uid, string _name)
            {
                this.photon_player_id = _photon_player_id;
                this.player_id = _player_id;
                this.uid = _uid;
                this.name = _name;
                return;
            }

            public int PhotonPlayerId
            {
                get
                {
                    return this.photon_player_id;
                }
            }

            public int PlayerId
            {
                get
                {
                    return this.player_id;
                }
            }

            public string UID
            {
                get
                {
                    return this.uid;
                }
            }

            public string Name
            {
                get
                {
                    return this.name;
                }
            }
        }

        public class RoomMemberManager
        {
            private ChatUtility.RoomMember tmp_entry_member;
            private MyPhoton.MyPlayer tmp_leave_member;
            private List<ChatUtility.RoomMember> room_members;
            private List<ChatUtility.RoomMember> entry_members;
            private List<ChatUtility.RoomMember> leave_members;

            public RoomMemberManager()
            {
                base..ctor();
                this.room_members = new List<ChatUtility.RoomMember>();
                this.entry_members = new List<ChatUtility.RoomMember>();
                this.leave_members = new List<ChatUtility.RoomMember>();
                return;
            }

            public void Clear()
            {
                this.room_members.Clear();
                return;
            }

            public void Refresh(List<MyPhoton.MyPlayer> _new_players)
            {
                JSON_MyPhotonPlayerParam param;
                ChatUtility.RoomMember member;
                int num;
                int num2;
                <Refresh>c__AnonStorey316 storey;
                <Refresh>c__AnonStorey317 storey2;
                <Refresh>c__AnonStorey318 storey3;
                storey = new <Refresh>c__AnonStorey316();
                storey._new_players = _new_players;
                storey.<>f__this = this;
                storey2 = new <Refresh>c__AnonStorey317();
                storey2.<>f__ref$790 = storey;
                storey2.i = 0;
                goto Label_0113;
            Label_0034:
                if (storey._new_players[storey2.i].photonPlayerID > -1)
                {
                    goto Label_0057;
                }
                goto Label_0103;
            Label_0057:
                this.tmp_entry_member = this.room_members.Find(new Predicate<ChatUtility.RoomMember>(storey2.<>m__2BC));
                if (this.tmp_entry_member != null)
                {
                    goto Label_0103;
                }
                param = JSON_MyPhotonPlayerParam.Parse(storey._new_players[storey2.i].json);
                member = new ChatUtility.RoomMember();
                member.SetParam(storey._new_players[storey2.i].photonPlayerID, storey._new_players[storey2.i].playerID, param.UID, param.playerName);
                if (this.entry_members.Contains(member) != null)
                {
                    goto Label_0103;
                }
                this.entry_members.Add(member);
            Label_0103:
                storey2.i += 1;
            Label_0113:
                if (storey2.i < storey._new_players.Count)
                {
                    goto Label_0034;
                }
                storey3 = new <Refresh>c__AnonStorey318();
                storey3.<>f__this = this;
                storey3.i = 0;
                goto Label_01E2;
            Label_0147:
                if (this.room_members[storey3.i].PhotonPlayerId > -1)
                {
                    goto Label_0169;
                }
                goto Label_01D2;
            Label_0169:
                this.tmp_leave_member = storey._new_players.Find(new Predicate<MyPhoton.MyPlayer>(storey3.<>m__2BD));
                if (this.tmp_leave_member != null)
                {
                    goto Label_01D2;
                }
                if (this.leave_members.Contains(this.room_members[storey3.i]) != null)
                {
                    goto Label_01D2;
                }
                this.leave_members.Add(this.room_members[storey3.i]);
            Label_01D2:
                storey3.i += 1;
            Label_01E2:
                if (storey3.i < this.room_members.Count)
                {
                    goto Label_0147;
                }
                num = 0;
                goto Label_023C;
            Label_0200:
                if (this.room_members.Contains(this.entry_members[num]) == null)
                {
                    goto Label_0221;
                }
                goto Label_0238;
            Label_0221:
                this.room_members.Add(this.entry_members[num]);
            Label_0238:
                num += 1;
            Label_023C:
                if (num < this.entry_members.Count)
                {
                    goto Label_0200;
                }
                num2 = 0;
                goto Label_0291;
            Label_0254:
                if (this.room_members.Contains(this.leave_members[num2]) != null)
                {
                    goto Label_0275;
                }
                goto Label_028D;
            Label_0275:
                this.room_members.Remove(this.leave_members[num2]);
            Label_028D:
                num2 += 1;
            Label_0291:
                if (num2 < this.leave_members.Count)
                {
                    goto Label_0254;
                }
                return;
            }

            public List<ChatUtility.RoomMember> RoomMembers
            {
                get
                {
                    return this.room_members;
                }
            }

            public List<ChatUtility.RoomMember> EntryMembers
            {
                get
                {
                    return this.entry_members;
                }
            }

            public List<ChatUtility.RoomMember> LeaveMembers
            {
                get
                {
                    return this.leave_members;
                }
            }

            [CompilerGenerated]
            private sealed class <Refresh>c__AnonStorey316
            {
                internal List<MyPhoton.MyPlayer> _new_players;
                internal ChatUtility.RoomMemberManager <>f__this;

                public <Refresh>c__AnonStorey316()
                {
                    base..ctor();
                    return;
                }
            }

            [CompilerGenerated]
            private sealed class <Refresh>c__AnonStorey317
            {
                internal int i;
                internal ChatUtility.RoomMemberManager.<Refresh>c__AnonStorey316 <>f__ref$790;

                public <Refresh>c__AnonStorey317()
                {
                    base..ctor();
                    return;
                }

                internal bool <>m__2BC(ChatUtility.RoomMember a)
                {
                    return (a.PlayerId == this.<>f__ref$790._new_players[this.i].playerID);
                }
            }

            [CompilerGenerated]
            private sealed class <Refresh>c__AnonStorey318
            {
                internal int i;
                internal ChatUtility.RoomMemberManager <>f__this;

                public <Refresh>c__AnonStorey318()
                {
                    base..ctor();
                    return;
                }

                internal bool <>m__2BD(MyPhoton.MyPlayer a)
                {
                    return (a.playerID == this.<>f__this.room_members[this.i].PlayerId);
                }
            }
        }
    }
}

