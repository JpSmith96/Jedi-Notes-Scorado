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
        [HttpGet("Note/{id:int}")]
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
        [HttpPost("Note")]
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
        [HttpPut("Note/{id:int}")]
        public void UpdateNote([FromBody]JediNote note, int id)
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

        [HttpDelete("Note/{id:int}")]
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

        [HttpGet("Notes/{owner}/{rank}")]
        public List<JediNote> GetNotes(string owner, eJediRank rank)
        {
            try
            {
                return _noteService.GetAllJediNotes(owner, rank);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Issue Retreiving Notes List");
                return null;
            }
        }



    }
}
