using BookingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces.Guest2
{
    public interface ITourFeedbackRepository
    {
        TourFeedback Add(TourFeedback tourFeedback);
    }
}
