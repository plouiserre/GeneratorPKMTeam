namespace GeneratorPKMTeam.Domain.Models
{

    public class ResultatTirage
    {
        public ResultatTirageStatus ResultatStatus { get; set; }
        public double NoteResultatTirage { get; set; }
    }

    public enum ResultatTirageStatus
    {
        Faible, //>20%
        Passables, // > 40%
        Acceptable, //>60%
        Bonnes, // > 80%
        Excellent, //>100%
        Parfait // 100
    }
}