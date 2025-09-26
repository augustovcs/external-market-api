import ast

from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.support.ui import WebDriverWait
from csv import reader, writer
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
    
    
    loop_true = True
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
                time.sleep(0.00006)
            for url in yahoo_url_list[:]:
                print(url)
                
        if choice_01 == "3":
            for i in tqdm(range(len_website), desc="Loading... ", colour='green', total=len_website):
                time.sleep(0.00003)
            print(f"\nFULL STOCK LIST SYMBOLS: {website_url}")
        
        if choice_01 == "5":
            loop_true = False
            
       


def scrape(url):
    driver = webdriver.Firefox
    driver_reserve = webdriver.Chrome
    