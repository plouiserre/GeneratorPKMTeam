using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Handler.RechercherPKMType;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driving;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class GeneratePKMTeamHandler : IGeneratePKMTeamHandler
    {
        private ITrouverTypePKMEquipePKM _trouverTypePKMEquipePKM;
        public List<TiragePKMTypes> TiragePKMTypes;

        public GeneratePKMTeamHandler(ITrouverTypePKMEquipePKM trouverTypePKMEquipePKM)
        {
            _trouverTypePKMEquipePKM = trouverTypePKMEquipePKM;
        }

        public Dictionary<int, List<PKM>> Generer()
        {
            var resultat = new Dictionary<int, List<PKM>>();
            for (int i = 0; i < 10; i++)
            {
                var equipePKM = _trouverTypePKMEquipePKM.GenererEquipePKM();
                resultat.Add(i, equipePKM);
            }
            return resultat;
        }
    }
}