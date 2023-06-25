using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WACCA_SongTable
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class ChartInfo
    {
        [JsonProperty]
        public int songId;
        [JsonProperty]
        public float BPM;
        [JsonProperty]
        public string songArtist;
        [JsonProperty]
        public string songName;
        [JsonProperty]
        public string? songNameTranslated;
        [JsonProperty]
        public List<ChartDifficulty> difficulties;
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    internal class ChartDifficulty
    {
        [JsonProperty]
        public int chartId;
        [JsonProperty]
        public float level;
        [JsonProperty]
        public string noteDesigner;
    }
}

