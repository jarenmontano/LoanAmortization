/* Loan.cs
 * Purpose: this class will hold all of the data about a loan
 * it will also be able to calculate things based off of the
 * loan amout, term, and rate.
 * Author: Jaren Montano
 * Date: 9/30/2023
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LoanAmortization
{
    internal class Loan
    {
        private int numPayments;
        private double balance;
        private double monthInterest;
        private double principal;

        public double PaymentAmount { get; set; }
        public double LoanAmount { get; set; }
        public double Rate { get; set; }
        public double TotalInterestPaid { get; set; }

        public int Years {

            get
            {
                return numPayments/12;
            }
            set
            {
                numPayments = value * 12;
            }
        }

        public Loan(double loan, double interestRate, int years)
        {
            LoanAmount = loan;
            if (interestRate < 1) {
                Rate = interestRate;
            }
            else//error handling / convert to decimal
            {
                Rate = interestRate / 100;
            }

            numPayments = 12 * years;
            TotalInterestPaid = 0;
            DeterminePaymentAmount();
            
            
        }

        //Determine the payment amount based on number of
        //Years, Loan Amount, and Rate
        private void DeterminePaymentAmount()
        {
            double term;
            term = Math.Pow((1 + Rate / 12), numPayments);
            PaymentAmount = (LoanAmount * Rate / 12.0 * term) / (term - 1.0);
        }

        //return information about the loan
        public override string ToString()
        {
            return "\nLoan Amount: " + LoanAmount.ToString("C") +
                "\nInterest Rate: " + Rate +
                "\nNumber of Years for Loan: " +
                (numPayments / 12) +
                "\nMonthly payment: " + PaymentAmount.ToString("C");
        }

        //the purpose is to return string containing amoritization table
        public string ReturnAmortizationSchedule()
        {

            string schedule = "Month\t\tInt.\t\tPrin.\t\tNew" +
                "\nNo.\t\tPd.\t\tPd.\t\tBalance\n";
            schedule += "_____\t\t_____\t\t_____\t\t_____\n";

            balance = LoanAmount; 

            for(int month = 1; month <= numPayments; month++)
            {
                CalculateMonthCharges(month, numPayments);
                schedule += month + "\t" + monthInterest.ToString("N2").PadLeft(13)
                    + "\t" + principal.ToString("N2").PadLeft(15) + "\t" +
                    balance.ToString("C").PadLeft(17) + "\n";
            }

            return schedule;
        }


        //calculate the monthly interest and new balance for each month
        private void CalculateMonthCharges(int month, int numPayments)
        {
            double payment = PaymentAmount;
            monthInterest = Rate/ 12  * balance;

            if (month == numPayments)
            {
                principal = balance;
                payment = balance + monthInterest;
            }
            else
            {
                principal= payment - monthInterest;
            }

            balance -= principal;
        }

        //calculates the interest paid over life of the loan
        public void DetermineTotalInterestPaid()
        {
            TotalInterestPaid = 0;
            balance = LoanAmount;

            for(int month = 1;month <= numPayments; month++)
            {
                CalculateMonthCharges(month, numPayments);
                TotalInterestPaid += monthInterest;
            }

        }

    }
}
