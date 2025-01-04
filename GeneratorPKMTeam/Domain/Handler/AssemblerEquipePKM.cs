using GeneratorPKMTeam.Domain.Handler.OrdrePKMType;
using GeneratorPKMTeam.Domain.Handler.SelectionPKM;
using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public class AssemblerEquipePKM : IAssemblerEquipePKM
    {
        private IDefinirOrdrePKMType _definirOrdrePKMType;
        private IRecuperationPKMs _recuperationPKMs;

        public AssemblerEquipePKM(IDefinirOrdrePKMType definirOrdrePKMType, IRecuperationPKMs recuperationPKMs)
        {
            _definirOrdrePKMType = definirOrdrePKMType;
            _recuperationPKMs = recuperationPKMs;
        }

        public List<PKM> Assembler(TiragePKMTypes tirage)
        {
            try
            {
                var resultat = new List<PKM>();

                var typeOrdonnees = _definirOrdrePKMType.Generer(tirage.PKMTypes);

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