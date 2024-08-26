using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;


namespace DownloaderAplikacija
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public async Task DownloadFileAsync(string url, string filePath)
        {
            using (HttpClient klijent = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await klijent.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();
                    


                    // Zapisivanje bajtova u datoteku asinhrono
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                    {
                        await fileStream.WriteAsync(fileBytes, 0, fileBytes.Length);
                       
                    }
                    MessageBox.Show("Download completed");
                }
                catch (Exception ex) {                   
                    MessageBox.Show("Download failed "+ex);
                }

            }
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files | *.txt| jpeg | *.jpeg| png| *.png| mp4| *.mp4| mp3| *.mp3";
            saveFileDialog.Title = "Save an Image File";

            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                string url = unosLinka.Text;

                await DownloadFileAsync(url, filePath);                        
            }

        }
    }
}
