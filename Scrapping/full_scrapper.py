from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from selenium.webdriver.support.ui import WebDriverWait


"""
URL FONTS
"""

def get_websites():
    symbol_list = ['IBM', 'GOOG', 'AAPL', 'MSFT', 'FB']
    for symbol in symbol_list:
        website_url = []
        website_url.append(f"https://finance.yahoo.com/quote/{symbol}/history/")
        print(website_url)


def scrape(url):
    driver = webdriver.Firefox
    driver_reserve = webdriver.Chrome
    