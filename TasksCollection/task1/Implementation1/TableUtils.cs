using System;
using System.Windows.Forms;

namespace Implementation1
{
    public class TableUtils
    {
        public static void CustomizeTable(DataGridView dataGridView, int n, int m)
        {
            dataGridView.RowCount = n;
            dataGridView.ColumnCount = m;
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                dataGridView.Rows[i].Height = 25;
            }
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                dataGridView.Columns[i].Width = 25;
            }
        }

        public static void FillTable(int[,] arr, DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                for (int j = 0; j < dataGridView.ColumnCount; j++)
                {
                    dataGridView.Rows[i].Cells[j].Value = arr[i, j].ToString();
                }
            }
        }

        public static int[,] ReadTable(DataGridView dataGridView)
        {
            int[,] arr = new int[dataGridView.RowCount, dataGridView.ColumnCount];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    arr[i, j] = Convert.ToInt32(dataGridView.Rows[i].Cells[j].Value);
                }
            }
            return arr;
        }
    }
}
