using UAssetAPI;
using UAssetAPI.ExportTypes;
using UAssetAPI.UnrealTypes;
//using System.Text.Json;
using Newtonsoft.Json;
using WACCA_SongTable;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.PropertyTypes.Structs;

const EngineVersion UnrealVersion = EngineVersion.VER_UE4_19;
string? basePath = null; // @"WindowsNoEditor\Mercury\Content\Table";

//namespace WACCA_SongTable;

void Read(UAsset asset)
{
    int failedCategoryCount = 0;
    var exports = asset.Exports;
    //var options = new JsonSerializerOptions
    //{
    //    WriteIndented = true
    //};
    List<ChartInfo> charts = new();

    foreach (Export export in exports)
    {
        //Console.WriteLine(asset.);
        if (export is DataTableExport dataTable)
        {
            foreach (StructPropertyData entry in dataTable.Table.Data)
            {
#pragma warning disable CS8604,CS8601 // Possible null reference assignment/argument.
                List<ChartDifficulty> chartDiff = new()
                {
                    new()
                    {
                        chartId = 0,
                        level = float.Parse(entry.Value[35].ToString()),
                        noteDesigner = entry.Value[31].ToString()
                    },
                    new()
                    {
                        chartId = 1,
                        level = float.Parse(entry.Value[36].ToString()),
                        noteDesigner = entry.Value[32].ToString()
                    },
                    new()
                    {
                        chartId = 2,
                        level = float.Parse(entry.Value[37].ToString()),
                        noteDesigner = entry.Value[33].ToString()
                    },
                    new()
                    {
                        chartId = 3,
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
                //json += JsonConvert.SerializeObject(chartInfo, Formatting.Indented) + ",";
                charts.Add(chartInfo);
                //for (int j = 0; j < entry.Value.Count; j++)
                //{
                //    var inst = entry.Value[j];
                //    //Console.WriteLine($"{inst.Name}: {inst.RawValue}");
                //}
            }
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "MusicList.json"), JsonConvert.SerializeObject(charts, Formatting.Indented));
        }
        //if (cat is RawExport) failedCategoryCount++;
        //if (cat is NormalExport usNormal)
        //{
        //    foreach (PropertyData dat in usNormal.Data)
        //    {
        //        Console.WriteLine(dat.Name);
        //        Console.WriteLine(dat.RawValue);
        //        //GetUnknownProperties(unknownTypes, dat);
        //    }
        //}
    }

    //var data = table?.Table.Data[0];
    //var jsonString = JsonSerializer.Serialize(table?.Table.Data, options);
    //Console.WriteLine($"Name: {}");
    //Console.WriteLine($"Data: {data.Name} {data.RawValue}");
    //File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "MusicTable.json"), jsonString);
}

//static void WriteTo(TextWriter writer)
//{

//}


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

    //var targetPath = Path.GetFileName(Path.ChangeExtension(tableName, "json"));
    //if (File.Exists(targetPath))
    //{
    //    return;
    //    //targetPath = ResolveFileOverwrite(targetPath);
    //}

    //Console.WriteLine($"Exporting to {targetPath}");
    //using StreamWriter writer = File.CreateText(targetPath);
    //exporter.WriteTo(writer);
    //writer.Flush();
}
