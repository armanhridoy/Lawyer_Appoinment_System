using LawyerAppointmentSystem.DataBase;
using LawyerAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LawyerAppointmentSystem.Repository;

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken);
    Task<Payment> AddPaymentAsync(Payment payment, CancellationToken cancellationToken);
    Task<Payment> UpdatePaymentAsync(Payment payment, CancellationToken cancellationToken);
    Task<Payment> DeletePaymentAsync(long id, CancellationToken cancellationToken);
    Task<Payment> GetPaymentByIdAsync(long id, CancellationToken cancellationToken);
}
public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _context;
    public PaymentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Payment> AddPaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        await _context.AddAsync(payment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return payment;
    }

    public async Task<Payment> DeletePaymentAsync(long id, CancellationToken cancellationToken)
    {
        var data = await _context.Payments.FindAsync(id, cancellationToken);
        if (data != null)
        {
            _context.Payments.Remove(data);
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }

    public async Task<IEnumerable<Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken)
    {
        var data = await _context.Payments.ToListAsync(cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Payment> GetPaymentByIdAsync(long id, CancellationToken cancellationToken)
    {
        var data =await _context.Payments.FindAsync(id, cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Payment> UpdatePaymentAsync(Payment payment, CancellationToken cancellationToken)
    {
        var data = await _context.Payments.FindAsync(payment.Id, cancellationToken);
        if (data != null)
        {
            data.Amount = payment.Amount;
            data.PaymentDate = payment.PaymentDate;
            //data.AppointmentId = payment.AppointmentId;
            await _context.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
}
