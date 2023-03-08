import socket

endpoint = ('localhost', 8000)

client = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
client.connect(endpoint)

message = bytes([0x0, 0x01, 0x02, 0x03, 0x04, 0x05])
client.send(message)

#(data, address) = s.recvfrom(1024)