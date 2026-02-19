using LawyerAppointmentSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LawyerAppointmentSystem.Controllers;

public class AppointmentController : Controller
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly ILawyerRepository _lawyerRepository;
    private readonly IApplicationUserRepository _applicationUserRepository;
    public AppointmentController(IAppointmentRepository appointmentRepository, ILawyerRepository lawyerRepository, IApplicationUserRepository applicationUserRepository)
    {
        _appointmentRepository = appointmentRepository;
        _lawyerRepository = lawyerRepository;
        _applicationUserRepository = applicationUserRepository;
    }
    public async Task <IActionResult> Index (CancellationToken cancellationToken)
    {
        var data = await _appointmentRepository.GetAllAppointmentsAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(long id, CancellationToken cancellationToken)
    {
        ViewBag.LawyerId = _lawyerRepository.Dropdown();
        ViewBag.ApplicationUserId = _applicationUserRepository.Dropdown();
        if (id == 0)
        {
            return View(new Models.Appointment());
        }
        else
        {
            var data = await _appointmentRepository.GetAppointmentByIdAsync(id, cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(Models.Appointment appointment, CancellationToken cancellationToken)
    {
        if (appointment.Id == 0)
        {
            await _appointmentRepository.AddAppointmentAsync(appointment, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            await _appointmentRepository.UpdateAppointmentAsync(appointment, cancellationToken);
            return RedirectToAction("Index");
        }
    }
    [HttpGet]
    public async Task<IActionResult> Details(long id, CancellationToken cancellationToken)
    {
        var data = await _appointmentRepository.GetAppointmentByIdAsync(id, cancellationToken);
        if (data != null)
        {
            return View(data);
        }
        return NotFound();
    }
    [HttpGet]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var data = await _appointmentRepository.GetAppointmentByIdAsync(id, cancellationToken);
        if (data != null)
        {
            return View(data);
        }
        return NotFound();
    }
}
