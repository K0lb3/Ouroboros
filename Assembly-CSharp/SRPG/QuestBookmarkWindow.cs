// Decompiled with JetBrains decompiler
// Type: SRPG.QuestBookmarkWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(100, "クエスト選択", FlowNode.PinTypes.Output, 100)]
  public class QuestBookmarkWindow : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private SRPG_Button ButtonBookmarkBeginEdit;
    [SerializeField]
    private SRPG_Button ButtonBookmarkEndEdit;
    [SerializeField]
    private SRPG_Button[] ButtonSections;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private GameObject ItemContainer;
    [SerializeField]
    private GameObject QuestSelectorTemplate;
    [SerializeField]
    private GameObject BookmarkNotFoundText;
    [SerializeField]
    private ScrollRect ScrollRectObj;
    [SerializeField]
    private Text TitleText;
    [SerializeField]
    private Text DescriptionText;
    private readonly string BookmarkTitle;
    private readonly string BookmarkEditTitle;
    private readonly string BookmarkDescription;
    private readonly string BookmarkEditDescription;
    private readonly string BookmarkSectionName;
    private readonly int MaxBookmarkCount;
    private string mLastSectionName;
    private Dictionary<string, List<QuestBookmarkWindow.ItemAndQuests>> mSectionToPieces;
    private List<QuestBookmarkWindow.ItemAndQuests> mBookmarkedPieces;
    private List<QuestBookmarkWindow.ItemAndQuests> mBookmarkedPiecesOrigin;
    private List<GameObject> mCurrentUnitObjects;
    private bool mIsBookmarkEditing;
    private string[] mAvailableSections;

    public QuestBookmarkWindow()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
    }

    private void Awake()
    {
      if (this.ButtonSections != null)
      {
        foreach (Component buttonSection in this.ButtonSections)
        {
          BookmarkToggleButton component = (BookmarkToggleButton) buttonSection.get_gameObject().GetComponent<BookmarkToggleButton>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            component.Activate(false);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ItemTemplate, (UnityEngine.Object) null))
        this.ItemTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ButtonBookmarkBeginEdit, (UnityEngine.Object) null))
      {
        this.ButtonBookmarkBeginEdit.AddListener(new SRPG_Button.ButtonClickEvent(this.OnBookmarkBeginEditButtonClick));
        ((Component) this.ButtonBookmarkBeginEdit).get_gameObject().SetActive(true);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ButtonBookmarkEndEdit, (UnityEngine.Object) null))
      {
        this.ButtonBookmarkEndEdit.AddListener(new SRPG_Button.ButtonClickEvent(this.OnBookmarkEndEditButtonClick));
        ((Component) this.ButtonBookmarkEndEdit).get_gameObject().SetActive(false);
      }
      this.ResetScrollPosition();
      this.RequestQuestBookmark();
    }

    private void ResetScrollPosition()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ScrollRectObj, (UnityEngine.Object) null))
        return;
      this.ScrollRectObj.set_normalizedPosition(new Vector2(1f, 1f));
    }

    private void ToggleSectionButton(int index)
    {
      for (int index1 = 0; index1 < this.ButtonSections.Length; ++index1)
      {
        BookmarkToggleButton component = (BookmarkToggleButton) ((Component) this.ButtonSections[index1]).GetComponent<BookmarkToggleButton>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.Activate(index1 == index);
      }
    }

    private void RequestQuestBookmark()
    {
      Network.RequestAPI((WebAPI) new ReqQuestBookmark(new Network.ResponseCallback(this.QuestBookmarkResponseCallback)), false);
    }

    private void RequestQuestBookmarkUpdate(IEnumerable<string> add, IEnumerable<string> delete)
    {
      Network.RequestAPI((WebAPI) new ReqQuestBookmarkUpdate(add, delete, new Network.ResponseCallback(this.QuestBookmarkUpdateResponseCallback)), false);
    }

    private void QuestBookmarkResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.QuestBookmark_RequestMax:
          case Network.EErrCode.QuestBookmark_AlreadyLimited:
            Network.RemoveAPI();
            Network.ResetError();
            UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.TXT_QUESTBOOKMARK_BOOKMARK_NOT_FOUND"), (UIUtility.DialogResultEvent) null, (GameObject) null, true, -1);
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        this.Initialize(JSONParser.parseJSONObject<WebAPI.JSON_BodyResponse<QuestBookmarkWindow.JSON_Body>>(www.text).body.result);
        Network.RemoveAPI();
      }
    }

    private void QuestBookmarkUpdateResponseCallback(WWWResult www)
    {
      if (FlowNode_Network.HasCommonError(www))
        return;
      if (Network.IsError)
      {
        switch (Network.ErrCode)
        {
          case Network.EErrCode.QuestBookmark_RequestMax:
          case Network.EErrCode.QuestBookmark_AlreadyLimited:
            Network.RemoveAPI();
            Network.ResetError();
            UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.TXT_QUESTBOOKMARK_BOOKMARK_NOT_FOUND"), (UIUtility.DialogResultEvent) null, (GameObject) null, true, -1);
            break;
          default:
            FlowNode_Network.Retry();
            break;
        }
      }
      else
      {
        this.mBookmarkedPiecesOrigin = this.mBookmarkedPieces.ToList<QuestBookmarkWindow.ItemAndQuests>();
        if (this.mLastSectionName == this.BookmarkSectionName)
          this.RefreshSection(0);
        this.EndBookmarkEditing();
        Network.RemoveAPI();
      }
    }

    private void Initialize(QuestBookmarkWindow.JSON_Item[] bookmarkItems)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      PlayerData player = instance.Player;
      SectionParam[] sections = instance.Sections;
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      long serverTime = Network.GetServerTime();
      foreach (QuestParam availableQuest in player.AvailableQuests)
      {
        if (this.IsAvailableQuest(availableQuest, serverTime) && !stringList1.Contains(availableQuest.ChapterID))
          stringList1.Add(availableQuest.ChapterID);
      }
      using (List<string>.Enumerator enumerator = stringList1.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          string current = enumerator.Current;
          ChapterParam area = instance.FindArea(current);
          if (area != null && !stringList2.Contains(area.section))
            stringList2.Add(area.section);
        }
      }
      this.mAvailableSections = stringList2.ToArray();
      Dictionary<string, List<QuestParam>> dictionary = new Dictionary<string, List<QuestParam>>();
      foreach (QuestParam questParam in ((IEnumerable<QuestParam>) MonoSingleton<GameManager>.Instance.Quests).Where<QuestParam>((Func<QuestParam, bool>) (q => q.type == QuestTypes.Free)))
      {
        List<QuestParam> questParamList;
        if (!dictionary.TryGetValue(questParam.world, out questParamList))
        {
          questParamList = new List<QuestParam>();
          dictionary[questParam.world] = questParamList;
        }
        questParamList.Add(questParam);
      }
      int index1 = 0;
      if (bookmarkItems != null && bookmarkItems.Length > 0)
      {
        foreach (QuestBookmarkWindow.JSON_Item bookmarkItem in bookmarkItems)
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(bookmarkItem.iname);
          List<QuestParam> itemDropQuestList = QuestDropParam.Instance.GetItemDropQuestList(itemParam, GlobalVars.GetDropTableGeneratedDateTime());
          this.mBookmarkedPiecesOrigin.Add(new QuestBookmarkWindow.ItemAndQuests()
          {
            itemName = itemParam.iname,
            quests = itemDropQuestList
          });
        }
        this.mBookmarkedPieces = this.mBookmarkedPiecesOrigin.ToList<QuestBookmarkWindow.ItemAndQuests>();
      }
      this.mSectionToPieces[this.BookmarkSectionName] = this.mBookmarkedPieces;
      if (index1 < this.ButtonSections.Length)
        DataSource.Bind<string>(((Component) this.ButtonSections[index1]).get_gameObject(), this.BookmarkSectionName);
      int index2;
      for (index2 = index1 + 1; index2 < this.ButtonSections.Length; ++index2)
      {
        if (index2 - 1 < sections.Length)
        {
          SectionParam sectionParam = sections[index2 - 1];
          if (sectionParam.IsDateUnlock() && stringList2.Contains(sectionParam.iname))
          {
            SRPG_Button buttonSection = this.ButtonSections[index2];
            DataSource.Bind<string>(((Component) buttonSection).get_gameObject(), sectionParam.iname);
            ((BookmarkToggleButton) ((Component) buttonSection).GetComponent<BookmarkToggleButton>()).EnableShadow(false);
            List<QuestParam> questParamList1 = dictionary[sectionParam.iname];
            OrderedDictionary source = new OrderedDictionary();
            using (List<QuestParam>.Enumerator enumerator = questParamList1.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                QuestParam current = enumerator.Current;
                ItemParam hardDropPiece = QuestDropParam.Instance.GetHardDropPiece(current.iname, GlobalVars.GetDropTableGeneratedDateTime());
                List<QuestParam> questParamList2;
                if (source.Contains((object) hardDropPiece.iname))
                {
                  questParamList2 = source[(object) hardDropPiece.iname] as List<QuestParam>;
                }
                else
                {
                  questParamList2 = new List<QuestParam>();
                  source[(object) hardDropPiece.iname] = (object) questParamList2;
                }
                questParamList2.Add(current);
              }
            }
            this.mSectionToPieces[sectionParam.iname] = source.Cast<DictionaryEntry>().Select<DictionaryEntry, QuestBookmarkWindow.ItemAndQuests>((Func<DictionaryEntry, QuestBookmarkWindow.ItemAndQuests>) (kv => new QuestBookmarkWindow.ItemAndQuests()
            {
              itemName = kv.Key as string,
              quests = kv.Value as List<QuestParam>
            })).ToList<QuestBookmarkWindow.ItemAndQuests>();
          }
          else
            ((BookmarkToggleButton) ((Component) this.ButtonSections[index2]).GetComponent<BookmarkToggleButton>()).EnableShadow(true);
        }
        else
          ((BookmarkToggleButton) ((Component) this.ButtonSections[index2]).GetComponent<BookmarkToggleButton>()).EnableShadow(true);
      }
      foreach (SectionParam sectionParam in sections)
      {
        if (sectionParam.IsDateUnlock() && stringList2.Contains(sectionParam.iname) && index2 < this.ButtonSections.Length)
        {
          DataSource.Bind<string>(((Component) this.ButtonSections[index2]).get_gameObject(), sectionParam.iname);
          List<QuestParam> questParamList1 = dictionary[sectionParam.iname];
          OrderedDictionary source = new OrderedDictionary();
          using (List<QuestParam>.Enumerator enumerator = questParamList1.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              QuestParam current = enumerator.Current;
              ItemParam hardDropPiece = QuestDropParam.Instance.GetHardDropPiece(current.iname, GlobalVars.GetDropTableGeneratedDateTime());
              List<QuestParam> questParamList2;
              if (source.Contains((object) hardDropPiece.iname))
              {
                questParamList2 = source[(object) hardDropPiece.iname] as List<QuestParam>;
              }
              else
              {
                questParamList2 = new List<QuestParam>();
                source[(object) hardDropPiece.iname] = (object) questParamList2;
              }
              questParamList2.Add(current);
            }
          }
          this.mSectionToPieces[sectionParam.iname] = source.Cast<DictionaryEntry>().Select<DictionaryEntry, QuestBookmarkWindow.ItemAndQuests>((Func<DictionaryEntry, QuestBookmarkWindow.ItemAndQuests>) (kv => new QuestBookmarkWindow.ItemAndQuests()
          {
            itemName = kv.Key as string,
            quests = kv.Value as List<QuestParam>
          })).ToList<QuestBookmarkWindow.ItemAndQuests>();
        }
      }
      foreach (SRPG_Button buttonSection in this.ButtonSections)
        buttonSection.AddListener(new SRPG_Button.ButtonClickEvent(this.OnSectionSelect));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TitleText, (UnityEngine.Object) null))
        this.TitleText.set_text(LocalizedText.Get(this.BookmarkTitle));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DescriptionText, (UnityEngine.Object) null))
        this.DescriptionText.set_text(LocalizedText.Get(this.BookmarkDescription));
      this.RefreshSection(0);
    }

    private void OnBookmarkBeginEditButtonClick(SRPG_Button button)
    {
      if (this.mIsBookmarkEditing)
        return;
      ((Component) this.ButtonBookmarkBeginEdit).get_gameObject().SetActive(false);
      ((Component) this.ButtonBookmarkEndEdit).get_gameObject().SetActive(true);
      if (this.mBookmarkedPieces.Count >= this.MaxBookmarkCount)
        this.SetActivateWithoutBookmarkedUnit(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TitleText, (UnityEngine.Object) null))
        this.TitleText.set_text(LocalizedText.Get(this.BookmarkEditTitle));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DescriptionText, (UnityEngine.Object) null))
        this.DescriptionText.set_text(LocalizedText.Get(this.BookmarkEditDescription));
      this.mIsBookmarkEditing = true;
    }

    private void OnBookmarkEndEditButtonClick(SRPG_Button button)
    {
      if (!this.mIsBookmarkEditing)
        return;
      string[] array1 = this.mBookmarkedPiecesOrigin.Except<QuestBookmarkWindow.ItemAndQuests>((IEnumerable<QuestBookmarkWindow.ItemAndQuests>) this.mBookmarkedPieces).Select<QuestBookmarkWindow.ItemAndQuests, string>((Func<QuestBookmarkWindow.ItemAndQuests, string>) (x => x.itemName)).ToArray<string>();
      string[] array2 = this.mBookmarkedPieces.Except<QuestBookmarkWindow.ItemAndQuests>((IEnumerable<QuestBookmarkWindow.ItemAndQuests>) this.mBookmarkedPiecesOrigin).Select<QuestBookmarkWindow.ItemAndQuests, string>((Func<QuestBookmarkWindow.ItemAndQuests, string>) (x => x.itemName)).ToArray<string>();
      if (array1.Length > 0 || array2.Length > 0)
        this.RequestQuestBookmarkUpdate((IEnumerable<string>) array2, (IEnumerable<string>) array1);
      else
        this.EndBookmarkEditing();
    }

    private void EndBookmarkEditing()
    {
      ((Component) this.ButtonBookmarkBeginEdit).get_gameObject().SetActive(true);
      ((Component) this.ButtonBookmarkEndEdit).get_gameObject().SetActive(false);
      this.SetActivateWithoutBookmarkedUnit(true);
      this.SetDeactivateNotAvailableUnit();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TitleText, (UnityEngine.Object) null))
        this.TitleText.set_text(LocalizedText.Get(this.BookmarkTitle));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.DescriptionText, (UnityEngine.Object) null))
        this.DescriptionText.set_text(LocalizedText.Get(this.BookmarkDescription));
      this.mIsBookmarkEditing = false;
    }

    private void SetDeactivateNotAvailableUnit()
    {
      using (List<GameObject>.Enumerator enumerator1 = this.mCurrentUnitObjects.GetEnumerator())
      {
        while (enumerator1.MoveNext())
        {
          GameObject current = enumerator1.Current;
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          QuestBookmarkWindow.\u003CSetDeactivateNotAvailableUnit\u003Ec__AnonStorey362 unitCAnonStorey362 = new QuestBookmarkWindow.\u003CSetDeactivateNotAvailableUnit\u003Ec__AnonStorey362();
          // ISSUE: reference to a compiler-generated field
          unitCAnonStorey362.\u003C\u003Ef__this = this;
          QuestBookmarkWindow.ItemAndQuests dataOfClass = DataSource.FindDataOfClass<QuestBookmarkWindow.ItemAndQuests>(current, (QuestBookmarkWindow.ItemAndQuests) null);
          QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
          bool flag = false;
          // ISSUE: reference to a compiler-generated field
          unitCAnonStorey362.currentTime = Network.GetServerTime();
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          QuestBookmarkWindow.\u003CSetDeactivateNotAvailableUnit\u003Ec__AnonStorey363 unitCAnonStorey363 = new QuestBookmarkWindow.\u003CSetDeactivateNotAvailableUnit\u003Ec__AnonStorey363();
          // ISSUE: reference to a compiler-generated field
          unitCAnonStorey363.\u003C\u003Ef__ref\u0024866 = unitCAnonStorey362;
          // ISSUE: reference to a compiler-generated field
          unitCAnonStorey363.\u003C\u003Ef__this = this;
          using (List<QuestParam>.Enumerator enumerator2 = dataOfClass.quests.GetEnumerator())
          {
            while (enumerator2.MoveNext())
            {
              // ISSUE: reference to a compiler-generated field
              unitCAnonStorey363.quest = enumerator2.Current;
              // ISSUE: reference to a compiler-generated method
              if (((IEnumerable<QuestParam>) availableQuests).Any<QuestParam>(new Func<QuestParam, bool>(unitCAnonStorey363.\u003C\u003Em__3E2)))
              {
                flag = true;
                break;
              }
            }
          }
          ((BookmarkUnit) current.GetComponent<BookmarkUnit>()).Overlay.SetActive(!flag);
        }
      }
    }

    private bool IsAvailableQuest(QuestParam questParam, long currentTime)
    {
      return !string.IsNullOrEmpty(questParam.ChapterID) && !questParam.IsMulti && questParam.IsDateUnlock(currentTime);
    }

    private void OnSectionSelect(SRPG_Button button)
    {
      string dataOfClass = DataSource.FindDataOfClass<string>(((Component) button).get_gameObject(), (string) null);
      if (dataOfClass == this.mLastSectionName)
        return;
      if (dataOfClass != this.BookmarkSectionName && !((IEnumerable<string>) this.mAvailableSections).Contains<string>(dataOfClass))
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.TXT_QUESTBOOKMARK_BOOKMARK_NOT_AVAIABLE_SECTION", new object[1]
        {
          (object) Array.IndexOf<SRPG_Button>(this.ButtonSections, button)
        }), (UIUtility.DialogResultEvent) null, (GameObject) null, true, -1);
      else
        this.RefreshSection(dataOfClass, button);
    }

    private void RefreshSection(int index)
    {
      if (index >= this.ButtonSections.Length)
        return;
      SRPG_Button buttonSection = this.ButtonSections[index];
      this.RefreshSection(DataSource.FindDataOfClass<string>(((Component) buttonSection).get_gameObject(), (string) null), buttonSection);
    }

    private void RefreshSection(string sectionName, SRPG_Button button)
    {
      using (List<GameObject>.Enumerator enumerator = this.mCurrentUnitObjects.GetEnumerator())
      {
        while (enumerator.MoveNext())
          UnityEngine.Object.Destroy((UnityEngine.Object) enumerator.Current);
      }
      this.mCurrentUnitObjects.Clear();
      this.CreateUnitPanels((IEnumerable<QuestBookmarkWindow.ItemAndQuests>) this.mSectionToPieces[sectionName], sectionName);
      if (this.mIsBookmarkEditing && this.mBookmarkedPieces.Count >= this.MaxBookmarkCount)
        this.SetActivateWithoutBookmarkedUnit(false);
      this.ToggleSectionButton(Array.IndexOf<SRPG_Button>(this.ButtonSections, button));
      this.ResetScrollPosition();
      if (sectionName == this.BookmarkSectionName)
      {
        bool flag = this.mCurrentUnitObjects.Count <= 0;
        this.BookmarkNotFoundText.SetActive(flag);
        ((Component) this.DescriptionText).get_gameObject().SetActive(!flag);
      }
      else
      {
        this.BookmarkNotFoundText.SetActive(false);
        ((Component) this.DescriptionText).get_gameObject().SetActive(true);
      }
      this.mLastSectionName = sectionName;
    }

    private void CreateUnitPanels(IEnumerable<QuestBookmarkWindow.ItemAndQuests> targetPieces, string sectionName)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      QuestBookmarkWindow.\u003CCreateUnitPanels\u003Ec__AnonStorey365 panelsCAnonStorey365 = new QuestBookmarkWindow.\u003CCreateUnitPanels\u003Ec__AnonStorey365();
      // ISSUE: reference to a compiler-generated field
      panelsCAnonStorey365.sectionName = sectionName;
      UnitParam[] allUnits = MonoSingleton<GameManager>.Instance.MasterParam.GetAllUnits();
      Dictionary<string, QuestParam> dictionary = ((IEnumerable<QuestParam>) MonoSingleton<GameManager>.Instance.Player.AvailableQuests).ToDictionary<QuestParam, string>((Func<QuestParam, string>) (quest => quest.iname));
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      QuestBookmarkWindow.\u003CCreateUnitPanels\u003Ec__AnonStorey364 panelsCAnonStorey364 = new QuestBookmarkWindow.\u003CCreateUnitPanels\u003Ec__AnonStorey364();
      foreach (QuestBookmarkWindow.ItemAndQuests targetPiece in targetPieces)
      {
        // ISSUE: reference to a compiler-generated field
        panelsCAnonStorey364.itemQuests = targetPiece;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        QuestBookmarkWindow.\u003CCreateUnitPanels\u003Ec__AnonStorey366 panelsCAnonStorey366 = new QuestBookmarkWindow.\u003CCreateUnitPanels\u003Ec__AnonStorey366();
        // ISSUE: reference to a compiler-generated field
        panelsCAnonStorey366.\u003C\u003Ef__ref\u0024869 = panelsCAnonStorey365;
        // ISSUE: reference to a compiler-generated field
        panelsCAnonStorey366.\u003C\u003Ef__ref\u0024868 = panelsCAnonStorey364;
        GameObject root = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.ItemTemplate);
        BookmarkUnit component = (BookmarkUnit) root.GetComponent<BookmarkUnit>();
        // ISSUE: reference to a compiler-generated method
        bool flag1 = this.mBookmarkedPieces.Exists(new Predicate<QuestBookmarkWindow.ItemAndQuests>(panelsCAnonStorey366.\u003C\u003Em__3E4));
        component.BookmarkIcon.SetActive(flag1);
        long serverTime = Network.GetServerTime();
        bool flag2 = false;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        // ISSUE: reference to a compiler-generated field
        foreach (QuestParam questParam1 in !(panelsCAnonStorey365.sectionName == this.BookmarkSectionName) ? panelsCAnonStorey364.itemQuests.quests.Where<QuestParam>(new Func<QuestParam, bool>(panelsCAnonStorey366.\u003C\u003Em__3E5)) : (IEnumerable<QuestParam>) panelsCAnonStorey364.itemQuests.quests)
        {
          QuestParam questParam2;
          if (dictionary.TryGetValue(questParam1.iname, out questParam2) && this.IsAvailableQuest(questParam2, serverTime))
          {
            flag2 = true;
            break;
          }
        }
        component.Overlay.SetActive(!flag2);
        component.Button.AddListener(new SRPG_Button.ButtonClickEvent(this.OnUnitSelect));
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        panelsCAnonStorey366.itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(panelsCAnonStorey364.itemQuests.itemName);
        // ISSUE: reference to a compiler-generated method
        UnitParam data = ((IEnumerable<UnitParam>) allUnits).FirstOrDefault<UnitParam>(new Func<UnitParam, bool>(panelsCAnonStorey366.\u003C\u003Em__3E6));
        // ISSUE: reference to a compiler-generated field
        DataSource.Bind<ItemParam>(root, panelsCAnonStorey366.itemParam);
        DataSource.Bind<UnitParam>(root, data);
        // ISSUE: reference to a compiler-generated field
        DataSource.Bind<QuestBookmarkWindow.ItemAndQuests>(root, panelsCAnonStorey364.itemQuests);
        root.get_transform().SetParent(this.ItemContainer.get_transform(), false);
        this.mCurrentUnitObjects.Add(root);
        GameParameter.UpdateAll(root);
        root.SetActive(true);
      }
    }

    private void OnUnitSelect(SRPG_Button button)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      QuestBookmarkWindow.\u003COnUnitSelect\u003Ec__AnonStorey367 selectCAnonStorey367 = new QuestBookmarkWindow.\u003COnUnitSelect\u003Ec__AnonStorey367();
      // ISSUE: reference to a compiler-generated field
      selectCAnonStorey367.\u003C\u003Ef__this = this;
      if (!((Selectable) button).get_interactable())
        return;
      QuestBookmarkWindow.ItemAndQuests dataOfClass1 = DataSource.FindDataOfClass<QuestBookmarkWindow.ItemAndQuests>(((Component) button).get_gameObject(), (QuestBookmarkWindow.ItemAndQuests) null);
      // ISSUE: reference to a compiler-generated field
      selectCAnonStorey367.currentTime = Network.GetServerTime();
      QuestParam[] availableQuests = MonoSingleton<GameManager>.Instance.Player.AvailableQuests;
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated method
      QuestParam[] questParamArray = !(this.mLastSectionName == this.BookmarkSectionName) ? dataOfClass1.quests.Where<QuestParam>(new Func<QuestParam, bool>(selectCAnonStorey367.\u003C\u003Em__3E8)).ToArray<QuestParam>() : QuestDropParam.Instance.GetItemDropQuestList(MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(dataOfClass1.itemName), GlobalVars.GetDropTableGeneratedDateTime()).Where<QuestParam>(new Func<QuestParam, bool>(selectCAnonStorey367.\u003C\u003Em__3E7)).ToArray<QuestParam>();
      if (questParamArray.Length <= 0)
        return;
      List<QuestParam> questParamList = new List<QuestParam>();
      foreach (QuestParam questParam1 in questParamArray)
      {
        // ISSUE: reference to a compiler-generated method
        foreach (QuestParam questParam2 in ((IEnumerable<QuestParam>) availableQuests).Where<QuestParam>(new Func<QuestParam, bool>(selectCAnonStorey367.\u003C\u003Em__3E9)))
        {
          if (questParam1.iname == questParam2.iname)
            questParamList.Add(questParam1);
        }
      }
      if (questParamList.Count <= 0)
      {
        QuestParam questParam = questParamArray[0];
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys.TXT_QUESTBOOKMARK_BOOKMARK_NOT_AVAIABLE_QUEST", (object) questParam.title, (object) questParam.name), (UIUtility.DialogResultEvent) null, (GameObject) null, true, -1);
      }
      else if (this.mIsBookmarkEditing)
        this.OnUnitSelectBookmark(dataOfClass1, (BookmarkUnit) ((Component) button).GetComponent<BookmarkUnit>());
      else if (questParamArray.Length > 1)
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.QuestSelectorTemplate, (UnityEngine.Object) null))
          return;
        GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.QuestSelectorTemplate);
        gameObject.get_transform().SetParent(((Component) this).get_transform().get_parent(), false);
        QuestBookmarkKakeraWindow component = (QuestBookmarkKakeraWindow) gameObject.GetComponent<QuestBookmarkKakeraWindow>();
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          return;
        UnitParam dataOfClass2 = DataSource.FindDataOfClass<UnitParam>(((Component) button).get_gameObject(), (UnitParam) null);
        component.Refresh(dataOfClass2, (IEnumerable<QuestParam>) questParamArray);
      }
      else
      {
        GlobalVars.SelectedQuestID = questParamArray[0].iname;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
    }

    private void SetActivateWithoutBookmarkedUnit(bool doActivate)
    {
      using (List<GameObject>.Enumerator enumerator = this.mCurrentUnitObjects.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          GameObject current = enumerator.Current;
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          QuestBookmarkWindow.\u003CSetActivateWithoutBookmarkedUnit\u003Ec__AnonStorey368 unitCAnonStorey368 = new QuestBookmarkWindow.\u003CSetActivateWithoutBookmarkedUnit\u003Ec__AnonStorey368();
          BookmarkUnit component = (BookmarkUnit) current.GetComponent<BookmarkUnit>();
          // ISSUE: reference to a compiler-generated field
          unitCAnonStorey368.param = DataSource.FindDataOfClass<ItemParam>(((Component) component).get_gameObject(), (ItemParam) null);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated method
          if (unitCAnonStorey368.param != null && this.mBookmarkedPieces.FirstOrDefault<QuestBookmarkWindow.ItemAndQuests>(new Func<QuestBookmarkWindow.ItemAndQuests, bool>(unitCAnonStorey368.\u003C\u003Em__3EA)) == null)
          {
            ((Selectable) component.Button).set_interactable(doActivate);
            component.Overlay.SetActive(!doActivate);
          }
        }
      }
    }

    private bool AddBookmark(QuestBookmarkWindow.ItemAndQuests item)
    {
      if (this.mBookmarkedPieces.Count > this.MaxBookmarkCount)
        return false;
      this.mBookmarkedPieces.Add(item);
      if (this.mBookmarkedPieces.Count >= this.MaxBookmarkCount)
        this.SetActivateWithoutBookmarkedUnit(false);
      return true;
    }

    private void DeleteBookmark(QuestBookmarkWindow.ItemAndQuests item)
    {
      this.mBookmarkedPieces.RemoveAt(this.mBookmarkedPieces.FindIndex((Predicate<QuestBookmarkWindow.ItemAndQuests>) (p => p.itemName == item.itemName)));
      if (this.mBookmarkedPieces.Count >= this.MaxBookmarkCount)
        return;
      this.SetActivateWithoutBookmarkedUnit(true);
      this.SetDeactivateNotAvailableUnit();
    }

    private void OnUnitSelectBookmark(QuestBookmarkWindow.ItemAndQuests target, BookmarkUnit unit)
    {
      if (this.mBookmarkedPieces.Exists((Predicate<QuestBookmarkWindow.ItemAndQuests>) (p => p.itemName == target.itemName)))
      {
        this.DeleteBookmark(target);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) unit, (UnityEngine.Object) null))
          return;
        unit.BookmarkIcon.SetActive(false);
      }
      else
      {
        bool flag = this.AddBookmark(target);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) unit, (UnityEngine.Object) null) || !flag)
          return;
        unit.BookmarkIcon.SetActive(true);
      }
    }

    private class ItemAndQuests
    {
      public string itemName;
      public List<QuestParam> quests;
    }

    public class JSON_Body
    {
      public QuestBookmarkWindow.JSON_Item[] result;
    }

    public class JSON_Item
    {
      public string iname;
    }
  }
}
