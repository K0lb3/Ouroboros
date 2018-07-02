def EventShopItemListSet(json):
    this={}#EventShopItemListSetjson)
    #if(json==null||json.item==null||(string.IsNullOrEmpty(json.item.iname)||json.cost==null)||string.IsNullOrEmpty(json.cost.type))
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
    #if(this.saleType==ESaleType.EventCoin&&this.cost_iname==null)
    #returnfalse
    if 'isreset' in json:
        this['is_reset'] = json['isreset']==1
    if 'start' in json:
        this['start'] = json['start']
    if 'end' in json:
        this['end'] = json['end']
    if 'sold' in json:
        this['is_soldout'] = json['sold']>0
    #if(json.children!=null)
    if 'children' in json:
        this['children'] = json['children']
    #if(json.children!=null)
        #this.shopItemType=EShopItemType.Set
    #else
        if 'item' in json:
            this['shopItemType'] = ShopData.String2ShopItemType
        #if(this.shopItemType==EShopItemType.Unknown)
        if 'item' in json:
            this['shopItemType'] = ShopData.Iname2ShopItemType
    #if(this.IsConceptCard)
    #MonoSingleton<GameManager>.Instance.Player.SetConceptCardNum(this.iname,json.item.has_count)
    #returntrue
return this
