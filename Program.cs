using UAssetAPI;
using UAssetAPI.ExportTypes;
using UAssetAPI.UnrealTypes;
using UAssetAPI.PropertyTypes.Structs;
using Newtonsoft.Json;
using WACCA_SongTable;

const EngineVersion UnrealVersion = EngineVersion.VER_UE4_19;
string? basePath = null; // @"WindowsNoEditor\Mercury\Content\Table";

void Read(UAsset asset)
{
    var exports = asset.Exports;
    List<ChartInfo> charts = new();

    foreach (Export export in exports)
    {
        if (export is DataTableExport dataTable)
        {
            foreach (StructPropertyData entry in dataTable.Table.Data)
            {
#pragma warning disable CS8604,CS8601 // Possible null reference assignment/argument.
                List<ChartDifficulty> chartDiff = new()
                {
                    new()
                    {
                        chartId = 1,
                        level = float.Parse(entry.Value[35].ToString()),
                        noteDesigner = entry.Value[31].ToString()
                    },
                    new()
                    {
                        chartId = 2,
                        level = float.Parse(entry.Value[36].ToString()),
                        noteDesigner = entry.Value[32].ToString()
                    },
                    new()
                    {
                        chartId = 3,
                        level = float.Parse(entry.Value[37].ToString()),
                        noteDesigner = entry.Value[33].ToString()
                    },
                    new()
                    {
                        chartId = 4,
                        level = float.Parse(entry.Value[38].ToString()),
                        noteDesigner = entry.Value[34].ToString()
                    }
                };

                ChartInfo chartInfo = new()
                {
                    songId = int.Parse(entry.Value[0].ToString()),
                    BPM = float.Parse(entry.Value[29].ToString()),
                    songName = entry.Value[1].ToString(),
                    songArtist = entry.Value[2].ToString(),
                    difficulties = chartDiff
                };
#pragma warning restore CS8604,CS8601 // Possible null reference assignment/argument.

                charts.Add(chartInfo);
            }
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "MusicList.json"), JsonConvert.SerializeObject(charts, Formatting.Indented));
        }
    }
}

while (basePath == null || !Directory.Exists(basePath))
{
    if (basePath != null)
        Console.WriteLine("Invalid path to Table directory.");
    Console.Write("Path to the game's Table directory: ");
    basePath = Console.ReadLine()!.Trim();
    continue;
}

var files = Directory.GetFiles(basePath, "MusicParameterTable.uasset");
foreach (var tableName in files)
{
    var asset = new UAsset(tableName, UnrealVersion);
    Read(asset);
}
