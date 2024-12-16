import socket

def handle_client(client_socket):
    # Принимаем сообщение от клиента
    message = client_socket.recv(1024).decode('utf-8')
    print(f"Получено сообщение: {message}")
    
    # Отправляем ответ обратно клиенту
    client_socket.send("Ответ от сервера".encode('utf-8'))
    
    # Закрываем соединение
    client_socket.close()

def start_server():
    # Создаём TCP сокет
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    
    # Привязываем сокет к порту 8765
    server.bind(('localhost', 8765))
    
    # Ожидаем подключений
    server.listen(5)
    print("Сервер запущен на порту 8765")
    
    while True:
        # Принимаем подключение от клиента
        client_socket, addr = server.accept()
        print(f"Подключено: {addr}")
        
        # Обрабатываем клиента
        handle_client(client_socket)

# Запуск сервера
start_server()
