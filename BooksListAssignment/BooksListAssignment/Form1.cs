using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace BooksListAssignment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public DataTable Readcsv(string fileName)
        {
            DataTable dt = new DataTable("Data");
            using (OleDbConnection cn = new OleDbConnection("Provider=Microsoft.JET.OLEDB.4.0;Data Source=\"" +
                                                            Path.GetDirectoryName(fileName) +
                                                            "\";Extended Properties='text;HDR=yes;FMT=Delimited(;)';"))
            {
                using (OleDbCommand cmd =
                    new OleDbCommand(string.Format("select * from [{0}]", new FileInfo(fileName).Name), cn))
                {
                    cn.Open();
                    using (OleDbDataAdapter adapter=new OleDbDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;

        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog()
                    {Filter = "CSV|*.csv", ValidateNames = true, Multiselect = false})
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        dataGridView1.DataSource = Readcsv(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Message",MessageBoxButtons.OK,MessageBoxIcon.Error);
                throw;
            }
        }
    }
}
