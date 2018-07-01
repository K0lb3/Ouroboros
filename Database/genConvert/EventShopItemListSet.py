def EventShopItemListSet(json):
    this={}#EventShopItemListSetjson)
    #returnfalse
    if 'id' in json:
        this['id'] = json['id']
    if 'item' in json:
        this['iname'] = json['item'].iname
    if 'item' in json:
        this['num'] = json['item'].num
    if 'item' in json:
        this['max_num'] = json['item'].maxnum
    if 'item' in json:
        this['bougthnum'] = json['item'].boughtnum
    if 'cost' in json:
        this['saleValue'] = json['cost'].value
    if 'cost' in json:
        this['saleType'] = ShopData.String2SaleType
    if 'cost' in json:
        this['cost_iname'] = json['cost'].iname==null?GlobalVars.EventShopItem.shop_cost_iname:json['cost'].iname
    #returnfalse
    if 'isreset' in json:
        this['is_reset'] = json['isreset']==1
    if 'sold' in json:
        this['is_soldout'] = json['sold']>0
    if 'children' in json:
        this['children'] = json['children']
    #returntrue
return this
