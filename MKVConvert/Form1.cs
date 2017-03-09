using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MKVConvert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSD_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"C:\Program Files\Handbrake\Handbrake.exe"))
            {
                btnSD.BackColor = Color.Green;
                EncodeSD();
            }
            else
            {

                DialogResult dialogResult = MessageBox.Show("Handbreak is not installed, do you want to install?", "Handbreak installation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Process.Start("https://handbrake.fr/downloads.php");
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Please install Handbrake before running");

                    this.Close();
                }
            } 
        }

        public void EncodeSD(bool copyAudio = true)
        {
            string[] beforeDirSD = Directory.GetFiles(txtBxSource.Text, "*.*");
            long beforeSizeSD = Sizes(beforeDirSD);

            string[] files = Directory.GetFiles(txtBxSource.Text, "*.*");

            int converted = 0;
            int errors = 0;


            if (ChkBxMKV.Checked == false)
            {
                foreach (string currentFile in files)
                {
                    if (currentFile.EndsWith("avi") || currentFile.EndsWith("mp4") || currentFile.EndsWith("ts"))
                    {

                        // Create launch settings
                        ProcessStartInfo psi = new ProcessStartInfo()
                        {
                            FileName = @"C:\Program Files\Handbrake\HandBrakeCLI.exe"
                        };
                        if (copyAudio)
                        {
                            psi.Arguments = "-i \"" + currentFile + "\" -t 1 -o \"" + currentFile + "-output.mkv\" -f mkv -O -w " + txtBxSDWid.Text + " -l " + txtBxSDHei.Text + " --modulus 8 -e x264 -b " + txtBxSDBR.Text + " -2 --vfr -a 1 -E faac -B 160 -6 dpl2 -R Auto -D 0 --gain=0 --audio-copy-mask none --audio-fallback ffac3 -x weightp=1:subq=10:rc-lookahead=10:trellis=2:b-adapt=2:psy-rd=1.00,0.10 --verbose=1";
                        }
                        else
                        {
                            //psi.Arguments = "-i \"" + currentFile + "\" -t 1 -o \"" + currentFile + "-output.mkv\" -f mkv -O -w " + width + " -l " + height + " --modulus 8 -e x264 -b " + bitrate + " -2 --vfr -a 1 -E faac -B 160 -6 dpl2 -R Auto -D 0 --gain=0 --audio-copy-mask none --audio-fallback ffac3 -x weightp=1:subq=10:rc-lookahead=10:trellis=2:b-adapt=2:psy-rd=1.00,0.10 --verbose=1";
                        }

                        try
                        {
                            // Create process based on settings
                            Process prc = new Process()
                            {
                                StartInfo = psi
                            };
                            prc.Start();

                            // Wait for process to exit
                            while (!prc.HasExited)
                            {
                                Thread.Sleep(500);
                            }

                            switch (prc.ExitCode)
                            {
                                case 0:
                                    converted++;
                                    break;
                                default:
                                    errors++;
                                    break;
                            }

                            // Sort file out
                            if (File.Exists(currentFile + "-output.mkv"))
                            {
                                File.Delete(currentFile);

                                string finalName = string.Empty;
                                if (currentFile.EndsWith("ts"))
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 2), 2), "mkv");
                                else
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 3), 3), "mkv");

                                File.Move(currentFile + "-output.mkv", finalName);
                            }
                        }
                        catch
                        { }
                    }
                }

                string[] afterDirSD = Directory.GetFiles(txtBxSource.Text, "*.*");
                long afterSizeSD = Sizes(afterDirSD);

                if (afterSizeSD > beforeSizeSD == true)
                {
                    arrowUp.Image = Properties.Resources.up;
                    arrowUp.Visible = true;

                }

                if (afterSizeSD < beforeSizeSD == true)
                {
                    arrowDown.Image = Properties.Resources.down;
                    arrowDown.Visible = true;
                }

                else
                {
                    sameAs.Image = Properties.Resources.Same;
                    sameAs.Visible = true;

                }

                txtBxAfter.Text = InGB(afterSizeSD);
                                
                var message = new StringBuilder();
                message.AppendLine(txtOutput.Text + "Job Complete" + Environment.NewLine);
                message.AppendLine(txtOutput.Text + "Converted: " + converted.ToString() + Environment.NewLine);
                message.AppendLine(txtOutput.Text + "Errors: " + errors.ToString() + Environment.NewLine);
              
                MessageBox.Show(message.ToString());

            }

            else
                
            {
                foreach (string currentFile in files)
                {
                    if (currentFile.EndsWith("avi") || currentFile.EndsWith("mp4") || currentFile.EndsWith("ts") || currentFile.EndsWith("mkv"))
                    {

                        // Create launch settings
                        ProcessStartInfo psi = new ProcessStartInfo()
                        {
                            FileName = @"C:\Program Files\Handbrake\HandBrakeCLI.exe"
                        };
                        if (copyAudio)
                        {
                            psi.Arguments = "-i \"" + currentFile + "\" -t 1 -o \"" + currentFile + "-output.mkv\" -f mkv -O -w " + txtBxSDWid.Text + " -l " + txtBxSDHei.Text + " --modulus 8 -e x264 -b " + txtBxSDBR.Text + " -2 --vfr -a 1 -E faac -B 160 -6 dpl2 -R Auto -D 0 --gain=0 --audio-copy-mask none --audio-fallback ffac3 -x weightp=1:subq=10:rc-lookahead=10:trellis=2:b-adapt=2:psy-rd=1.00,0.10 --verbose=1";
                        }
                        else
                        {
                            //psi.Arguments = "-i \"" + currentFile + "\" -t 1 -o \"" + currentFile + "-output.mkv\" -f mkv -O -w " + width + " -l " + height + " --modulus 8 -e x264 -b " + bitrate + " -2 --vfr -a 1 -E faac -B 160 -6 dpl2 -R Auto -D 0 --gain=0 --audio-copy-mask none --audio-fallback ffac3 -x weightp=1:subq=10:rc-lookahead=10:trellis=2:b-adapt=2:psy-rd=1.00,0.10 --verbose=1";
                        }

                        try
                        {
                            // Create process based on settings
                            Process prc = new Process()
                            {
                                StartInfo = psi
                            };
                            prc.Start();

                            // Wait for process to exit
                            while (!prc.HasExited)
                            {
                                Thread.Sleep(500);
                            }

                            switch (prc.ExitCode)
                            {
                                case 0:
                                    converted++;
                                    break;
                                default:
                                    errors++;
                                    break;
                            }

                            // Sort file out
                            if (File.Exists(currentFile + "-output.mkv"))
                            {
                                File.Delete(currentFile);

                                string finalName = string.Empty;
                                if (currentFile.EndsWith("ts"))
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 2), 2), "mkv");
                                else
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 3), 3), "mkv");

                                File.Move(currentFile + "-output.mkv", finalName);
                            }
                        }
                        catch
                        { }
                    }
                }

                string[] afterDirSD = Directory.GetFiles(txtBxSource.Text, "*.*");
                long afterSizeSD = Sizes(afterDirSD);

                if (afterSizeSD > beforeSizeSD == true)
                {
                    arrowUp.Image = Properties.Resources.up;
                    arrowUp.Visible = true;

                }

                if (afterSizeSD < beforeSizeSD == true)
                {
                    arrowDown.Image = Properties.Resources.down;
                    arrowDown.Visible = true;
                }

                else
                {
                    sameAs.Image = Properties.Resources.Same;
                    sameAs.Visible = true;

                }

                txtBxAfter.Text = InGB(afterSizeSD);

                var message = new StringBuilder();
                message.AppendLine(txtOutput.Text + "Job Complete" + Environment.NewLine);
                message.AppendLine(txtOutput.Text + "Converted: " + converted.ToString() + Environment.NewLine);
                message.AppendLine(txtOutput.Text + "Errors: " + errors.ToString() + Environment.NewLine);

                MessageBox.Show(message.ToString());

            }
        }

        private void BtnHD_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"C:\Program Files\Handbrake\Handbrake.exe"))
            {
                btnHD.BackColor = Color.Green;
                EncodeHD();
            }
            else
            {

                DialogResult dialogResult = MessageBox.Show("Handbreak is not installed, do you want to install?", "Handbreak installation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("https://handbrake.fr/downloads.php");
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Please install Handbrake before running");

                    this.Close();
                }
            }
        }

        public void EncodeHD(bool copyAudio = true)
        {
            string[] beforeDirHD = Directory.GetFiles(txtBxSource.Text, "*.*");
            long beforeSizeHD = Sizes(beforeDirHD);

            int converted = 0;
            int errors = 0;

            string[] files = Directory.GetFiles(txtBxSource.Text, "*.*");

            if (ChkBxMKV.Checked == false)
            {
                foreach (string currentFile in files)
                {
                    if (currentFile.EndsWith("avi") || currentFile.EndsWith("mp4") || currentFile.EndsWith("ts"))
                    {
                        // Create launch settings
                        ProcessStartInfo psi = new ProcessStartInfo()
                        {
                            FileName = @"C:\Program Files\Handbrake\HandBrakeCLI.exe"
                        };
                        if (copyAudio)
                        {
                            psi.Arguments = "-i \"" + currentFile + "\" -t 1 -o \"" + currentFile + "-output.mkv\" -f mkv -O -e x264 -b " + txtBxHDBR.Text + " -2 --vfr -a 1 -E faac -B 160 -6 dpl2 -R Auto -D 0 --gain=0 --audio-copy-mask none --audio-fallback ffac3 -x weightp=1:subq=10:rc-lookahead=10:trellis=2:b-adapt=2:psy-rd=1.00,0.10 --verbose=1";
                        }
                        else
                        {

                        }

                        try
                        {
                            // Create process based on settings
                            Process prc = new Process()
                            {
                                StartInfo = psi
                            };
                            prc.Start();

                            // Wait for process to exit
                            while (!prc.HasExited)
                            {
                                Thread.Sleep(500);
                            }

                            switch (prc.ExitCode)
                            {
                                case 0:
                                    converted++;
                                    break;
                                default:
                                    errors++;
                                    break;
                            }

                            // Sort file out
                            if (File.Exists(currentFile + "-output.mkv"))
                            {
                                File.Delete(currentFile);

                                string finalName = string.Empty;
                                if (currentFile.EndsWith("ts"))
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 2), 2), "mkv");
                                else
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 3), 3), "mkv");

                                File.Move(currentFile + "-output.mkv", finalName);
                            }
                        }
                        catch
                        { }
                    }
                }

                string[] afterDirHD = Directory.GetFiles(txtBxSource.Text, "*.*");
                long afterSizeHD = Sizes(afterDirHD);

                if (afterSizeHD > beforeSizeHD == true)
                {
                    arrowUp.Image = Properties.Resources.up;
                    arrowUp.Visible = true;

                }

                if (afterSizeHD < beforeSizeHD == true)
                {
                    arrowDown.Image = Properties.Resources.down;
                    arrowDown.Visible = true;
                }

                else
                {
                    sameAs.Image = Properties.Resources.Same;
                    sameAs.Visible = true;

                }

                txtBxAfter.Text = InGB(afterSizeHD);

                var message = new StringBuilder();
                message.AppendLine(txtOutput.Text + "Job Complete" + Environment.NewLine);
                message.AppendLine(txtOutput.Text + "Converted: " + converted.ToString() + Environment.NewLine);
                message.AppendLine(txtOutput.Text + "Errors: " + errors.ToString() + Environment.NewLine);

                MessageBox.Show(message.ToString());
            }

            else
            {
                foreach (string currentFile in files)
                {
                    if (currentFile.EndsWith("avi") || currentFile.EndsWith("mp4") || currentFile.EndsWith("ts") || currentFile.EndsWith("mkv"))
                    {
                        // Create launch settings
                        ProcessStartInfo psi = new ProcessStartInfo()
                        {
                            FileName = @"C:\Program Files\Handbrake\HandBrakeCLI.exe"
                        };
                        if (copyAudio)
                        {
                            psi.Arguments = "-i \"" + currentFile + "\" -t 1 -o \"" + currentFile + "-output.mkv\" -f mkv -O -e x264 -b " + txtBxHDBR.Text + " -2 --vfr -a 1 -E faac -B 160 -6 dpl2 -R Auto -D 0 --gain=0 --audio-copy-mask none --audio-fallback ffac3 -x weightp=1:subq=10:rc-lookahead=10:trellis=2:b-adapt=2:psy-rd=1.00,0.10 --verbose=1";
                        }
                        else
                        {

                        }

                        try
                        {
                            // Create process based on settings
                            Process prc = new Process()
                            {
                                StartInfo = psi
                            };
                            prc.Start();

                            // Wait for process to exit
                            while (!prc.HasExited)
                            {
                                Thread.Sleep(500);
                            }

                            switch (prc.ExitCode)
                            {
                                case 0:
                                    converted++;
                                    break;
                                default:
                                    errors++;
                                    break;
                            }

                            // Sort file out
                            if (File.Exists(currentFile + "-output.mkv"))
                            {
                                File.Delete(currentFile);

                                string finalName = string.Empty;
                                if (currentFile.EndsWith("ts"))
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 2), 2), "mkv");
                                else
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 3), 3), "mkv");

                                File.Move(currentFile + "-output.mkv", finalName);
                            }
                        }
                        catch
                        { }
                    }
                }

                string[] afterDirHD = Directory.GetFiles(txtBxSource.Text, "*.*");
                long afterSizeHD = Sizes(afterDirHD);

                if (afterSizeHD > beforeSizeHD == true)
                {
                    arrowUp.Image = Properties.Resources.up;
                    arrowUp.Visible = true;

                }

                if (afterSizeHD < beforeSizeHD == true)
                {
                    arrowDown.Image = Properties.Resources.down;
                    arrowDown.Visible = true;
                }

                else
                {
                    sameAs.Image = Properties.Resources.Same;
                    sameAs.Visible = true;

                }

                txtBxAfter.Text = InGB(afterSizeHD);

                var message = new StringBuilder();
                message.AppendLine(txtOutput.Text + "Job Complete" + Environment.NewLine);
                message.AppendLine(txtOutput.Text + "Converted: " + converted.ToString() + Environment.NewLine);
                message.AppendLine(txtOutput.Text + "Errors: " + errors.ToString() + Environment.NewLine);

                MessageBox.Show(message.ToString());
            }
           
        }
             
        private void BtnHD2HD_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"C:\Program Files\Handbrake\Handbrake.exe"))
            {
                btnHD2HD.BackColor = Color.Green;
                EncodeHD2HD();
            }
            else
            {

                DialogResult dialogResult = MessageBox.Show("Handbreak is not installed, do you want to install?", "Handbreak installation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("https://handbrake.fr/downloads.php");
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Please install Handbrake before running");

                    this.Close();
                }
            }
        }

        public void EncodeHD2HD()
        {
            string[] beforeDirHD2HD = Directory.GetFiles(txtBxSource.Text, "*.*");
            long beforeSizeHD2HD = Sizes(beforeDirHD2HD);

            int converted = 0;
            int errors = 0;

            bool copyAudio = true;

            string[] files = Directory.GetFiles(txtBxSource.Text, "*.*");

            if (ChkBxMKV.Checked == false)
            {
                foreach (string currentFile in files)
                {
                    if (currentFile.EndsWith("avi") || currentFile.EndsWith("mp4") || currentFile.EndsWith("ts"))
                    {
                        // Create launch settings
                        ProcessStartInfo psi = new ProcessStartInfo()
                        {
                            FileName = @"C:\Program Files\Handbrake\HandBrakeCLI.exe"
                        };
                        if (copyAudio)
                        {
                            psi.Arguments = "-i \"" + currentFile + "\" -t 1 -o \"" + currentFile + "-output.mkv\" -f mkv -O -w " + txtBxHD2HDWid + " -l " + txtBxHD2HDHei + " --modulus 8 -e x264 -b " + txtBxHD2HDBR + " -2 --vfr -a 1 -E faac -B 160 -6 dpl2 -R Auto -D 0 --gain=0 --audio-copy-mask none --audio-fallback ffac3 -x weightp=1:subq=10:rc-lookahead=10:trellis=2:b-adapt=2:psy-rd=1.00,0.10 --verbose=1";
                        }
                        else
                        {

                        }

                        try
                        {
                            // Create process based on settings
                            Process prc = new Process()
                            {
                                StartInfo = psi
                            };
                            prc.Start();

                            // Wait for process to exit
                            while (!prc.HasExited)
                            {
                                Thread.Sleep(500);
                            }

                            switch (prc.ExitCode)
                            {
                                case 0:
                                    converted++;
                                    break;
                                default:
                                    errors++;
                                    break;
                            }

                            // Sort file out
                            if (File.Exists(currentFile + "-output.mkv"))
                            {
                                File.Delete(currentFile);

                                string finalName = string.Empty;
                                if (currentFile.EndsWith("ts"))
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 2), 2), "mkv");
                                else
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 3), 3), "mkv");

                                File.Move(currentFile + "-output.mkv", finalName);
                            }
                        }
                        catch
                        { }
                    }
                }

                string[] afterDirHD2HD = Directory.GetFiles(txtBxSource.Text, "*.*");
                long afterSizeHD2HD = Sizes(afterDirHD2HD);

                if (afterSizeHD2HD > beforeSizeHD2HD == true)
                {
                    arrowUp.Image = Properties.Resources.up;
                    arrowUp.Visible = true;

                }

                if (afterSizeHD2HD < beforeSizeHD2HD == true)
                {
                    arrowDown.Image = Properties.Resources.down;
                    arrowDown.Visible = true;
                }

                else
                {
                    sameAs.Image = Properties.Resources.Same;
                    sameAs.Visible = true;

                }

                txtBxAfter.Text = InGB(afterSizeHD2HD);

                var message = new StringBuilder();
                message.AppendLine(txtOutput.Text + "Job Complete" + Environment.NewLine);
                message.AppendLine(txtOutput.Text + "Converted: " + converted.ToString() + Environment.NewLine);
                message.AppendLine(txtOutput.Text + "Errors: " + errors.ToString() + Environment.NewLine);

                MessageBox.Show(message.ToString());
            }

            else
            {
                foreach (string currentFile in files)
                {
                    if (currentFile.EndsWith("avi") || currentFile.EndsWith("mp4") || currentFile.EndsWith("ts") || currentFile.EndsWith("mkv"))
                    {
                        // Create launch settings
                        ProcessStartInfo psi = new ProcessStartInfo()
                        {
                            FileName = @"C:\Program Files\Handbrake\HandBrakeCLI.exe"
                        };
                        if (copyAudio)
                        {
                            psi.Arguments = "-i \"" + currentFile + "\" -t 1 -o \"" + currentFile + "-output.mkv\" -f mkv -O -w " + txtBxHD2HDWid + " -l " + txtBxHD2HDHei + " --modulus 8 -e x264 -b " + txtBxHD2HDBR + " -2 --vfr -a 1 -E faac -B 160 -6 dpl2 -R Auto -D 0 --gain=0 --audio-copy-mask none --audio-fallback ffac3 -x weightp=1:subq=10:rc-lookahead=10:trellis=2:b-adapt=2:psy-rd=1.00,0.10 --verbose=1";
                        }
                        else
                        {

                        }

                        try
                        {
                            // Create process based on settings
                            Process prc = new Process()
                            {
                                StartInfo = psi
                            };
                            prc.Start();

                            // Wait for process to exit
                            while (!prc.HasExited)
                            {
                                Thread.Sleep(500);
                            }

                            switch (prc.ExitCode)
                            {
                                case 0:
                                    converted++;
                                    break;
                                default:
                                    errors++;
                                    break;
                            }

                            // Sort file out
                            if (File.Exists(currentFile + "-output.mkv"))
                            {
                                File.Delete(currentFile);

                                string finalName = string.Empty;
                                if (currentFile.EndsWith("ts"))
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 2), 2), "mkv");
                                else
                                    finalName = currentFile.Replace(currentFile.Substring((currentFile.Length - 3), 3), "mkv");

                                File.Move(currentFile + "-output.mkv", finalName);
                            }
                        }
                        catch
                        { }
                    }
                }

                string[] afterDirHD2HD = Directory.GetFiles(txtBxSource.Text, "*.*");
                long afterSizeHD2HD = Sizes(afterDirHD2HD);

                if (afterSizeHD2HD > beforeSizeHD2HD == true)
                {
                    arrowUp.Image = Properties.Resources.up;
                    arrowUp.Visible = true;

                }

                if (afterSizeHD2HD < beforeSizeHD2HD == true)
                {
                    arrowDown.Image = Properties.Resources.down;
                    arrowDown.Visible = true;
                }

                else
                {
                    sameAs.Image = Properties.Resources.Same;
                    sameAs.Visible = true;

                }

                txtBxAfter.Text = InGB(afterSizeHD2HD);

                var message = new StringBuilder();
                message.AppendLine(txtOutput.Text + "Job Complete" + Environment.NewLine);
                message.AppendLine(txtOutput.Text + "Converted: " + converted.ToString() + Environment.NewLine);
                message.AppendLine(txtOutput.Text + "Errors: " + errors.ToString() + Environment.NewLine);
                
                MessageBox.Show(message.ToString());
            }
        }

        private static long Sizes(string[] path)
        {
            long temp = 0;

            foreach (string name in path)
            {
                FileInfo info = new FileInfo(name);
                temp += info.Length;
            }

            return temp;
        }

        private static string InGB(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnSourceBrow_Click(object sender, EventArgs e)
        {
            btnSD.BackColor = default(Color);
            btnHD.BackColor = default(Color);
            btnHD2HD.BackColor = default(Color);

            arrowUp.Visible = false;
            arrowDown.Visible = false;
            sameAs.Visible = false;

            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
                txtBxSource.Text = folderPath;
            }
            string[] beforeDir = Directory.GetFiles(txtBxSource.Text, "*.*");
            long beforeSize = Sizes(beforeDir);

            txtBxBefore.Text = InGB(beforeSize);

            var fileCount = Directory.EnumerateFiles(txtBxSource.Text, "*", SearchOption.AllDirectories).Count();
            txtBxFileCount.Text = fileCount.ToString();

            string[] files = Directory.GetFiles(txtBxSource.Text, "*.*");
            foreach (string currentFile in files)
            {
                if (currentFile.EndsWith("avi"))
                {
                    aviChkBox.Checked = true;
                }
                if (currentFile.EndsWith("mp4"))
                {
                    mp4ChkBx.Checked = true;
                }
                if(currentFile.EndsWith("ts"))
                {
                    tsChkBx.Checked = true;
                }
                if(currentFile.EndsWith("mkv"))
                {
                    mkvChkBx.Checked = true;
                    ChkBxMKV.Checked = true;
                }
            }         

            txtBxAfter.Clear();
            txtOutput.Clear();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
               
        private void TxtOutput_TextChanged(object sender, EventArgs e)
        {
            txtOutput.SelectionStart = txtOutput.Text.Length;
            txtOutput.ScrollToCaret();
        }
              
    }
}
