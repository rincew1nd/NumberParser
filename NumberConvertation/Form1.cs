using System;
using System.Windows.Forms;

namespace NumberConvertation
{
    public partial class Form1 : Form
    {
        private Translator translator;

        public Form1()
        {
            InitializeComponent();
            translator = new Translator();
            this.AcceptButton = TranButton;
        }

        private void TranButton_Click(object sender, EventArgs e)
        {
            // Reset values of fields to default
            this.NumberOutput.Text = "";
            this.errorLable.Text = "";

            // Try to parse string to int
            var result = translator.TryParse(this.NumberInput.Text);

            // Fill fields with result values
            this.errorLable.Text = result["error"];
            this.NumberOutput.Text = result["result"];
            if (result["result"] == "error")
                this.staroslavNum.Text = "error";
            else
                this.staroslavNum.Text = translator.ConvertToOldRussianNumber(int.Parse(result["result"]));
        }
    }
}
