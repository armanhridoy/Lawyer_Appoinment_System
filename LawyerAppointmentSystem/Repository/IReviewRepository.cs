using LawyerAppointmentSystem.DataBase;
using LawyerAppointmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LawyerAppointmentSystem.Repository;

public interface IReviewRepository
{
    Task<IEnumerable<Models.Review>> GetAllReviewsAsync(CancellationToken cancellationToken);
    Task<Review> AddReviewAsync(Review review, CancellationToken cancellationToken);
    Task<Review> UpdateReviewAsync(Review review, CancellationToken cancellationToken);
    Task<Review> DeleteReviewAsync(long id, CancellationToken cancellationToken);
    Task<Review> GetReviewByIdAsync(long id, CancellationToken cancellationToken);
}
public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _reviewRepository;
     public ReviewRepository(ApplicationDbContext reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }
    public async Task<Review> AddReviewAsync(Review review, CancellationToken cancellationToken)
    {
        await _reviewRepository.AddAsync(review, cancellationToken);
        await _reviewRepository.SaveChangesAsync(cancellationToken);
        return review;
    }

    public async Task<Review> DeleteReviewAsync(long id, CancellationToken cancellationToken)
    {
        var data = await _reviewRepository.Reviews.FindAsync(id, cancellationToken);
        if (data != null)
        {
            _reviewRepository.Reviews.Remove(data);
            await _reviewRepository.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }

    public async Task<IEnumerable<Review>> GetAllReviewsAsync(CancellationToken cancellationToken)
    {
        var data = await _reviewRepository.Reviews.ToListAsync(cancellationToken);
        if (data != null)
        {
            return data;
        }
        return null;
    }

    public async Task<Review> GetReviewByIdAsync(long id, CancellationToken cancellationToken)
    {
        var data = await _reviewRepository.Reviews.FindAsync(id, cancellationToken);        if (data != null)
        {
            return data;
        }
            return null;
    }

    public async Task<Review> UpdateReviewAsync(Review review, CancellationToken cancellationToken)
    {
        var data = await _reviewRepository.Reviews.FindAsync(review.Id, cancellationToken);
        if (data != null)
        {
            data.Rating = review.Rating;
            data.Comment = review.Comment;
            await _reviewRepository.SaveChangesAsync(cancellationToken);
            return data;
        }
        return null;
    }
}
