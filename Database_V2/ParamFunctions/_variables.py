import os
import json
from Functions.Translation import TRANSLATION

def loadGameFiles2(files):
    ret = []
    dir = os.path.dirname(os.path.realpath(__file__)).replace('\\ParamFunctions','\\resources\\GameFiles\\')

    for file in files:
        with open(dir + file, "rt", encoding='utf8') as f:
            ret.append(json.loads(f.read()))
    return ret
[ENUM,SYS,UNITLORE]=loadGameFiles2(['ENums.json','sys.json','unit.json'])

#hotfixes
for param in ENUM:
    if type(ENUM[param])==dict:
        ENUM[param]={int(k):v for k,v in ENUM[param].items()}

RAWELEMENT={
    0: 'None',
    1: 'Fire',
    2: 'Water',
    3: 'Wind',
    4: 'Thunder',
    5: 'Light',
    6: 'Dark',
    10:    'Water',
    100:   'Wind',
    1000:  'Thunder',
    10000: 'Light',
    100000: 'Dark',
    111111: 'None', 
    }

RAWBIRTH = {
    "その他": "Foreign World",
    "エンヴィリア": "Envylia",
    "スロウスシュタイン": "Slothstein",
    "ルストブルグ": "Lustburg",
    "サガ地方": "Saga Region",
    "ラーストリス": "Wratharis",
    "ワダツミ": "Wadatsumi",
    "砂漠地帯": "Desert Zone",
    "ノーザンブライド": "Northern Pride",
    "グリードダイク": "Greed Dike",
    "グラトニー＝フォス": "Gluttony Foss",
    '神戸あいしす':'Kobe Aisushi', #placeholder? only one Doro enemy and 8 buffs/conds
    '神戸IJ': 'Kobe IJ',
    'ラットン田崎': 'Tasaki Kobe',
    '神戸芸人': 'Kobe Entertainer',
    '神戸師範': 'Comedian Sihan Kobe'
    }

TAG_ARTIFACT = {
    "剣": "Sword",
    "杖": "Rod",
    "ローブ": "Robe",
    "鎧": "Armor",
    "具足": "Legguards",
    "籠手": "Gauntlet",
    "盾": "Shield",
    "アクセサリー": "Earring",
    "大剣": "Greatsword/Scythe",
    "短剣": "Dagger",
    "銃": "Gun",
    "ランス": "Spear",
    "ウェディングソード": "Wedding Sword",
    "弓": "Bow",
    "グローブ": "Gloves",
    "サーベル": "Saber",
    "ライフル": "Sniper Rifle",
    "槍": "Spear",
    "カード": "Cards",
    "忍者刀": "Kunai",
    "チャクラム": "Chakram",
    "弦楽器": "Harp",
    "斧": "Axe",
    "双剣": "Twin Swords",
    "薬瓶": "Drug Vial",
    "獣": "Claw",
    "大麻": "Praying Staff",
    "鎚": "Hammer",
    "刀": "Katana",
    "ロケットランチャー": "Rocket Launcher",
    "薙刀": "Halberd",
    "風水盤": "Feng Shui",
    "タクト": "Tact",
    "手袋": "Gloves",
    "グレネードランチャー": "Grenade Launcher",
    "長銃": "Rifle",  # JB_DGO
    "台本": "Script",  # JB_ACT_F
    "無手": "Paw",  # JB_DIS_CAT
    "大砲": "Cannon",  # JB_ART
    "銃槍": "Gunlance",  # JB_GUNL
    "装備なし": "N/A",  # JB_LUROB01
    "杖剣": "Swordstick",  # JB_HENC_WATER
    "カタール": "Qatar", #[Default Gear] Assassin
    "ほうき": "Broom", #[Default Gear] Shrine Maiden",
    "メイス": "Mace", #[Default Gear] Exclusive: Salomo (Thief)
    }

TAG_UNIT= {
    "人族": "Human",
    "男性": "Male",
    "女性": "Female",
    "モンスター": "Monster",
    "十戒衆": "Ten Commandments",
    "亜人種": "Subordinate",
    "メタル": "Metal",
    "異族": "Special",
    "魔獣": "Beast",
    "植物": "Plant",
    "魔人": "Demon",
    "機械": "Machine",
    "昆虫": "Insect",
    "不定形": "Amorphous",
    "浮遊": "Floating",
    "FOE": "Enemy",
    "水棲": "Aquatic Lifeform",
    "壊せるオブジェクト": "Breakable Objects",
    "不死者": "Undead",
    }