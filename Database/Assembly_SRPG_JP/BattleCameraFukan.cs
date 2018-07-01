// Decompiled with JetBrains decompiler
// Type: SRPG.BattleCameraFukan
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleCameraFukan : MonoBehaviour
  {
    public BattleCameraControl BattleCameraControl;
    public GameObject DefaultButton;
    public GameObject UpViewButton;
    private bool m_Disp;

    public BattleCameraFukan()
    {
      base.\u002Ector();
    }

    public bool isDisp
    {
      get
      {
        return this.m_Disp;
      }
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.DefaultButton, (Object) null))
        this.SetButtonEvent(this.DefaultButton, (BattleCameraFukan.ClickEvent) (() => this.OnClick(BattleCameraFukan.ButtonType.DEFAULT)));
      if (Object.op_Inequality((Object) this.UpViewButton, (Object) null))
        this.SetButtonEvent(this.UpViewButton, (BattleCameraFukan.ClickEvent) (() => this.OnClick(BattleCameraFukan.ButtonType.UPVIEW)));
      this.SetDisp(false);
    }

    private void Update()
    {
    }

    private Button SetButtonEvent(GameObject go, BattleCameraFukan.ClickEvent callback)
    {
      Button component = (Button) go.GetComponent<Button>();
      if (Object.op_Inequality((Object) component, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) component.get_onClick()).AddListener(new UnityAction((object) callback, __methodptr(Invoke)));
      }
      return component;
    }

    public void SetCameraMode(SceneBattle.CameraMode mode)
    {
      SceneBattle instance = SceneBattle.Instance;
      if (!Object.op_Inequality((Object) instance, (Object) null) || !instance.IsControlBattleUI(SceneBattle.eMaskBattleUI.CAMERA))
        return;
      instance.OnCameraModeChange(mode);
      switch (mode)
      {
        case SceneBattle.CameraMode.DEFAULT:
          if (Object.op_Inequality((Object) this.DefaultButton, (Object) null))
            this.DefaultButton.SetActive(false);
          if (Object.op_Inequality((Object) this.UpViewButton, (Object) null))
            this.UpViewButton.SetActive(true);
          if (!Object.op_Inequality((Object) this.BattleCameraControl, (Object) null))
            break;
          this.BattleCameraControl.SetDisp(true);
          break;
        case SceneBattle.CameraMode.UPVIEW:
          if (Object.op_Inequality((Object) this.DefaultButton, (Object) null))
            this.DefaultButton.SetActive(true);
          if (Object.op_Inequality((Object) this.UpViewButton, (Object) null))
            this.UpViewButton.SetActive(false);
          if (!Object.op_Inequality((Object) this.BattleCameraControl, (Object) null))
            break;
          this.BattleCameraControl.SetDisp(false);
          break;
      }
    }

    public void SetDisp(bool value)
    {
      Animator component = (Animator) ((Component) this).GetComponent<Animator>();
      if (Object.op_Inequality((Object) component, (Object) null))
        component.SetBool("open", value);
      if (value)
      {
        SceneBattle instance = SceneBattle.Instance;
        if (Object.op_Inequality((Object) instance, (Object) null))
        {
          if (instance.isUpView)
          {
            if (Object.op_Inequality((Object) this.DefaultButton, (Object) null))
              this.DefaultButton.SetActive(true);
            if (Object.op_Inequality((Object) this.UpViewButton, (Object) null))
              this.UpViewButton.SetActive(false);
          }
          else
          {
            if (Object.op_Inequality((Object) this.DefaultButton, (Object) null))
              this.DefaultButton.SetActive(false);
            if (Object.op_Inequality((Object) this.UpViewButton, (Object) null))
              this.UpViewButton.SetActive(true);
          }
        }
        else
        {
          if (Object.op_Inequality((Object) this.DefaultButton, (Object) null))
            this.DefaultButton.SetActive(true);
          if (Object.op_Inequality((Object) this.UpViewButton, (Object) null))
            this.UpViewButton.SetActive(true);
        }
      }
      else
      {
        if (Object.op_Inequality((Object) this.DefaultButton, (Object) null))
          this.DefaultButton.SetActive(false);
        if (Object.op_Inequality((Object) this.UpViewButton, (Object) null))
          this.UpViewButton.SetActive(false);
      }
      this.m_Disp = value;
    }

    private void OnClick(BattleCameraFukan.ButtonType buttonType)
    {
      if (!Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
        return;
      if (buttonType == BattleCameraFukan.ButtonType.DEFAULT)
      {
        this.SetCameraMode(SceneBattle.CameraMode.DEFAULT);
      }
      else
      {
        if (buttonType != BattleCameraFukan.ButtonType.UPVIEW)
          return;
        this.SetCameraMode(SceneBattle.CameraMode.UPVIEW);
      }
    }

    public void OnEventCall(string key, string value)
    {
      string key1 = key;
      if (key1 == null)
        return;
      // ISSUE: reference to a compiler-generated field
      if (BattleCameraFukan.\u003C\u003Ef__switch\u0024map6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        BattleCameraFukan.\u003C\u003Ef__switch\u0024map6 = new Dictionary<string, int>(1)
        {
          {
            "DISP",
            0
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (!BattleCameraFukan.\u003C\u003Ef__switch\u0024map6.TryGetValue(key1, out num) || num != 0)
        return;
      if (value == "on")
        this.SetDisp(true);
      else
        this.SetDisp(false);
    }

    private enum ButtonType
    {
      DEFAULT,
      UPVIEW,
    }

    private delegate void ClickEvent();
  }
}
