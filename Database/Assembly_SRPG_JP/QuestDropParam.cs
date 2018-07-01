// Decompiled with JetBrains decompiler
// Type: SRPG.QuestDropParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    private readonly float LOAD_ASYNC_OWN_TIME_LIMIT;
    private bool mIsLoaded;
    private IEnumerator mStartLoadAsyncIEnumerator;

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
      if (this.mIsLoaded || this.mStartLoadAsyncIEnumerator == null)
        return;
      this.StopCoroutine(this.mStartLoadAsyncIEnumerator);
      this.mStartLoadAsyncIEnumerator = (IEnumerator) null;
    }

    protected void Start()
    {
      this.LoadAsync();
    }

    public bool Load()
    {
      return this.LoadJson(this.MASTER_PATH, false);
    }

    private void LoadAsync()
    {
      this.LoadJson(this.MASTER_PATH, true);
    }

    private bool LoadJson(string path, bool isAsync)
    {
      if (this.mIsLoaded || string.IsNullOrEmpty(path))
        return false;
      string src = AssetManager.LoadTextData(path);
      if (string.IsNullOrEmpty(src))
        return false;
      try
      {
        JSON_QuestDropParam jsonObject = JSONParser.parseJSONObject<JSON_QuestDropParam>(src);
        if (jsonObject == null)
          throw new InvalidJSONException();
        if (isAsync)
        {
          this.DeserializeAsync(jsonObject);
        }
        else
        {
          this.Deserialize(jsonObject);
          this.mIsLoaded = true;
        }
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
      if (json.simpleQuestDrops != null)
      {
        for (int index = 0; index < json.simpleQuestDrops.Length; ++index)
        {
          SimpleQuestDropParam simpleQuestDropParam = new SimpleQuestDropParam();
          if (simpleQuestDropParam.Deserialize(json.simpleQuestDrops[index]))
            this.mSimpleQuestDrops.Add(simpleQuestDropParam);
        }
      }
      this.mIsLoaded = true;
    }

    private void DeserializeAsync(JSON_QuestDropParam json)
    {
      this.mStartLoadAsyncIEnumerator = this.StartLoadAsync(json);
      this.StartCoroutine(this.mStartLoadAsyncIEnumerator);
    }

    [DebuggerHidden]
    private IEnumerator StartLoadAsync(JSON_QuestDropParam json)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestDropParam.\u003CStartLoadAsync\u003Ec__Iterator7F()
      {
        json = json,
        \u003C\u0024\u003Ejson = json,
        \u003C\u003Ef__this = this
      };
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

    private void CompleteLoading()
    {
      if (this.mIsLoaded)
        return;
      while (!this.mIsLoaded)
        this.mStartLoadAsyncIEnumerator.MoveNext();
    }

    public List<ItemParam> GetQuestDropList(string quest_iname, DateTime date_time)
    {
      List<ItemParam> itemParamList = new List<ItemParam>();
      SimpleDropTableList simpleDropTables = this.FindSimpleDropTables(quest_iname);
      if (simpleDropTables != null)
      {
        List<ItemParam> currTimeDropItems = this.GetCurrTimeDropItems(new List<SimpleDropTableList>()
        {
          simpleDropTables
        }, date_time);
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

    public List<BattleCore.DropItemParam> GetQuestDropItemParamList(string quest_iname, DateTime date_time)
    {
      List<BattleCore.DropItemParam> dropItemParamList = new List<BattleCore.DropItemParam>();
      SimpleDropTableList simpleDropTables = this.FindSimpleDropTables(quest_iname);
      if (simpleDropTables != null)
      {
        List<BattleCore.DropItemParam> timeDropItemParams = this.GetCurrTimeDropItemParams(new List<SimpleDropTableList>()
        {
          simpleDropTables
        }, date_time);
        if (timeDropItemParams != null)
          dropItemParamList.AddRange((IEnumerable<BattleCore.DropItemParam>) timeDropItemParams);
      }
      List<BattleCore.DropItemParam> enemyDropItemParams = this.GetEnemyDropItemParams(quest_iname, date_time);
      if (enemyDropItemParams != null)
      {
        using (List<BattleCore.DropItemParam>.Enumerator enumerator = enemyDropItemParams.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            BattleCore.DropItemParam param = enumerator.Current;
            if (!dropItemParamList.Exists((Predicate<BattleCore.DropItemParam>) (drop => drop.Iname == param.Iname)))
              dropItemParamList.Add(param);
          }
        }
      }
      return dropItemParamList;
    }

    public EnemyDropList FindSimpleLocalMaps(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (EnemyDropList) null;
      this.CompleteLoading();
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
      this.CompleteLoading();
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
      List<BattleCore.DropItemParam> dropItemParamList1 = this.GetQuestDropItemParamList(quest_iname, time1);
      List<BattleCore.DropItemParam> dropItemParamList2 = this.GetQuestDropItemParamList(quest_iname, time2);
      if (dropItemParamList1.Count != dropItemParamList2.Count)
        return false;
      for (int index = 0; index < dropItemParamList1.Count; ++index)
      {
        if (dropItemParamList1[index].Iname != dropItemParamList2[index].Iname)
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

    private List<BattleCore.DropItemParam> GetEnemyDropItemParams(string quest_iname, DateTime date_time)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(quest_iname);
      if (quest == null || quest.map.Count <= 0)
        return (List<BattleCore.DropItemParam>) null;
      EnemyDropList simpleLocalMaps = this.FindSimpleLocalMaps(quest.map[0].mapSetName);
      if (simpleLocalMaps == null)
        return (List<BattleCore.DropItemParam>) null;
      return this.GetCurrTimeDropItemParams(simpleLocalMaps.drp_tbls, date_time);
    }

    private List<BattleCore.DropItemParam> GetCurrTimeDropItemParams(List<SimpleDropTableList> drop_tbls, DateTime date_time)
    {
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
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
            string[] strArray3 = (string[]) null;
            string[] strArray4 = (string[]) null;
            using (List<SimpleDropTableParam>.Enumerator enumerator2 = current1.smp_drp_tbls.GetEnumerator())
            {
              while (enumerator2.MoveNext())
              {
                SimpleDropTableParam current2 = enumerator2.Current;
                if (!current2.IsSuffix)
                {
                  strArray1 = current2.dropList;
                  strArray2 = current2.dropcards;
                }
                else if (current2.IsAvailablePeriod(date_time) && (strArray3 == null || 0 < DateTime.Compare(t1, current2.beginAt)) && (strArray4 == null || 0 < DateTime.Compare(t1, current2.beginAt)))
                {
                  strArray3 = current2.dropList;
                  strArray4 = current2.dropcards;
                  t1 = current2.beginAt;
                }
              }
            }
            string[] strArray5 = strArray3 ?? strArray1;
            if (strArray5 != null)
              stringList1.AddRange((IEnumerable<string>) strArray5);
            string[] strArray6 = strArray4 ?? strArray2;
            if (strArray6 != null)
              stringList2.AddRange((IEnumerable<string>) strArray6);
          }
        }
      }
      if (stringList1.Count == 0 && stringList2.Count == 0)
        return (List<BattleCore.DropItemParam>) null;
      List<BattleCore.DropItemParam> dropItemParamList = new List<BattleCore.DropItemParam>();
      for (int index = 0; index < stringList1.Count; ++index)
      {
        BattleCore.DropItemParam dropItemParam = new BattleCore.DropItemParam(MonoSingleton<GameManager>.Instance.GetItemParam(stringList1[index]));
        dropItemParamList.Add(dropItemParam);
      }
      for (int index = 0; index < stringList2.Count; ++index)
      {
        BattleCore.DropItemParam dropItemParam = new BattleCore.DropItemParam(MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(stringList2[index]));
        dropItemParamList.Add(dropItemParam);
      }
      return dropItemParamList;
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
        case QuestTypes.MultiGps:
          flag = !this.IsEqualsDropList(quest.iname, GlobalVars.GetDropTableGeneratedDateTime(), TimeManager.ServerTime);
          break;
      }
      return flag;
    }

    public List<QuestParam> GetItemDropQuestList(ItemParam item, DateTime date_time)
    {
      List<QuestParam> questParamList1 = new List<QuestParam>();
      List<QuestParam> questParamList2 = new List<QuestParam>();
      this.CompleteLoading();
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
