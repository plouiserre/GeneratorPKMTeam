// See https://aka.ms/new-console-template for more information
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Infrastructure.Connector;
using GeneratorPKMTeam.Infrastructure.Services;

var PMKPersistence = new PKMTypePersistence();

var loadPKMTypes = new ChargerPKMTypes(PMKPersistence);
var selectPKMTypes = new ChoisirPKMTypes();
var resultatCombatPKMTypeATK = new ResultatCombatPKMTypeATK();
var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF();
var resultFightPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);
var gererResultatTiragePKMTypes = new GererResultatTiragePKMTypes();
var choisirMeilleuresCombinaisonsTypes = new ChoisirMeilleuresCombinaisonsTypes(loadPKMTypes, selectPKMTypes, resultFightPKMTypes,
                gererResultatTiragePKMTypes);
var handler = new GeneratePKMTeamHandler(choisirMeilleuresCombinaisonsTypes);

handler.Generer();

var tiragesAAfficher = handler.TypesChoisis;

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

var connector = new PKMJson();
var pkms = connector.RecupererListePKMs();
foreach (var pkm in pkms.TousPKMs)
{
    Console.WriteLine("Nom " + pkm.Nom + " generation " + pkm.Generation + " types " + String.Join(" ", pkm.PKMTypes));
}

Console.Read();
