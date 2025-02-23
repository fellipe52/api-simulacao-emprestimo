namespace Domain.Entities.PaymentFlowSummary
{
    public class PaymentSchedule
    {
        public int Month { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal Balance { get; set; }

        // Construtor
        public PaymentSchedule(int month, decimal principal, decimal interest, decimal balance)
        {
            Month = month;
            Principal = principal;
            Interest = interest;
            Balance = balance;
        }
    }
}