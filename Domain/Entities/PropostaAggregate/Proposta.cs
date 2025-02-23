namespace Domain.Entities.PropostaAggregate
{
    public class Proposta
    {
        public decimal LoanAmount { get; set; }
        public decimal LoanAmountInterestRate { get; set; }
        public int NumberOfMonths { get; set; }

        // Construtor
        public Proposta(decimal loanAmount, decimal loanAmountInterestRate, int numberOfMonths)
        {
            LoanAmount = loanAmount;
            LoanAmountInterestRate = loanAmountInterestRate;
            NumberOfMonths = numberOfMonths;
        }
    }
}