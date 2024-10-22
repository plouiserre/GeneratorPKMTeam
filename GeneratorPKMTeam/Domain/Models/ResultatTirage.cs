namespace GeneratorPKMTeam.Domain.Models
{

    public class ResultatTirage
    {
        public ResultatTirageStatus ResultatStatus { get; set; }
        public double NoteResultatTirage { get; set; }
    }

    public enum ResultatTirageStatus
    {
        Faible, //>30%
        Acceptable, //>80%
        Parfait //<80%
    }
}