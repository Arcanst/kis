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
        private BigInteger ExtendedBase { get; set; }
        private int Dimension { get; set; }
        public Dictionary<string, Polynomial> Polynomials { get; set; }
        public PolynomialFieldRepresentation ExtendedField { get; set; }
        private Random Random { get; set; }

        public Form1()
        {
            InitializeComponent();
            this.Polynomials = new Dictionary<string, Polynomial>();
            this.Base = 0;
            Random = new Random();
        }

        private void buttonCountBasic_Click(object sender, EventArgs e)
        {
            if (Base <= 1)
            {
                MessageBox.Show("Przed wykonaniem obliczeń wpisz podstawę ciała.");
                return;
            }

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
            else
            {
                labelFieldSpecifier.Text = string.Format("GF(?)");
                Base = 0;
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

        private void label11_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Odwrotność multiplikatywna modulo", label11);
        }

        private void label8_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Potęgowanie modulo", label8);
        }

        private void addPolynomialBtn_Click(object sender, EventArgs e)
        {
            if (this.ExtendedBase > 0 && this.Dimension > 0)
            {
                using (PolynomialForm form = new PolynomialForm(this.ExtendedBase, this.Dimension))
                {
                    var result = form.ShowDialog();
                    if(result == DialogResult.OK)
                    {
                        this.Polynomials.Add(GetUniquePolynomialName(), form.Polynomial);
                        listBox1.DataSource = this.Polynomials.ToArray();
                    }
                }
            }
        }

        private string GenerateString(int numberOfPositions=3)
        {
            string availableLetters = "abcdefghijklmnoupqrstuvwxyz";
            string result = "";

            for (int i = 0; i < numberOfPositions; i++)
                result += availableLetters[Random.Next(availableLetters.Length)];

            return result;
        }

        private string GetUniquePolynomialName()
        {
            var key = GenerateString();

            while(this.Polynomials.ContainsKey(key))
            {
                key = GenerateString();
            }

            return key;
        }

        private void textBoxExtendedBase_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxExtendedBase.Text))
            {
                BigInteger extendedBase;
                var res = BigInteger.TryParse(textBoxExtendedBase.Text, out extendedBase);
                if (res)
                {
                    ExtendedBase = extendedBase;
                    this.ExtendedField = new PolynomialFieldRepresentation(this.ExtendedBase, this.Dimension);

                    if (Dimension > 0 )
                        labelExtendedFieldSpecifier.Text = string.Format("GF({0}^{1})", ExtendedBase, Dimension);
                    else
                        labelExtendedFieldSpecifier.Text = string.Format("GF({0}^?)", ExtendedBase);
                }
            }
            else
            {
                ExtendedBase = 0;

                if (Dimension > 0)
                    labelExtendedFieldSpecifier.Text = string.Format("GF(?^{0})", Dimension);
                else
                    labelExtendedFieldSpecifier.Text = string.Format("GF(?^?)");
            }
        }

        private void textBoxDimension_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxDimension.Text))
            {
                int dimension;
                var res = int.TryParse(textBoxDimension.Text, out dimension);
                if (res)
                {
                    Dimension = dimension;
                    this.ExtendedField = new PolynomialFieldRepresentation(this.ExtendedBase, this.Dimension);

                    if (ExtendedBase > 0)
                        labelExtendedFieldSpecifier.Text = string.Format("GF({0}^{1})", ExtendedBase, Dimension);
                    else
                        labelExtendedFieldSpecifier.Text = string.Format("GF(?^{0})", Dimension);
                }
            }
            else
            {
                Dimension = 0;

                if (ExtendedBase > 0)
                    labelExtendedFieldSpecifier.Text = string.Format("GF({0}^?)", ExtendedBase);
                else
                    labelExtendedFieldSpecifier.Text = string.Format("GF(?^?)");
            }
        }

        private void removePolynomialBtn_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Zaznacz wielomian, który chcesz usunąć.");
                return;
            }
            var key = ((KeyValuePair<string, Polynomial>)this.listBox1.Items[listBox1.SelectedIndex]).Key;

            if (this.Polynomials.ContainsKey(key))
            {
                Polynomials.Remove(key);
                listBox1.DataSource = this.Polynomials.ToArray();
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Zaznacz wielomian, który chcesz zobaczyć.");
                return;
            }
            var key = ((KeyValuePair<string, Polynomial>)this.listBox1.Items[listBox1.SelectedIndex]).Key;

            using (PolynomialForm form = new PolynomialForm(this.ExtendedBase, this.Dimension, this.Polynomials[key], true))
            {
                form.savePolynomialBtn.Text = "Ok";
                form.ShowDialog();
            }
        }

        private void calculatePolynomialsBtn_Click(object sender, EventArgs e)
        {
            if(ExtendedBase < 0 || Dimension < 0)
            {
                MessageBox.Show("Przed wykonaniem obliczeń wpisz podstawę i rozszerzenie ciała.");
                return;
            }

            if(!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                if(!this.Polynomials.ContainsKey(textBox1.Text) || !this.Polynomials.ContainsKey(textBox2.Text))
                {
                    MessageBox.Show("Jeden z wielomianów w dodawaniu nie istnieje.");
                    return;
                }

                var a = this.Polynomials[textBox1.Text];
                var b = this.Polynomials[textBox2.Text];

                var c = a + b;
                var name = GetUniquePolynomialName();
                this.Polynomials.Add(name, c);
                textBox3.Text = name;
            }
            if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrEmpty(textBox5.Text))
            {
                if (!this.Polynomials.ContainsKey(textBox4.Text) || !this.Polynomials.ContainsKey(textBox5.Text))
                {
                    MessageBox.Show("Jeden z wielomianów w odejmowaniu nie istnieje.");
                    return;
                }

                var a = this.Polynomials[textBox4.Text];
                var b = this.Polynomials[textBox5.Text];

                var c = a - b;
                var name = GetUniquePolynomialName();
                this.Polynomials.Add(name, c);
                textBox6.Text = name;
            }
            if (!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrEmpty(textBox8.Text))
            {
                if (!this.Polynomials.ContainsKey(textBox7.Text) || !this.Polynomials.ContainsKey(textBox8.Text))
                {
                    MessageBox.Show("Jeden z wielomianów w dodawaniu nie istnieje.");
                    return;
                }

                var a = new Polynomial(this.ExtendedField, this.Polynomials[textBox7.Text]);
                var b = new Polynomial(this.ExtendedField, this.Polynomials[textBox8.Text]);

                var c = a * b;
                var name = GetUniquePolynomialName();
                this.Polynomials.Add(name, c);
                textBox9.Text = name;
            }
            if (!string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrEmpty(textBox11.Text))
            {
                if (!this.Polynomials.ContainsKey(textBox10.Text) || !this.Polynomials.ContainsKey(textBox11.Text))
                {
                    MessageBox.Show("Jeden z wielomianów w dodawaniu nie istnieje.");
                    return;
                }

                var a = this.Polynomials[textBox10.Text];
                var b = this.Polynomials[textBox11.Text];

                var c = a / b;
                var name1 = GetUniquePolynomialName();
                var name2 = GetUniquePolynomialName();
                this.Polynomials.Add(name1, c.Item1);
                this.Polynomials.Add(name2, c.Item2);
                textBox12.Text = name1;
                textBox13.Text = name2;
            }
            listBox1.DataSource = this.Polynomials.ToArray();
        }

        private void addGeneratorBtn_Click(object sender, EventArgs e)
        {
            using (PolynomialForm form = new PolynomialForm(this.ExtendedBase, this.Dimension + 1))
            {
                var result = form.ShowDialog();
                if(result == DialogResult.OK)
                {
                    this.ExtendedField = new PolynomialFieldRepresentation(this.ExtendedBase, this.Dimension, form.Polynomial.Coefficients.Select(prop => prop.Value).ToArray());
                }
            }
        }

        private void findIrreduciblesBtn_Click(object sender, EventArgs e)
        {
            using (PolynomialForm form = new PolynomialForm(this.ExtendedField))
            {
                form.ShowDialog();
            }
        }
    }
}