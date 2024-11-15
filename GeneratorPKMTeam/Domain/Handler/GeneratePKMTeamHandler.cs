using GeneratorPKMTeam.Domain.CustomException;
using GeneratorPKMTeam.Domain.Handler.RechercherPKMType;
using GeneratorPKMTeam.Domain.Handler.ResultatCombatPKMType;
using GeneratorPKMTeam.Domain.Models;
using GeneratorPKMTeam.Domain.Port.Driving;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class GeneratePKMTeamHandler : IGeneratePKMTeamHandler
    {
        private IChoisirMeilleuresCombinaisonsTypes _meilleursCombinaisonsTypes;
        public List<TiragePKMTypes> TypesChoisis;

        public GeneratePKMTeamHandler(IChoisirMeilleuresCombinaisonsTypes meilleursCombinaisonsTypes)
        {
            _meilleursCombinaisonsTypes = meilleursCombinaisonsTypes;
        }

        public void Generer()
        {
            TypesChoisis = _meilleursCombinaisonsTypes.Choisir();
        }


    }
}