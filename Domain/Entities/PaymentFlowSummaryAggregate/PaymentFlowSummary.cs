namespace Domain.Entities.PaymentFlowSummary
{
    public class PaymentFlowSummary(decimal monthlyPayment, decimal totalInterest, decimal totalPayment, List<PaymentSchedule> paymentSchedule)
    {
        public decimal MonthlyPayment { get; set; } = monthlyPayment;
        public decimal TotalInterest { get; set; } = totalInterest;
        public decimal TotalPayment { get; set; } = totalPayment;
        public List<PaymentSchedule> PaymentSchedule { get; set; } = paymentSchedule;
    }
}