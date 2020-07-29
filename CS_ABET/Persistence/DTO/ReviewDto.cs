using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CS_ABET.Persistence.DTO
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public int RatingOutOfFive { get; set; }

        public string ReviewText { get; set; }
    }
}