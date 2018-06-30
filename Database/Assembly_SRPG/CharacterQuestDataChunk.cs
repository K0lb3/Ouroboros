namespace SRPG
{
    using System;
    using System.Collections.Generic;

    public class CharacterQuestDataChunk
    {
        public string areaName;
        public string unitName;
        public UnitParam unitParam;
        public List<QuestParam> questParams;

        public CharacterQuestDataChunk()
        {
            this.questParams = new List<QuestParam>();
            base..ctor();
            return;
        }

        public void SetUnitNameFromChapterID(string chapterID)
        {
            this.unitName = chapterID;
            return;
        }
    }
}

