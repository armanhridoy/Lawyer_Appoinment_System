using LawyerAppointmentSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LawyerAppointmentSystem.Controllers;

public class LawyerAvailabilityController : Controller
{
    private readonly ILawyerAvailabilityRepository _lawyerAvailabilityRepository;
    private readonly ILawyerRepository _lawyerRepository;
    public LawyerAvailabilityController(ILawyerAvailabilityRepository lawyerAvailabilityRepository, ILawyerRepository lawyerRepository)
    {
        _lawyerAvailabilityRepository = lawyerAvailabilityRepository;
        _lawyerRepository = lawyerRepository;
    }
    public async Task <IActionResult> Index(CancellationToken cancellationToken)
    {

        var data = await _lawyerAvailabilityRepository.GetAllLawyerAvailabilityAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> CreateOrEdit(long id, CancellationToken cancellationToken)
    {
        ViewBag.LawyerId = _lawyerRepository.Dropdown();
        if (id == 0)
        { 
            return View(new Models.LawyerAvailability());
        }
        else
        {
            var data = await _lawyerAvailabilityRepository.GetLawyerAvailabilityByIdAsync(id, cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(Models.LawyerAvailability lawyerAvailability, CancellationToken cancellationToken)
    {
        if (lawyerAvailability.Id == 0)
        {
            await _lawyerAvailabilityRepository.AddLawyerAvailabilityAsync(lawyerAvailability, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            await _lawyerAvailabilityRepository.UpdateLawyerAvailabilityAsync(lawyerAvailability, cancellationToken);
            return RedirectToAction("Index");
        }
     }
     [HttpGet]
     public async Task<IActionResult> Details(long id, CancellationToken cancellationToken)
     {
         var data = await _lawyerAvailabilityRepository.GetLawyerAvailabilityByIdAsync(id, cancellationToken);
         if (data != null)
         {
             return View(data);
         }
         return NotFound();
     }
     [HttpGet]
     public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
     {
         var data = await _lawyerAvailabilityRepository.DeleteLawyerAvailabilityAsync(id, cancellationToken);
         if (data == null)
         {
            return NotFound();

        }
         await _lawyerAvailabilityRepository.DeleteLawyerAvailabilityAsync(id, cancellationToken);
            return RedirectToAction("Index");
    }
}
