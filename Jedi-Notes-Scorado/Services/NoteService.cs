using Jedi_Notes_Scorado.Models;

namespace Jedi_Notes_Scorado.Services
{

    /// <summary>
    /// I like to seperate the controller and sql bearing code out
    /// I find it makes it easier to read through what api functions are doing
    /// and what the actual meat and potatoes of the code is doing.
    /// Then just dependancy injecting these as a scoped interface
    /// so if they need to be used elsewhere, we're avoiding duplicate code
    /// </summary>
    public interface INoteService
    {

        public JediNote GetJediNote(int id);

        public void CreateJediNote(JediNote note);

        public void UpdateJediNote(JediNote note, int id);

        public void DeleteJediNote(int id);

    }

    public class NoteService : INoteService
    {
        public void CreateJediNote(JediNote note)
        {
            throw new NotImplementedException();
        }

        public void DeleteJediNote(int id)
        {
            throw new NotImplementedException();
        }

        public JediNote GetJediNote(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateJediNote(JediNote note, int id)
        {
            throw new NotImplementedException();
        }
    }
}
