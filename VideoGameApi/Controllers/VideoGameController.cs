using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VideoGameApi.Data;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController(VideoGameDbContext context) : ControllerBase
    {

        private readonly VideoGameDbContext _context = context;
      

        [HttpGet("List")]
        public async Task<ActionResult<List<VideoGame>>> GetVideoGames()
        {
            return Ok(await _context.VideoGames.ToListAsync());
        }

        [HttpGet("ListId/{id}")]
        public async Task<ActionResult<VideoGame>> GetVideoGameById(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);

            if (game is null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpPost("Includ")]

        public async Task<ActionResult<VideoGame>> IncludVideoGame(VideoGame myGame)
        {
            if (myGame is null)
            {
                return BadRequest("Já existe uma tarefa com esse nome");
            }

            _context.VideoGames.Add(myGame);
            await _context.SaveChangesAsync();         

            return CreatedAtAction(nameof(GetVideoGameById), new { id = myGame.Id }, myGame);
        }

        [HttpPut("Modify/{id}")]
        public async Task<IActionResult> ModifyVideoGame(int id, VideoGame updateGame)
        {
            var game = await _context.VideoGames.FindAsync(id);

            if (game is null)
            {
                return NotFound();
            }

            game.Title = updateGame.Title;
            game.Platform = updateGame.Platform;
            game.Developer = updateGame.Developer;
            game.Publisher = updateGame.Publisher;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("Delete{id}")]
        public async Task<IActionResult> DeleteVideoGme(int id)
        {
            var game = await _context.VideoGames.FindAsync(id);

            if (game is null)
            {
                return NotFound();
            }

            _context.VideoGames.Remove(game);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
