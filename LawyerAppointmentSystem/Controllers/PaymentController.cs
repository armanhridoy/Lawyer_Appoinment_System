using LawyerAppointmentSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LawyerAppointmentSystem.Controllers;

public class PaymentController : Controller
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    public PaymentController(IPaymentRepository paymentRepository, IAppointmentRepository appointmentRepository)
    {
        _paymentRepository = paymentRepository;
        _appointmentRepository = appointmentRepository;
    }

    public async Task <IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _paymentRepository.GetAllPaymentsAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(long id, CancellationToken cancellationToken)
    {
        ViewBag.Appointments = _appointmentRepository.DropDown();
        if (id == 0)
        {
            return View(new Models.Payment());
        }
        else
        {
            var data = await _paymentRepository.GetPaymentByIdAsync(id, cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }
    }
    [HttpPost]
    public async  Task<IActionResult>CreateOrEdit(Models.Payment payment, CancellationToken cancellationToken)
    {
        if (payment.Id == 0)
        {
            await _paymentRepository.AddPaymentAsync(payment, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            await _paymentRepository.UpdatePaymentAsync(payment, cancellationToken);
            return RedirectToAction("Index");
        }
    }
    [HttpGet]
    public async Task<IActionResult> Details(long id, CancellationToken cancellationToken)
    {
        var data = await _paymentRepository.GetPaymentByIdAsync(id, cancellationToken);
        if (data != null)
        {
            return View(data);
        }
        return NotFound();
    }
        [HttpGet]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            var data = await _paymentRepository.DeletePaymentAsync(id, cancellationToken);
            if (data != null)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
    }
}
