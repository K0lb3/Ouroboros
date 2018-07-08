from datetime import *

def FixFields(fields):
    remove=[]
    uses=['name','value']
    for f in range(0,len(fields)):
        field = fields[f]
        for i in uses:
            if len(field[i])==0:
                remove.append(f)
    fin = [i for j, i in enumerate(fields) if j not in remove]
    return(fin)

def TimeDif_hms(time,time2=datetime.now()):
    FMT = '%H:%M:%S'
    tdelta = datetime.strptime(time, FMT) - time2
    if tdelta.days < 0:
        tdelta = timedelta(days=0,
            seconds=tdelta.seconds)

    return str(tdelta)
