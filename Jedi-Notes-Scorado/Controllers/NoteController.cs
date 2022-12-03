using Jedi_Notes_Scorado.Models;
using Jedi_Notes_Scorado.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jedi_Notes_Scorado.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {

        private INoteService _noteService;
        private ILogger<NoteController> _logger;


        public NoteController(INoteService noteService, ILogger<NoteController> logger)
        {
            _noteService = noteService;
            _logger = logger;
        }


        /// <summary>
        /// Get Jedi Note via Note ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public JediNote GetNote(int id)
        {
            try
            {
                return _noteService.GetJediNote(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Issue Retrieving Note");
                return null;
            }

        }

        /// <summary>
        /// Create a new note with note as object
        /// </summary>
        /// <param name="note"></param>
        [HttpPost]
        public void CreateNote([FromBody]JediNote note)
        {
            try
            {
                _noteService.CreateJediNote(note);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Issue Creating New Note");
            }
        }

        /// <summary>
        /// Update an existing note with note object and id reference
        /// </summary>
        /// <param name="note"></param>
        /// <param name="id"></param>
        [HttpPut()]
        public void UpdateNote([FromBody]JediNote note)
        {
            try
            {
                _noteService.UpdateJediNote(note);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Issue Updating New Note");

            }
        }

        [HttpDelete("{id:int}")]
        public void DeleteNote(int id)
        {
            try
            {
                _noteService.DeleteJediNote(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Issue Deleting Note");

            }
        }

        /// <summary>
        /// Gets a list of all notes by rank and owner with a bool to sort by either ascending or descending
        /// </summary>
        /// <param name="isSortDescending">false</param>
        /// <param name="rank">0</param>
        /// <param name="owner"></param>
        /// <returns></returns>
        [HttpGet("Notes/{isSortDescending:bool?}/{rank}/{owner?}")]
        public List<JediNote> GetNotes(bool isSortDescending,eJediRank rank,string owner = "" )
        {
            try
            {
                return _noteService.GetAllJediNotes(isSortDescending,rank,owner);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue Retreiving Notes List");
                return null;
            }
        }



    }
}
