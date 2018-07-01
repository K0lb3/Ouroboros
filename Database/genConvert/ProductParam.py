def ProductParam(json):
    this={}#ProductParamjson)
    #returnfalse
    if 'product_id' in json:
        this['mProductId'] = json['product_id']
    if 'platform' in json:
        this['mPlatform'] = json['platform']
    if 'name' in json:
        this['mName'] = LocalizedText.Get
    if 'description' in json:
        this['mDescription'] = LocalizedText.Get
    if 'additional_paid_coin' in json:
        this['mAdditionalPaidCoin'] = json['additional_paid_coin']
    if 'additional_free_coin' in json:
        this['mAdditionalFreeCoin'] = json['additional_free_coin']
        this['']
        this['mSale']
        if 'sale' in json:
            this['mSale']['Name'] = !string.IsNullOrEmpty?LocalizedText.Get:string.Empty
        this['mSale']
        if 'sale' in json:
            this['mSale']['Description'] = !string.IsNullOrEmpty?LocalizedText.Get:string.Empty
        this['mSale']
        if 'sale' in json:
            this['mSale']['AdditionalFreeCoin'] = json['sale'].additional_free_coin>0?json['sale'].additional_free_coin:0
    if 'enabled' in json:
        this['mEnabled'] = json['enabled']
    if 'remaining_days' in json:
        this['mRemainingDays'] = json['remaining_days']
    #returntrue
return this
