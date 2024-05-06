/* LoanApp.cs
 * Purpose: The purpose of this application is to get data from the user
 * then connect it to the class loan,
 * then will print all of the info from the class loan.cs
 * Author: Jaren Montano
 * Date: 9/30/2023
 * 
 */



using System;
using System.Dynamic;
using static System.Console;

namespace LoanAmortization 
{
    internal class LoanApp
    {
        static void Main(string[] args)
        {

            int years;
            double loanAmount;
            double interestRate;
            string inValue;
            char anotherLoan = 'N';

            do
            {
                GetInputValues(out loanAmount, out interestRate, out years);
                Loan ln = new Loan(loanAmount, interestRate, years);
                Clear();
                WriteLine(ln);
                WriteLine();
                WriteLine(ln.ReturnAmortizationSchedule()); 
                ln.DetermineTotalInterestPaid(); 
                WriteLine("Payment Amount: {0:C}", ln.PaymentAmount);
                WriteLine("Interest Paid over life of loan: {0:C}", ln.TotalInterestPaid);
                WriteLine("Do another Calculation? (Y or N)");
                inValue = ReadLine();
                anotherLoan = Convert.ToChar(inValue);

            }
            while ((anotherLoan == 'Y') || (anotherLoan == 'y'));
        }

        private static void GetInputValues(out double loanAmount, out double interestRate, out int years)
        {
            Clear();
            loanAmount = GetLoanAmount();
            interestRate = GetInterestRate();
            years = GetYears();

        }

        private static int GetYears()
        {
            string sValue;
            int years;
            Write("Please Enter the number of years for the loan:  ");
            sValue = ReadLine();
            while ((int.TryParse(sValue, out years) == false) || years < 1 || years > 30)
            {
                WriteLine("Invalid Data Entered " + "for years.");
                Write("\nPlease re-enter years (1-30 only):  ");
                sValue = ReadLine();
            }
            return years;
        }

        private static double GetInterestRate()
        {
            string sValue;
            double interestRate;
            Write("Please Enter a interest rate (as a decimal value - i.e. 0.06):  ");
            sValue = ReadLine();
            while ((double.TryParse(sValue, out interestRate) == false) || interestRate < 0 || interestRate > 1)
            {
                WriteLine("Invalid Data Entered " + "for interest rate.");
                Write("\nPlease re-enter a decimal value (like 0.06):  ");
                sValue = ReadLine();
            }
            return interestRate;
        }

        private static double GetLoanAmount()
        {
            string sValue;
            double loanAmount;
            Write("Please Enter a loan amount: ");
            sValue = ReadLine();
            while ((double.TryParse(sValue, out loanAmount) == false) || loanAmount < 1 || loanAmount > 500000)
            {
                WriteLine("Invalid Data Entered " + "for loan amount.");
                Write("\nPlease re-enter the loan amount (less than $500,000):  ");
                sValue = ReadLine();
            }
            return loanAmount;
        }
    }
} 