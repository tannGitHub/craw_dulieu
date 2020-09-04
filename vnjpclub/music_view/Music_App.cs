using Microsoft.Office.Core;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace vnjpclub.music_view
{
    public partial class Music_App : Form
    {
        int isFileOpen = -1;
        ArrayList listFile;
        public Music_App()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (isFileOpen == 0)
            {
                if (listFile.Count > 0)
                {
                    GetTextFromPowerPoint(txtPath.Text);
                }
                else
                {

                }
            }
            else if (isFileOpen == 1)
            {

            }
        }

        public static string GetTextFromPowerPoint(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Vui lòng chọn đường dẫn file hoặc thư mục.");
            }

            if (!File.Exists(filePath))
            {
                MessageBox.Show("File PowerPoint không tồn tại.");
            }

            var stringBuilder = new StringBuilder();

            try
            {
                PowerPoint.Application pptApp = new PowerPoint.Application();
                PowerPoint.Presentations pptPresentations = pptApp.Presentations;
                PowerPoint.Presentation pptPresentation = pptPresentations.Open(filePath,
                                         MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse);
                PowerPoint.Slides pptSlides = pptPresentation.Slides;

                if (pptSlides != null)
                {
                    var slidesCount = pptSlides.Count;
                    if (slidesCount > 0)
                    {
                        for (int slideIndex = 1; slideIndex <= slidesCount; slideIndex++)
                        {
                            var slide = pptSlides[slideIndex];
                            foreach (PowerPoint.Shape textShape in slide.Shapes)
                            {
                                if (textShape.HasTextFrame == MsoTriState.msoTrue &&
                                         textShape.TextFrame.HasText == MsoTriState.msoTrue)
                                {
                                    PowerPoint.TextRange pptTextRange = textShape.TextFrame.TextRange;
                                    if (pptTextRange != null && pptTextRange.Length > 0)
                                    {
                                        stringBuilder.Append(" " + pptTextRange.Text);
                                    }
                                }
                            }
                            stringBuilder.Append("Tanxuongdong");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("This file is error");
            }

            return stringBuilder.ToString();
        }

        private void btnBrowerFolder_Click(object sender, EventArgs e)
        {
            isFileOpen = 1;
            if (brFolder.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(brFolder.SelectedPath.ToString());
                txtPath.Text = brFolder.SelectedPath.ToString();
            }
        }

        private void btnBrowerFile_Click(object sender, EventArgs e)
        {
            listFile = new ArrayList();
            isFileOpen = 0;
            if (brFile.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = brFile.FileNames[0].ToString();
                foreach (String file in brFile.FileNames)
                {
                    listFile.Add(file);
                }
            }
        }

        private void Music_App_Load(object sender, EventArgs e)
        {
            brFile.Filter = "PowerPoint File files (*.pptx)|*.pptx|PowerPoint File files (*.ppt)|*.ppt|All files (*.*)|*.*";
            brFile.InitialDirectory = @"C:\";
            brFile.RestoreDirectory = true;
            brFile.Title = "Browse Text Files";
            brFile.DefaultExt = "pptx";
            brFile.Multiselect = false;
            listFile = new ArrayList();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}
