// See https://aka.ms/new-console-template for more information
using GeneratorPKMTeam.Domain;
using GeneratorPKMTeam.Domain.Handler;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Infrastructure.Connector;
using GeneratorPKMTeam.Infrastructure.Services;

int generation = 3;
var PMKTypePersistence = new PKMTypePersistence();
var PKMPersistence = new PKMPersistence();

var loadPKMTypes = new ChargerPKMTypes(PMKTypePersistence);
var selectPKMTypes = new ChoisirPKMTypes();
var resultatCombatPKMTypeATK = new ResultatCombatPKMTypeATK();
var resultatCombatPKMTypeDEF = new ResultatCombatPKMTypeDEF();
var resultFightPKMTypes = new ResultatCombatPKMTypes(resultatCombatPKMTypeATK, resultatCombatPKMTypeDEF);
var gererResultatTiragePKMTypes = new GererResultatTiragePKMTypes();
var choisirMeilleuresCombinaisonsTypes = new ChoisirMeilleuresCombinaisonsTypes(loadPKMTypes, selectPKMTypes, resultFightPKMTypes,
                gererResultatTiragePKMTypes);
var determinerTousLesTypesExistant = new DeterminerTousLesTypesExistant(PKMPersistence);
var definirOrdrePKMType = new DefinirOrdrePKMType(determinerTousLesTypesExistant, generation);
var recuperationPKM = new RecuperationPKMs(PKMPersistence, generation);
var assemblerEquipePKM = new AssemblerEquipePKM(definirOrdrePKMType, recuperationPKM);
var trouverTypePKMEquipePKM = new TrouverTypePKMEquipePKM(choisirMeilleuresCombinaisonsTypes, assemblerEquipePKM);
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
