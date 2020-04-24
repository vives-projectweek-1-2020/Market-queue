import RPi.GPIO as GPIO
import time
import requests
GPIO.setmode(GPIO.BCM)

GPIO.setup(4, GPIO.IN)
oldstate = GPIO.LOW
while True:
    if GPIO.input(4) == GPIO.HIGH and oldstate == GPIO.LOW:
        print('LOW -> HIGH')
        oldstate = GPIO.HIGH
        requests.get('http://91.181.93.103:4040/add/visitor?area_id=1&duration=1')
        time.sleep(0.5)
    elif GPIO.input(4) == GPIO.LOW and oldstate == GPIO.HIGH:
        print('HIGH -> LOW')
        oldstate = GPIO.LOW
        time.sleep(0.5) 
    



