using System.IO;
using System.Text;
using System.Linq;

namespace OsuBeatmapLib
{
    public class Song
    {
        private string title { get; }
        private string unicodeTitle { get; }
        private string artist { get; }
        private string unicodeArtist { get; }
        private List<Beatmap> beatmaps { get; }

        private string[] _files;
        private string _folderPath;

        public Song(string id)
        {
            _folderPath = "/Songs/" + id;
            beatmaps = new List<Beatmap>();

            LoadFiles();
            ProcessFiles();

            if (beatmaps.Any())
            {
                title           = beatmaps[0].metadata.title;
                unicodeTitle    = beatmaps[0].metadata.titleUnicode;
                artist          = beatmaps[0].metadata.artist;
                unicodeArtist   = beatmaps[0].metadata.artistUnicode;
            }
            else throw new Exception("No beatmaps found");
        }

        private void LoadFiles()
        {
            try
            {
                _files = Directory.GetFiles(_folderPath);
            } 
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Error: Folder not found - {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void ProcessFiles()
        {
            foreach (string file in _files)
            {
                if (file.EndsWith(".osu"))
                {
                    try
                    {
                        string path = _folderPath + "/" + file;
                        IEnumerable<string> fullContent = File.ReadAllLines(path);
                        Beatmap beatmap = new Decoder().Decode(fullContent);
                        beatmaps.Add(beatmap);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
        }
    }
}