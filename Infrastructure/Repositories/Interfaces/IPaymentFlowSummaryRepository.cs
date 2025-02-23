using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.PaymentFlowSummary;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IPaymentFlowSummaryRepository
    {
        public Task InsertPaymentFlow(PaymentFlowSummary paymentRequest);
        public Task SavePaymentSchedule(List<PaymentSchedule> paymentSchedule, decimal paymentId);
    }
}
