using Core.Notifications;
using Domain.Entities.PaymentFlowSummary;
using Domain.Entities.PropostaAggregate;
using Infrastructure.Repositories.Interfaces;
using UseCase.Dtos;
using UseCase.UseCase.Interfaces;

namespace UseCase.UseCase
{
    public class LoanUseCase : ILoanUseCase
    {
        private readonly IPropostaRepository _propostaRepository;

        private readonly IPaymentFlowSummaryRepository _paymentFlowSummaryRepository;

        private readonly NotificationContext _notificationContext;
        public LoanUseCase(
            IPropostaRepository propostaRepository,
            IPaymentFlowSummaryRepository paymentFlowSummaryRepository,
            NotificationContext notificationContext)
        {
            _propostaRepository = propostaRepository;
            _paymentFlowSummaryRepository = paymentFlowSummaryRepository;
            _notificationContext = notificationContext;
        }

        public async Task<LoanResponse> CreateLoanAsync(LoanRequest loanRequest)
        {
            Proposta proposta = null;
            PaymentFlowSummary paymentFlowSummary = null;
            LoanResponse loanResponse = null;
            List<PaymentScheduleResponse> lstPaymentSchedule = new List<PaymentScheduleResponse>();

            if (loanRequest.LoanAmount < 0)
            {
                _notificationContext.AssertArgumentNotNull(loanRequest, $"O valor total do emprestimo não deve ser {loanRequest.LoanAmount}");
                return loanResponse;
            }

            if (loanRequest.numberOfMonths < 0)
            {
                _notificationContext.AssertArgumentNotNull(loanRequest, $"O numero de parcelas não deve ser {loanRequest.numberOfMonths}");
                return loanResponse;
            }

            proposta = new Proposta(loanRequest.LoanAmount, loanRequest.LoanAmountlInterestRate, loanRequest.numberOfMonths);

            await _propostaRepository.InsertProposta(proposta);

            decimal monthlyPaymentResponse = CalcularPMT(loanRequest.LoanAmount, loanRequest.LoanAmountlInterestRate / 12, loanRequest.numberOfMonths);

            decimal balance = loanRequest.LoanAmount;
            decimal interest, principal;
            decimal totalInterest = 0; 
            decimal totalPayment = 0;

            for (int months = 1; months <= loanRequest.numberOfMonths; months++)
            {
                interest = balance * loanRequest.LoanAmountlInterestRate / 12;

                principal = monthlyPaymentResponse - interest;

                balance -= principal;

                totalInterest += interest;


                totalPayment = totalInterest + loanRequest.LoanAmount;
                lstPaymentSchedule.Add(
                        new PaymentScheduleResponse
                        {
                            balance = balance,
                            interest = interest,
                            month = months,
                            principal = principal
                        });

                loanResponse = new LoanResponse
                {
                    monthlyPayment = monthlyPaymentResponse,
                    totalInterest = totalInterest,
                    totalPayment = totalPayment,
                    paymentSchedule = lstPaymentSchedule
                };

                paymentFlowSummary = new PaymentFlowSummary(monthlyPaymentResponse, totalInterest, totalPayment, new List<PaymentSchedule> { new PaymentSchedule(months, principal, interest, balance) });

                await _paymentFlowSummaryRepository.InsertPaymentFlow(paymentFlowSummary);
            }

            return loanResponse;
        }

        public decimal CalcularPMT(decimal P, decimal i, int n)
        {
            decimal numerador = P * i * (decimal)Math.Pow((double)(1 + i), n);
            decimal denominador = (decimal)Math.Pow((double)(1 + i), n) - 1;

            return numerador / denominador;
        }

    }
}