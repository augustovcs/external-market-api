import pathlib
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.firefox.options import Options
from csv import reader
import pandas as pd
import time
import datetime
from tqdm import tqdm
import os as operations
from topSymbols import returnTopSymbols
from dateutil.relativedelta import relativedelta
import json

"""
URL FONTS
"""

topSymbolList = returnTopSymbols()




def get_websites():
    
    website_url = []
    

    try:
        with open('/home/augustoviegascs/Documents/dotnet/EMAnalysisWeb/StockSymbolList.csv', 'r') as f:
            reader_line = reader(f)
            next(reader_line)  
            for row in reader_line:
                website_url.append(row[0])
    
    except FileNotFoundError:
        print("File not found/Error generic 102")
    """print(f"\nFULL STOCK LIST SYMBOLS: {website_url}")"""
    
    website_index_list = []
    
    for line in website_url:
        elements = [item.strip().strip("'") for item in line.split(',')]
        website_index_list.extend(elements)
        
    """print(f"\nSYMBOL SELECTED: {website_index_list[1]}")"""
    """print(f"SYMBOLS AVAILABLE: {len(website_index_list)}")"""
    
    len_website = len(website_index_list)


    convert_period01 = datetime.datetime.now() - relativedelta(years=5)
    period01_timedate = int(convert_period01.timestamp())
    period02_timedate = int(datetime.datetime.now().timestamp())

    yahoo_url_topsymbol = [f"https://finance.yahoo.com/quote/{symbol}/history/?period1={period01_timedate}&period2={period02_timedate}" for symbol in topSymbolList]
    yahoo_url_list = [f"https://finance.yahoo.com/quote/{symbol}/history/?period1={period01_timedate}&period2={period02_timedate}" for symbol in website_index_list]
    
            
    return yahoo_url_topsymbol
            
       
def init_scrape(driver, site_url, stock_symbol):
    
    
    driver.get(site_url)
    time.sleep(5.5)

    rows_added = driver.find_elements(By.XPATH, '//table[contains(@class, "yf-1jecxey")]//tr')
    #stock_symbol = driver_firefox.find_element(By.CLASS_NAME, "yf-4vbjci").text
    
    
    dir_stocklist = pathlib.Path("/home/augustoviegascs/Documents/dotnet/EMAnalysisWeb2.0/EMAnalysisWeb2.0/StockSymbolList.csv")
    dir_read = pd.read_csv(dir_stocklist)
    
    
    StockSymbolList = []
    for index, row in dir_read.iterrows():
        StockSymbolList.append({
            "symbol": row["symbol"],
            "name": row["name"]
        })
        
    #stock_symbol = StockSymbolList[index]["symbol"]
    #stock_name = StockSymbolList[index]["name"]
    print(stock_symbol)
    
    data_list = {
        "STOCK SYMBOL": stock_symbol,
        "timestamp": {}
    }
    
    for row in tqdm(rows_added, desc=f"Downloading {stock_symbol} data", leave=False, colour="green"):
        cols = row.find_elements(By.TAG_NAME, 'td')
        if len(cols) > 5:
            date_history = cols[0].text
            open_price = cols[1].text
            high_price = cols[2].text
            low_price = cols[3].text
            close_price = cols[4].text
            volume_total = cols[6].text
            
            date_history = datetime.datetime.strptime(date_history, "%b %d, %Y")
            date_history = date_history.strftime("%Y-%m-%d")
            
            volume_total = volume_total.replace('"', '')
            

            data_list["timestamp"][date_history] = {
                "open": open_price,
                "high": high_price,
                "low": low_price,
                "close": close_price,
                "volume": volume_total
            }

     
    dictionary_frame = pd.DataFrame.from_dict(data_list["timestamp"], orient='index')
    dictionary_frame.index.name = "timestamp"
    
    create_dir = operations.makedirs(f'StockData/{datetime.date.today()}', exist_ok=True)
    if create_dir == False:
        print(f'Directory creation failed! {OSError}')
    
    dictionary_frame.to_csv(f"StockData/{datetime.date.today()}/{stock_symbol}.csv")
            

def scrapper():
    
    options = Options()
    options.add_argument("--headless")
    options.add_argument("--headless")  # roda sem interface gráfica
    options.add_argument("--disable-gpu")  # desativa GPU (não precisa pra texto)
    options.add_argument("--disable-extensions")  # sem extensões
    options.add_argument("--disable-dev-shm-usage")  # evita gargalos de memória compartilhada
    options.add_argument("--no-sandbox")  # libera algumas restrições (cuidado em prod)
    
    # reduzir uso de recursos
    options.set_preference("permissions.default.image", 2)  # bloqueia imagens
    options.set_preference("dom.ipc.plugins.enabled.libflashplayer.so", "false")  # sem flash
    options.set_preference("media.autoplay.default", 1)  # não carrega áudio/vídeo
    options.set_preference("javascript.enabled", True)  # mantém JS (se a página precisar renderizar)
    options.set_preference("network.http.pipelining", True)  # melhora requisições
    options.set_preference("network.http.proxy.pipelining", True)
    options.set_preference("network.http.max-connections", 96)
    options.set_preference("network.http.max-persistent-connections-per-server", 48)    
    

    driver_firefox = webdriver.Firefox(options=options)
    value = topSymbolList
    
    yahoo_website = get_websites()
    for index, site_url in enumerate(tqdm(yahoo_website, desc="Starting Data Process", colour="blue")):
        stock_symbol = topSymbolList[index]
        init_scrape(driver_firefox, site_url, stock_symbol)
    
    driver_firefox.quit()
    
