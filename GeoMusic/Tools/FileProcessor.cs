using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace GeoMusic.Tools
{
    public static class FileProcessor
    {
        public static string Path { get; set; }

        public static IEnumerable<string> GetSubFoldersNames()
        {
            return Directory.GetDirectories(Path).ToList();
        }

        public static Bitmap GetInstrumentCover()
        {
            var filepath = Directory.GetFiles(Path, "*.jpg").FirstOrDefault(f => f.Contains("Cover"));
            return new Bitmap(filepath);
        }

        public static string GetNoteImage(int note)
        {
            return Directory.GetFiles(Path, "*.jpg").FirstOrDefault(f => f.Contains(note.ToString()));
        }

        public static string GetOffImage()
        {
            return Directory.GetFiles(Path, "*.jpg").FirstOrDefault(f => f.Contains("Off"));
        }

        public static Bitmap[] LoadInstrument(string path)
        {

            var files = Directory.GetFiles(path, "*.jpg");
            var highestnote = files.Select(file => int.Parse(file.Split('\\').Last().Split('-').First())).ToArray().Max();
            var instrument = new Bitmap[highestnote + 1];

            foreach (var file in files)
            {

                var filename = file.Split('\\').Last();
                var note = filename.Split('-').First();
                instrument[int.Parse(note)] = new Bitmap(file);
            }

            return instrument;
        }

    }
}