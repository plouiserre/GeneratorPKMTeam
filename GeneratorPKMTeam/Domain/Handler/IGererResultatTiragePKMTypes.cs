using GeneratorPKMTeam.Domain.Models;

namespace GeneratorPKMTeam.Domain.Handler
{
    public interface IGererResultatTiragePKMTypes
    {
        List<TiragePKMTypes> TirerPKMTypes(List<TiragePKMTypes> tiragesPKMTypesSauvegardes, TiragePKMTypes tiragePKMTypesATraiter);
    }
}