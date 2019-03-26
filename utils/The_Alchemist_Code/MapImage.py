from ._main import DIRS,resPath
from PIL import Image, ImageFont, ImageDraw
import io
import os
import json 

Inkfree=os.path.join(resPath,'Inkfree.ttf')
ebrima=os.path.join(resPath,'ebrima.ttf')
##### SETTINGS ################################################################################
DEBUG=False #show printed map on command
TILE_RESOLUTION=48
BORDER_WIDTH=1
HEIGHTSTEPS = 35
FONT={
	'TEXT':	 ImageFont.truetype(font=Inkfree, size=int(TILE_RESOLUTION/4), index=0, encoding=''),
	'HEADER':   ImageFont.truetype(font=ebrima, size=int(TILE_RESOLUTION/3), index=0, encoding='')
}
COLOR={
	'PARTY':		(65,105,225),
	'ALLY':		 (58,190,98),
	'ENEMY':		(166,16,30),
	'TREASURE':	 (249,224,0),
	'JEWEL':		(64,224,208),
	'WALL':		 (79,77,77),
	'TRAP':		(238,130,238),
	'BLOCKED':	  (139,137,137),
	'HEIGHT_MIN':   (245,245,220),
	'HEIGHT_MAX':   (66, 43, 0),
	'HEADER':	   (0,0,0),
	'HEADER_BORDER':(255,255,255),
	'BORDER':	   (127,127,127),
	'TEXT':		 (0,0,0),
	'TEXT_HEADER':  (255,255,255),
}

##### CONSTANTS #######################################################
UNIT=DIRS['Enemy']
UNIT.update(DIRS['Unit'])

##### QUEST PAGE DECISION #############################################

##### CREATE MAP IMAGE '''''''''''''''''''''''''''''''''''''''''''''''''''''''''
def MapImage(MAP):
	Scene=MAP['Scene']
	width=Scene['w']
	height=Scene['h']

	tiles = ConvertData(MAP)
	if height>width:
		tiles=RotateTiles(tiles)
		width,height=(height,width)
	img = DrawImage(tiles,width,height)

	#img.save('my.png')
	if DEBUG:
		img.show()

	#convert to binary
	imgByteArr = io.BytesIO()
	img.save(imgByteArr, format='PNG')
	imgByteArr = imgByteArr.getvalue()
	return imgByteArr

def RotateTiles(tiles):
	return (
		[
	    		[
	            	tiles[y][x]
	            	for y in range(len(tiles))
	    	]
	    	for x in range(len(tiles[0]))
		]
		)

def ColorGradient(c0,cE,n):
	grow=[
		int((c0[i]-cE[i])/(n-1))
		for i in range(len(c0))
	]
	
	return [
		(
			(c0[0]-i*grow[0]),	 #Red
			(c0[1]-i*grow[1]),	 #Green
			(c0[2]-i*grow[2])	  #Blue
		)
		for i in range(0,n)
	]

def Coloring(tile):
	if tile['type']=='tile':
		return(grad[max(tile['height'],HEIGHTSTEPS-1)])
	return(COLOR[tile['type'].upper()])

def ConvertData(MAP):
	Scene=MAP['Scene']
	Set=MAP['Set']
	width=Scene['w']
	height=Scene['h']
	#CONVERT ARRAY TO MATRIX
	AIO=[
		[
			{
				'text': str(Scene['grid'][y*width+x]['h']),
				'height': Scene['grid'][y*width+x]['h'],
				'type': 'blocked' if 'tile' in Scene['grid'][y*width+x] else 'tile'
			}
			for x in range(width)
		]
		for y in range(height)
	]

	#ADD SPAWNS TO MAP
	def placeSpawn(unit,number,typ):
		tile=AIO[unit['pos']['y']][unit['pos']['x']]
		tile.update({
			'text': '{height}\n[{pre}{unit}]'.format(unit=number,pre=typ[0].upper(),height= tile['height']),
			'type': typ
			})

	sides=['party','ally','enemy','treasure','jewel','wall','trap']
	for side in sides:
		if side in Set:
			for i,unit in enumerate(Set[side]):
				placeSpawn(unit,i+1,side)
	return AIO

def DrawImage(tiles,width,height):
	global grad
	grad = ColorGradient(COLOR['HEIGHT_MIN'],COLOR['HEIGHT_MAX'],HEIGHTSTEPS)

	border={
		'use':	  True,
		'width':	BORDER_WIDTH
		}
	#upsize and add borders
	data=[]
	#top COLOR['HEADER']
	border['length']=border['width']*width + (width+1)*TILE_RESOLUTION
	for x in range(width+1):
		data+=[COLOR['HEADER']]*TILE_RESOLUTION + ([COLOR['HEADER_BORDER']]*border['width'] if x!=width else [])
	data+= data*(TILE_RESOLUTION-1) + [COLOR['HEADER_BORDER']]*border['length']

	#define horizontal border
	border_hori=[COLOR['HEADER_BORDER']]*(TILE_RESOLUTION+border['width'])+[COLOR['BORDER']]*(border['length']-border['width']-TILE_RESOLUTION)
	border_hori+=border_hori*(border['width']-1)
	#
	for y in range(height):
		#add row COLOR['HEADER'] with COLOR['HEADER'] border
		line=[COLOR['HEADER']]*TILE_RESOLUTION + [COLOR['HEADER_BORDER']]*border['width']
		for x in range(width):
			#add normal tiles with border 
			line+=[Coloring(tiles[y][x])]*TILE_RESOLUTION + ([COLOR['BORDER']]*border['width'] if x+1!=width else [])
		#add line multiple times for the right height
		data+=line*TILE_RESOLUTION + ( border_hori if y+1!=height else [])
	#define image
	img = Image.new('RGB', ((width+1)*TILE_RESOLUTION+(width)*border['width'], (height+1)*TILE_RESOLUTION+(height)*border['width']), "white")
	#add data
	img.putdata(data)

	return WriteText(tiles,img,width,height,border)

def WriteText(AIO,img,width,height,border):
	####	add header values
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

##### WRITE TEXT ########################################################################
	draw=ImageDraw.Draw(img)
	for y in range(height+1):
		for x in range(width+1):

			text=AIO[y][x]['text']
			if AIO[y][x]['type'] == 'Header':
				tcolor=COLOR['TEXT_HEADER']
				tfont=FONT['HEADER']
			else:
				tcolor=COLOR['TEXT']
				tfont=FONT['TEXT']

			w, h = draw.textsize(text,tfont)
			if '\n' not in text:
				px=x*(TILE_RESOLUTION+border['width']) + int(TILE_RESOLUTION/2 - w/2)
				py=y*(TILE_RESOLUTION+border['width']) + int(TILE_RESOLUTION/2 - h/2)
				draw.text(
					(px,py),  # Coordinates
					text,  # Text
					tcolor,
					tfont
					)
			else:
				lines=text.split('\n')
				#center position
				cx = x*(TILE_RESOLUTION+border['width']) + (TILE_RESOLUTION/2)
				#upper position
				uy = y*(TILE_RESOLUTION+border['width']) + (TILE_RESOLUTION/2) - (h/2)
				for i,line in enumerate(lines[::-1]):
					wl, hl = draw.textsize(line,tfont)
					draw.text(
						(	# Coordinates
							int(cx-(wl/2)),
							int(uy+(h*i/len(lines)))
						),  
						line,  # Text
						tcolor,
						tfont
						)


	return img