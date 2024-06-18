namespace ModifiedTimeModifier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnModifyFiles_Click(object sender, EventArgs e)
        {
            string folderPath1 = txtFolderPath.Text;
            if (string.IsNullOrEmpty(folderPath1) || !Directory.Exists(folderPath1))
            {
                MessageBox.Show("Please select a valid folder path.");
                return;
            }

            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;

            if (startDate > endDate)
            {
                MessageBox.Show("Start date must be earlier than end date.");
                return;
            }

            ModifyFileDates(folderPath1, startDate, endDate);
            MessageBox.Show("File dates modified successfully.");
        }

        void ModifyFileDates(string folderPath, DateTime startDate, DateTime endDate)
        {
            Random random = new Random();
            TimeSpan range = endDate - startDate;

            foreach (string file in Directory.GetFiles(folderPath))
            {
                // Generate a random timespan within the date range
                TimeSpan randomTimespan = new TimeSpan((long)(random.NextDouble() * range.Ticks));
                DateTime randomDate = startDate + randomTimespan;

                // Set the file's last write time to the random date and time
                File.SetLastWriteTime(file, randomDate);
            }
        }
    }
}
