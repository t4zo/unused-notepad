using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace BlocoNotas
{
    public partial class BlocoNotasForm : Form
    {
        public BlocoNotasForm()
        {
            InitializeComponent();
        }

        private void SaveFileAs_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void SaveFileAsOk(object sender, CancelEventArgs e)
        {
            var text = txtEdit.Text;
            var path = saveFileDialog.FileName;

            using (var sw = new StreamWriter(path))
            {
                sw.Write(text);
            }
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void OpenFileOk(object sender, CancelEventArgs e)
        {
            var path = openFileDialog.FileName;

            using (var sr = new StreamReader(path))
            {
                var text = sr.ReadToEnd();
                txtEdit.Text = text;
            }
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            var text = txtEdit.Text;
            var path = openFileDialog.FileName;

            using (var sw = new StreamWriter(path))
            {
                sw.Write(text);
            }
        }

        private void New_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Deseja salvar as alterações?", "Bloco de Notas", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                SaveFileAs_Click(sender, e);
            }
            else if (result == DialogResult.No)
            {
                txtEdit.ResetText();
                txtEdit.ResetCursor();
                saveFileDialog.Reset();
                openFileDialog.Reset();
            }
        }

        private void ToggleWordWrap_Click(object sender, EventArgs e)
        {
            txtEdit.WordWrap ^= true;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Deseja salvar as alterações?", "Bloco de Notas", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                SaveFileAs_Click(sender, e);
            }
            else if (result == DialogResult.No)
            {
                Close();
            }
        }

        private void Print_Click(object sender, EventArgs e)
        {
            try
            {
                //printDialog.Document = printDocument;
                //var result = printDialog.ShowDialog();
                //if (result == DialogResult.OK)
                //{
                printDocument.PrintPage += PrintPage;
                printDocument.Print();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            //var streamToPrint = new StreamReader(openFileDialog.FileName);
            var printFont = new Font("Arial", 12);

            var count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            //string line = null;

            // Calculate the number of lines per page.
            //var linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);

            // Print each line of the file.
            var yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
            ev.Graphics.DrawString(txtEdit.Text, printFont, Brushes.Black, leftMargin, yPos);

            //while (count < linesPerPage && (line = streamToPrint.ReadLine()) != null)
            //{
            //    var yPos = topMargin + count * printFont.GetHeight(ev.Graphics);
            //    ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos);

            //    count++;
            //}

            // If more lines exist, print another page.
            //ev.HasMorePages = line != null;
        }
    }
}
