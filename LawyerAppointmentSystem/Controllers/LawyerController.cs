using LawyerAppointmentSystem.Models;
using LawyerAppointmentSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LawyerAppointmentSystem.Controllers;

public class LawyerController : Controller
{
    private readonly ILawyerRepository _lawyerRepository;
    public LawyerController(ILawyerRepository lawyerRepository)
    {
        _lawyerRepository = lawyerRepository;
    }
    public async Task <IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _lawyerRepository.GetAllLawyersAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult>CreateOrEdit(long Id,CancellationToken cancellationToken)
    {
        if (Id == 0)
        {
            return View(new Lawyer());
        }
        else
        {
            var data = await _lawyerRepository.GetLawyerByIdAsync(Id, cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }
    }
    [HttpPost] 
    public async Task<IActionResult>CreateOrEdit(Lawyer lawyer,CancellationToken cancellationToken)
    {
        if (lawyer.Id == 0)
        {
            await _lawyerRepository.AddLawyerAsync(lawyer, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            await _lawyerRepository.UpdateLawyerAsync(lawyer, cancellationToken);
            return RedirectToAction("Index");
        }
    }
    [HttpGet]
    public async Task<IActionResult>Details(long id,CancellationToken cancellationToken)
    {
        var data = await _lawyerRepository.GetLawyerByIdAsync(id, cancellationToken);
        if (data != null)
        {
            return View(data);
        }
        return NotFound();
    }
    [HttpGet]
    public async Task<IActionResult>Delete(long id,CancellationToken cancellationToken)
    {
        var data = await _lawyerRepository.GetLawyerByIdAsync(id, cancellationToken);
        if (data == null)
        {
            return NotFound();
        }
        await _lawyerRepository.DeleteLawyerAsync(id, cancellationToken);
        return RedirectToAction("Index");

    }
}
