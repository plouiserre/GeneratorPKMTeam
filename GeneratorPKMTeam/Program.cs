// See https://aka.ms/new-console-template for more information
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Infrastructure.Connector;
using GeneratorPKMTeam.Infrastructure.Services;

var connector = new PKMTypeJson();
var PMKPersistence = new PKMTypePersistence();

var loadPKMTypes = new ChargerPKMTypes(PMKPersistence);
var selectPKMTypes = new ChoisirPKMTypes();
var fightPKMTypes = new CombattrePKMTypes();
var resultFightPKMTypes = new ResultFightPKMTypes();
var gererResultatTiragePKMTypes = new GererResultatTiragePKMTypes();
var handler = new GeneratePKMTeamHandler(loadPKMTypes, selectPKMTypes, fightPKMTypes, resultFightPKMTypes,
                gererResultatTiragePKMTypes);

handler.Generer();

var tiragesAAfficher = handler.TiragePKMTypes;

for (int i = 0; i < tiragesAAfficher.Count; i++)
{
    var tirage = tiragesAAfficher[i];
    int tirageNumero = i + 1;
    Console.WriteLine("Tirage n°" + tirageNumero);
    foreach (var pkmType in tirage.PKMTypes)
    {
        Console.WriteLine(pkmType.Nom);
    }
    Console.WriteLine("-------FIN Tirage n°" + tirageNumero);
}

Console.Read();
