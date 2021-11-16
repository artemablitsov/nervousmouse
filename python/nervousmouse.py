import pyautogui
import math
import time
from random import randrange

moves = [-10,-9,-8,-7,-6,-5,-4,-3,-2,-1,0,1,2,3,4,5,6,7,8,9,10]
w,h = pyautogui.size()
radius_big = 3.0*h/(4.0 * 2.0)
radius_small = radius_big/10
startx, starty = w/2,h/2
pos = 0
while (True):
    for iouter in range(-180,180,10):        
        angleouter = float(iouter) * math.pi / 180
        xcenter = int(radius_big * math.cos(angleouter))
        ycenter = int(radius_big * math.sin(angleouter))
        startxcenter, startycenter = w/2 + xcenter,h/2 + ycenter
        for iinner in range(-180,180,10):        
            angleinner = float(iinner) * math.pi / 180
            x = int(radius_small * math.cos(angleinner))
            y = int(radius_small * math.sin(angleinner))
            pyautogui.moveTo(startxcenter + x, startycenter + y)
        pyautogui.moveTo(startx, starty)
        pyautogui.scroll(moves[pos], x=startx, y=starty)
        time.sleep(10)
        if pos >= len(moves) - 1:
            pos = 0
        else:
            pos = pos + 1

