using BookingApp.Domain.Models;
using BookingApp.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal class VoucherRepository
    {
        private const string filePath = "../../../Resources/Data/vouchers.csv";
        private readonly Serializer<Voucher> _serializer;
        private List<Voucher> _vouchers;

        public VoucherRepository()
        {
            _serializer = new Serializer<Voucher>();
            _vouchers = _serializer.FromCSV(filePath);
        }

        public Voucher Save(Voucher voucher)
        {
            voucher.id = NextId();
            _vouchers = _serializer.FromCSV(filePath);
            _vouchers.Add(voucher);
            _serializer.ToCSV(filePath, _vouchers);
            return voucher;
        }

        public List<Voucher> GetAll()
        {
            return _serializer.FromCSV(filePath);
        }

        public List<Voucher> GetVouchersByUserId(int userId)
        {
            List<Voucher> vouchers = GetAll();
            List<Voucher> userVouchers = new List<Voucher>();
           

            foreach (Voucher voucher in vouchers)
            {
                if (voucher.guest.id ==userId)
                {
                    userVouchers.Add(voucher);
                }
            }

            return userVouchers;
        }

        public Voucher Update(Voucher voucher)
        {
            _vouchers = _serializer.FromCSV(filePath);
            Voucher current = _vouchers.Find(v => v.id == voucher.id);
            int index = _vouchers.IndexOf(current);
            _vouchers.Remove(current);
            _vouchers.Insert(index, voucher);
            _serializer.ToCSV(filePath, _vouchers);
            return voucher;
        }

        private int NextId()
        {
            _vouchers = _serializer.FromCSV(filePath);
            if (_vouchers.Count < 1)
            {
                return 1;
            }
            return _vouchers.Max(v => v.id) + 1;
        }
    }
}
