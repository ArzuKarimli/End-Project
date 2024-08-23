using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;
        public ReviewService(IReviewRepository repository)
        {
            _repository = repository;
        }
        public async Task AddReview(Review review)
        {
          await _repository.CreateAsync(review);    
        }

        public async Task<IEnumerable<Review>> GetAllReviews()
        {
           return await _repository.GetAllAsync();
        }
    }
}
