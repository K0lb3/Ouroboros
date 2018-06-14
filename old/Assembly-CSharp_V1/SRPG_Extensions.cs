// Decompiled with JetBrains decompiler
// Type: SRPG_Extensions
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public static class SRPG_Extensions
{
  private static readonly string[] mPrefixes = new string[3]{ "U_", "M_", "F_" };

  public static string GenerateLocalizedID(this System.Type type, string id, string name)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append((object) type);
    stringBuilder.Replace(".", "_");
    stringBuilder.Append("_");
    stringBuilder.Append(id);
    stringBuilder.Append("_");
    stringBuilder.Append(name);
    return stringBuilder.ToString();
  }

  public static void WriteLocalizedEntry(this StreamWriter writer, string localizedID, string originString)
  {
    writer.WriteLine("{0}\t{1}", (object) localizedID, (object) originString);
  }

  public static IntVector2 ToOffset(this EUnitDirection direction)
  {
    switch (direction)
    {
      case EUnitDirection.PositiveX:
        return new IntVector2(1, 0);
      case EUnitDirection.PositiveY:
        return new IntVector2(0, 1);
      case EUnitDirection.NegativeY:
        return new IntVector2(0, -1);
      default:
        return new IntVector2(-1, 0);
    }
  }

  public static Vector3 ToVector(this EUnitDirection direction)
  {
    switch (direction)
    {
      case EUnitDirection.PositiveX:
        return Vector3.get_right();
      case EUnitDirection.PositiveY:
        return Vector3.get_forward();
      case EUnitDirection.NegativeX:
        return Vector3.get_left();
      case EUnitDirection.NegativeY:
        return Vector3.get_back();
      default:
        return Vector3.get_left();
    }
  }

  public static Quaternion ToRotation(this EUnitDirection direction)
  {
    switch (direction)
    {
      case EUnitDirection.PositiveX:
        return Quaternion.LookRotation(Vector3.get_right());
      case EUnitDirection.PositiveY:
        return Quaternion.LookRotation(Vector3.get_forward());
      case EUnitDirection.NegativeX:
        return Quaternion.LookRotation(Vector3.get_left());
      case EUnitDirection.NegativeY:
        return Quaternion.LookRotation(Vector3.get_back());
      default:
        return Quaternion.get_identity();
    }
  }

  public static UnitData GetInstanceData(this GameParameter.UnitInstanceTypes InstanceType, GameObject gameObject)
  {
    UnitData unitData = (UnitData) null;
    switch (InstanceType)
    {
      case GameParameter.UnitInstanceTypes.Any:
        unitData = DataSource.FindDataOfClass<UnitData>(gameObject, (UnitData) null);
        if (unitData == null)
        {
          Unit dataOfClass = DataSource.FindDataOfClass<Unit>(gameObject, (Unit) null);
          if (dataOfClass != null)
          {
            unitData = dataOfClass.UnitData;
            break;
          }
          break;
        }
        break;
      case GameParameter.UnitInstanceTypes.CurrentTurn:
        if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null) && SceneBattle.Instance.Battle.CurrentUnit != null)
          return SceneBattle.Instance.Battle.CurrentUnit.UnitData;
        break;
      case GameParameter.UnitInstanceTypes.ArenaPlayerUnit0:
      case GameParameter.UnitInstanceTypes.ArenaPlayerUnit1:
      case GameParameter.UnitInstanceTypes.ArenaPlayerUnit2:
        ArenaPlayer dataOfClass1 = DataSource.FindDataOfClass<ArenaPlayer>(gameObject, (ArenaPlayer) null);
        if (dataOfClass1 != null)
        {
          int index = (int) (InstanceType - 4);
          unitData = dataOfClass1.Unit[index];
          break;
        }
        break;
      case GameParameter.UnitInstanceTypes.EnemyArenaPlayerUnit0:
      case GameParameter.UnitInstanceTypes.EnemyArenaPlayerUnit1:
      case GameParameter.UnitInstanceTypes.EnemyArenaPlayerUnit2:
        ArenaPlayer selectedArenaPlayer = (ArenaPlayer) GlobalVars.SelectedArenaPlayer;
        if (selectedArenaPlayer != null)
        {
          int index = (int) (InstanceType - 7);
          unitData = selectedArenaPlayer.Unit[index];
          break;
        }
        break;
      case GameParameter.UnitInstanceTypes.PartyUnit0:
      case GameParameter.UnitInstanceTypes.PartyUnit1:
      case GameParameter.UnitInstanceTypes.PartyUnit2:
        PlayerData player1 = MonoSingleton<GameManager>.Instance.Player;
        int index1 = (int) (InstanceType - 10);
        long unitUniqueId1 = player1.Partys[(int) GlobalVars.SelectedPartyIndex].GetUnitUniqueID(index1);
        unitData = player1.FindUnitDataByUniqueID(unitUniqueId1);
        break;
      case GameParameter.UnitInstanceTypes.VersusUnit:
        PlayerData player2 = MonoSingleton<GameManager>.Instance.Player;
        long unitUniqueId2 = player2.Partys[7].GetUnitUniqueID(0);
        unitData = player2.FindUnitDataByUniqueID(unitUniqueId2);
        break;
    }
    return unitData;
  }

  public static void GetInstanceData(this GameParameter.ItemInstanceTypes instanceType, int index, GameObject gameObject, out ItemParam itemParam, out int itemNum)
  {
    switch (instanceType)
    {
      case GameParameter.ItemInstanceTypes.Any:
        ItemData dataOfClass1 = DataSource.FindDataOfClass<ItemData>(gameObject, (ItemData) null);
        if (dataOfClass1 != null)
        {
          itemParam = dataOfClass1.Param;
          itemNum = dataOfClass1.Num;
          return;
        }
        itemParam = DataSource.FindDataOfClass<ItemParam>(gameObject, (ItemParam) null);
        itemNum = 0;
        return;
      case GameParameter.ItemInstanceTypes.Inventory:
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (0 <= index && index < player.Inventory.Length && player.Inventory[index] != null)
        {
          itemParam = player.Inventory[index].Param;
          itemNum = player.Inventory[index].Num;
          return;
        }
        break;
      case GameParameter.ItemInstanceTypes.QuestReward:
        if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
        {
          QuestParam questParam = MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID) ?? DataSource.FindDataOfClass<QuestParam>(gameObject, (QuestParam) null);
          if (questParam != null && 0 <= index && (questParam.bonusObjective != null && index < questParam.bonusObjective.Length))
          {
            itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(questParam.bonusObjective[index].item);
            itemNum = questParam.bonusObjective[index].itemNum;
            return;
          }
          break;
        }
        break;
      case GameParameter.ItemInstanceTypes.Equipment:
        EquipData dataOfClass2 = DataSource.FindDataOfClass<EquipData>(gameObject, (EquipData) null);
        if (dataOfClass2 != null)
        {
          itemParam = dataOfClass2.ItemParam;
          itemNum = 0;
          return;
        }
        break;
      case GameParameter.ItemInstanceTypes.EnhanceMaterial:
        EnhanceMaterial dataOfClass3 = DataSource.FindDataOfClass<EnhanceMaterial>(gameObject, (EnhanceMaterial) null);
        if (dataOfClass3 != null && dataOfClass3.item != null)
        {
          itemParam = dataOfClass3.item.Param;
          itemNum = dataOfClass3.item.Num;
          return;
        }
        break;
      case GameParameter.ItemInstanceTypes.EnhanceEquipData:
        EnhanceEquipData dataOfClass4 = DataSource.FindDataOfClass<EnhanceEquipData>(gameObject, (EnhanceEquipData) null);
        if (dataOfClass4 != null && dataOfClass4.equip != null)
        {
          itemParam = dataOfClass4.equip.ItemParam;
          itemNum = 0;
          return;
        }
        break;
      case GameParameter.ItemInstanceTypes.SellItem:
        SellItem dataOfClass5 = DataSource.FindDataOfClass<SellItem>(gameObject, (SellItem) null);
        if (dataOfClass5 != null && dataOfClass5.item != null)
        {
          itemParam = dataOfClass5.item.Param;
          itemNum = dataOfClass5.num;
          return;
        }
        break;
    }
    itemParam = (ItemParam) null;
    itemNum = 0;
  }

  public static string GetPath(this GameObject go, GameObject root = null)
  {
    StringBuilder stringBuilder = GameUtility.GetStringBuilder();
    if (Object.op_Equality((Object) root, (Object) null))
    {
      for (Transform transform = go.get_transform(); Object.op_Inequality((Object) transform, (Object) null); transform = transform.get_parent())
      {
        stringBuilder.Insert(0, ((Object) ((Component) transform).get_gameObject()).get_name());
        stringBuilder.Insert(0, '/');
      }
    }
    else
    {
      for (Transform transform = go.get_transform(); Object.op_Inequality((Object) transform, (Object) null) && Object.op_Inequality((Object) ((Component) transform).get_gameObject(), (Object) root); transform = transform.get_parent())
      {
        stringBuilder.Insert(0, ((Object) ((Component) transform).get_gameObject()).get_name());
        stringBuilder.Insert(0, '/');
      }
    }
    return stringBuilder.ToString();
  }

  public static T RequireComponent<T>(this Component component) where T : Component
  {
    Component component1 = (Component) (object) component.GetComponent<T>();
    if (Object.op_Inequality((Object) component1, (Object) null))
      return (T) component1;
    return component.get_gameObject().AddComponent<T>();
  }

  public static T RequireComponent<T>(this GameObject go) where T : Component
  {
    Component component = (Component) (object) go.GetComponent<T>();
    if (Object.op_Inequality((Object) component, (Object) null))
      return (T) component;
    return go.AddComponent<T>();
  }

  public static void AddClickListener(this Button button, ButtonExt.ButtonClickEvent listener)
  {
    ((Component) button).get_gameObject().RequireComponent<ButtonExt>().AddListener(listener);
  }

  public static void RemoveClickListener(this Button button, ButtonExt.ButtonClickEvent listener)
  {
    ((Component) button).get_gameObject().RequireComponent<ButtonExt>().RemoveListener(listener);
  }

  public static string ComposeURL(this FlowNode_WebView.URL_Mode URLMode, string URL)
  {
    switch (URLMode)
    {
      case FlowNode_WebView.URL_Mode.APIHost:
        return Network.Host + URL;
      case FlowNode_WebView.URL_Mode.SiteHost:
        return Network.SiteHost + URL;
      case FlowNode_WebView.URL_Mode.NewsHost:
        return Network.NewsHost + URL;
      default:
        return URL;
    }
  }

  public static float Evaluate(this ObjectAnimator.CurveType curve, float t)
  {
    switch (curve)
    {
      case ObjectAnimator.CurveType.EaseIn:
        return 1f - Mathf.Cos((float) ((double) t * 3.14159274101257 * 0.5));
      case ObjectAnimator.CurveType.EaseOut:
        return Mathf.Cos((float) ((1.0 - (double) t) * 3.14159274101257 * 0.5));
      case ObjectAnimator.CurveType.EaseInOut:
        return (float) ((1.0 - (double) Mathf.Cos(t * 3.141593f)) * 0.5);
      default:
        return t;
    }
  }

  public static float ToSpan(this CameraInterpSpeed speed)
  {
    if (speed == CameraInterpSpeed.Immediate)
      return 0.0f;
    return (float) ((double) speed * 0.25 + 0.5);
  }

  public static UnlockTargets ToUnlockTargets(this EShopType type)
  {
    switch (type)
    {
      case EShopType.Normal:
        return UnlockTargets.Shop;
      case EShopType.Tabi:
        return UnlockTargets.ShopTabi;
      case EShopType.Kimagure:
        return UnlockTargets.ShopKimagure;
      case EShopType.Monozuki:
        return UnlockTargets.ShopMonozuki;
      case EShopType.Tour:
        return UnlockTargets.Tour;
      case EShopType.Arena:
        return UnlockTargets.Arena;
      case EShopType.Multi:
        return UnlockTargets.MultiPlay;
      case EShopType.AwakePiece:
        return UnlockTargets.ShopAwakePiece;
      case EShopType.Artifact:
        return UnlockTargets.Artifact;
      default:
        return (UnlockTargets) 0;
    }
  }

  public static string GetDescription(this TrophyObjective self)
  {
    GameManager instance = MonoSingleton<GameManager>.Instance;
    string str1 = string.Empty;
    string str2 = string.Empty;
    char[] chArray = new char[1]{ ',' };
    if (!string.IsNullOrEmpty(self.Param.Expr))
      return string.Format(LocalizedText.Get(self.Param.Expr), (object) self.ival);
    switch (self.type)
    {
      case TrophyConditionTypes.winquest:
        if (string.IsNullOrEmpty(self.sval))
          return string.Format(LocalizedText.Get("sys.TROPHY_WINQUEST_NORMAL"), (object) self.ival);
        QuestParam quest1 = instance.FindQuest(self.sval);
        return string.Format(LocalizedText.Get("sys.TROPHY_WINQUEST"), quest1 == null ? (object) ("?" + self.sval) : (object) quest1.name, (object) self.ival);
      case TrophyConditionTypes.killenemy:
        UnitParam unitParam1 = instance.GetUnitParam(self.sval);
        return string.Format(LocalizedText.Get("sys.TROPHY_KILLENEMY"), unitParam1 == null ? (object) ("?" + self.sval) : (object) unitParam1.name, (object) self.ival);
      case TrophyConditionTypes.getitem:
        ItemParam itemParam = instance.GetItemParam(self.sval);
        return string.Format(LocalizedText.Get("sys.TROPHY_GETITEM"), itemParam == null ? (object) ("?" + self.sval) : (object) itemParam.name, (object) self.ival);
      case TrophyConditionTypes.playerlv:
        return string.Format(LocalizedText.Get("sys.TROPHY_PLAYERLV"), (object) self.ival);
      case TrophyConditionTypes.winelite:
        return string.Format(LocalizedText.Get("sys.TROPHY_WINQUEST_ELITE"), (object) self.ival);
      case TrophyConditionTypes.winevent:
        return string.Format(LocalizedText.Get("sys.TROPHY_WINQUEST_EVENT"), (object) self.ival);
      case TrophyConditionTypes.gacha:
        return string.Format(LocalizedText.Get("sys.TROPHY_GACHA"), (object) self.ival);
      case TrophyConditionTypes.multiplay:
        return string.Format(LocalizedText.Get("sys.TROPHY_MULTIPLAY"), (object) self.ival);
      case TrophyConditionTypes.ability:
        return string.Format(LocalizedText.Get("sys.TROPHY_ABILITY"), (object) self.ival);
      case TrophyConditionTypes.soubi:
        return string.Format(LocalizedText.Get("sys.TROPHY_SOUBI"), (object) self.ival);
      case TrophyConditionTypes.buygold:
        return string.Format(LocalizedText.Get("sys.TROPHY_BUYGOLD"), (object) self.ival);
      case TrophyConditionTypes.vip:
        return string.Format(LocalizedText.Get("sys.TROPHY_VIP"), (object) self.ival);
      case TrophyConditionTypes.stamina:
        return string.Format(LocalizedText.Get("sys.TROPHY_STAMINA"), (object) int.Parse(self.sval.Substring(0, 2)), (object) int.Parse(self.sval.Substring(3, 2)));
      case TrophyConditionTypes.arena:
        return string.Format(LocalizedText.Get("sys.TROPHY_ARENA"), (object) self.ival);
      case TrophyConditionTypes.winarena:
        return string.Format(LocalizedText.Get("sys.TROPHY_WINARENA"), (object) self.ival);
      case TrophyConditionTypes.review:
        return LocalizedText.Get("sys.TROPHY_REVIEW");
      case TrophyConditionTypes.followtwitter:
        return LocalizedText.Get("sys.TROPHY_FOLLOWTWITTER");
      case TrophyConditionTypes.fggid:
        return LocalizedText.Get("sys.TROPHY_FGGID");
      case TrophyConditionTypes.unitlevel:
        UnitParam unitParam2 = instance.GetUnitParam(self.sval);
        return string.Format(LocalizedText.Get("sys.TROPHY_UNITLV"), unitParam2 == null ? (object) ("?" + self.sval) : (object) unitParam2.name, (object) self.ival);
      case TrophyConditionTypes.evolutionnum:
        UnitParam unitParam3 = instance.GetUnitParam(self.sval);
        return string.Format(LocalizedText.Get("sys.TROPHY_EVOLUTIONCNT"), unitParam3 == null ? (object) ("?" + self.sval) : (object) unitParam3.name, (object) (self.ival + 1));
      case TrophyConditionTypes.joblevel:
        string[] strArray1 = self.sval.Split(chArray);
        UnitParam unitParam4 = instance.GetUnitParam(strArray1[0]);
        JobParam jobParam1 = instance.GetJobParam(strArray1[1]);
        return string.Format(LocalizedText.Get("sys.TROPHY_JOBLV"), unitParam4 == null ? (object) ("?" + strArray1[0]) : (object) unitParam4.name, jobParam1 == null ? (object) ("?" + strArray1[1]) : (object) jobParam1.name, (object) self.ival);
      case TrophyConditionTypes.logincount:
        return string.Format(LocalizedText.Get("sys.TROPHY_LOGINCNT"), (object) self.ival);
      case TrophyConditionTypes.upunitlevel:
        return string.Format(LocalizedText.Get("sys.TROPHY_UNITLVUP"), (object) SRPG_Extensions.GetUnitName(self.sval), (object) self.ival);
      case TrophyConditionTypes.makeunitlevel:
        if (!string.IsNullOrEmpty(self.sval))
          return string.Format(LocalizedText.Get("sys.TROPHY_UNITLVMAKE"), (object) SRPG_Extensions.GetUnitName(self.sval), (object) self.ival);
        return string.Format(LocalizedText.Get("sys.TROPHY_UNITLVMAKE_DEFAULT"), (object) self.ival);
      case TrophyConditionTypes.unitequip:
        return string.Format(LocalizedText.Get("sys.TROPHY_EQUIP"), (object) SRPG_Extensions.GetUnitName(self.sval), (object) self.ival);
      case TrophyConditionTypes.upjoblevel:
        if (!string.IsNullOrEmpty(self.sval))
        {
          string[] strArray2 = self.sval.Split(chArray);
          UnitParam unitParam5 = instance.GetUnitParam(strArray2[0]);
          JobParam jobParam2 = instance.GetJobParam(strArray2[1]);
          str2 = unitParam5 == null ? "?" + strArray2[0] : unitParam5.name;
          str1 = jobParam2 == null ? "?" + strArray2[1] : jobParam2.name;
        }
        if (!string.IsNullOrEmpty(str2))
        {
          if (!string.IsNullOrEmpty(str1))
            return string.Format(LocalizedText.Get("sys.TROPHY_JOBLVUP"), (object) str2, (object) str1, (object) self.ival);
          return string.Format(LocalizedText.Get("sys.TROPHY_JOBLVUP_ANYJOB"), (object) str2, (object) self.ival);
        }
        if (!string.IsNullOrEmpty(str1))
          return string.Format(LocalizedText.Get("sys.TROPHY_JOBLVUP_ANYJOB_ANYUNIT"), (object) self.ival);
        return string.Format(LocalizedText.Get("sys.TROPHY_JOBLVUP"), (object) str2, (object) str1, (object) self.ival);
      case TrophyConditionTypes.makejoblevel:
        if (!string.IsNullOrEmpty(self.sval))
        {
          string[] strArray2 = self.sval.Split(chArray);
          UnitParam unitParam5 = instance.GetUnitParam(strArray2[0]);
          JobParam jobParam2 = instance.GetJobParam(strArray2[1]);
          string str3 = unitParam5 == null ? "?" + strArray2[0] : unitParam5.name;
          string str4 = jobParam2 == null ? "?" + strArray2[1] : jobParam2.name;
          if (!string.IsNullOrEmpty(str3))
          {
            if (!string.IsNullOrEmpty(str4))
              return string.Format(LocalizedText.Get("sys.TROPHY_JOBLVMAKE"), (object) str3, (object) str4, (object) self.ival);
            return string.Format(LocalizedText.Get("sys.TROPHY_JOBLVMAKE_ANYJOB"), (object) str3, (object) self.ival);
          }
          if (!string.IsNullOrEmpty(str4))
            return string.Format(LocalizedText.Get("sys.TROPHY_JOBLVMAKE_ANYUNIT"), (object) str4, (object) self.ival);
        }
        return string.Format(LocalizedText.Get("sys.TROPHY_JOBLVMAKE_DEFAULT"), (object) self.ival);
      case TrophyConditionTypes.limitbreak:
        if (!string.IsNullOrEmpty(self.sval))
          return string.Format(LocalizedText.Get("sys.TROPHY_LIMITBREAK"), (object) SRPG_Extensions.GetUnitName(self.sval), (object) self.ival);
        return string.Format(LocalizedText.Get("sys.TROPHY_LIMITBREAK_DEFAULT"), (object) self.ival);
      case TrophyConditionTypes.evoltiontimes:
        if (string.IsNullOrEmpty(self.sval) || string.IsNullOrEmpty(SRPG_Extensions.GetUnitName(self.sval)))
          return string.Format(LocalizedText.Get("sys.TROPHY_EVOLUTIONTIMES_ANY"), (object) self.ival);
        return string.Format(LocalizedText.Get("sys.TROPHY_EVOLUTIONTIMES"), (object) SRPG_Extensions.GetUnitName(self.sval), (object) self.ival);
      case TrophyConditionTypes.changejob:
        if (!string.IsNullOrEmpty(self.sval))
          return string.Format(LocalizedText.Get("sys.TROPHY_CHANGEJOB"), (object) SRPG_Extensions.GetUnitName(self.sval), (object) self.ival);
        return string.Format(LocalizedText.Get("sys.TROPHY_CHANGEJOB_DEFAULT"), (object) self.ival);
      case TrophyConditionTypes.changeability:
        return string.Format(LocalizedText.Get("sys.TROPHY_CHANGEABILITY"), (object) SRPG_Extensions.GetUnitName(self.sval), (object) self.ival);
      case TrophyConditionTypes.makeabilitylevel:
        if (string.IsNullOrEmpty(self.sval))
          return string.Format(LocalizedText.Get("sys.TROPHY_ABILITYLV_DEFAULT"), (object) self.ival);
        string[] strArray3 = self.sval.Split(chArray);
        string empty1 = string.Empty;
        if (string.IsNullOrEmpty(strArray3[1]))
          return string.Format(LocalizedText.Get("sys.TROPHY_ABILITYLV_ANYABILITY"), (object) SRPG_Extensions.GetUnitName(strArray3[0]), (object) self.ival);
        AbilityParam abilityParam = instance.GetAbilityParam(strArray3[1]);
        string str5 = abilityParam == null ? "?" + strArray3[1] : abilityParam.name;
        if (!string.IsNullOrEmpty(strArray3[0]))
          return string.Format(LocalizedText.Get("sys.TROPHY_ABILITYLV"), (object) SRPG_Extensions.GetUnitName(strArray3[0]), (object) str5, (object) self.ival);
        return string.Format(LocalizedText.Get("sys.TROPHY_ABILITYLV_ANYUNIT"), (object) str5, (object) self.ival);
      case TrophyConditionTypes.winquestsoldier:
        return string.Format(LocalizedText.Get("sys.TROPHY_WINSOLIDER"), (object) self.ival);
      case TrophyConditionTypes.winmulti:
        return string.Format(LocalizedText.Get("sys.TROPHY_WINMULTI"), (object) SRPG_Extensions.GetQuestName(self.sval), (object) self.ival);
      case TrophyConditionTypes.buyatshop:
        string empty2 = string.Empty;
        string empty3 = string.Empty;
        if (!string.IsNullOrEmpty(self.sval))
        {
          string[] strArray2 = self.sval.Split(chArray);
          if (!string.IsNullOrEmpty(strArray2[0]))
          {
            int shopType = instance.MasterParam.GetShopType(strArray2[0]);
            string shopName = instance.Player.GetShopName((EShopType) shopType);
            if (!string.IsNullOrEmpty(strArray2[1]))
              return string.Format(LocalizedText.Get("sys.TROPHY_BUYATSHOP"), (object) shopName, (object) empty3, (object) self.ival);
            return string.Format(LocalizedText.Get("sys.TROPHY_BUYATSHOP_ANYITEM"), (object) shopName, (object) self.ival);
          }
          string itemName = SRPG_Extensions.GetItemName(strArray2[1]);
          if (!string.IsNullOrEmpty(strArray2[1]))
            return string.Format(LocalizedText.Get("sys.TROPHY_BUYATSHOP_ANYSHOP"), (object) itemName, (object) self.ival);
        }
        return string.Format(LocalizedText.Get("sys.TROPHY_BUYATSHOP_DEFAULT"), (object) self.ival);
      case TrophyConditionTypes.artifacttransmute:
        if (string.IsNullOrEmpty(self.sval) || string.IsNullOrEmpty(SRPG_Extensions.GetArtifactName(self.sval)))
          return string.Format(LocalizedText.Get("sys.TROPHY_AFORDRILL_ANY"), (object) self.ival);
        return string.Format(LocalizedText.Get("sys.TROPHY_AFORDRILL"), (object) SRPG_Extensions.GetArtifactName(self.sval), (object) self.ival);
      case TrophyConditionTypes.artifactstrength:
        if (string.IsNullOrEmpty(self.sval) || string.IsNullOrEmpty(SRPG_Extensions.GetArtifactName(self.sval)))
          return string.Format(LocalizedText.Get("sys.TROPHY_AFSTRENGTHEN_ANY"), (object) self.ival);
        return string.Format(LocalizedText.Get("sys.TROPHY_AFSTRENGTHEN"), (object) SRPG_Extensions.GetArtifactName(self.sval), (object) self.ival);
      case TrophyConditionTypes.artifactevolution:
        if (string.IsNullOrEmpty(self.sval) || string.IsNullOrEmpty(SRPG_Extensions.GetArtifactName(self.sval)))
          return string.Format(LocalizedText.Get("sys.TROPHY_AFVOLUTION_ANY"), (object) self.ival);
        return string.Format(LocalizedText.Get("sys.TROPHY_AFVOLUTION"), (object) SRPG_Extensions.GetArtifactName(self.sval), (object) self.ival);
      case TrophyConditionTypes.winmultimore:
        return string.Format(LocalizedText.Get("sys.TROPHY_WINMULTIMORE"), (object) SRPG_Extensions.GetQuestName(self.sval), (object) self.ival);
      case TrophyConditionTypes.winmultiless:
        return string.Format(LocalizedText.Get("sys.TROPHY_WINMULTILESS"), (object) SRPG_Extensions.GetQuestName(self.sval), (object) self.ival);
      case TrophyConditionTypes.collectunits:
        return string.Format(LocalizedText.Get("sys.TROPHY_COLLECTUNITS"), (object) self.ival);
      case TrophyConditionTypes.totaljoblv11:
        return string.Format(LocalizedText.Get("sys.TROPHY_TOTALJOBLV11"), (object) self.ival);
      case TrophyConditionTypes.totalunitlvs:
        return string.Format(LocalizedText.Get("sys.TROPHY_TOTALUNITLVS"), (object) self.ival);
      case TrophyConditionTypes.childrencomp:
        return string.Format(LocalizedText.Get("sys.TROPHY_CHILDRENCOMP"), (object) self.ival);
      case TrophyConditionTypes.wintower:
        if (string.IsNullOrEmpty(self.sval))
          return string.Format(LocalizedText.Get("sys.TROPHY_WINTOWER_NORMAL"), (object) self.ival);
        QuestParam quest2 = instance.FindQuest(self.sval);
        if (quest2 != null)
          return string.Format(LocalizedText.Get("sys.TROPHY_WINTOWER"), (object) quest2.title, (object) quest2.name, (object) self.ival);
        DebugUtility.Log("「" + self.sval + "」quest_id is not found.");
        return string.Empty;
      case TrophyConditionTypes.losequest:
        if (string.IsNullOrEmpty(self.sval))
          return string.Format(LocalizedText.Get("sys.TROPHY_LOSEQUEST_NORMAL"), (object) self.ival);
        QuestParam quest3 = instance.FindQuest(self.sval);
        return string.Format(LocalizedText.Get("sys.TROPHY_LOSEQUEST"), quest3 == null ? (object) ("?" + self.sval) : (object) quest3.name, (object) self.ival);
      case TrophyConditionTypes.loseelite:
        return string.Format(LocalizedText.Get("sys.TROPHY_LOSEQUEST_ELITE"), (object) self.ival);
      case TrophyConditionTypes.loseevent:
        return string.Format(LocalizedText.Get("sys.TROPHY_LOSEQUEST_EVENT"), (object) self.ival);
      case TrophyConditionTypes.losetower:
        if (string.IsNullOrEmpty(self.sval))
          return string.Format(LocalizedText.Get("sys.TROPHY_LOSETOWER_NORMAL"), (object) self.ival);
        QuestParam quest4 = instance.FindQuest(self.sval);
        if (quest4 != null)
          return string.Format(LocalizedText.Get("sys.TROPHY_LOSETOWER"), (object) quest4.title, (object) quest4.name, (object) self.ival);
        DebugUtility.Log("「" + self.sval + "」quest_id is not found.");
        return string.Empty;
      case TrophyConditionTypes.losearena:
        return string.Format(LocalizedText.Get("sys.TROPHY_LOSEARENA"), (object) self.ival);
      case TrophyConditionTypes.dailyall:
        return string.Format(LocalizedText.Get("sys.TROPHY_DAILYALL"), (object) self.ival);
      case TrophyConditionTypes.vs:
        return string.Format(LocalizedText.Get("sys.TROPHY_VS"), (object) self.ival);
      case TrophyConditionTypes.vswin:
        return string.Format(LocalizedText.Get("sys.TROPHY_VSWIN"), (object) self.ival);
      case TrophyConditionTypes.vslose:
        return string.Format(LocalizedText.Get("sys.TROPHY_VSLOSE"), (object) self.ival);
      case TrophyConditionTypes.fblogin:
        return LocalizedText.Get("sys.TROPHY_FBLOGIN");
      default:
        return string.Empty;
    }
  }

  private static string GetUnitName(string unitid)
  {
    string str = string.Empty;
    if (!string.IsNullOrEmpty(unitid))
    {
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(unitid);
      str = unitParam == null ? "?" + unitid : unitParam.name;
    }
    return str;
  }

  private static string GetItemName(string itemid)
  {
    string str = string.Empty;
    if (!string.IsNullOrEmpty(itemid))
    {
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(itemid);
      str = itemParam == null ? "?" + itemid : itemParam.name;
    }
    return str;
  }

  private static string GetArtifactName(string itemid)
  {
    string str = string.Empty;
    if (!string.IsNullOrEmpty(itemid))
    {
      ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(itemid);
      str = artifactParam == null ? "?" + itemid : artifactParam.name;
    }
    return str;
  }

  private static string GetQuestName(string questid)
  {
    string str = string.Empty;
    if (!string.IsNullOrEmpty(questid))
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questid);
      str = quest == null ? "?" + questid : quest.name;
    }
    return str;
  }

  public static float ToFloat(this EventAction_Dialog.TextSpeedTypes speed)
  {
    switch (speed)
    {
      case EventAction_Dialog.TextSpeedTypes.Slow:
        return 0.2f;
      case EventAction_Dialog.TextSpeedTypes.Fast:
        return 0.01f;
      default:
        return 0.05f;
    }
  }

  public static float ToFloat(this Event2dAction_Dialog.TextSpeedTypes speed)
  {
    switch (speed)
    {
      case Event2dAction_Dialog.TextSpeedTypes.Slow:
        return 0.2f;
      case Event2dAction_Dialog.TextSpeedTypes.Fast:
        return 0.01f;
      default:
        return 0.05f;
    }
  }

  public static float ToFloat(this Event2dAction_Dialog2.TextSpeedTypes speed)
  {
    switch (speed)
    {
      case Event2dAction_Dialog2.TextSpeedTypes.Slow:
        return 0.2f;
      case Event2dAction_Dialog2.TextSpeedTypes.Fast:
        return 0.01f;
      default:
        return 0.05f;
    }
  }

  public static float ToFloat(this Event2dAction_Telop.TextSpeedTypes speed)
  {
    switch (speed)
    {
      case Event2dAction_Telop.TextSpeedTypes.Slow:
        return 0.2f;
      case Event2dAction_Telop.TextSpeedTypes.Fast:
        return 0.01f;
      default:
        return 0.05f;
    }
  }

  public static int ToYMD(this DateTime date)
  {
    return date.Year % 100 * 10000 + date.Month % 100 * 100 + date.Day % 100;
  }

  public static DateTime FromYMD(this int ymd)
  {
    return new DateTime(ymd / 10000 + 2000, ymd / 100 % 100, ymd % 100);
  }

  public static string ToPrefix(this ESex sex)
  {
    return SRPG_Extensions.mPrefixes[(int) sex];
  }

  public static string ToColorValue(this Color32 src)
  {
    return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", new object[4]{ (object) (byte) src.r, (object) (byte) src.g, (object) (byte) src.b, (object) (byte) src.a });
  }

  public static int GetMaxTeamCount(this PartyWindow2.EditPartyTypes type)
  {
    GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
    if (Object.op_Equality((Object) instanceDirect, (Object) null))
    {
      switch (type)
      {
        case PartyWindow2.EditPartyTypes.Normal:
        case PartyWindow2.EditPartyTypes.Event:
          return 5;
        case PartyWindow2.EditPartyTypes.Tower:
          return 6;
        default:
          return 1;
      }
    }
    else
    {
      FixParam fixParam = instanceDirect.MasterParam.FixParam;
      switch (type)
      {
        case PartyWindow2.EditPartyTypes.Normal:
          return fixParam.PartyNumNormal;
        case PartyWindow2.EditPartyTypes.Event:
          return fixParam.PartyNumEvent;
        case PartyWindow2.EditPartyTypes.MP:
          return fixParam.PartyNumMulti;
        case PartyWindow2.EditPartyTypes.Arena:
          return fixParam.PartyNumArenaAttack;
        case PartyWindow2.EditPartyTypes.ArenaDef:
          return fixParam.PartyNumArenaDefense;
        case PartyWindow2.EditPartyTypes.Character:
          return fixParam.PartyNumChQuest;
        case PartyWindow2.EditPartyTypes.Tower:
          return fixParam.PartyNumTower;
        default:
          return 1;
      }
    }
  }

  public static PlayerPartyTypes ToPlayerPartyType(this PartyWindow2.EditPartyTypes type)
  {
    switch (type)
    {
      case PartyWindow2.EditPartyTypes.Normal:
        return PlayerPartyTypes.Normal;
      case PartyWindow2.EditPartyTypes.Event:
        return PlayerPartyTypes.Event;
      case PartyWindow2.EditPartyTypes.MP:
        return PlayerPartyTypes.Multiplay;
      case PartyWindow2.EditPartyTypes.Arena:
        return PlayerPartyTypes.Arena;
      case PartyWindow2.EditPartyTypes.ArenaDef:
        return PlayerPartyTypes.ArenaDef;
      case PartyWindow2.EditPartyTypes.Character:
        return PlayerPartyTypes.Character;
      case PartyWindow2.EditPartyTypes.Tower:
        return PlayerPartyTypes.Tower;
      case PartyWindow2.EditPartyTypes.Versus:
        return PlayerPartyTypes.Versus;
      default:
        throw new InvalidPartyTypeException();
    }
  }
}
