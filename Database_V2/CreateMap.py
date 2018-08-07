from PIL import Image, ImageFont, ImageDraw
import json
import os.path

def MapImage(MAP):
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
        if 'tile' in tile:
            grey=127+7*tile['h']
            return (grey,grey,grey)
        else:
            return grad[tile['h']]

    Scene=MAP['Scene']
    Set=MAP['Set']
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
    width=Scene['w']
    height=Scene['h']

    #color gradient
    grad = ColorGradient(c0, cE, 13)

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
            line+=[Coloring(Scene['grid'][y*width+x])]*tile_res + ([border['color']]*border['width'] if x+1!=width else [])
        #add line multiple times for the right height
        data+=line*tile_res + ( border_hori if y+1!=height else [])
    #define image
    img = Image.new('RGB', ((width+1)*tile_res+(width)*border['width'], (height+1)*tile_res+(height)*border['width']), "white")
    #add data
    img.putdata(data)

    #patch height
    heights=[
        tile['h']
        for tile in Scene['grid']
    ]
    heights.insert(0,'\\')
    for i in range(width):
        heights.insert(i+1,chr(i+65))
    for i in range(1,height+1):
        insert=(width+1)*i
        heights.insert(insert, i)

    #add text
    scale=tile_res

    offset=int(scale/4)
    draw=ImageDraw.Draw(img)
    for i,h in enumerate(heights):
        x=(i%(width+1))     *(scale+border['width']) + offset
        y=int(i/(width+1))  *(scale+border['width']) + offset
        draw.text(
            (x,y),  # Coordinates
            str(h),  # Text
            (0,0,0) if i>width+1 and i%(width+1)!=0 else (255,255,255),  # Color
            ImageFont.truetype(font='ebrima.ttf', size=int(scale/2), index=0, encoding='') #Font
            )
    #img.save('my.png')
    img.show()

PATH=os.path.dirname(os.path.realpath(__file__))
with open(PATH+'/_converted2/Quests.json','rt',encoding='utf8') as f:
    quests=json.loads(f.read())

MapImage(quests['QE_ST_NO_040079']['map'][0])