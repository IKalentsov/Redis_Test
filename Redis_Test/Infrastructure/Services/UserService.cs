using Microsoft.Extensions.Caching.Distributed;
using Redis_Test.Domain.Entities;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Redis_Test.Infrastructure.Services;

public class UserService
{
    private readonly AppDbContext _db;
    private readonly IDistributedCache _cache;

    public UserService(AppDbContext context, IDistributedCache distributedCache)
    {
        _db = context;
        _cache = distributedCache;
    }

    public async Task<User?> GetUser(int id)
    {
        User? user = null;
        // пытаемся получить данные из кэша по id
        var userString = await _cache.GetStringAsync(id.ToString());
        //десериализируем из строки в объект User
        if (userString != null) user = JsonSerializer.Deserialize<User>(userString);

        // если данные не найдены в кэше
        if (user == null)
        {
            // обращаемся к базе данных
            user = await _db.Users.FindAsync(id);
            // если пользователь найден, то добавляем в кэш
            if (user != null)
            {
                Console.WriteLine($"{user.Name} извлечен из базы данных");
                // сериализуем данные в строку в формате json
                userString = JsonSerializer.Serialize(user);
                // сохраняем строковое представление объекта в формате json в кэш на 1 минуту
                await _cache.SetStringAsync(user.Id.ToString(), userString, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                });
            }
        }
        else
        {
            Console.WriteLine($"{user.Name} извлечен из кэша");
        }
        return user;
    }
}
