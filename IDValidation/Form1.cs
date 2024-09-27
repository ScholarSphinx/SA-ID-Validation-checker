using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDValidation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string idNumber = textBox1.Text;
            bool bValid = ValidateSouthAfricanID(idNumber);
            if (bValid == true)
            {
                MessageBox.Show(idNumber + " is a valid South African ID number!");
            }
            else
            {
                MessageBox.Show(idNumber + " is not a valid South African ID number!");
            }
        }

        public bool ValidateSouthAfricanID(string idNumber)
        {
            // Ensure the ID is 13 digits long
            if (idNumber.Length != 13 || !long.TryParse(idNumber, out _))
            {
                return false; // Invalid ID length or non-numeric
            }

            // Step 1: Add all the digits in the odd positions EXCLUDING the last digit
            int sumOdd = 0;
            for (int i = 0; i < 12; i += 2)
            {
                sumOdd += int.Parse(idNumber[i].ToString()); // 1st, 3rd, 5th, etc.
            }

            // Step 2: Concatenate digits in even positions and multiply by 2
            string evenDigits = "";
            for (int i = 1; i < 12; i += 2)
            {
                evenDigits += idNumber[i]; // 2nd, 4th, 6th, etc.
            }

            int evenNumberMultiplied = int.Parse(evenDigits) * 2;

            // Step 3: Add the digits of the result of the multiplication
            int sumEven = 0;
            foreach (char digit in evenNumberMultiplied.ToString())
            {
                sumEven += int.Parse(digit.ToString());
            }

            // Step 4: Add the results of step 1 and step 3
            int totalSum = sumOdd + sumEven;

            // Step 5: Subtract the second digit of the result from 10
            int secondDigit = totalSum % 10;
            int result = 10 - secondDigit;

            // Step 6: If the result is 10, change it to 0
            if (result == 10)
            {
                result = 0;
            }

            // Step 6: Compare result with the last digit of the ID number
            int lastDigit = int.Parse(idNumber[12].ToString());

            return result == lastDigit;
        }

    }
}
