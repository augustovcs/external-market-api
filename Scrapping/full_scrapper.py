from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.support.ui import WebDriverWait
from csv import reader, writer

"""
URL FONTS
"""

def get_websites():
    
    symbol_list = reader(open('/home/augustoviegascs/Documents/dotnet/api_external_scrapper/StockSymbolList.csv', 'r'))
    
    for symbol in symbol_list:
        website_url = []
        website_url.append(f"https://finance.yahoo.com/quote/{symbol}/history/")
        print(website_url)


def scrape(url):
    driver = webdriver.Firefox
    driver_reserve = webdriver.Chrome
    