from ToEmbed._main import DIRS,LinkDB,ConvertFields,StrBuff,StrCondition,Embed
from PIL import Image, ImageFont, ImageDraw
import io

def quest(iname,page):
    Quest=DIRS['quests'][iname]

    map=MapImage(Quest['map'][0])

    #create basic embed
    embed= Embed(
        title=Quest['name'], #page name
        #url=LinkDB('quest',iname)  #page link
        )
    #embed.set_thumbnail(url=LinkDB('/items/icons/',gear['icon'],'.png',False))
    return(embed,map)

def MapImage(MAP):
    #settings
    tile_res=32 #resolution of a tile

    c0 = (245,245,220)  #color for lowest height
    cE = (139,69,19)    #color for highest heigt

    header = (0,0,0) #color for headers
    header_b=(255,255,255) #color for header border

    border={
        'use':      True,
        'color':    (0,0,0),
        'width':    1
        }

    #data
    def ColorGradient(c0,cE,n):
        grow=[
            int((c0[i]-cE[i])/(n-1))
            for i in range(len(c0))
        ]
        return [
            (
                (c0[0]-i*grow[0]),     #Red
                (c0[1]-i*grow[1]),     #Green
                (c0[2]-i*grow[2])      #Blue
            )
            for i in range(0,n)
        ]

    def Coloring(tile):
        if tile['type']=='Tile':
            return(grad[tile['height']])
        if tile['type']=='Enemy':
            return(166,16,30)
        if tile['type']=='Party':
            return(	65,105,225)
        if tile['type']=='Blocked':
            return(139,137,137)


    Scene=MAP['Scene']
    Set=MAP['Set']
    width=Scene['w']
    height=Scene['h']

    #color gradient
    grad = ColorGradient(c0, cE, 13)


    #convert to array
    AIO=[
        [
            {
                'text': str(Scene['grid'][y*width+x]['h']),
                'height': Scene['grid'][y*width+x]['h'],
                'type': 'Blocked' if 'tile' in Scene['grid'][y*width+x] else 'Tile'
            }
            for x in range(width)
        ]
        for y in range(height)
    ]
    #add party
    if 'party' in Set:
        for i,unit in enumerate(Set['party']):
            tile=AIO[unit['pos']['y']][unit['pos']['x']]
            tile.update({
                'text': '{height} U{unit}'.format(unit=i+1,height= tile['height']),
                'type': 'Party'
            })
    if 'enemy' in Set:
        for i,unit in enumerate(Set['enemy']):
            tile=AIO[unit['pos']['y']][unit['pos']['x']]
            tile.update({
                'text': '{height} E{enemy}'.format(enemy=i+1,height= tile['height']),
                'type': 'Enemy'
            })

    #convert to color array
    #upsize and add borders
    data=[]
    #top header
    border['length']=border['width']*width + (width+1)*tile_res
    for x in range(width+1):
        data+=[header]*tile_res + ([header_b]*border['width'] if x!=width else [])
    data+= data*(tile_res-1) + [header_b]*border['length']

    #define horizontal border
    border_hori=[header_b]*(tile_res+border['width'])+[border['color']]*(border['length']-border['width']-tile_res)
    border_hori+=border_hori*(border['width']-1)
    #
    for y in range(height):
        #add row header with header border
        line=[header]*tile_res + [header_b]*border['width']
        for x in range(width):
            #add normal tiles with border 
            line+=[Coloring(AIO[y][x])]*tile_res + ([border['color']]*border['width'] if x+1!=width else [])
        #add line multiple times for the right height
        data+=line*tile_res + ( border_hori if y+1!=height else [])
    #define image
    img = Image.new('RGB', ((width+1)*tile_res+(width)*border['width'], (height+1)*tile_res+(height)*border['width']), "white")
    #add data
    img.putdata(data)

    #add headers for text
    AIO.insert(0,[
        {
            'text': chr(65+i),
            'type': 'Header'
        }
        for i in range(width)
    ])
    AIO[0].insert(0,{'text':'\\','type':'Header'})
    for y in range(1,height+1):
        AIO[y].insert(0,{
            'text': str(y),
            'type': 'Header'
        })

    #add text
    scale=tile_res

    draw=ImageDraw.Draw(img)
    font=ImageFont.truetype(font='ebrima.ttf', size=int(scale/3), index=0, encoding='')
    for y in range(height+1):
        for x in range(width+1):
            w, h = draw.textsize(AIO[y][x]['text'],font)
            px=x*(scale+border['width']) + int(scale/2 - w/2)
            py=y*(scale+border['width']) + int(scale/2 - h/2)
            
            draw.text(
                (px,py),  # Coordinates
                AIO[y][x]['text'],  # Text
                (0,0,0) if AIO[y][x]['type']!='Header' else (255,255,255),  # Color
                font #Font
                )
    #img.save('my.png')
    #img.show()

    #convert to binary
    imgByteArr = io.BytesIO()
    img.save(imgByteArr, format='PNG')
    imgByteArr = imgByteArr.getvalue()
    return imgByteArr