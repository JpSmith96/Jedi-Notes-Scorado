namespace Jedi_Notes_Scorado.Models
{
    public class JediNote
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }   


        //outside of the exercise, I can see owner and Jedi Rank being split of into it's own class (JediPersonnel perhaps)
        public string Owner { get; set; }

        public eJediRank JediRankType { get; set; }

        public string JediRank { get { return Enum.GetName(JediRankType); } }
    }


    public enum eJediRank
    {
        Any= -1,
        Master = 0,
        Knight = 1,
        Padawan = 2,
        NotFromAJedi = 9999 //little easter-egg, couldn't help myself.

    }
}
