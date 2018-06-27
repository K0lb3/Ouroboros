from MainFunctions import *
from SkillParam import convert_raw_skill


def Skill_GenericDescription(skill,loc):
    skill=convert_raw_skill(master[siname],loc)

    effect_type=skill['effect_type']
    if effect_type == "Equipment":
        desc=''.formart()
    if effect_type == "Attack":
        desc=''.formart()
    if effect_type == "Defend":
        desc=''.formart()
    if effect_type == "Heal":
        desc=''.formart()
    if effect_type == "Buff":
        desc=''.formart()
    if effect_type == "Debuff":
        desc=''.formart()
    if effect_type == "Revive":
        desc=''.formart()
    if effect_type == "Shield":
        desc=''.formart()
    if effect_type == "ReflectDamage":
        desc=''.formart()
    if effect_type == "DamageControl":
        desc=''.formart()
    if effect_type == "FailCondition":
        desc=''.formart()
    if effect_type == "CureCondition":
        desc=''.formart()
    if effect_type == "DisableCondition":
        desc=''.formart()
    if effect_type == "GemsGift":
        desc=''.formart()
    if effect_type == "GemsIncDec":
        desc=''.formart()
    if effect_type == "Guard":
        desc=''.formart()
    if effect_type == "Teleport":
        desc=''.formart()
    if effect_type == "Changing":
        desc=''.formart()
    if effect_type == "RateHeal":
        desc=''.formart()
    if effect_type == "RateDamage":
        desc=''.formart()
    if effect_type == "PerfectAvoid":
        desc=''.formart()
    if effect_type == "Throw":
        desc=''.formart()
    if effect_type == "EffReplace":
        desc=''.formart()
    if effect_type == "SetTrick":
        desc=''.formart()
    if effect_type == "TransformUnit":
        desc=''.formart()
    if effect_type == "SetBreakObj":
        desc=''.formart()
    if effect_type == "ChangeWeather":
        desc=''.formart()
    if effect_type == "RateDamageCurrent":
        desc=''.formart()

    return desc
