// Decompiled with JetBrains decompiler
// Type: SRPG.ChatUtility
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SRPG
{
  public class ChatUtility
  {
    private static readonly string CHAT_INSPECTION_MASTER_PATH = "Data/ChatWord";
    private static readonly string CHAT_CHANNEL_MASTER_PATH = "Data/Channel";

    public static bool SetupChatChannelMaster()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("ChatWindow Error:gm is NotInstance!");
        return false;
      }
      if (MonoSingleton<GameManager>.Instance.GetChatChannelMaster() != null && MonoSingleton<GameManager>.Instance.GetChatChannelMaster().Length > 0)
        return true;
      string src = AssetManager.LoadTextData(ChatUtility.CHAT_CHANNEL_MASTER_PATH);
      if (string.IsNullOrEmpty(src))
      {
        DebugUtility.LogError("ChatWindow Error:[" + ChatUtility.CHAT_CHANNEL_MASTER_PATH + "] is Not Found or Not Data!");
        return false;
      }
      bool flag = false;
      try
      {
        Json_ChatChannelMasterParam[] jsonArray = JSONParser.parseJSONArray<Json_ChatChannelMasterParam>(src);
        if (jsonArray != null)
        {
          if (MonoSingleton<GameManager>.Instance.Deserialize(GameUtility.Config_Language, new JSON_ChatChannelMaster() { channels = jsonArray }))
            flag = true;
        }
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.ToString());
        flag = false;
      }
      return flag;
    }

    public static List<ChatUtility.ChatInspectionMaster> LoadInspectionMaster(ref bool is_success)
    {
      List<ChatUtility.ChatInspectionMaster> inspectionMasterList = new List<ChatUtility.ChatInspectionMaster>();
      is_success = false;
      string src = AssetManager.LoadTextData(ChatUtility.CHAT_INSPECTION_MASTER_PATH);
      if (string.IsNullOrEmpty(src))
      {
        DebugUtility.LogError("ChatWindow Error:[" + ChatUtility.CHAT_INSPECTION_MASTER_PATH + "] is Not Found or Not Data!");
        return inspectionMasterList;
      }
      try
      {
        ChatUtility.JSON_ChatInspectionMaster[] jsonArray = JSONParser.parseJSONArray<ChatUtility.JSON_ChatInspectionMaster>(src);
        if (jsonArray == null)
          throw new InvalidJSONException();
        foreach (ChatUtility.JSON_ChatInspectionMaster json in jsonArray)
        {
          ChatUtility.ChatInspectionMaster inspectionMaster = new ChatUtility.ChatInspectionMaster();
          if (inspectionMaster.Deserialize(json))
            inspectionMasterList.Add(inspectionMaster);
        }
      }
      catch (Exception ex)
      {
        DebugUtility.LogWarning("ChatWindow/SetupInspectionMaster parse error! e=" + ex.ToString());
        return inspectionMasterList;
      }
      is_success = true;
      return inspectionMasterList;
    }

    public static string ReplaceNGWord(string _text, List<ChatUtility.ChatInspectionMaster> _inspection_datas, string _okikae)
    {
      string[] illegalWord = ChatUtility.GetIllegalWord(_text);
      if (illegalWord.Length > 0)
      {
        for (int index = 0; index < illegalWord.Length; ++index)
        {
          string oldValue = illegalWord[index];
          string newValue = new StringBuilder().Insert(0, _okikae, oldValue.Length).ToString();
          _text = _text.Replace(oldValue, newValue);
        }
      }
      if (_inspection_datas != null && _inspection_datas.Count > 0)
      {
        for (int index = 0; index < _inspection_datas.Count; ++index)
        {
          if (!string.IsNullOrEmpty(_inspection_datas[index].word) && _text.Contains(_inspection_datas[index].word))
          {
            string newValue = new StringBuilder().Insert(0, _okikae, _inspection_datas[index].word.Length).ToString();
            _text = _text.Replace(_inspection_datas[index].word, newValue);
          }
        }
      }
      return _text;
    }

    private static string[] GetIllegalWord(string _text)
    {
      List<string> stringList = new List<string>();
      MatchCollection matchCollection1 = new Regex("\\d{1,4}(-|ー|‐|－)\\d{1,4}(-|ー|‐|－)\\d{4}|\\d{10,11}").Matches(_text);
      if (matchCollection1 != null && matchCollection1.Count > 0)
      {
        foreach (Match match in matchCollection1)
          stringList.Add(match.Value);
      }
      MatchCollection matchCollection2 = new Regex("(.)+@(.)+(\\.|どっと|ドット|dot)(.*)").Matches(_text);
      if (matchCollection2 != null && matchCollection2.Count > 0)
      {
        foreach (Match match in matchCollection2)
          stringList.Add(match.Value);
      }
      return stringList.ToArray();
    }

    public static ChatUtility.Json_ChatTemplateData LoadChatTemplateMessage()
    {
      if (!PlayerPrefsUtility.HasKey(PlayerPrefsUtility.CHAT_TEMPLATE_MESSAGE))
        return ChatUtility.CreateNewTemplateMessagePrefsData();
      ChatUtility.Json_ChatTemplateData new_prefs_data = (ChatUtility.Json_ChatTemplateData) null;
      try
      {
        if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.CHAT_TEMPLATE_MESSAGE))
          new_prefs_data = (ChatUtility.Json_ChatTemplateData) JsonUtility.FromJson<ChatUtility.Json_ChatTemplateData>(PlayerPrefsUtility.GetString(PlayerPrefsUtility.CHAT_TEMPLATE_MESSAGE, string.Empty));
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
      }
      if (new_prefs_data == null)
        new_prefs_data = ChatUtility.CreateNewTemplateMessagePrefsData();
      List<string> templateMessageList = ChatUtility.GetDefaultTemplateMessageList();
      if (new_prefs_data.messages.Length < templateMessageList.Count)
      {
        List<string> stringList = new List<string>((IEnumerable<string>) new_prefs_data.messages);
        for (int length = new_prefs_data.messages.Length; length < templateMessageList.Count; ++length)
          stringList.Add(templateMessageList[length]);
        new_prefs_data.messages = stringList.ToArray();
        ChatUtility.SaveTemplateMessage(new_prefs_data);
      }
      return new_prefs_data;
    }

    public static void SaveTemplateMessage(ChatUtility.Json_ChatTemplateData new_prefs_data)
    {
      try
      {
        PlayerPrefsUtility.SetString(PlayerPrefsUtility.CHAT_TEMPLATE_MESSAGE, JsonUtility.ToJson((object) new_prefs_data), false);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) MonoSingleton<ChatWindow>.Instance, (UnityEngine.Object) null))
          return;
        MonoSingleton<ChatWindow>.Instance.LoadTemplateMessage();
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
      }
    }

    private static ChatUtility.Json_ChatTemplateData CreateNewTemplateMessagePrefsData()
    {
      ChatUtility.Json_ChatTemplateData new_prefs_data = new ChatUtility.Json_ChatTemplateData();
      List<string> templateMessageList = ChatUtility.GetDefaultTemplateMessageList();
      new_prefs_data.messages = templateMessageList.ToArray();
      ChatUtility.SaveTemplateMessage(new_prefs_data);
      return new_prefs_data;
    }

    private static List<string> GetDefaultTemplateMessageList()
    {
      List<string> stringList = new List<string>();
      string empty = string.Empty;
      bool success = false;
      int num = 0;
      while (true)
      {
        string str = LocalizedText.Get("sys.CHAT_DEFAULT_TEMPLATE_MESSAGE_" + (object) (num + 1), ref success);
        if (success)
        {
          stringList.Add(str);
          ++num;
        }
        else
          break;
      }
      return stringList;
    }

    public static bool IsMultiQuestNow()
    {
      return UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) && SceneBattle.Instance.Battle != null && (SceneBattle.Instance.Battle.IsMultiPlay && !SceneBattle.Instance.Battle.IsMultiTower) && !SceneBattle.Instance.Battle.IsMultiVersus;
    }

    public class Json_ChatInspectionMaster
    {
      public ChatUtility.JSON_ChatInspectionMaster[] chatinspections;
    }

    public class JSON_ChatInspectionMaster
    {
      public int pk;
      public ChatUtility.JSON_ChatInspectionMaster.Fields fields;

      public class Fields
      {
        public int id;
        public string ngword;
        public int reflection;
      }
    }

    public class ChatInspectionMaster
    {
      public bool reflection = true;
      public int id;
      public string word;

      public bool Deserialize(ChatUtility.JSON_ChatInspectionMaster json)
      {
        if (json == null || json.fields == null)
          return false;
        this.id = json.fields.id;
        this.word = json.fields.ngword;
        this.reflection = json.fields.reflection == 1;
        return true;
      }
    }

    public class Json_ChatTemplateData
    {
      public string[] messages;
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
        this.room_members = new List<ChatUtility.RoomMember>();
        this.entry_members = new List<ChatUtility.RoomMember>();
        this.leave_members = new List<ChatUtility.RoomMember>();
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

      public void Refresh(List<MyPhoton.MyPlayer> _new_players)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ChatUtility.RoomMemberManager.\u003CRefresh\u003Ec__AnonStorey317 refreshCAnonStorey317 = new ChatUtility.RoomMemberManager.\u003CRefresh\u003Ec__AnonStorey317();
        // ISSUE: reference to a compiler-generated field
        refreshCAnonStorey317._new_players = _new_players;
        // ISSUE: reference to a compiler-generated field
        refreshCAnonStorey317.\u003C\u003Ef__this = this;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ChatUtility.RoomMemberManager.\u003CRefresh\u003Ec__AnonStorey318 refreshCAnonStorey318 = new ChatUtility.RoomMemberManager.\u003CRefresh\u003Ec__AnonStorey318();
        // ISSUE: reference to a compiler-generated field
        refreshCAnonStorey318.\u003C\u003Ef__ref\u0024791 = refreshCAnonStorey317;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        for (refreshCAnonStorey318.i = 0; refreshCAnonStorey318.i < refreshCAnonStorey317._new_players.Count; ++refreshCAnonStorey318.i)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (refreshCAnonStorey317._new_players[refreshCAnonStorey318.i].photonPlayerID > -1)
          {
            // ISSUE: reference to a compiler-generated method
            this.tmp_entry_member = this.room_members.Find(new Predicate<ChatUtility.RoomMember>(refreshCAnonStorey318.\u003C\u003Em__332));
            if (this.tmp_entry_member == null)
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              JSON_MyPhotonPlayerParam photonPlayerParam = JSON_MyPhotonPlayerParam.Parse(refreshCAnonStorey317._new_players[refreshCAnonStorey318.i].json);
              ChatUtility.RoomMember roomMember = new ChatUtility.RoomMember();
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              roomMember.SetParam(refreshCAnonStorey317._new_players[refreshCAnonStorey318.i].photonPlayerID, refreshCAnonStorey317._new_players[refreshCAnonStorey318.i].playerID, photonPlayerParam.UID, photonPlayerParam.playerName);
              if (!this.entry_members.Contains(roomMember))
                this.entry_members.Add(roomMember);
            }
          }
        }
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        ChatUtility.RoomMemberManager.\u003CRefresh\u003Ec__AnonStorey319 refreshCAnonStorey319 = new ChatUtility.RoomMemberManager.\u003CRefresh\u003Ec__AnonStorey319();
        // ISSUE: reference to a compiler-generated field
        refreshCAnonStorey319.\u003C\u003Ef__this = this;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        for (refreshCAnonStorey319.i = 0; refreshCAnonStorey319.i < this.room_members.Count; ++refreshCAnonStorey319.i)
        {
          // ISSUE: reference to a compiler-generated field
          if (this.room_members[refreshCAnonStorey319.i].PhotonPlayerId > -1)
          {
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated method
            this.tmp_leave_member = refreshCAnonStorey317._new_players.Find(new Predicate<MyPhoton.MyPlayer>(refreshCAnonStorey319.\u003C\u003Em__333));
            // ISSUE: reference to a compiler-generated field
            if (this.tmp_leave_member == null && !this.leave_members.Contains(this.room_members[refreshCAnonStorey319.i]))
            {
              // ISSUE: reference to a compiler-generated field
              this.leave_members.Add(this.room_members[refreshCAnonStorey319.i]);
            }
          }
        }
        for (int index = 0; index < this.entry_members.Count; ++index)
        {
          if (!this.room_members.Contains(this.entry_members[index]))
            this.room_members.Add(this.entry_members[index]);
        }
        for (int index = 0; index < this.leave_members.Count; ++index)
        {
          if (this.room_members.Contains(this.leave_members[index]))
            this.room_members.Remove(this.leave_members[index]);
        }
      }

      public void Clear()
      {
        this.room_members.Clear();
      }
    }

    public class RoomMember
    {
      private int photon_player_id;
      private int player_id;
      private string uid;
      private string name;

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

      public void SetParam(int _photon_player_id, int _player_id, string _uid, string _name)
      {
        this.photon_player_id = _photon_player_id;
        this.player_id = _player_id;
        this.uid = _uid;
        this.name = _name;
      }
    }

    public class RoomInfo
    {
      private ChatWindow chat_window;
      private bool is_active;
      private QuestParam quest_param;

      public bool IsActive
      {
        get
        {
          return this.is_active;
        }
      }

      public QuestParam QuestParam
      {
        get
        {
          return this.quest_param;
        }
      }

      public void Init(ChatWindow _chat_window)
      {
        this.chat_window = _chat_window;
      }

      private void SetParam(string _iname)
      {
        this.quest_param = MonoSingleton<GameManager>.Instance.FindQuest(_iname);
      }

      public void Run()
      {
        this.Refresh();
      }

      private void Refresh()
      {
        if (!this.is_active && PunMonoSingleton<MyPhoton>.Instance.IsConnectedInRoom())
        {
          this.SetParam(JSON_MyPhotonRoomParam.Parse(PunMonoSingleton<MyPhoton>.Instance.GetCurrentRoom().json).iname);
          this.is_active = true;
          this.chat_window.ChangeChatTypeTab(ChatWindow.eChatType.Room);
        }
        else if (ChatUtility.IsMultiQuestNow())
          this.is_active = true;
        else if (this.is_active && !PunMonoSingleton<MyPhoton>.Instance.IsConnectedInRoom())
        {
          this.chat_window.ExitRoomSelf();
          this.is_active = false;
        }
        else
          this.is_active = PunMonoSingleton<MyPhoton>.Instance.IsConnectedInRoom();
      }

      public void ExitRoom()
      {
        this.is_active = false;
      }
    }
  }
}
