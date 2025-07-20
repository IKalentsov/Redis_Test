# Redis_Test

# Установка WSL и Redis

1. Установить WSL для работы с Redis на Windows
``` bash
wsl --install
```
2. Далее ввести эти команды:
```bash
curl -fsSL https://packages.redis.io/gpg | sudo gpg --dearmor -o /usr/share/keyrings/redis-archive-keyring.gpg

echo "deb [signed-by=/usr/share/keyrings/redis-archive-keyring.gpg] https://packages.redis.io/deb $(lsb_release -cs) main" | sudo tee /etc/apt/sources.list.d/redis.list

sudo apt-get update
sudo apt-get install redis
```
3. Запускаем такой командой:
```bash
sudo service redis-server start
```

# Запуск проекта
1. Если нет файла appsettings.Development.json - добавить с такой конфигурацией
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=Redis_Test;User Id=postgres;Password=admin"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database": "Information"
    }
  }
}
```
2. При запуске проекта будет запущен сам проект и развернута БД PostgreSQL.

# Проверка работы
1. В Postman вбиваем команду `GET` по адресу
`https://localhost:xxxx/user/1`

Вместо `xxxx` вставить свой порт, у меня `7181`.


# Дополнительная информация.
1. Проект сделан на .Net 9
2. Используется БД PostgreSQL
3. Добавлены миграции
