using LawyerAppointmentSystem.DataBase;
using LawyerAppointmentSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LawyerAppointmentSystem.Repository;

public interface IApplicationUserRepository
{
    Task<IEnumerable<ApplicationUser>> GetAllApplicationUserAsync(CancellationToken cancellationToken);
    Task<ApplicationUser>AddApplicationUserAsync(ApplicationUser applicationUser, CancellationToken cancellationToken);  
    Task<ApplicationUser>UpdateApplicationUserAsync(ApplicationUser applicationUser, CancellationToken cancellationToken);
    Task<ApplicationUser> DeleteApplicationUser(long id, CancellationToken cancellationToken);
    Task<ApplicationUser> GetApplicationUserByIdasync(long id, CancellationToken cancellationToken);
    IEnumerable<SelectListItem> Dropdown();
}
public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly ApplicationDbContext _context;
    public ApplicationUserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ApplicationUser> AddApplicationUserAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
    {
        await _context.AddAsync(applicationUser);
        await _context.SaveChangesAsync(cancellationToken);
        return applicationUser;
    }
    public async Task<ApplicationUser> DeleteApplicationUser(long id, CancellationToken cancellationToken)
    {
        var user = await _context.ApplicationUsers.FindAsync(id , cancellationToken);
        if (user != null)
        {
            _context.ApplicationUsers.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return null; //////check
    }

    public IEnumerable<SelectListItem> Dropdown()
    {
        var data = _context.ApplicationUsers
            .Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString()
            })
            .ToList();

        return data;
    }



    public async Task<IEnumerable<ApplicationUser>> GetAllApplicationUserAsync(CancellationToken cancellationToken)
    {
        var data = await _context.ApplicationUsers.ToListAsync(cancellationToken);
        if(data != null)
        {
            return data;
        }
        return null;
    }
    public async Task<ApplicationUser> GetApplicationUserByIdasync(long id, CancellationToken cancellationToken)
    {
        var user = await _context.ApplicationUsers.FindAsync(id , cancellationToken);
        if (user != null)
        {
            return user;
        }
        return null;
    }
    public async Task<ApplicationUser> UpdateApplicationUserAsync(ApplicationUser applicationUser, CancellationToken cancellationToken)
    {
        var data = await _context.ApplicationUsers.FindAsync(applicationUser.Id , cancellationToken);
        if (data != null)
        {
            data.FullName = applicationUser.FullName;
            data.Email = applicationUser.Email;
            data.Phone = applicationUser.Phone;
            //data.CreatedAt = applicationUser.CreatedAt;
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
}
