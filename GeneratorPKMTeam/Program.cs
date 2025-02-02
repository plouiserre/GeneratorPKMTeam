// See https://aka.ms/new-console-template for more information
using GeneratorPKMTeam.Domain;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Handler.OrdrePKMType;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Handler.SelectionPKM;
using GeneratorPKMTeam.Infrastructure.Connector;
using GeneratorPKMTeam.Infrastructure.Services;


int generation = 3;
var PMKTypePersistence = new PKMTypePersistence();
var PKMPersistence = new PKMPersistence();
var PKMStatsPersistence = new PKMStatsPersistence();
Console.WriteLine("Merci de choisir votre starter");
string starterPKMName = Console.ReadLine();
GererStarterPKM pKM = new GererStarterPKM(PKMPersistence);
pKM.ChoisirStarter(starterPKMName);

var chargerPKMTypes = new ChargerPKMTypes(PMKTypePersistence);
var choisirPKMTypes = new ChoisirPKMTypes(pKM);
var resultatCombatPKMTypeATK = new ResultatCombatPKMTypeATK();
var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF();
var resultFightPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);
var noterEquipePKM = new NoterEquipePKM(resultFightPKMTypes, PMKTypePersistence);
var determinerTousLesTypesExistant = new DeterminerTousLesTypesExistant(PKMPersistence, pKM);
var recupererPKMTypeDouble = new RecupererPKMTypeDouble();
var recupererPKMTypeSimple = new RecupererPKMTypeSimple();
var gererRecuperationPKMType = new GererRecuperationPKMType(recupererPKMTypeDouble, recupererPKMTypeSimple);
var definirOrdrePKMType = new DefinirOrdrePKMType(determinerTousLesTypesExistant, pKM, gererRecuperationPKMType, generation);
var determinerMeilleurPKMParStats = new DeterminerMeilleurPKMParStats(PKMStatsPersistence);
var recuperationPKM = new RecuperationPKMs(PKMPersistence, pKM, determinerMeilleurPKMParStats, generation);
var assemblerEquipePKM = new AssemblerEquipePKM(chargerPKMTypes, choisirPKMTypes, definirOrdrePKMType, recuperationPKM);
var trouverTypePKMEquipePKM = new TrouverEquipePKM(noterEquipePKM, assemblerEquipePKM);
var handler = new GeneratePKMTeamHandler(trouverTypePKMEquipePKM);

var pkmsTeams = handler.Generer();


foreach (var pkmsTeam in pkmsTeams)
{
    int numeroTirage = pkmsTeam.Key + 1;
    Console.WriteLine("-------Tirage n°" + numeroTirage + "-------");
    foreach (var pkm in pkmsTeam.Value)
    {
        if (pkm.PKMTypes.Count == 1)
            Console.WriteLine(pkm.Nom + " de type " + pkm.PKMTypes[0]);
        else
            Console.WriteLine(pkm.Nom + " de type " + pkm.PKMTypes[0] + " et de type " + pkm.PKMTypes[1]);
    }
    Console.WriteLine("-------FIN Tirage n°" + numeroTirage + "-------");
}

Console.Read();
