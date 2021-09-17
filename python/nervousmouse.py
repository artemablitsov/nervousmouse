import pyautogui
import math
import time
from random import randrange


while (True):
    startx, starty = pyautogui.position()    
    radius = 300
    for i in range(-180,180,10):        
        angle = float(i) * math.pi / 180
        x = int(radius * math.cos(angle))
        y = int(radius * math.sin(angle))
        pyautogui.moveTo(startx + x, starty + y)
        # time.sleep(0.02)
    rnd = randrange(-20, 20)
    pyautogui.scroll(rnd, x=startx, y=starty)
    time.sleep(15)
