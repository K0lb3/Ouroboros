// Decompiled with JetBrains decompiler
// Type: SRPG.AwardItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "UpdateConfigPlayerInfo", FlowNode.PinTypes.Input, 0)]
  public class AwardItem : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject AwardBG;
    [SerializeField]
    private Text AwardTxt;
    public AwardItem.PlayerType Type;
    private ImageArray mImageArray;
    private bool IsDone;
    private string mSelectedAward;
    private bool IsRefresh;
    private AwardParam mAwardParam;

    public AwardItem()
    {
      base.\u002Ector();
    }

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.IsRefresh = false;
      this.SetUp();
    }

    private void Awake()
    {
      if (Object.op_Inequality((Object) this.AwardBG, (Object) null))
      {
        this.AwardBG.SetActive(false);
        ImageArray component = (ImageArray) this.AwardBG.GetComponent<ImageArray>();
        if (Object.op_Inequality((Object) component, (Object) null))
          this.mImageArray = component;
      }
      if (!Object.op_Inequality((Object) this.AwardTxt, (Object) null))
        return;
      this.AwardTxt.set_text(LocalizedText.Get("sys.TEXT_NOT_SELECT"));
      ((Component) this.AwardTxt).get_gameObject().SetActive(false);
    }

    private void Start()
    {
      this.Initialize();
    }

    private void OnEnable()
    {
      this.Initialize();
    }

    private void Initialize()
    {
      this.SetUp();
      this.IsRefresh = false;
    }

    private void Update()
    {
      if (!this.IsDone || this.IsRefresh)
        return;
      this.IsRefresh = true;
      this.Refresh();
    }

    private void SetUp()
    {
      string str = string.Empty;
      if (this.Type == AwardItem.PlayerType.Player)
      {
        PlayerData dataOfClass = DataSource.FindDataOfClass<PlayerData>(((Component) this).get_gameObject(), (PlayerData) null);
        if (dataOfClass != null)
          str = dataOfClass.SelectedAward;
      }
      else if (this.Type == AwardItem.PlayerType.Friend)
      {
        FriendData dataOfClass = DataSource.FindDataOfClass<FriendData>(((Component) this).get_gameObject(), (FriendData) null);
        if (dataOfClass != null)
          str = dataOfClass.SelectAward;
      }
      else if (this.Type == AwardItem.PlayerType.ArenaPlayer)
      {
        ArenaPlayer dataOfClass = DataSource.FindDataOfClass<ArenaPlayer>(((Component) this).get_gameObject(), (ArenaPlayer) null);
        if (dataOfClass != null)
          str = dataOfClass.SelectAward;
      }
      else if (this.Type == AwardItem.PlayerType.MultiPlayer)
      {
        JSON_MyPhotonPlayerParam dataOfClass = DataSource.FindDataOfClass<JSON_MyPhotonPlayerParam>(((Component) this).get_gameObject(), (JSON_MyPhotonPlayerParam) null);
        if (dataOfClass != null)
          str = dataOfClass.award;
      }
      else if (this.Type == AwardItem.PlayerType.ChatPlayer)
      {
        ChatPlayerData dataOfClass = DataSource.FindDataOfClass<ChatPlayerData>(((Component) this).get_gameObject(), (ChatPlayerData) null);
        if (dataOfClass != null)
          str = dataOfClass.award;
      }
      else if (this.Type == AwardItem.PlayerType.TowerPlayer)
      {
        TowerResuponse.TowerRankParam dataOfClass = DataSource.FindDataOfClass<TowerResuponse.TowerRankParam>(((Component) this).get_gameObject(), (TowerResuponse.TowerRankParam) null);
        if (dataOfClass != null)
          str = dataOfClass.selected_award;
      }
      this.mSelectedAward = str;
      if (!string.IsNullOrEmpty(this.mSelectedAward))
      {
        AwardParam awardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetAwardParam(this.mSelectedAward);
        if (awardParam != null)
          this.mAwardParam = awardParam;
      }
      else
        this.mAwardParam = (AwardParam) null;
      this.IsDone = true;
    }

    private void Refresh()
    {
      this.SetUp();
      if (this.mAwardParam != null)
      {
        if (Object.op_Inequality((Object) this.mImageArray, (Object) null))
        {
          if (this.mImageArray.Images.Length <= this.mAwardParam.grade)
          {
            this.SetExtraAwardImage();
            this.AwardTxt.set_text(string.Empty);
          }
          else
          {
            this.mImageArray.ImageIndex = this.mAwardParam.grade;
            this.AwardTxt.set_text(string.IsNullOrEmpty(this.mAwardParam.name) ? LocalizedText.Get("sys.TEXT_NOT_SELECT") : this.mAwardParam.name);
          }
        }
      }
      else
      {
        this.mImageArray.ImageIndex = 0;
        this.AwardTxt.set_text(LocalizedText.Get("sys.TEXT_NOT_SELECT"));
      }
      if (Object.op_Inequality((Object) this.AwardBG, (Object) null))
        this.AwardBG.SetActive(true);
      if (!Object.op_Inequality((Object) this.AwardTxt, (Object) null))
        return;
      ((Component) this.AwardTxt).get_gameObject().SetActive(true);
    }

    private bool SetExtraAwardImage()
    {
      if (this.mAwardParam == null)
        return false;
      string bg = this.mAwardParam.bg;
      if (string.IsNullOrEmpty(bg))
        return false;
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>(AwardListItem.EXTRA_GRADE_IMAGEPATH);
      if (Object.op_Inequality((Object) spriteSheet, (Object) null))
        this.mImageArray.set_sprite(spriteSheet.GetSprite(bg));
      return true;
    }

    public enum PlayerType : byte
    {
      Player,
      Friend,
      ArenaPlayer,
      MultiPlayer,
      ChatPlayer,
      TowerPlayer,
    }
  }
}
