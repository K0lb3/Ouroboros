// Decompiled with JetBrains decompiler
// Type: SRPG.QuestDropParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class QuestDropParam : MonoBehaviour
  {
    [SerializeField]
    public bool IsWarningPopupDisable;
    private static QuestDropParam mQuestDropParam;
    private List<SimpleLocalMapsParam> mSimpleLocalMaps;
    private Dictionary<string, EnemyDropList> mSimpleLocalMapsDict;
    private List<SimpleDropTableParam> mSimpleDropTables;
    private Dictionary<string, SimpleDropTableList> mSimpleDropTableDict;
    private List<SimpleQuestDropParam> mSimpleQuestDrops;
    private readonly string MASTER_PATH;

    public QuestDropParam()
    {
      base.\u002Ector();
    }

    public static QuestDropParam Instance
    {
      get
      {
        return QuestDropParam.mQuestDropParam;
      }
    }

    protected void Awake()
    {
      QuestDropParam.mQuestDropParam = this;
    }

    protected void OnDestroy()
    {
      QuestDropParam.mQuestDropParam = (QuestDropParam) null;
    }

    protected void Start()
    {
      this.LoadJson(this.MASTER_PATH);
    }

    private bool LoadJson(string path)
    {
      if (string.IsNullOrEmpty(path))
        return false;
      string src = AssetManager.LoadTextData(path);
      if (string.IsNullOrEmpty(src))
        return false;
      try
      {
        JSON_QuestDropParam jsonObject = JSONParser.parseJSONObject<JSON_QuestDropParam>(src);
        if (jsonObject == null)
          throw new InvalidJSONException();
        this.Deserialize(jsonObject);
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      return true;
    }

    public void Deserialize(JSON_QuestDropParam json)
    {
      this.mSimpleDropTables.Clear();
      this.mSimpleLocalMaps.Clear();
      this.mSimpleQuestDrops.Clear();
      if (json.simpleDropTable != null)
      {
        for (int index = 0; index < json.simpleDropTable.Length; ++index)
        {
          SimpleDropTableParam simpleDropTableParam = new SimpleDropTableParam();
          if (simpleDropTableParam.Deserialize(json.simpleDropTable[index]))
            this.mSimpleDropTables.Add(simpleDropTableParam);
        }
      }
      if (json.simpleLocalMaps != null)
      {
        for (int index = 0; index < json.simpleLocalMaps.Length; ++index)
        {
          SimpleLocalMapsParam simpleLocalMapsParam = new SimpleLocalMapsParam();
          if (simpleLocalMapsParam.Deserialize(json.simpleLocalMaps[index]))
            this.mSimpleLocalMaps.Add(simpleLocalMapsParam);
        }
      }
      if (json.simpleQuestDrops == null)
        return;
      for (int index = 0; index < json.simpleQuestDrops.Length; ++index)
      {
        SimpleQuestDropParam simpleQuestDropParam = new SimpleQuestDropParam();
        if (simpleQuestDropParam.Deserialize(json.simpleQuestDrops[index]))
          this.mSimpleQuestDrops.Add(simpleQuestDropParam);
      }
    }

    public ItemParam GetHardDropPiece(string quest_iname, DateTime date_time)
    {
      List<ItemParam> enemyDropItems = this.GetEnemyDropItems(quest_iname, date_time);
      if (enemyDropItems == null)
        return (ItemParam) null;
      using (List<ItemParam>.Enumerator enumerator = enemyDropItems.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ItemParam current = enumerator.Current;
          if (current != null && current.type == EItemType.UnitPiece)
            return current;
        }
      }
      return (ItemParam) null;
    }

    public List<ItemParam> GetQuestDropList(string quest_iname, DateTime date_time)
    {
      List<ItemParam> itemParamList = new List<ItemParam>();
      SimpleDropTableList simpleDropTables = this.FindSimpleDropTables(quest_iname);
      if (simpleDropTables != null)
      {
        List<ItemParam> currTimeDropItems = this.GetCurrTimeDropItems(new List<SimpleDropTableList>() { simpleDropTables }, date_time);
        if (currTimeDropItems != null)
          itemParamList.AddRange((IEnumerable<ItemParam>) currTimeDropItems);
      }
      List<ItemParam> enemyDropItems = this.GetEnemyDropItems(quest_iname, date_time);
      if (enemyDropItems != null)
      {
        using (List<ItemParam>.Enumerator enumerator = enemyDropItems.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            ItemParam current = enumerator.Current;
            if (!itemParamList.Contains(current))
              itemParamList.Add(current);
          }
        }
      }
      return itemParamList;
    }

    public EnemyDropList FindSimpleLocalMaps(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (EnemyDropList) null;
      EnemyDropList enemyDropList1;
      if (this.mSimpleLocalMapsDict.TryGetValue(iname, out enemyDropList1))
        return enemyDropList1;
      EnemyDropList enemyDropList2 = new EnemyDropList();
      for (int index1 = this.mSimpleLocalMaps.Count - 1; index1 >= 0; --index1)
      {
        if (!(this.mSimpleLocalMaps[index1].iname != iname) && this.mSimpleLocalMaps[index1].droplist != null)
        {
          for (int index2 = 0; index2 < this.mSimpleLocalMaps[index1].droplist.Length; ++index2)
          {
            if (!string.IsNullOrEmpty(this.mSimpleLocalMaps[index1].droplist[index2]))
            {
              SimpleDropTableList simpleDropTables = this.FindSimpleDropTables(this.mSimpleLocalMaps[index1].droplist[index2]);
              enemyDropList2.drp_tbls.Add(simpleDropTables);
            }
          }
        }
      }
      this.mSimpleLocalMapsDict.Add(iname, enemyDropList2);
      return enemyDropList2;
    }

    public SimpleDropTableList FindSimpleDropTables(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (SimpleDropTableList) null;
      SimpleDropTableList simpleDropTableList1;
      if (this.mSimpleDropTableDict.TryGetValue(iname, out simpleDropTableList1))
        return simpleDropTableList1;
      SimpleDropTableList simpleDropTableList2 = new SimpleDropTableList();
      for (int index = this.mSimpleDropTables.Count - 1; index >= 0; --index)
      {
        if (this.mSimpleDropTables[index].GetCommonName == iname)
          simpleDropTableList2.smp_drp_tbls.Add(this.mSimpleDropTables[index]);
      }
      this.mSimpleDropTableDict.Add(iname, simpleDropTableList2);
      return simpleDropTableList2;
    }

    public bool IsEqualsDropList(string quest_iname, DateTime time1, DateTime time2)
    {
      if (time1 == DateTime.MinValue || time2 == DateTime.MinValue)
        return true;
      List<ItemParam> questDropList1 = this.GetQuestDropList(quest_iname, time1);
      List<ItemParam> questDropList2 = this.GetQuestDropList(quest_iname, time2);
      if (questDropList1.Count != questDropList2.Count)
        return false;
      for (int index = 0; index < questDropList1.Count; ++index)
      {
        if (questDropList1[index].iname != questDropList2[index].iname)
          return false;
      }
      return true;
    }

    private List<ItemParam> GetEnemyDropItems(string quest_iname, DateTime date_time)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(quest_iname);
      if (quest == null || quest.map.Count <= 0)
        return (List<ItemParam>) null;
      EnemyDropList simpleLocalMaps = this.FindSimpleLocalMaps(quest.map[0].mapSetName);
      if (simpleLocalMaps == null)
        return (List<ItemParam>) null;
      return this.GetCurrTimeDropItems(simpleLocalMaps.drp_tbls, date_time);
    }

    private List<ItemParam> GetCurrTimeDropItems(List<SimpleDropTableList> drop_tbls, DateTime date_time)
    {
      List<string> stringList = new List<string>();
      DateTime t1 = DateTime.MinValue;
      using (List<SimpleDropTableList>.Enumerator enumerator1 = drop_tbls.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          SimpleDropTableList current1 = enumerator1.Current;
          if (current1.smp_drp_tbls.Count != 0)
          {
            string[] strArray1 = (string[]) null;
            string[] strArray2 = (string[]) null;
            using (List<SimpleDropTableParam>.Enumerator enumerator2 = current1.smp_drp_tbls.GetEnumerator())
            {
              while (enumerator2.MoveNext())
              {
                SimpleDropTableParam current2 = enumerator2.Current;
                if (!current2.IsSuffix)
                  strArray1 = current2.dropList;
                else if (current2.IsAvailablePeriod(date_time) && (strArray2 == null || 0 < DateTime.Compare(t1, current2.beginAt)))
                {
                  strArray2 = current2.dropList;
                  t1 = current2.beginAt;
                }
              }
            }
            string[] strArray3 = strArray2 ?? strArray1;
            if (strArray3 != null)
              stringList.AddRange((IEnumerable<string>) strArray3);
          }
        }
      }
      if (stringList.Count == 0)
        return (List<ItemParam>) null;
      List<ItemParam> itemParamList = new List<ItemParam>();
      for (int index = 0; index < stringList.Count; ++index)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(stringList[index]);
        itemParamList.Add(itemParam);
      }
      return itemParamList;
    }

    public bool IsChangedQuestDrops(QuestParam quest)
    {
      bool flag = false;
      switch (quest.type)
      {
        case QuestTypes.Story:
        case QuestTypes.Multi:
        case QuestTypes.Free:
        case QuestTypes.Event:
        case QuestTypes.Character:
        case QuestTypes.Gps:
        case QuestTypes.Extra:
        case QuestTypes.Beginner:
          flag = !this.IsEqualsDropList(quest.iname, GlobalVars.GetDropTableGeneratedDateTime(), TimeManager.ServerTime);
          break;
      }
      return flag;
    }

    public List<QuestParam> GetItemDropQuestList(ItemParam item, DateTime date_time)
    {
      List<QuestParam> questParamList1 = new List<QuestParam>();
      List<QuestParam> questParamList2 = new List<QuestParam>();
      using (List<SimpleQuestDropParam>.Enumerator enumerator = this.mSimpleQuestDrops.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          SimpleQuestDropParam current = enumerator.Current;
          if (current.item_iname == item.iname)
          {
            foreach (string iname in current.questlist)
            {
              QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(iname);
              if (quest != null)
                questParamList2.Add(quest);
            }
            break;
          }
        }
      }
      using (List<QuestParam>.Enumerator enumerator1 = questParamList2.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          QuestParam current = enumerator1.Current;
          if (!current.notSearch)
          {
            QuestTypes type = current.type;
            switch (type)
            {
              case QuestTypes.Story:
              case QuestTypes.Free:
              case QuestTypes.Event:
label_15:
                using (List<ItemParam>.Enumerator enumerator2 = this.GetQuestDropList(current.iname, date_time).GetEnumerator())
                {
                  while (enumerator2.MoveNext())
                  {
                    if (enumerator2.Current == item)
                    {
                      questParamList1.Add(current);
                      break;
                    }
                  }
                  continue;
                }
              default:
                switch (type - (byte) 10)
                {
                  case QuestTypes.Story:
                  case QuestTypes.Multi:
                  case QuestTypes.Tutorial:
                    goto label_15;
                  default:
                    continue;
                }
            }
          }
        }
      }
      return questParamList1;
    }
  }
}
