using Magalog.Data.Context;
using Magalog.Domain.Entitites;
using Magalog.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Magalog.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;
    public async Task Add(List<User> users)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            foreach (var user in users)
            {
                var existUser = await _context.Users
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(u => u.User_Id == user.User_Id);
                
                if (existUser == null)
                {
                    await _context.Users.AddAsync(user);
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }       
    }
}
