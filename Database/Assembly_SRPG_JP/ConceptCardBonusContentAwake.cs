// Decompiled with JetBrains decompiler
// Type: SRPG.ConceptCardBonusContentAwake
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85BFDF7F-5712-4D45-9CD6-3465C703DFDF
// Assembly location: S:\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class ConceptCardBonusContentAwake : MonoBehaviour
  {
    [SerializeField]
    private GameObject mSkillParamTemplate;
    [SerializeField]
    private ImageArray mAwakeIconImageArray;
    [SerializeField]
    private ImageArray mAwakeIconBgArray;
    [SerializeField]
    private ImageArray mProgressLine;
    private int mCreatedCount;
    private bool mIsEnable;

    public ConceptCardBonusContentAwake()
    {
      base.\u002Ector();
    }

    public bool IsEnable
    {
      get
      {
        return this.mIsEnable;
      }
    }

    public void Setup(ConceptCardEffectsParam[] effect_params, int awake_count, int awake_count_cap, bool is_enable)
    {
      if (Object.op_Equality((Object) this.mSkillParamTemplate, (Object) null))
        return;
      this.mIsEnable = is_enable;
      Transform parent = this.mSkillParamTemplate.get_transform().get_parent();
      List<string> stringList = new List<string>();
      for (int index = 0; index < effect_params.Length; ++index)
      {
        if (!string.IsNullOrEmpty(effect_params[index].card_skill))
        {
          SkillParam skillParam = MonoSingleton<GameManager>.Instance.MasterParam.GetSkillParam(effect_params[index].card_skill);
          if (skillParam != null && !string.IsNullOrEmpty(effect_params[index].add_card_skill_buff_awake) && !stringList.Contains(skillParam.iname))
          {
            BaseStatus total_add = new BaseStatus();
            BaseStatus total_scale = new BaseStatus();
            effect_params[index].GetAddCardSkillBuffStatusAwake(awake_count, awake_count_cap, ref total_add, ref total_scale);
            GameObject root = (GameObject) Object.Instantiate<GameObject>((M0) this.mSkillParamTemplate);
            root.get_transform().SetParent(parent, false);
            ((StatusList) root.GetComponentInChildren<StatusList>()).SetValues(total_add, total_scale, false);
            if (Object.op_Inequality((Object) this.mAwakeIconImageArray, (Object) null))
              this.mAwakeIconImageArray.ImageIndex = awake_count - 1;
            DataSource.Bind<SkillParam>(root, skillParam);
            DataSource.Bind<bool>(((Component) this).get_gameObject(), is_enable);
            GameParameter.UpdateAll(root);
            stringList.Add(skillParam.iname);
            ++this.mCreatedCount;
          }
        }
      }
      if (Object.op_Inequality((Object) this.mAwakeIconBgArray, (Object) null))
        this.mAwakeIconBgArray.ImageIndex = !is_enable ? 1 : 0;
      this.mSkillParamTemplate.SetActive(false);
      ((Component) this).get_gameObject().SetActive(this.mCreatedCount > 0);
    }

    public void SetProgressLineImage(bool is_enable, bool is_active = true)
    {
      this.mProgressLine.ImageIndex = !is_enable ? 1 : 0;
      ((Component) this.mProgressLine).get_gameObject().SetActive(is_active);
    }
  }
}
