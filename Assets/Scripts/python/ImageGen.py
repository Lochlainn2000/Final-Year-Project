import socket
import sys



name = str(sys.argv[2])
s = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
s.connect(("127.0.0.1", 5056))

msg = s.recv(1024)
print(msg.decode("utf-8"))

# send prompt
s.send(bytes(sys.argv[1],"utf-8"))
#send name
print(name)
s.send(bytes(name,"utf-8"))
file = open("Assets/Resources/Generated/"+name+".png", "wb")
image_chunk = s.recv(2048)  # Adjust buffer size as needed
i = 1
print("Test: " + str(i))

# Save the received image
while image_chunk:
    i+=1
    print("Test: " + str(i))
    file.write(image_chunk)
    image_chunk = s.recv(2048)

file.close()
print("Image received and saved")