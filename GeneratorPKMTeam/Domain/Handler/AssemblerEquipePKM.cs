using GeneratorPKMTeam.Domain.Handler.OrdrePKMType;
using GeneratorPKMTeam.Domain.Handler.SelectionPKM;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class AssemblerEquipePKM : IAssemblerEquipePKM
    {
        private IChargerPKMTypes _chargementPKMTypes;
        private IChoisirPKMTypes _choisirPKMTypes;
        private IDefinirOrdrePKMType _definirOrdrePKMType;
        private IRecuperationPKMs _recuperationPKMs;

        public AssemblerEquipePKM(IChargerPKMTypes chargementPKMTypes, IChoisirPKMTypes choisirPKMTypes,
                                IDefinirOrdrePKMType definirOrdrePKMType, IRecuperationPKMs recuperationPKMs)
        {
            _chargementPKMTypes = chargementPKMTypes;
            _choisirPKMTypes = choisirPKMTypes;
            _definirOrdrePKMType = definirOrdrePKMType;
            _recuperationPKMs = recuperationPKMs;
        }

        public List<PKM> Assembler()
        {
            try
            {
                var resultat = new List<PKM>();

                var tousPKMTypes = _chargementPKMTypes.AvoirPKMDatas();

                var PKMTypesChoisis = _choisirPKMTypes.SelectionnerPKMTypes(tousPKMTypes);

                var typeOrdonnees = _definirOrdrePKMType.Generer(PKMTypesChoisis);

                resultat = _recuperationPKMs.Recuperer(typeOrdonnees);

                return resultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}