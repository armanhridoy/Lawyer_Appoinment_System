using LawyerAppointmentSystem.Models;
using LawyerAppointmentSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LawyerAppointmentSystem.Controllers;

public class ApplicationUserController : Controller
{
    private readonly IApplicationUserRepository _applicationUserRepository;
    public ApplicationUserController(IApplicationUserRepository applicationUserRepository)
    {
        _applicationUserRepository = applicationUserRepository;
    }

    public async Task <IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _applicationUserRepository.GetAllApplicationUserAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(long id, CancellationToken cancellationToken)
    {
        if (id == 0)
        {
            return View(new ApplicationUser());
        }
        else
        {
            var data = await _applicationUserRepository.GetApplicationUserByIdasync(id, cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }
    }
    [HttpPost]
    public async Task <IActionResult> CreateOrEdit (ApplicationUser applicationUser ,CancellationToken cancellationToken)
    {
        if (applicationUser.Id == 0)
        {
            await _applicationUserRepository.AddApplicationUserAsync(applicationUser, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            await _applicationUserRepository.UpdateApplicationUserAsync(applicationUser, cancellationToken);
            return RedirectToAction("Index");   
        }   
    }
    [HttpGet]
    public async Task<IActionResult>Details (long id, CancellationToken cancellationToken)
    {
        var data = await _applicationUserRepository.GetApplicationUserByIdasync(id, cancellationToken);
        if (data != null)
        {
            return View(data);
        }
        return NotFound();
    }
    [HttpGet]
    public async Task<IActionResult> Delete (long id, CancellationToken cancellationToken)
    {
        var data = await _applicationUserRepository.GetApplicationUserByIdasync(id, cancellationToken);
        if (data != null)
        {
            await _applicationUserRepository.DeleteApplicationUser(id, cancellationToken);
            return RedirectToAction("Index");
            // return View(data);
        }
        return NotFound();
    }
}
