using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookingApp.Domain.Models.Guest1;
using IronPdf;

namespace BookingApp.Application.UseCases.Guest1
{
    public class PDFService
    {
        public PDFService()
        {

        }

        public void saveToPDF(AccommodationReservation reservation)
        {
            var renderer = new ChromePdfRenderer();
            var pdf = renderer.RenderHtmlAsPdf(processReservation(reservation));
            

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Save PDF",
                CheckPathExists = true,
                DefaultExt = "pdf",
                Filter = "pdf files (*.pdf)|*.pdf",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
  
                string filePath = saveFileDialog.FileName;
                //Convert HTML to PDF & save on path.
                
                pdf.SaveAs(filePath);
                //Clear HTML & Show Message.
 
                MessageBox.Show("File created successfully.");
            }

        }

        public string processReservation(AccommodationReservation reservation)
        {

            string str = "<h1>Reservation information:</h1><h3>Accommodation name: ";
            str = str + reservation.accomodation.name + "</h3><div>Location: " + reservation.accomodation.location.ToString() +
                "</div><div>Accommodation type: " + reservation.accomodation.type.ToString() + "</div><br><div>Check-in date: " + reservation.startDate.ToString("dd'/'MM'/'yyyy") +
                "</div><div>Check-out date: " + reservation.endDate.ToString("dd'/'MM'/'yyyy") + "</div><br><div>Number of guests: " + reservation.guestNumber.ToString() + "</div>";


            return str;

        }


    }
}
