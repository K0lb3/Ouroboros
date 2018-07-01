// Decompiled with JetBrains decompiler
// Type: SRPG.TipsInfoDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(3, "閉じる", FlowNode.PinTypes.Input, 3)]
  [FlowNode.Pin(1, "次のページボタン", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "前のページボタン", FlowNode.PinTypes.Input, 2)]
  public class TipsInfoDetail : MonoBehaviour, IFlowInterface
  {
    private const int PIN_NEXT_BUTTON = 1;
    private const int PIN_PREV_BUTTON = 2;
    private const int PIN_CLOSE = 3;
    [SerializeField]
    private ImageArray ImageData;
    [SerializeField]
    private Button PrevButton;
    [SerializeField]
    private Button NextButton;
    [SerializeField]
    private GameObject ParentPageIcon;
    [SerializeField]
    private GameObject TemplatePageIcon;
    [SerializeField]
    private Button CloseButton;
    [SerializeField]
    private BackHandler CloseButtonBackHandler;
    [SerializeField]
    private Text TitleText;
    private List<Toggle> mToggleIconList;
    private TipsParam mTipsParam;

    public TipsInfoDetail()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      this.mTipsParam = ((IEnumerable<TipsParam>) MonoSingleton<GameManager>.Instance.MasterParam.Tips).FirstOrDefault<TipsParam>((Func<TipsParam, bool>) (t => t.iname == GlobalVars.LastReadTips));
      if (this.mTipsParam == null)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.TitleText, (UnityEngine.Object) null))
        this.TitleText.set_text(this.mTipsParam.title);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CloseButton, (UnityEngine.Object) null))
        this.EnabledCloseButton(MonoSingleton<GameManager>.Instance.Tips.Contains(this.mTipsParam.iname));
      List<Sprite> spriteList = new List<Sprite>();
      SpriteSheet spriteSheet = AssetManager.Load<SpriteSheet>("Tips/tips_images");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) spriteSheet, (UnityEngine.Object) null) && this.mTipsParam.images != null)
      {
        foreach (string image in this.mTipsParam.images)
          spriteList.Add(spriteSheet.GetSprite(image));
      }
      this.ImageData.Images = spriteList.ToArray();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ImageData, (UnityEngine.Object) null) && this.ImageData.Images.Length == 0)
      {
        Debug.LogError((object) "ImageData not data.");
      }
      else
      {
        this.ImageData.ImageIndex = 0;
        this.TemplatePageIcon.SetActive(false);
        for (int index = 0; index < this.ImageData.Images.Length; ++index)
        {
          GameObject gameObject = (GameObject) UnityEngine.Object.Instantiate<GameObject>((M0) this.TemplatePageIcon);
          Vector2 vector2 = Vector2.op_Implicit(gameObject.get_transform().get_localScale());
          gameObject.get_transform().SetParent(this.ParentPageIcon.get_transform());
          gameObject.get_transform().set_localScale(Vector2.op_Implicit(vector2));
          gameObject.get_gameObject().SetActive(true);
          ((UnityEngine.Object) gameObject).set_name(((UnityEngine.Object) this.TemplatePageIcon).get_name() + (index + 1).ToString());
          this.mToggleIconList.Add((Toggle) gameObject.GetComponent<Toggle>());
        }
        this.mToggleIconList[0].set_isOn(true);
        this.SetButtonInteractable();
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          if (this.ImageData.ImageIndex >= this.ImageData.Images.Length - 1)
            break;
          ++this.ImageData.ImageIndex;
          this.SetButtonInteractable();
          this.SetTobbleIcon();
          break;
        case 2:
          if (this.ImageData.ImageIndex <= 0)
            break;
          --this.ImageData.ImageIndex;
          this.SetButtonInteractable();
          this.SetTobbleIcon();
          break;
      }
    }

    private void SetButtonInteractable()
    {
      if (this.ImageData.Images.Length == 1)
      {
        ((Selectable) this.NextButton).set_interactable(false);
        ((Selectable) this.PrevButton).set_interactable(false);
        this.EnabledCloseButton(true);
      }
      else if (this.ImageData.ImageIndex == this.ImageData.Images.Length - 1)
      {
        ((Selectable) this.NextButton).set_interactable(false);
        ((Selectable) this.PrevButton).set_interactable(true);
        this.EnabledCloseButton(true);
      }
      else
      {
        if (this.ImageData.ImageIndex != 0)
          return;
        ((Selectable) this.NextButton).set_interactable(true);
        ((Selectable) this.PrevButton).set_interactable(false);
        this.EnabledCloseButton(MonoSingleton<GameManager>.Instance.Tips.Contains(this.mTipsParam.iname));
      }
    }

    private void SetTobbleIcon()
    {
      for (int index = 0; index < this.mToggleIconList.Count; ++index)
      {
        if (index == this.ImageData.ImageIndex)
          this.mToggleIconList[index].set_isOn(true);
        else
          this.mToggleIconList[index].set_isOn(false);
      }
    }

    private void EnabledCloseButton(bool isEnable)
    {
      ((Selectable) this.CloseButton).set_interactable(isEnable);
      ((Behaviour) this.CloseButtonBackHandler).set_enabled(isEnable);
    }
  }
}
