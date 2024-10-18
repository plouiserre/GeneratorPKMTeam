// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using GeneratorPKMTeam;
using GeneratorPKMTeam.Infrastructure.Connector;

// string data = File.ReadAllText(@"PKMType.json");
// var datas = JsonSerializer.Deserialize<PKMDatas>(data);
var connector = new PKMTypeJson();
var result = connector.GetPKMDatas();
Console.Read();
