using Microsoft.EntityFrameworkCore;
using Security.Entities;

namespace Security.Data;

public class UserRepository
{
    private readonly SecurityContext _context;

    public UserRepository(SecurityContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAll()
    {
        return _context.Users.ToList();
    }

    public User Get(long id)
    {
        return _context.Users.FirstOrDefault(b => b.Id == id);    
    }

    public void Add(User entity)
    {
        _context.Users.Add(entity);
        _context.SaveChanges();
    }

    public void Edit(User entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Remove(long id)
    {
        var item = Get(id);
        _context.Users.Remove(item);
        _context.SaveChanges();
    }

    public void ResetDb()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

}