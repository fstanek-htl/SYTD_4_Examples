import socket

def wuff(count, volume):
    client = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    client.connect(('localhost', 8118))

    message = bytes([count, volume])
    client.send(message)

wuff(2, 120)