import ast
import os
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

"""
URL FONTS
"""

def get_websites():
    
    website_url = []

    try:
        with open('/home/augustoviegascs/Documents/dotnet/api_external_scrapper/StockSymbolList.csv', 'r') as f:
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


    yahoo_url_list = [f"https://finance.yahoo.com/quote/{symbol}/history/?period1=1601252877&period2=1759012790" for symbol in website_index_list]
    
    
    loop_true = False
    while loop_true:

        print("\n --- EM SCRAPPER API MENU 1.1.2 --- ")
        print("1 - START EM SCRAPPER API ")
        print("2 - YAHOO FULL STOCK LIST URLS ")
        print("3 - API FULL STOCK SYMBOL LIST ")
        print("4 - ANALYZE A SYMBOL ")
        print("5 - EXIT \n")
        
        choice_01 = input("Your choice: ")
        if choice_01 == "1":
            for i in tqdm(range(len_website), desc="Loading... ", colour='green', total=len_website):
                time.sleep(0.00010)
            print("AVAILABLE SOON!!!!!")
            
    
        if choice_01 == "2":
            for i in tqdm(range(len_website), desc="Loading... ", colour='green', total=len_website):
                time.sleep(0.00001)
            for url in yahoo_url_list[:]:
                print(url)
                
        if choice_01 == "3":
            for i in tqdm(range(len_website), desc="Loading... ", colour='green', total=len_website):
                time.sleep(0.00001)
            print(f"\nFULL STOCK LIST SYMBOLS: {website_url}")
        
        if choice_01 == "5":
            loop_true = False
            
        
    return yahoo_url_list
            
       


def scrape():

    options = Options()
    options.add_argument("--headless") 
    
    driver_firefox = webdriver.Firefox(options=options)
    yahoo_website = get_websites()
    print(yahoo_website[0])

    
    driver_firefox.get(yahoo_website[0])
    time.sleep(5.5)

    """
    date_history = 0
    open_price = 0
    high_price = 0
    low_price = 0
    close_price = 0
    volume_total = 0 """

    
    rows_added = driver_firefox.find_elements(By.XPATH, '//table[contains(@class, "yf-1jecxey")]//tr')
    #stock_symbol = driver_firefox.find_element(By.CLASS_NAME, "yf-4vbjci").text
    
    
    dir_stocklist = pathlib.Path("/home/augustoviegascs/Documents/dotnet/api_external_scrapper/StockSymbolList.csv")
    dir_read = pd.read_csv(dir_stocklist)
    
    
    StockSymbolList = []
    for index, row in dir_read.iterrows():
        StockSymbolList.append({
            "symbol": row["symbol"],
            "name": row["name"]
        })
        
    
    stock_symbol = StockSymbolList[0]["symbol"]
    stock_name = StockSymbolList[0]["name"]


    print(stock_symbol)
    
    data_list = {
        "STOCK SYMBOL": stock_symbol,
        "timestamp": {}
    }
    
    for row in rows_added:
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
            
    
            
    driver_firefox.quit()
    driver_chrome = webdriver.Chrome
    
    
    
    
