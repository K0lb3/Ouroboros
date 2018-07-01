// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactRarityCheck
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(2, "キャンセルを選択", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(100, "ウィンドウを閉じる", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(1, "売却を選択", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(0, "分解を選択", FlowNode.PinTypes.Input, 0)]
  public class ArtifactRarityCheck : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_DECOMPOSE = 0;
    private const int INPUT_SELL = 1;
    private const int INPUT_CANCEL = 2;
    private const int OUTPUT_CLOSE_ACTION = 100;
    [SerializeField]
    private GameObject mArtifactTemplate;
    [SerializeField]
    private RectTransform mArtifactObjectParent;
    [SerializeField]
    private LText mLText;
    [SerializeField]
    private GameObject mButtonDecompose;
    [SerializeField]
    private GameObject mButtonSell;
    private ArtifactRarityCheck.Type mType;
    public ArtifactRarityCheck.OnArtifactRarityCheckDecideEvent OnDecideEvent;
    public ArtifactRarityCheck.OnArtifactRarityCheckCancelEvent OnCancelEvent;
    private GameObject mArgGo;
    private List<ArtifactData> mArtifactDataList;
    private int mRarity;

    public ArtifactRarityCheck()
    {
      base.\u002Ector();
    }

    private void Start()
    {
      if (Object.op_Equality((Object) this.mArgGo, (Object) null) || this.mArtifactDataList == null || (this.mType == ArtifactRarityCheck.Type.NONE || Object.op_Equality((Object) this.mLText, (Object) null)))
        return;
      if (Object.op_Inequality((Object) this.mButtonDecompose, (Object) null) && this.mType == ArtifactRarityCheck.Type.DECOMPOSE)
        this.mButtonSell.SetActive(false);
      else if (Object.op_Inequality((Object) this.mButtonSell, (Object) null) && this.mType == ArtifactRarityCheck.Type.SELL)
        this.mButtonDecompose.SetActive(false);
      this.mLText.set_text(string.Format(LocalizedText.Get(this.mLText.get_text()), (object) this.mRarity.ToString()));
      this.mArtifactTemplate.SetActive(false);
      using (List<ArtifactData>.Enumerator enumerator = this.mArtifactDataList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ArtifactData current = enumerator.Current;
          if ((int) current.Rarity + 1 >= this.mRarity)
          {
            GameObject gameObject = (GameObject) Object.Instantiate<GameObject>((M0) this.mArtifactTemplate);
            gameObject.get_transform().SetParent((Transform) this.mArtifactObjectParent, false);
            DataSource.Bind<ArtifactData>(gameObject, current);
            gameObject.SetActive(true);
          }
        }
      }
    }

    public void Setup(ArtifactRarityCheck.Type type, GameObject arg_go, ArtifactData[] artifact_data, int rarity)
    {
      this.mType = type;
      this.mArgGo = arg_go;
      this.mArtifactDataList = new List<ArtifactData>((IEnumerable<ArtifactData>) artifact_data);
      this.mRarity = rarity;
    }

    private void OnDecide()
    {
      if (this.OnDecideEvent == null)
        return;
      this.OnDecideEvent(this.mArgGo);
    }

    private void OnCancel()
    {
      if (this.OnCancelEvent == null)
        return;
      this.OnCancelEvent(this.mArgGo);
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 0:
          this.OnDecide();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 1:
          this.OnDecide();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
        case 2:
          this.OnCancel();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
          break;
      }
    }

    public enum Type
    {
      NONE,
      SELL,
      DECOMPOSE,
    }

    public delegate void OnArtifactRarityCheckDecideEvent(GameObject go);

    public delegate void OnArtifactRarityCheckCancelEvent(GameObject go);
  }
}
