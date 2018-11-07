using System.IO;
using System.Windows;
using ApplicationLogic.Interfaces;
using DataTransfer.Interfaces;
using Microsoft.Win32;

namespace WPF_UI
{
    internal class FileDialog : IFilePathProvider
    {
        private ILogger _logger;

        public FileDialog(ILogger logger)
        {
            _logger = logger;
        }

        public string GetFilePath()
        {
            _logger.Trace("Searching for file");
            OpenFileDialog fileDialog = new OpenFileDialog();

            string result;
            if (fileDialog.ShowDialog() == true)
            {
               result = fileDialog.FileName;
                _logger.Trace("Opening file: " + result);
            }
            else
            {
                result = string.Empty;
                _logger.Trace("No file has been chosen");
            }

            if (Path.GetExtension(result) != ".dll")
            {
                string info = "File is not a .dll assembly";
                _logger.Trace(info);
                MessageBox.Show(info);
                result = null;
            }

            return result;
        }
    }
}