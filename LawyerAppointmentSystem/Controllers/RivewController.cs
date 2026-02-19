using LawyerAppointmentSystem.Models;
using LawyerAppointmentSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LawyerAppointmentSystem.Controllers;

public class RivewController : Controller
{
    private readonly IReviewRepository _reviewRepository;
    private readonly ILawyerRepository _lawyerRepository;
    public RivewController(IReviewRepository reviewRepository, ILawyerRepository lawyerRepository)
    {
        _reviewRepository = reviewRepository;
        _lawyerRepository = lawyerRepository;
    }

    public async Task <IActionResult> Index(CancellationToken cancellationToken)
    {
        var data = await _reviewRepository.GetAllReviewsAsync(cancellationToken);
        return View(data);
    }
    [HttpGet]
    public  async Task<IActionResult>CreateOrEdit(long id, CancellationToken cancellationToken)
    {
        ViewBag.Lawyers = _lawyerRepository.Dropdown();
        if (id == 0)
        {
            return View(new Models.Review());
        }
        else
        {
            var data = await _reviewRepository.GetReviewByIdAsync(id, cancellationToken);
            if (data != null)
            {
                return View(data);
            }
            return NotFound();
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateOrEdit(Models.Review review, CancellationToken cancellationToken)
    {
        if (review.Id == 0)
        {
            await _reviewRepository.AddReviewAsync(review, cancellationToken);
            return RedirectToAction("Index");
        }
        else
        {
            await _reviewRepository.UpdateReviewAsync(review, cancellationToken);
            return RedirectToAction("Index");
        }
     }
     [HttpGet]
     public async Task<IActionResult> Details(long id, CancellationToken cancellationToken)
     {
         var data = await _reviewRepository.GetReviewByIdAsync(id, cancellationToken);
         if (data != null)
         {
             return View(data);
         }
         return NotFound();
     }
     [HttpGet]
     public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
     {
         var data = await _reviewRepository.GetReviewByIdAsync(id, cancellationToken);
         if (data != null)
         {
             await _reviewRepository.DeleteReviewAsync(id, cancellationToken);
             return RedirectToAction("Index");
         }
         return NotFound();
    }
}
