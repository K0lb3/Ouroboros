def BundleParam(json):
    this={}#BundleParamjson)
    #returnfalse
    if 'product_id' in json:
        this['mProductId'] = json['product_id']
    if 'platform' in json:
        this['mPlatform'] = json['platform']
    if 'name' in json:
        this['mName'] = json['name']
    if 'description' in json:
        this['mDescription'] = json['description']
    if 'additional_paid_coin' in json:
        this['mAdditionalPaidCoin'] = json['additional_paid_coin']
    if 'additional_free_coin' in json:
        this['mAdditionalFreeCoin'] = json['additional_free_coin']
    if 'end_date' in json:
        this['mEndDate'] = json['end_date']
    if 'image' in json:
        this['mImage'] = json['image']
    if 'display_order' in json:
        this['mDisplayOrder'] = json['display_order']
    if 'max_purchase_limit' in json:
        this['mMaxPurchaseLimit'] = json['max_purchase_limit']
    if 'max_purchase_limit' in json:
        this['mPurchaseLimit'] = json['max_purchase_limit']-json.purchase_count
        this['']
        this['mContents']
        if 'contents' in json:
            this['mContents']['Items'] = this.Deserialize
        this['mContents']
        if 'contents' in json:
            this['mContents']['Units'] = this.Deserialize
        this['mContents']
        if 'contents' in json:
            this['mContents']['Equipments'] = this.Deserialize
    #returntrue
return this
