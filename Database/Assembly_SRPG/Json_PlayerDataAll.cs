namespace SRPG
{
    using System;

    public class Json_PlayerDataAll
    {
        public Json_PlayerData player;
        public Json_Unit[] units;
        public Json_Item[] items;
        public Json_Mail[] mails;
        public Json_Party[] parties;
        public Json_Friend[] friends;
        public Json_Artifact[] artifacts;
        public JSON_ConceptCard[] concept_cards;
        public Json_Skin[] skins;
        public Json_Notify notify;
        public Json_MultiFuids[] fuids;
        public int status;
        public string cuid;
        public long tut;
        public int first_contact;
        public Json_Versus vs;
        public string[] tips;

        public Json_PlayerDataAll()
        {
            base..ctor();
            return;
        }
    }
}

