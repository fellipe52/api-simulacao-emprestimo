namespace UseCase.Dtos
{
    public class LoanRequest
    {
        public decimal LoanAmount { get; set; }
        public decimal LoanAmountlInterestRate { get; set; }
        public int numberOfMonths { get; set; }
    }
}