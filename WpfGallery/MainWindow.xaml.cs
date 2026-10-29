using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WpfGallery
{
    public partial class MainWindow : Window
    {
        private static readonly string[] Extensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog();
            if (dialog.ShowDialog() != true) return;

            string folder = dialog.FolderName;
            PathText.Text = folder;

            var files = Directory.GetFiles(folder)
                .Where(f => Extensions.Contains(Path.GetExtension(f).ToLower()));

            var images = new List<BitmapImage>();

            foreach (var file in files)
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(file);
                bitmap.DecodePixelWidth = 600;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                images.Add(bitmap);
            }

            GalleryList.ItemsSource = images;
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            GalleryList.ItemsSource = null;
            PathText.Text = string.Empty;
            SliderPhoto.Value = 1;
        }
    }
}
