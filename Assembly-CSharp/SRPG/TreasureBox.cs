// Decompiled with JetBrains decompiler
// Type: SRPG.TreasureBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class TreasureBox : MonoBehaviour
  {
    public AnimationClip OpenAnimation;
    public float DropDelay;
    public float GoldDelay;
    public Vector3 DropOffset;
    private DropItemEffect mDropItem;
    private bool mDropSpawned;
    private bool mGoldSpawned;
    private bool mOpened;
    private DropGoldEffect mDropGold;

    public TreasureBox()
    {
      base.\u002Ector();
    }

    public Unit owner { set; get; }

    private void Start()
    {
    }

    private void OnDestroy()
    {
      GameUtility.DestroyGameObject((Component) this.mDropItem);
      GameUtility.DestroyGameObject((Component) this.mDropGold);
    }

    public bool IsPlaying()
    {
      if (Object.op_Inequality((Object) ((Component) this).get_gameObject(), (Object) null))
        return ((Animation) ((Component) this).GetComponent<Animation>()).get_isPlaying();
      return false;
    }

    private void Update()
    {
      if (!this.mOpened)
        return;
      if (!this.mDropSpawned && Object.op_Inequality((Object) this.mDropItem, (Object) null))
      {
        this.DropDelay -= Time.get_deltaTime();
        if ((double) this.DropDelay <= 0.0)
        {
          this.mDropSpawned = true;
          ((Component) this.mDropItem).get_gameObject().SetActive(true);
        }
      }
      if (!this.mGoldSpawned && Object.op_Inequality((Object) this.mDropGold, (Object) null))
      {
        this.GoldDelay -= Time.get_deltaTime();
        if ((double) this.GoldDelay <= 0.0)
        {
          this.mGoldSpawned = true;
          ((Component) this.mDropGold).get_gameObject().SetActive(true);
          this.mDropGold = (DropGoldEffect) null;
        }
      }
      if (this.IsPlaying() || Object.op_Implicit((Object) this.mDropGold) || Object.op_Implicit((Object) this.mDropItem))
        return;
      Object.Destroy((Object) ((Component) this).get_gameObject());
    }

    public void Open(Unit.DropItem DropItem, DropItemEffect dropItemTemplate, int numGolds, DropGoldEffect dropGoldTemplate)
    {
      ((Animation) ((Component) this).GetComponent<Animation>()).AddClip(this.OpenAnimation, ((Object) this.OpenAnimation).get_name());
      ((Animation) ((Component) this).GetComponent<Animation>()).Play(((Object) this.OpenAnimation).get_name());
      this.mOpened = true;
      Transform transform = ((Component) this).get_transform();
      if (numGolds > 0)
      {
        this.mDropGold = (DropGoldEffect) Object.Instantiate<DropGoldEffect>((M0) dropGoldTemplate);
        ((Component) this.mDropGold).get_transform().set_position(transform.get_position());
        this.mDropGold.DropOwner = this.owner;
        this.mDropGold.Gold = numGolds;
        ((Component) this.mDropGold).get_gameObject().SetActive(false);
      }
      if (!Object.op_Inequality((Object) dropItemTemplate, (Object) null) || DropItem == null)
        return;
      this.mDropItem = (DropItemEffect) Object.Instantiate<DropItemEffect>((M0) dropItemTemplate);
      ((Component) this.mDropItem).get_transform().set_position(Vector3.op_Addition(transform.get_position(), this.DropOffset));
      if (Object.op_Inequality((Object) SceneBattle.Instance, (Object) null))
        SceneBattle.Popup2D(((Component) this.mDropItem).get_gameObject(), ((Component) this.mDropItem).get_transform().get_position(), 0, 0.0f);
      this.mDropItem.DropOwner = this.owner;
      this.mDropItem.DropItem = DropItem;
      ((Component) this.mDropItem).get_gameObject().SetActive(false);
    }
  }
}
