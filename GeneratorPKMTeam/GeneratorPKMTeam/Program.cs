// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using GeneratorPKMTeam;

string data = File.ReadAllText(@"PKMType.json");
var datas = JsonSerializer.Deserialize<PKMDatas>(data);
Console.Read();
