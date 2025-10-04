from topSymbols import *
import json
from datetime import datetime
from dateutil import relativedelta

def unitTesting():
    topSymbolList = returnTopSymbols()
    json_data = json.dump(topSymbolList, fp=open('topSymbols.json', 'w'))
    print(json_data)
    
    
    