using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicArithmetic;
using System.Numerics;
using GFCalulator.Forms;

namespace GFCalulator
{
    public partial class Form1 : Form
    {
        private BigInteger Base { get; set; }

        public Form1()
        {
            InitializeComponent();
            Base = 0;
        }

        private void buttonCountBasic_Click(object sender, EventArgs e)
        {
            if (Base <= 1)
                return;

            if(!string.IsNullOrEmpty(textBoxAddElement1.Text) && !string.IsNullOrEmpty(textBoxAddElement2.Text))
            {
                BigInteger aBI, bBI;
                var aRes = BigInteger.TryParse(textBoxAddElement1.Text, out aBI);
                var bRes = BigInteger.TryParse(textBoxAddElement2.Text, out bBI);

                if(aRes && bRes)
                {
                    Modular a = new Modular(aBI, Base);
                    Modular b = new Modular(bBI, Base);
                    Modular res = a + b;
                    textBoxAddSum.Text = res.ToString();
                }
            }
            if (!string.IsNullOrEmpty(textBoxMinuend.Text) && !string.IsNullOrEmpty(textBoxSubtrahend.Text))
            {
                BigInteger aBI, bBI;
                var aRes = BigInteger.TryParse(textBoxMinuend.Text, out aBI);
                var bRes = BigInteger.TryParse(textBoxSubtrahend.Text, out bBI);

                if (aRes && bRes)
                {
                    Modular a = new Modular(aBI, Base);
                    Modular b = new Modular(bBI, Base);
                    Modular res = a- b;
                    textBoxDifference.Text = res.ToString();
                }
            }
            if (!string.IsNullOrEmpty(textBoxMultiplicand.Text) && !string.IsNullOrEmpty(textBoxMultiplier.Text))
            {
                BigInteger aBI, bBI;
                var aRes = BigInteger.TryParse(textBoxMultiplicand.Text, out aBI);
                var bRes = BigInteger.TryParse(textBoxMultiplier.Text, out bBI);

                if (aRes && bRes)
                {
                    Modular a = new Modular(aBI, Base);
                    Modular b = new Modular(bBI, Base);
                    Modular res = a * b;
                    textBoxProduct.Text = res.ToString();
                }
            }
            if (!string.IsNullOrEmpty(textBoxBase.Text) && !string.IsNullOrEmpty(textBoxExponent.Text))
            {
                BigInteger aBI, bBI;
                var aRes = BigInteger.TryParse(textBoxBase.Text, out aBI);
                var bRes = BigInteger.TryParse(textBoxExponent.Text, out bBI);

                if (aRes && bRes)
                {
                    Modular a = new Modular(aBI, Base);
                    Modular b = new Modular(bBI, Base);
                    Modular res = a ^ b;
                    textBoxBaseToExponent.Text = res.ToString();
                }
            }
            if (!string.IsNullOrEmpty(textBoxElementToInverse.Text))
            {
                BigInteger aBI;
                var aRes = BigInteger.TryParse(textBoxElementToInverse.Text, out aBI);

                if (aRes)
                {
                    Modular a = new Modular(aBI, Base);
                    Modular res = !a;
                    textBoxElementInversed.Text = res.ToString();
                }
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBoxBasicBase.Text))
            {
                BigInteger basicBase;
                var res = BigInteger.TryParse(textBoxBasicBase.Text, out basicBase);
                if (res)
                {
                    Base = basicBase;
                    labelFieldSpecifier.Text = string.Format("GF({0})", Base);
                }
            }
        }

        private void buttonAdditiveTable_Click(object sender, EventArgs e)
        {
            if (Base != 0)
            {
                GridForm form = new GridForm(Base, Modular.GetAdditiveGroup(Base), "Tabliczka dodawania nad " + labelFieldSpecifier.Text);
                form.Show();
            }
            else
                MessageBox.Show("Przed wygenerowaniem tabliczki dodawania wpisz podstawę ciała.");
        }

        private void buttonMultiplicativeTable_Click(object sender, EventArgs e)
        {
            if (Base != 0)
            {
                GridForm form = new GridForm(Base, Modular.GetMultiplicativeGroup(Base), "Tabliczka mnożenia nad " + labelFieldSpecifier.Text);
                form.Show();
            }
            else
                MessageBox.Show("Przed wygenerowaniem tabliczki mnożenia wpisz podstawę ciała.");
        }
    }
}
