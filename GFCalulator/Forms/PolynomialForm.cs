using BasicArithmetic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GFCalulator.Forms
{
    public partial class PolynomialForm : Form
    {
        private BigInteger Characteristic { get; set; }
        private int Dimension { get; set; }
        public Polynomial Polynomial { get; set; }
        public bool ReadOnly { get; set; }

        public PolynomialForm(BigInteger characteristic, int dimension, Polynomial polynomial = null, bool readOnly = false)
        {
            InitializeComponent();
            this.Characteristic = characteristic;
            this.Dimension = dimension;
            this.ReadOnly = readOnly;

            for (int i = 0; i < this.Dimension; i++)
                this.polynomialGrid.Columns.Add(i.ToString(), i.ToString());


            this.polynomialGrid.Rows.Add();
            this.polynomialGrid.ReadOnly = readOnly;
            this.polynomialGrid.AllowUserToOrderColumns = false;
            this.polynomialGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.polynomialGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            if (polynomial != null)
            {
                this.Polynomial = polynomial;

                for (int i = 0; i < this.Dimension; i++)
                    this.polynomialGrid.Rows[0].Cells[i].Value = this.Polynomial[i];
            }
        }

        public PolynomialForm(PolynomialFieldRepresentation field)
        {
            InitializeComponent();

            for (int i = 0; i < field.Dimension + 1; i++)
                this.polynomialGrid.Columns.Add(i.ToString(), i.ToString());

            var irreducibles = field.FindIrreduciblePolynomials();

            for (int i = 0; i < irreducibles.Count; i++)
            {
                this.polynomialGrid.Rows.Add();
                this.polynomialGrid.ReadOnly = true;
                this.polynomialGrid.AllowUserToOrderColumns = false;
                this.polynomialGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                this.polynomialGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                for (int j = 0; j < irreducibles[i].Coefficients.Length; j++)
                    this.polynomialGrid.Rows[i].Cells[j].Value = irreducibles[i].Coefficients[j];
            }

            this.savePolynomialBtn.Hide();
        }

        private void savePolynomialBtn_Click(object sender, EventArgs e)
        {
            if (!this.ReadOnly)
            {
                BigInteger[] coefficients = new BigInteger[this.Dimension];

                for (int i = 0; i < this.Dimension; i++)
                    coefficients[i] = BigInteger.Parse((string)polynomialGrid.Rows[0].Cells[i].Value);

                this.Polynomial = new Polynomial(new PolynomialFieldRepresentation(this.Characteristic, this.Dimension), coefficients);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
