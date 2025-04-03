using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuBeatmapLib
{
    internal class Decoder
    {
        private enum Section
        {
            General,
            Editor,
            Metadata,
            Difficulty,
            Events,
            TimingPoints,
            Colours,
            HitObjects
        }
        private Section _currentSection;

        public Beatmap Decode(IEnumerable<string> data)
        {
            Beatmap beatmap = new Beatmap();
            foreach (string line in data)
            {
                ParseLine(line, beatmap);
            }
            return beatmap;
        }

        private void ParseLine(string line, Beatmap beatmap)
        {
            if (line.StartsWith("//")) return;
            if (line.StartsWith("["))
            {
                // Section
                string section = line.Substring(1, line.Length - 2);
                _currentSection = (Section)Enum.Parse(typeof(Section), section);
                return;
            }
            if (_currentSection == Section.General  || 
                _currentSection == Section.Metadata ||
                _currentSection == Section.Difficulty)
            {
                // Key-Value pair
                string[] pair = line.Split(':');
                string key = pair[0].Trim();
                string value = pair[1].Trim();
                switch (_currentSection)
                {
                    case Section.General:
                        ParseGeneral(key, value, beatmap);
                        break;
                    case Section.Metadata:
                        ParseMetadata(key, value, beatmap);
                        break;
                    case Section.Difficulty:
                        ParseDifficulty(key, value, beatmap);
                        break;
                }
            }
        }

        private void ParseGeneral(string key, string value, Beatmap beatmap)
        {
            switch (key)
            {
                case "AudioFilename":
                    beatmap.general.audioFilename = value;
                    break;
                case "AudioLeadIn":
                    beatmap.general.audioLeadIn = int.Parse(value);
                    break;
                case "PreviewTime":
                    beatmap.general.previewTime = int.Parse(value);
                    break;
                case "Countdown":
                    beatmap.general.countdown = int.Parse(value);
                    break;
                case "StackLeniency":
                    beatmap.general.stackLeniency = double.Parse(value);
                    break;
            }
        }

        private void ParseMetadata(string key, string value, Beatmap beatmap)
        {
            switch (key)
            {
                case "Title":
                    beatmap.metadata.title = value;
                    break;
                case "TitleUnicode":
                    beatmap.metadata.titleUnicode = value;
                    break;
                case "Artist":
                    beatmap.metadata.artist = value;
                    break;
                case "ArtistUnicode":
                    beatmap.metadata.artistUnicode = value;
                    break;
                case "Creator":
                    beatmap.metadata.creator = value;
                    break;
                case "Version":
                    beatmap.metadata.version = value;
                    break;
                case "Source":
                    beatmap.metadata.source = value;
                    break;
                case "Tags":
                    beatmap.metadata.tags = value;
                    break;
                case "BeatmapID":
                    beatmap.metadata.beatmapID = int.Parse(value);
                    break;
                case "BeatmapSetID":
                    beatmap.metadata.beatmapSetID = int.Parse(value);
                    break;
            }
        }

        private void ParseDifficulty(string key, string value, Beatmap beatmap)
        {
            switch (key)
            {
                case "HPDrainRate":
                    beatmap.difficulty.hpDrainRate = double.Parse(value);
                    break;
                case "CircleSize":
                    beatmap.difficulty.circleSize = double.Parse(value);
                    break;
                case "OverallDifficulty":
                    beatmap.difficulty.overallDifficulty = double.Parse(value);
                    break;
                case "ApproachRate":
                    beatmap.difficulty.approachRate = double.Parse(value);
                    break;
                case "SliderMultiplier":
                    beatmap.difficulty.sliderMultiplier = double.Parse(value);
                    break;
                case "SliderTickRate":
                    beatmap.difficulty.sliderTickRate = double.Parse(value);
                    break;
            }
        }

        private void ParseTimingPoints(string key, string value, Beatmap beatmap)
        {
            // TODO
            string[] values = value.Split(',');
            Beatmap.TimingPoint timingPoint = new Beatmap.TimingPoint();
            timingPoint.time = int.Parse(values[0]);
            timingPoint.beatLength = double.Parse(values[1]);
            timingPoint.meter = int.Parse(values[2]);
            timingPoint.sampleSet = int.Parse(values[3]);
            timingPoint.sampleIndex = int.Parse(values[4]);
            timingPoint.volume = int.Parse(values[5]);
            timingPoint.uninherited = values[6] == "1";
            timingPoint.effects = int.Parse(values[7]);
        }
    }
}
