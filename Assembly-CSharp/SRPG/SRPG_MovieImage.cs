// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_MovieImage
// Assembly: Assembly-CSharp, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9BA76916-D0BD-4DB6-A90B-FE0BCC53E511
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(9, "Started", FlowNode.PinTypes.Output, 0)]
  [FlowNode.Pin(10, "Finished", FlowNode.PinTypes.Output, 1)]
  [RequireComponent(typeof (CriManaPlayer))]
  public class SRPG_MovieImage : RawImage, IFlowInterface
  {
    public const int PINID_STARTED = 9;
    public const int PINID_FINISHED = 10;
    private CriManaPlayer mMoviePlayer;
    private bool mPlaying;

    public SRPG_MovieImage()
    {
      base.\u002Ector();
    }

    protected virtual void Awake()
    {
      ((UIBehaviour) this).Awake();
      GameUtility.SetNeverSleep();
      if (!Application.get_isPlaying())
        return;
      MyCriManager.Setup(false);
      this.mMoviePlayer = (CriManaPlayer) ((Component) this).GetComponent<CriManaPlayer>();
      if (!Object.op_Inequality((Object) this.mMoviePlayer, (Object) null))
        return;
      if (!GameUtility.Config_UseAssetBundles.Value)
        this.mMoviePlayer.set_moviePath(AssetManager.GetLocalizedObjectName("StreamingAssets/" + MyCriManager.GetLoadFileName(this.mMoviePlayer.get_moviePath()), false).Replace("StreamingAssets/", string.Empty));
      else
        this.mMoviePlayer.set_moviePath(MyCriManager.GetLoadFileName(AssetManager.GetLocalizedObjectName("StreamingAssets/" + this.mMoviePlayer.get_moviePath(), false).Replace("StreamingAssets/", string.Empty)));
    }

    private void Update()
    {
      if (!Object.op_Inequality((Object) this.mMoviePlayer, (Object) null) || this.mMoviePlayer.get_status() < 5)
        return;
      ((Graphic) this).set_material(this.mMoviePlayer.get_movieMaterial());
      ((Graphic) this).UpdateMaterial();
      if (this.mMoviePlayer.get_status() == 5)
      {
        if (this.mPlaying)
          return;
        this.mPlaying = true;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 9);
      }
      else
      {
        if (this.mMoviePlayer.get_status() != 6 || !this.mPlaying)
          return;
        this.mPlaying = false;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
      }
    }

    protected virtual void OnDestroy()
    {
      GameUtility.SetDefaultSleepSetting();
      ((UIBehaviour) this).OnDestroy();
    }

    public void Activated(int pinID)
    {
    }
  }
}
