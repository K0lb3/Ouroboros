// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_MovieImage
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "Finished", FlowNode.PinTypes.Output, 1)]
  [RequireComponent(typeof (CriManaMovieControllerForUI))]
  [FlowNode.Pin(9, "Started", FlowNode.PinTypes.Output, 0)]
  public class SRPG_MovieImage : RawImage, IFlowInterface
  {
    public const int PINID_STARTED = 9;
    public const int PINID_FINISHED = 10;
    private CriManaMovieControllerForUI mMovieController;
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
      this.mMovieController = (CriManaMovieControllerForUI) ((Component) this).GetComponent<CriManaMovieControllerForUI>();
      if (!Object.op_Inequality((Object) this.mMovieController, (Object) null))
        return;
      ((CriManaMovieMaterial) this.mMovieController).set_moviePath(MyCriManager.GetLoadFileName(((CriManaMovieMaterial) this.mMovieController).get_moviePath(), false));
    }

    private void Update()
    {
      if (!Object.op_Inequality((Object) this.mMovieController, (Object) null) || ((CriManaMovieMaterial) this.mMovieController).get_player().get_status() < 5)
        return;
      ((Graphic) this).set_material(((CriManaMovieMaterial) this.mMovieController).get_material());
      ((Graphic) this).UpdateMaterial();
      if (((CriManaMovieMaterial) this.mMovieController).get_player().get_status() == 5)
      {
        if (this.mPlaying)
          return;
        this.mPlaying = true;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 9);
      }
      else
      {
        if (((CriManaMovieMaterial) this.mMovieController).get_player().get_status() != 6 || !this.mPlaying)
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
