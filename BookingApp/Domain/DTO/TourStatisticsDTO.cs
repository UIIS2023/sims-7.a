using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.DTO
{
    public class TourStatisticsDTO
    {
        public int withVoucher { get; set; }
        public int withoutVoucher { get; set; }
        public int underAgeGuests { get; set; }
        public int adultGuests { get; set; }
        public int elderlyGuests { get; set; }
        public int totalGuests { get; set; }
        public double voucherPercentage { get; set; }
        public double withoutVoucherPercentage { get; set; }
        public double underAgePercentage { get; set; }
        public double adultPercentage { get; set; }
        public double elderlyPercentage { get; set; }


        public TourStatisticsDTO() 
        {
            this.adultGuests = 0;
            this.withoutVoucher = 0;
            this.withVoucher = 0;
            this.elderlyGuests = 0;
            this.underAgeGuests = 0;
        }



    }
}
