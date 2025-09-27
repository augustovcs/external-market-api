import ast

from selenium import webdriver
from selenium.webdriver.common.by import By 
from csv import reader
import time
from tqdm import tqdm

"""
URL FONTS
"""

def get_websites():
    
    symbol_list = reader(open('/home/augustoviegascs/Documents/dotnet/api_external_scrapper/StockSymbolList.csv', 'r'))
    website_url = []

    with open('/home/augustoviegascs/Documents/dotnet/api_external_scrapper/StockSymbolList.csv', 'r') as f:
        reader_line = reader(f)
        next(reader_line)  
        for row in reader_line:
            website_url.append(row[0])
    print(f"\nFULL STOCK LIST SYMBOLS: {website_url}")
    
    website_index_list = []
    
    for line in website_url:
        elements = [item.strip().strip("'") for item in line.split(',')]
        website_index_list.extend(elements)
        
    print(f"\nSYMBOL SELECTED: {website_index_list[1]}")
    print(f"SYMBOLS AVAILABLE: {len(website_index_list)}")
    
    len_website = len(website_index_list)


    yahoo_url_list = [f"https://finance.yahoo.com/quote/{symbol}/history/" for symbol in website_index_list]
    
    
    
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
    driver_firefox = webdriver.Firefox()
    yahoo_website = get_websites()
    print(yahoo_website[0])

    
    driver_firefox.get(yahoo_website[0])
    time.sleep(5.5)
    
    data_list = {}
    
    rows_added = driver_firefox.find_elements(By.XPATH, '//table[contains(@class, "yf-1jecxey")]//tr')
    for row in rows_added[:30]:
        cols = row.find_elements(By.TAG_NAME, 'td')
        if len(cols) > 5:
            data = cols[0].text
            open_price =cols[1].text
            high_price = cols[2].text
            low_price = cols[3].text
            close_price = cols[4].text
            volume_total = cols[6].text
            data_list.update({
                "DATE": data,
              "OPEN PRICE": open_price,
              "HIGH PRICE": high_price,
              "LOW PRICE": low_price,
              "CLOSE PRICE": close_price,
              "VOLUME": volume_total
                              })
            
            print(data_list)
            
    driver_firefox.quit()
    
    driver_chrome = webdriver.Chrome
    
    
