using CS_ABET.Persistence;
using CS_ABET.Persistence.DTO;
using CS_ABET.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CS_ABET.Controllers.Enterprise
{
    public class ReviewEC
    {
        public async Task<IEnumerable<ReviewDto>> Get(int id)
        {
            var results = new List<ReviewDto>();
            using (var db = new AbetContext())
            {
                try
                {
                    results = await db.Reviews.Where(c => c.Id == id)
                    .Select(s =>
                    new ReviewDto
                    {
                        Id = s.Id,
                        RatingOutOfFive = s.RatingOutOfFive,
                        ReviewText = s.ReviewText
                    }).ToListAsync();
                }
                catch (Exception e)
                {

                }
            }

            return results;
        }

        public async Task<IEnumerable<ReviewDto>> Get()
        {
            var results = new List<ReviewDto>();
            using (var db = new AbetContext())
            {
                try
                {
                    results = await db.Reviews.Select(s =>
                    new ReviewDto
                    {
                        Id = s.Id,
                        RatingOutOfFive = s.RatingOutOfFive,
                        ReviewText = s.ReviewText
                    }).ToListAsync();
                }
                catch (Exception e)
                {

                }
            }

            return results;
        }

        public async Task<ReviewDto> AddOrUpdate(ReviewDto courseDto)
        {
            using (var db = new AbetContext())
            {
                db.Reviews.AddOrUpdate(
                        new Review
                        {
                            Id = courseDto.Id,
                            RatingOutOfFive = courseDto.RatingOutOfFive,
                            ReviewText = courseDto.ReviewText
                        });
                await db.SaveChangesAsync();
            }


            return courseDto;
        }

        public ReviewDto Remove(ReviewDto courseDto)
        {
            using (var db = new AbetContext())
            {
                var result = db.Reviews.Remove(new Review
                {
                    Id = courseDto.Id,
                    RatingOutOfFive = courseDto.RatingOutOfFive,
                    ReviewText = courseDto.ReviewText
                });
                db.SaveChanges();
                return new ReviewDto
                {
                    Id = result.Id,
                    RatingOutOfFive = courseDto.RatingOutOfFive,
                    ReviewText = courseDto.ReviewText
                };
            }
        }
    }
}