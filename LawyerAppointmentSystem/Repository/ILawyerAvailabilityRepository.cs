using LawyerAppointmentSystem.DataBase;
using LawyerAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LawyerAppointmentSystem.Repository;

public interface ILawyerAvailabilityRepository
{
    Task<IEnumerable<LawyerAvailability>>GetAllLawyerAvailabilityAsync(CancellationToken cancellationToken); 
    Task<LawyerAvailability> AddLawyerAvailabilityAsync(LawyerAvailability lawyerAvailability, CancellationToken cancellationToken);
    Task<LawyerAvailability> UpdateLawyerAvailabilityAsync(LawyerAvailability lawyerAvailability, CancellationToken cancellationToken);
    Task<LawyerAvailability> DeleteLawyerAvailabilityAsync(long id, CancellationToken cancellationToken);
    Task<LawyerAvailability> GetLawyerAvailabilityByIdAsync(long id, CancellationToken cancellationToken);
}
public class LawyerAvailabilityRepository : ILawyerAvailabilityRepository
{
    private readonly ApplicationDbContext _context;
    public LawyerAvailabilityRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<LawyerAvailability> AddLawyerAvailabilityAsync(LawyerAvailability lawyerAvailability, CancellationToken cancellationToken)
    {
        await _context.AddAsync(lawyerAvailability, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return lawyerAvailability;
    }
    public async Task<LawyerAvailability> DeleteLawyerAvailabilityAsync(long id, CancellationToken cancellationToken)
    {
        var data = await _context.LawyerAvailabilities.FindAsync(id , cancellationToken);
        if (data != null)
        {
            _context.LawyerAvailabilities.Remove(data);
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
    public async Task<IEnumerable<LawyerAvailability>> GetAllLawyerAvailabilityAsync(CancellationToken cancellationToken)
    {
        var data = await _context.LawyerAvailabilities.Include(x=>x.Lawyer).ToListAsync(cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }
    public async Task<LawyerAvailability> GetLawyerAvailabilityByIdAsync(long id, CancellationToken cancellationToken)
    {
        var data = await _context.LawyerAvailabilities.FindAsync(id , cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }
    public async Task<LawyerAvailability> UpdateLawyerAvailabilityAsync(LawyerAvailability lawyerAvailability, CancellationToken cancellationToken)
    {
        var data = await _context.LawyerAvailabilities.FindAsync(lawyerAvailability.Id , cancellationToken);
        if (data != null)
        {
            data.Day = lawyerAvailability.Day;
            data.StartTime = lawyerAvailability.StartTime;
            data.EndTime = lawyerAvailability.EndTime;
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
}
