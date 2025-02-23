using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCase.Dtos
{
    public class LoanResponse
    {
        public decimal monthlyPayment { get; set; }
        public decimal totalInterest { get; set; }
        public decimal totalPayment { get; set; }
        public List<PaymentScheduleResponse> paymentSchedule { get; set; }
    }

    public class PaymentScheduleResponse
    {
        public int month { get; set; }
        public decimal principal { get; set; }
        public decimal interest { get; set; }
        public decimal balance { get; set; }
    }

}