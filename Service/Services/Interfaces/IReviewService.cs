using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IReviewService
    {
        Task AddReview(Review review);
        Task<IEnumerable<Review>> GetAllReviews();
    }
}
