using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OsuBeatmapLib
{
    internal class Beatmap
    {
        public struct General
        {
            public string audioFilename;
            public int audioLeadIn;
            public int previewTime;
            public int countdown;
            public double stackLeniency;
        }

        public struct Metadata
        {
            public string title;
            public string titleUnicode;
            public string artist;
            public string artistUnicode;
            public string creator;
            public string version;
            public string source;
            public string tags;
            public int beatmapID;
            public int beatmapSetID;
        }

        public struct Difficulty
        {
            public double hpDrainRate;
            public double circleSize;
            public double overallDifficulty;
            public double approachRate;
            public double sliderMultiplier;
            public double sliderTickRate;
        }

        public struct TimingPoint
        {
            public int time;            // in ms
            public double beatLength;
            public int meter;
            public int sampleSet;       // Ignorable
            public int sampleIndex;     // Ignorable
            public int volume;
            public bool uninherited;
            public int effects;         // 0: nothing 1: Kiai time
        }

        public General general;
        public Metadata metadata;
        public Difficulty difficulty;
        public List<TimingPoint> timingPoints;

        public Beatmap()
        {
        }

        public int getBPM()
        {
            return (int)(60000 / timingPoints[0].beatLength);
        }
    }
}
