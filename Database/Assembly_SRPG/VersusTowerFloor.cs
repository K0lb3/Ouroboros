// Decompiled with JetBrains decompiler
// Type: SRPG.VersusTowerFloor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class VersusTowerFloor : MonoBehaviour
  {
    public Text FriendName;
    public Text FloorText;
    public GameObject UnitObj;
    public GameObject FloorInfoObj;
    public GameObject RingObj;
    public Sprite CurrentSprite;
    public Sprite DefaultSprite;
    public Image FloorImage;
    private int mCurrentFloor;

    public VersusTowerFloor()
    {
      base.\u002Ector();
    }

    public int Floor
    {
      get
      {
        return this.mCurrentFloor;
      }
    }

    public void Refresh(int idx, int max)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int versusTowerFloor = instance.Player.VersusTowerFloor;
      int floor = idx + 1;
      if (idx >= 0 && idx < max)
      {
        this.mCurrentFloor = floor;
        if (Object.op_Inequality((Object) this.FloorInfoObj, (Object) null))
          this.FloorInfoObj.SetActive(true);
        if (Object.op_Inequality((Object) this.FloorText, (Object) null))
        {
          this.FloorText.set_text(floor.ToString() + LocalizedText.Get("sys.MULTI_VERSUS_FLOOR"));
          if (floor == versusTowerFloor)
            ((Graphic) this.FloorText).set_color(new Color(1f, 1f, 0.0f));
          else
            ((Graphic) this.FloorText).set_color(new Color(1f, 1f, 1f));
        }
        if (Object.op_Inequality((Object) this.FloorImage, (Object) null))
        {
          if (floor == versusTowerFloor)
          {
            if (Object.op_Inequality((Object) this.CurrentSprite, (Object) null))
              this.FloorImage.set_sprite(this.CurrentSprite);
          }
          else if (Object.op_Inequality((Object) this.DefaultSprite, (Object) null))
            this.FloorImage.set_sprite(this.DefaultSprite);
        }
        if (Object.op_Inequality((Object) this.RingObj, (Object) null))
          this.RingObj.SetActive(versusTowerFloor == floor);
        VersusFriendScore[] versusFriendScore = instance.GetVersusFriendScore(floor);
        if (versusFriendScore != null && versusFriendScore.Length > 0 && floor != versusTowerFloor)
        {
          int length = versusFriendScore.Length;
          string empty = string.Empty;
          if (Object.op_Inequality((Object) this.UnitObj, (Object) null))
          {
            this.UnitObj.SetActive(true);
            DataSource.Bind<UnitData>(this.UnitObj, versusFriendScore[0].unit);
            GameParameter.UpdateAll(this.UnitObj);
          }
          int num = length - 1;
          string str = num <= 0 ? versusFriendScore[0].name : string.Format(LocalizedText.Get("sys.MULTI_VERSUS_FRIEND_NAME"), (object) versusFriendScore[0].name, (object) num);
          if (!Object.op_Inequality((Object) this.FriendName, (Object) null))
            return;
          this.FriendName.set_text(str);
        }
        else
        {
          if (!Object.op_Inequality((Object) this.UnitObj, (Object) null))
            return;
          this.UnitObj.SetActive(false);
        }
      }
      else
      {
        this.mCurrentFloor = -1;
        if (Object.op_Inequality((Object) this.FloorInfoObj, (Object) null))
          this.FloorInfoObj.SetActive(false);
        if (Object.op_Inequality((Object) this.RingObj, (Object) null))
          this.RingObj.SetActive(false);
        if (!Object.op_Inequality((Object) this.FloorImage, (Object) null) || !Object.op_Inequality((Object) this.DefaultSprite, (Object) null))
          return;
        this.FloorImage.set_sprite(this.DefaultSprite);
      }
    }

    public void SetPlayerObject(GameObject playerObj)
    {
      if (!Object.op_Inequality((Object) playerObj, (Object) null))
        return;
      playerObj.SetActive(true);
      if (!Object.op_Inequality((Object) this.RingObj, (Object) null))
        return;
      playerObj.get_transform().set_position(this.RingObj.get_transform().get_position());
    }
  }
}
