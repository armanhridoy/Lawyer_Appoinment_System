using LawyerAppointmentSystem.DataBase;
using LawyerAppointmentSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LawyerAppointmentSystem.Repository;

public interface ILawyerRepository
{
    Task<IEnumerable<Lawyer>> GetAllLawyersAsync(CancellationToken cancellationToken);
    Task<Lawyer>AddLawyerAsync(Lawyer lawyer, CancellationToken cancellationToken);
    Task<Lawyer> UpdateLawyerAsync(Lawyer lawyer, CancellationToken cancellationToken);
    Task<Lawyer> DeleteLawyerAsync(long id, CancellationToken cancellationToken);
    Task<Lawyer> GetLawyerByIdAsync(long id, CancellationToken cancellationToken);
    IEnumerable<SelectListItem> Dropdown();
}
public class LawyerRepository : ILawyerRepository
{
    private readonly ApplicationDbContext _context;
    public LawyerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Lawyer> AddLawyerAsync(Lawyer lawyer, CancellationToken cancellationToken)
    {
        await _context.AddAsync(lawyer, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return lawyer;
    }

    public async Task<Lawyer> DeleteLawyerAsync(long id, CancellationToken cancellationToken)
    {
        var data = await _context.Lawyers.FindAsync(id, cancellationToken);
        if (data != null)
        {
            _context.Lawyers.Remove(data);
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }

    public IEnumerable<SelectListItem> Dropdown()
    {
       var data = _context.Lawyers.Select(x => new SelectListItem
       { Text = x.FullName, Value = x.Id.ToString() }).ToList();
        return data;
    }

    public async Task<IEnumerable<Lawyer>> GetAllLawyersAsync(CancellationToken cancellationToken)
    {
        var data = await _context.Lawyers.ToListAsync(cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Lawyer> GetLawyerByIdAsync(long id, CancellationToken cancellationToken)
    {
        var data = await _context.Lawyers.FindAsync(id, cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Lawyer> UpdateLawyerAsync(Lawyer lawyer, CancellationToken cancellationToken)
    {
        var data = await _context.Lawyers.FindAsync(lawyer.Id, cancellationToken);
        if (data != null)
        {
            data.FullName = lawyer.FullName;
            data.Specialization = lawyer.Specialization;
            data.ExperienceYear = lawyer.ExperienceYear;
            data.ConsultationFee = lawyer.ConsultationFee;
            data.IsActive = lawyer.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
            return data;

        }
        return null;
    }
}
