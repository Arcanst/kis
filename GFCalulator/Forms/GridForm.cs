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
    public partial class GridForm : Form
    {
        public BigInteger Characteristic { get; set; }

        public GridForm(BigInteger characteristic, BigInteger[,] table, string  title)
        {
            InitializeComponent();
            Characteristic = characteristic;
            Text = title;

            for (int i = 0; i < Characteristic; i++)
            {
                dataGridView.Columns.Add(i.ToString(), i.ToString());
                dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int i = 0; i < Characteristic; i++)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i].HeaderCell.Value = i.ToString();

                for (int j = 0; j < Characteristic; j++)
                    dataGridView.Rows[i].Cells[j].Value = table[i, j];
            }

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.Height = dataGridView.Height;
            this.Width = dataGridView.Width;
        }
    }
}