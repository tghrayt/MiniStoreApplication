using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiniStore.Context;
using MiniStore.Helper;
using MiniStore.Models;

namespace MiniStore;

public class DataBaseConfig
{
    public static void CreateMemoryDataBase(IServiceCollection services)
    {
        services.AddDbContext<StoreContext>(options =>
            options.UseInMemoryDatabase("StoreDB"));
        UsersInit(services);
    }
    private static void UsersInit(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        using (var context = serviceProvider.GetRequiredService<StoreContext>())
        {
            var user = new User(){UserName = "admin"};
            byte[] PasswordHash, PasswordSalt;
            UserHelper.CreatePasswordHash("admin", out PasswordHash, out PasswordSalt);
            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}