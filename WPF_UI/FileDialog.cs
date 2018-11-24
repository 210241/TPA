﻿using System.IO;
using System.Windows;
using ApplicationLogic.Interfaces;
using ApplicationLogic.Interfaces;
using Microsoft.Win32;

namespace WPF_UI
{
    internal class FileDialog : IFilePathProvider
    {
        private readonly ILogger _logger;

        public FileDialog(ILogger logger)
        {
            _logger = logger;
        }

        public string GetFilePath(string extension)
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

            if (Path.GetExtension(result) != extension)
            {
                string info = $"File is not a {extension} file";
                _logger.Trace(info);
                MessageBox.Show(info);
                result = null;
            }

            return result;
        }
    }
}