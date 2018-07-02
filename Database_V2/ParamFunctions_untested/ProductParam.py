def ProductParam(json):
    this={}#ProductParamjson)
    #if(json==null)
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
    #if(json.sale!=null)
        #this.mSale=newProductParam.ProductSaleInfo()
        this['']
        this['mSale']
        if 'sale' in json:
            this['mSale']['Name'] = !string.IsNullOrEmpty?json['sale'].name:string.Empty
        this['mSale']
        if 'sale' in json:
            this['mSale']['Description'] = !string.IsNullOrEmpty?json['sale'].description:string.Empty
        this['mSale']
        if 'sale' in json:
            this['mSale']['AdditionalFreeCoin'] = json['sale'].additional_free_coin>0?json['sale'].additional_free_coin:0
    if 'enabled' in json:
        this['mEnabled'] = json['enabled']
    if 'remaining_days' in json:
        this['mRemainingDays'] = json['remaining_days']
    #returntrue
return this
