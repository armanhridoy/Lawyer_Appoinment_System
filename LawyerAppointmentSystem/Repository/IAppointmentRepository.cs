using LawyerAppointmentSystem.DataBase;
using LawyerAppointmentSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LawyerAppointmentSystem.Repository;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(CancellationToken cancellationToken);
    Task<Appointment> AddAppointmentAsync(Appointment appointment, CancellationToken cancellationToken);
    Task<Appointment> UpdateAppointmentAsync(Appointment appointment, CancellationToken cancellationToken);
    Task<Appointment> DeleteAppointmentAsync(long id, CancellationToken cancellationToken);
    Task <Appointment> GetAppointmentByIdAsync(long id, CancellationToken cancellationToken);
    IEnumerable<SelectListItem> DropDown();
}
public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationDbContext _context;
    public AppointmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Appointment>AddAppointmentAsync(Appointment appointment, CancellationToken cancellationToken)
    {
        await _context.AddAsync(appointment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return appointment;
    }
    public async Task<Appointment> DeleteAppointmentAsync(long id, CancellationToken cancellationToken)
    {
        var appointment = await _context.Appointments.FindAsync(id, cancellationToken);
        if (appointment != null)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync(cancellationToken);
            return appointment;
        }
        return null;
    }

    public IEnumerable<SelectListItem> DropDown()
    {
        var data = _context.Appointments.Select(x => new SelectListItem
        { Text = x.ProblemDetails, Value = x.Id.ToString()}).ToList();
        return data;
    }


    public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(CancellationToken cancellationToken)
    {
        var data = await _context.Appointments.Include(x=>x.AppointmentDate).ToListAsync(cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Appointment> GetAppointmentByIdAsync(long id, CancellationToken cancellationToken)
    {
        var data = await _context.Appointments.FindAsync(id, cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment, CancellationToken cancellationToken)
    {
        var data = await _context.Appointments.FindAsync(appointment.Id, cancellationToken);
        if (data != null)
        {
            data .LawyerId = appointment.LawyerId;
            //data.ApplicationUserId = appointment.ApplicationUserId;
            data.AppointmentDate = appointment.AppointmentDate;
            //data.Status = appointment.Status;
            data.ProblemDetails = appointment.ProblemDetails;
            _context.Appointments.Update(data);
            await _context.SaveChangesAsync(cancellationToken);
            return data;    
        }
        return null;
    }
}
