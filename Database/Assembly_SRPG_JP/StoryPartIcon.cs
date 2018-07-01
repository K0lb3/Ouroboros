// Decompiled with JetBrains decompiler
// Type: SRPG.StoryPartIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class StoryPartIcon : MonoBehaviour
  {
    private const string ANIMATION_RELEASE_NAME = "open";
    private const float FADE_TIME = 0.1f;
    [SerializeField]
    private GameObject IconGo;
    [SerializeField]
    private GameObject LockGo;
    [SerializeField]
    private ImageArray LockCover;
    [SerializeField]
    private ImageArray LockCoverPart;
    [SerializeField]
    private ImageArray LockOpen;
    [SerializeField]
    private ImageArray BlurIcon;
    [SerializeField]
    private ImageArray BlurIcon2;
    [SerializeField]
    private Text TxtConditions;
    [SerializeField]
    private GameObject IconMask;
    [SerializeField]
    private GameObject LockMask;
    [SerializeField]
    private GameObject LockTitleMask;
    [SerializeField]
    private CanvasGroup mCanvasGroup;
    private int mStoryNum;
    private bool mLockFlag;
    private float mCountDelat;
    private StoryPartIcon.AlphaState mAlphaState;

    public StoryPartIcon()
    {
      base.\u002Ector();
    }

    public int StoryNum
    {
      get
      {
        return this.mStoryNum;
      }
    }

    public bool LockFlag
    {
      get
      {
        return this.mLockFlag;
      }
    }

    private void Start()
    {
    }

    private void Update()
    {
      if (Object.op_Equality((Object) this.mCanvasGroup, (Object) null))
        return;
      switch (this.mAlphaState)
      {
        case StoryPartIcon.AlphaState.Fadeout:
          this.mCountDelat += Time.get_deltaTime();
          if ((double) this.mCountDelat > 0.100000001490116)
          {
            this.mCanvasGroup.set_alpha(1f);
            break;
          }
          this.mCanvasGroup.set_alpha(this.mCountDelat / 0.1f);
          break;
        case StoryPartIcon.AlphaState.Fadein:
          this.mCountDelat += Time.get_deltaTime();
          if ((double) this.mCountDelat > 0.100000001490116)
          {
            this.mCanvasGroup.set_alpha(0.0f);
            break;
          }
          this.mCanvasGroup.set_alpha((float) (1.0 - (double) this.mCountDelat / 0.100000001490116));
          break;
      }
    }

    public bool Setup(bool lock_flag, int story_part)
    {
      this.mStoryNum = 1;
      this.mLockFlag = false;
      this.mAlphaState = StoryPartIcon.AlphaState.None;
      if (Object.op_Equality((Object) this.IconGo, (Object) null) || Object.op_Equality((Object) this.LockGo, (Object) null) || (Object.op_Equality((Object) this.LockCover, (Object) null) || Object.op_Equality((Object) this.LockCoverPart, (Object) null)) || (Object.op_Equality((Object) this.LockOpen, (Object) null) || Object.op_Equality((Object) this.BlurIcon, (Object) null) || Object.op_Equality((Object) this.BlurIcon2, (Object) null)))
        return false;
      ImageArray component = (ImageArray) this.IconGo.GetComponent<ImageArray>();
      if (Object.op_Equality((Object) component, (Object) null) || story_part > component.Images.Length)
        return false;
      int num = story_part - 1;
      this.mStoryNum = story_part;
      this.mLockFlag = lock_flag;
      if (!lock_flag)
      {
        this.IconGo.SetActive(true);
        this.LockGo.SetActive(false);
        this.IconMask.SetActive(true);
        this.LockMask.SetActive(false);
        this.LockTitleMask.SetActive(false);
        component.ImageIndex = num;
      }
      else
      {
        component.ImageIndex = num;
        this.IconGo.SetActive(false);
        this.LockGo.SetActive(true);
        this.IconMask.SetActive(false);
        this.LockMask.SetActive(true);
        this.LockTitleMask.SetActive(true);
        this.LockCover.ImageIndex = num;
        this.LockCoverPart.ImageIndex = num;
        this.LockOpen.ImageIndex = num;
        this.BlurIcon.ImageIndex = num;
        this.BlurIcon2.ImageIndex = num;
        if (Object.op_Inequality((Object) this.TxtConditions, (Object) null))
        {
          string storyPartWorldName = MonoSingleton<GameManager>.Instance.GetReleaseStoryPartWorldName(this.StoryNum);
          if (storyPartWorldName != null)
            this.TxtConditions.set_text(string.Format(LocalizedText.Get("sys.STORYPART_RELEASE_TIMING"), (object) storyPartWorldName));
        }
      }
      ((ImageArray) this.IconMask.GetComponent<ImageArray>()).ImageIndex = num;
      ((ImageArray) this.LockMask.GetComponent<ImageArray>()).ImageIndex = num;
      ((ImageArray) this.LockTitleMask.GetComponent<ImageArray>()).ImageIndex = num;
      return true;
    }

    public bool PlayReleaseAnim()
    {
      if (!this.mLockFlag)
        return false;
      ((Animator) this.LockGo.GetComponent<Animator>()).Play("open");
      return true;
    }

    public bool IsPlayingReleaseAnim()
    {
      if (!this.mLockFlag)
        return false;
      bool flag = false;
      AnimatorStateInfo animatorStateInfo = ((Animator) this.LockGo.GetComponent<Animator>()).GetCurrentAnimatorStateInfo(0);
      // ISSUE: explicit reference operation
      if ((double) ((AnimatorStateInfo) @animatorStateInfo).get_normalizedTime() < 1.0)
        flag = true;
      return flag;
    }

    public void ReleaseIcon()
    {
      this.IconGo.SetActive(true);
      this.IconMask.SetActive(true);
      this.LockGo.SetActive(false);
      this.LockMask.SetActive(false);
      this.LockTitleMask.SetActive(false);
      this.mLockFlag = false;
    }

    public void SetMask(bool mask_flag)
    {
      if (mask_flag)
      {
        if (this.mAlphaState != StoryPartIcon.AlphaState.Fadeout)
          this.mCountDelat = 0.0f;
        this.mAlphaState = StoryPartIcon.AlphaState.Fadeout;
      }
      else
      {
        if (this.mAlphaState != StoryPartIcon.AlphaState.Fadein)
          this.mCountDelat = 0.0f;
        this.mAlphaState = StoryPartIcon.AlphaState.Fadein;
      }
    }

    private enum AlphaState
    {
      None,
      Fadeout,
      Fadein,
    }
  }
}
