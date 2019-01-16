using System.IO;
using System.Windows;
using ApplicationLogic.Interfaces;
using Microsoft.Win32;

namespace WPF_UI
{
    internal class FileDialog : IFilePathProvider
    {

        public string GetFilePath(string extension)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            string result;
            if (fileDialog.ShowDialog() == true)
            {
               result = fileDialog.FileName;
            }
            else
            {
                result = string.Empty;
            }

            if (Path.GetExtension(result) != extension)
            {
                string info = $"File is not a {extension} file";
                MessageBox.Show(info);
                result = null;
            }

            return result;
        }
    }
}