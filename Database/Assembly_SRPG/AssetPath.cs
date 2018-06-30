namespace SRPG
{
    using GR;
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class AssetPath
    {
        private static StringBuilder mSB;
        private static string JobResourceID_None;

        static AssetPath()
        {
            mSB = new StringBuilder(0x200);
            JobResourceID_None = "none";
            return;
        }

        public static string AbilityIcon(AbilityParam ability)
        {
            mSB.Length = 0;
            mSB.Append("AbilityIcon/");
            mSB.Append(ability.icon);
            return mSB.ToString();
        }

        public static string ArtifactIcon(ArtifactParam arti)
        {
            mSB.Length = 0;
            mSB.Append("ArtiIcon/");
            mSB.Append(arti.icon);
            return mSB.ToString();
        }

        public static string Artifacts(ArtifactParam artifalct)
        {
            mSB.Length = 0;
            mSB.Append("Equipments/");
            mSB.Append(artifalct.asset);
            return mSB.ToString();
        }

        public static string ConceptCard(ConceptCardParam card)
        {
            mSB.Length = 0;
            mSB.Append("ConceptCard/");
            mSB.Append(card.icon);
            return mSB.ToString();
        }

        public static string ConceptCardIcon(ConceptCardParam card)
        {
            mSB.Length = 0;
            mSB.Append("ConceptCardIcon/");
            mSB.Append(card.icon);
            return mSB.ToString();
        }

        private static string GetJobResourceID(JobParam job)
        {
            if (job != null)
            {
                goto Label_000C;
            }
            return JobResourceID_None;
        Label_000C:
            if (string.IsNullOrEmpty(job.ac2d) != null)
            {
                goto Label_0023;
            }
            return job.ac2d;
        Label_0023:
            return job.model;
        }

        public static string ItemIcon(ItemParam param)
        {
            UnitParam param2;
            EItemType type;
            if (param == null)
            {
                goto Label_00BE;
            }
            switch ((param.type - 14))
            {
                case 0:
                    goto Label_0027;

                case 1:
                    goto Label_00B2;

                case 2:
                    goto Label_0073;
            }
            goto Label_00B2;
        Label_0027:
            if (string.IsNullOrEmpty(param.icon) != null)
            {
                goto Label_00BE;
            }
            mSB.Length = 0;
            mSB.Append("ArtiIcon/");
            mSB.Append(param.icon);
            return mSB.ToString();
            goto Label_00BE;
        Label_0073:
            if (string.IsNullOrEmpty(param.icon) != null)
            {
                goto Label_00BE;
            }
            param2 = MonoSingleton<GameManager>.Instance.MasterParam.GetUnitParam(param.iname);
            if (param2 == null)
            {
                goto Label_00BE;
            }
            return UnitIconSmall(param2, param2.GetJobId(0));
            goto Label_00BE;
        Label_00B2:
            return ItemIcon(param.icon);
        Label_00BE:
            return null;
        }

        public static string ItemIcon(string iconName)
        {
            if (string.IsNullOrEmpty(iconName) == null)
            {
                goto Label_000D;
            }
            return null;
        Label_000D:
            mSB.Length = 0;
            mSB.Append("ItemIcon/");
            mSB.Append(iconName);
            return mSB.ToString();
        }

        public static string JobEquipment(JobParam job)
        {
            mSB.Length = 0;
            mSB.Append("Equipments/");
            mSB.Append(job.wepmdl);
            return mSB.ToString();
        }

        public static string JobIconMedium(JobParam job)
        {
            mSB.Length = 0;
            mSB.Append("JobIconM/");
            mSB.Append(GetJobResourceID(job));
            return mSB.ToString();
        }

        public static string JobIconSmall(JobParam job)
        {
            mSB.Length = 0;
            mSB.Append("JobIcon/");
            mSB.Append(GetJobResourceID(job));
            return mSB.ToString();
        }

        public static string JobIconThumbnail()
        {
            return "JobIcon/small";
        }

        public static string LocalMap(string sceneName)
        {
            mSB.Length = 0;
            mSB.Append("LocalMaps/");
            mSB.Append(sceneName);
            return mSB.ToString();
        }

        public static string Navigation(QuestParam quest)
        {
            mSB.Length = 0;
            mSB.Append("UI/");
            mSB.Append(quest.navigation);
            return mSB.ToString();
        }

        public static string QuestEvent(string eventName)
        {
            mSB.Length = 0;
            mSB.Append("Events/");
            mSB.Append(eventName);
            return mSB.ToString();
        }

        public static string SkillEffect(SkillParam skill)
        {
            mSB.Length = 0;
            mSB.Append("SkillEff/");
            mSB.Append(skill.effect);
            return mSB.ToString();
        }

        public static string SkillScene(string sceneName)
        {
            mSB.Length = 0;
            mSB.Append("SkillBG/");
            mSB.Append(sceneName);
            return mSB.ToString();
        }

        public static string TrickEffect(string effect_name)
        {
            mSB.Length = 0;
            mSB.Append("MapGimmicks/");
            mSB.Append(effect_name);
            return mSB.ToString();
        }

        public static string TrickIconUI(string marker_name)
        {
            mSB.Length = 0;
            mSB.Append("PortraitsM/panel");
            if (string.IsNullOrEmpty(marker_name) != null)
            {
                goto Label_0042;
            }
            mSB.Append("_");
            mSB.Append(marker_name);
        Label_0042:
            return mSB.ToString();
        }

        public static string UnitCurrentJobIconMedium(UnitData unit)
        {
            return JobIconMedium((unit.CurrentJob == null) ? null : unit.CurrentJob.Param);
        }

        public static string UnitCurrentJobIconSmall(UnitData unit)
        {
            return JobIconSmall((unit.CurrentJob == null) ? null : unit.CurrentJob.Param);
        }

        public static string UnitEyeImage(UnitParam unit, string jobName)
        {
            JobParam param;
            string str;
            mSB.Length = 0;
            mSB.Append("UnitEyeImages/");
            param = null;
            if (string.IsNullOrEmpty(jobName) != null)
            {
                goto Label_0034;
            }
            param = MonoSingleton<GameManager>.Instance.GetJobParam(jobName);
        Label_0034:
            if (param == null)
            {
                goto Label_0046;
            }
            str = param.unit_image;
            goto Label_004E;
        Label_0046:
            str = unit.GetJobImage(jobName);
        Label_004E:
            mSB.Append((string.IsNullOrEmpty(str) != null) ? unit.model : str);
            return mSB.ToString();
        }

        public static string UnitIconMedium(UnitParam unit, string jobName)
        {
            JobParam param;
            string str;
            mSB.Length = 0;
            mSB.Append("PortraitsM/");
            param = null;
            if (string.IsNullOrEmpty(jobName) != null)
            {
                goto Label_0034;
            }
            param = MonoSingleton<GameManager>.Instance.GetJobParam(jobName);
        Label_0034:
            if (param == null)
            {
                goto Label_0046;
            }
            str = param.unit_image;
            goto Label_004E;
        Label_0046:
            str = unit.GetJobImage(jobName);
        Label_004E:
            mSB.Append((string.IsNullOrEmpty(str) != null) ? unit.model : str);
            return mSB.ToString();
        }

        public static string UnitIconSmall(UnitParam unit, string jobName)
        {
            JobParam param;
            string str;
            mSB.Length = 0;
            mSB.Append("Portraits/");
            param = null;
            if (string.IsNullOrEmpty(jobName) != null)
            {
                goto Label_0034;
            }
            param = MonoSingleton<GameManager>.Instance.GetJobParam(jobName);
        Label_0034:
            if (param == null)
            {
                goto Label_0046;
            }
            str = param.unit_image;
            goto Label_004E;
        Label_0046:
            str = unit.GetJobImage(jobName);
        Label_004E:
            mSB.Append((string.IsNullOrEmpty(str) != null) ? unit.model : str);
            return mSB.ToString();
        }

        public static string UnitImage(UnitParam unit, string jobName)
        {
            string str;
            JobParam param;
            mSB.Length = 0;
            mSB.Append("UnitImages/");
            param = MonoSingleton<GameManager>.Instance.GetJobParam(jobName);
            if (param == null)
            {
                goto Label_0039;
            }
            str = param.unit_image;
            goto Label_0041;
        Label_0039:
            str = unit.GetJobImage(jobName);
        Label_0041:
            mSB.Append((string.IsNullOrEmpty(str) != null) ? unit.image : str);
            return mSB.ToString();
        }

        public static string UnitImage2(UnitParam unit, string jobName)
        {
            string str;
            JobParam param;
            mSB.Length = 0;
            mSB.Append("UnitImages2/");
            param = MonoSingleton<GameManager>.Instance.GetJobParam(jobName);
            if (param == null)
            {
                goto Label_0039;
            }
            str = param.unit_image;
            goto Label_0041;
        Label_0039:
            str = unit.GetJobImage(jobName);
        Label_0041:
            mSB.Append((string.IsNullOrEmpty(str) != null) ? unit.image : str);
            return mSB.ToString();
        }

        public static string UnitSkinEyeImage(UnitParam unit, ArtifactParam skin, string jobName)
        {
            if (skin == null)
            {
                goto Label_005E;
            }
            mSB.Length = 0;
            mSB.Append("UnitEyeImages/");
            mSB.Append(unit.model);
            mSB.Append("_");
            mSB.Append(skin.asset);
            return mSB.ToString();
        Label_005E:
            return UnitEyeImage(unit, jobName);
        }

        public static string UnitSkinIconMedium(UnitParam unit, ArtifactParam skin, string jobName)
        {
            if (skin == null)
            {
                goto Label_005E;
            }
            mSB.Length = 0;
            mSB.Append("PortraitsM/");
            mSB.Append(unit.model);
            mSB.Append("_");
            mSB.Append(skin.asset);
            return mSB.ToString();
        Label_005E:
            return UnitIconMedium(unit, jobName);
        }

        public static string UnitSkinIconSmall(UnitParam unit, ArtifactParam skin, string jobName)
        {
            if (skin == null)
            {
                goto Label_005E;
            }
            mSB.Length = 0;
            mSB.Append("Portraits/");
            mSB.Append(unit.model);
            mSB.Append("_");
            mSB.Append(skin.asset);
            return mSB.ToString();
        Label_005E:
            return UnitIconSmall(unit, jobName);
        }

        public static string UnitSkinImage(UnitParam unit, ArtifactParam skin, string jobName)
        {
            if (skin == null)
            {
                goto Label_005E;
            }
            mSB.Length = 0;
            mSB.Append("UnitImages/");
            mSB.Append(unit.image);
            mSB.Append("_");
            mSB.Append(skin.asset);
            return mSB.ToString();
        Label_005E:
            return UnitImage(unit, jobName);
        }

        public static string UnitSkinImage2(UnitParam unit, ArtifactParam skin, string jobName)
        {
            if (skin == null)
            {
                goto Label_005E;
            }
            mSB.Length = 0;
            mSB.Append("UnitImages2/");
            mSB.Append(unit.image);
            mSB.Append("_");
            mSB.Append(skin.asset);
            return mSB.ToString();
        Label_005E:
            return UnitImage2(unit, jobName);
        }

        public static string UnitVoiceFileName(UnitParam unit, ArtifactParam artifact, string jobVoice)
        {
            if (string.IsNullOrEmpty(unit.voice) != null)
            {
                goto Label_0094;
            }
            mSB.Length = 0;
            mSB.Append(unit.voice);
            if (artifact == null)
            {
                goto Label_0065;
            }
            if (string.IsNullOrEmpty(artifact.voice) != null)
            {
                goto Label_0065;
            }
            mSB.Append(0x5f);
            mSB.Append(artifact.voice);
            goto Label_0089;
        Label_0065:
            if (string.IsNullOrEmpty(jobVoice) != null)
            {
                goto Label_0089;
            }
            mSB.Append(0x5f);
            mSB.Append(jobVoice);
        Label_0089:
            return mSB.ToString();
        Label_0094:
            return unit.voice;
        }

        public static string WeatherEffect(string effect_name)
        {
            mSB.Length = 0;
            mSB.Append("Weathers/");
            mSB.Append(effect_name);
            return mSB.ToString();
        }

        public static string WeatherIcon(string icon_name)
        {
            mSB.Length = 0;
            mSB.Append("Weathers/");
            mSB.Append(icon_name);
            return mSB.ToString();
        }
    }
}

