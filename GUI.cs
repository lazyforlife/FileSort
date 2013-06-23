using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FileSort
{
    public partial class GUI : Form
    {
        private string workingdir = "";

        private string[] sources;
        private string[] dests;




        public GUI()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //check for valid directory input
            string testdir = directoryBox.Text;

            if (Directory.Exists(testdir))
                generateButton.Enabled = true;

	    //@TODO add in the code for dynamic title
        }



        private void GUI_SizeChanged(object sender, EventArgs e)
        {
            // splitC.Size = new Size(this.Size.Width - 50, this.Size.Height - 170);
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            //load directory... 
            folderBrowserDialog1.ShowDialog();

            workingdir = folderBrowserDialog1.SelectedPath;

            directoryBox.Text = workingdir;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //generate lists and such
            saveButton.Enabled = true;

            sources = Directory.GetFiles(workingdir);


            //generate the destinations. 
            dests = R.GenerateDestinations(sources,titleTextBox.Text,(int)index_numbericUpDown.Value,spaceCheckBox.Checked,paddingCheckBox.Checked);

	    //clear the list view items
            foreach (ListViewItem i in listView1.Items)
                listView1.Items.Remove(i);

            for (int i = 0; i < sources.Length; i++)
            {
                ListViewItem lvi = new ListViewItem(dests[i]);
                lvi.SubItems.Add(sources[i]);
                listView1.Items.Add(lvi);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < sources.Length; i++)
            {
                dests[i] = listView1.Items[i].ToString();
            }

            R.Rename(sources, dests);

            MessageBox.Show("Operation Completed");
        }
    }
}
