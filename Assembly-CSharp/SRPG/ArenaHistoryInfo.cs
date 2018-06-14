// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaHistoryInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE644F5D-682F-4D6E-964D-A0DD77A288F7
// Assembly location: C:\Users\André\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ArenaHistoryInfo : MonoBehaviour
  {
    [Space(10f)]
    public Text Ranking;
    public Text created_at;
    public Text PlayerName;
    public Text PlayerLevel;
    public GameObject unit_icon;
    public ImageArray result_image;
    public ImageArray ranking_delta;
    public ImageArray history_type;
    public Image NewImage;

    public ArenaHistoryInfo()
    {
      base.\u002Ector();
    }

    private void OnEnable()
    {
      this.UpdateValue();
    }

    public void UpdateValue()
    {
      ArenaPlayerHistory dataOfClass = DataSource.FindDataOfClass<ArenaPlayerHistory>(((Component) this).get_gameObject(), (ArenaPlayerHistory) null);
      if (dataOfClass == null)
        return;
      this.PlayerLevel.set_text(dataOfClass.enemy.PlayerLevel.ToString());
      this.result_image.ImageIndex = !dataOfClass.IsWin() ? 1 : 0;
      ((Component) this.NewImage).get_gameObject().SetActive(dataOfClass.IsNew());
      if (dataOfClass.IsNew())
        ((Graphic) this.created_at).set_color(new Color((float) byte.MaxValue, (float) byte.MaxValue, 0.0f, 1f));
      this.history_type.ImageIndex = !dataOfClass.IsAttack() ? 1 : 0;
      this.Ranking.set_text(dataOfClass.ranking.up.ToString());
      ((Component) this.Ranking).get_gameObject().SetActive(dataOfClass.ranking.up != 0);
      if (dataOfClass.ranking.up > 0)
        this.ranking_delta.ImageIndex = 0;
      else if (dataOfClass.ranking.up < 0)
      {
        this.ranking_delta.ImageIndex = 1;
        ((Graphic) this.Ranking).set_color(new Color((float) byte.MaxValue, 0.0f, 0.0f, 1f));
      }
      else
        ((Component) this.ranking_delta).get_gameObject().SetActive(false);
      this.PlayerName.set_text(dataOfClass.enemy.PlayerName.ToString());
      this.created_at.set_text(dataOfClass.battle_at.ToString("MM/dd HH:mm"));
    }
  }
}
