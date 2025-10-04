from datetime import datetime
from dateutil import relativedelta

def unitTesting():
    time_start = datetime.now() - relativedelta.relativedelta(years=5)
    time_start = time_start.timestamp()
    timedate_actual = datetime.now().timestamp()
    print(int(time_start))
    print(int(timedate_actual))
    
    
    