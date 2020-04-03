using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App10
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            clear(this, null);
        }
        List<Decimal> numbers = new List<Decimal>();
        int state=0; String mathOperator; Boolean check=false; int opCount = 0;

        private void selectNum(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            String text = button.Text;
            if (result.Text == "0" || check) result.Text = "";
            check = false;
            result.Text += text;
            display.Text += text;
            decimal number;
                if (decimal.TryParse(result.Text, out number))
                {
                    result.Text = number.ToString();
                    numbers.Insert(state, number);
                    if (numbers.Count - 1 > state) numbers.RemoveAt(numbers.Count - 1);
                }
        }

        private void selectOperator(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string op = button.Text;
                if (op == ".")
                {
                    if (!(result.Text.Contains(".")) && numbers.Count - 1 >= state) //to prevent writing more than one or before entring a number
                    {
                        result.Text += ".";
                        display.Text += ".";
                        numbers.RemoveAt(state);
                    }
                }

                else if (op == "+/-")
                {
                    if (numbers.Count - 1 >= state) //To prevent entering it before the number
                    {
                        String nu = numbers[state].ToString();
                        result.Text = (numbers[state] *= -1).ToString();
                        display.Text = display.Text.Substring(0, display.Text.LastIndexOf(nu)) + (numbers[state]).ToString();
                    }
                }
                else if (op == "%")
                {
                    if (numbers.Count - 1 >= state)
                    {
                        String nu = numbers[state].ToString();
                        result.Text = (numbers[state] /= 100).ToString();
                        display.Text = display.Text.Substring(0, display.Text.LastIndexOf(nu)) + (numbers[state]).ToString();
                    }
                }
                else
                {
                    decimal isNumberCheck;
                    if (display.Text.Length>0&&decimal.TryParse(display.Text[display.Text.Length - 1].ToString(), out isNumberCheck)) //To prevent writing 2 operands consecutively
                     {
                        check = true;
                        state = 1;
                        opCount++;
                        if (opCount >= 2) calculate(this, null);
                        if (op == "div") { display.Text += "/"; }
                        else display.Text += op;
                        mathOperator = op;
                    }
                }
        }

        private void calculate(object sender, EventArgs e)
        {
            if (numbers.Count >= 2 && mathOperator!=null) {
                decimal finalResult = 0; decimal firstNum = numbers[0], secondNum = numbers[1];
                switch (mathOperator)
                {
                    case "div":
                        finalResult = firstNum / secondNum;
                        break;
                    case "X":
                        finalResult = firstNum * secondNum;
                        break;
                    case "+":
                        finalResult = firstNum + secondNum;
                        break;
                    case "-":
                        finalResult = firstNum - secondNum;
                        break;
                }
                result.Text = finalResult.ToString();
                display.Text = finalResult.ToString();
                numbers.Clear();
                numbers.Insert(0,finalResult);
                opCount = 1;
                state = 1;
                mathOperator = null;
            }
        }

        private void clear(object sender, EventArgs e)
        {
            numbers.Clear();
            state = 0;
            result.Text = "0";
            display.Text = "";
            check = false;
            opCount = 0;
            mathOperator = null;
        }
    }
}
