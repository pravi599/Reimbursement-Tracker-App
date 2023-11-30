using Microsoft.EntityFrameworkCore;
using ReimbursementTrackerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ReimbursementTrackerApp.Contexts;
using ReimbursementTrackerApp.Interfaces;

namespace ReimbursementTrackerApp.Repositories
{
    public class PaymentDetailsRepository : IRepository<int, PaymentDetails>
    {
        private readonly RTAppContext _context;

        public PaymentDetailsRepository(RTAppContext context)
        {
            _context = context;
        }

        // Implementing IRepository interface methods

        public PaymentDetails GetById(int paymentId)
        {
            return _context.PaymentDetails
                .Include(pd => pd.Request)
                .FirstOrDefault(pd => pd.PaymentId == paymentId);
        }

        public IList<PaymentDetails> GetAll()
        {
            return _context.PaymentDetails
                .Include(pd => pd.Request)
                .ToList();
        }

        public PaymentDetails Add(PaymentDetails paymentDetails)
        {

            _context.PaymentDetails.Add(paymentDetails);
            _context.SaveChanges();
            return paymentDetails;
        }

        public PaymentDetails Update(PaymentDetails paymentDetails)
        {
            _context.PaymentDetails.Update(paymentDetails);
            _context.SaveChanges();
            return paymentDetails;
        }

        public PaymentDetails Delete(int paymentId)
        {
            var paymentDetails = _context.PaymentDetails.Find(paymentId);
            if (paymentDetails != null)
            {
                _context.PaymentDetails.Remove(paymentDetails);
                _context.SaveChanges();
            }
            return paymentDetails;
        }
    }
}

