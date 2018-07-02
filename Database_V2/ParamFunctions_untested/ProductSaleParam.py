def ProductSaleParam(json):
    this={}#ProductSaleParamjson)
    #if(json==null)
    #returnfalse
    if 'fields' in json:
        this['ProductId'] = json['fields'].product_id
    if 'fields' in json:
        this['Platform'] = json['fields'].platform
    if 'fields' in json:
        this['Name'] = json['fields'].name
    if 'fields' in json:
        this['Description'] = json['fields'].description
    if 'fields' in json:
        this['AdditionalFreeCoin'] = json['fields'].additional_free_coin
    this['']
    this['Condition']
    if 'fields' in json:
        this['Condition']['type'] = json['fields'].condition_type
    this['Condition']
    if 'fields' in json:
        this['Condition']['value'] = json['fields'].condition_value
    #returntrue
return this
