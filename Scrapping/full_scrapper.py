import ast

from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.support.ui import WebDriverWait
from csv import reader, writer


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
        
    print(f"\nSYMBOL SELECTED: {website_index_list[412]}")
    print(f"SYMBOLS AVAILABLE: {len(website_index_list)}")
        
"""    for symbol in symbol_list:
        website_url = []
        website_url.append(f"https://finance.yahoo.com/quote/{symbol}/history/")
            
        print(website_url)"""


def scrape(url):
    driver = webdriver.Firefox
    driver_reserve = webdriver.Chrome
    