using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        static private List<VideoGame> videoGames = new()
        {
            new VideoGame
            {
                Id = 1,
                Title = "Spider-Man 2",
                Platform = "PS5",
                Developer = "Insomniac Games",
                Publisher = "Sony Interactive Entertainment"
            },

            new VideoGame
            {
                Id = 2,
                Title = "Fortnite",
                Platform = "Computer",
                Developer = "Epic Games",
                Publisher = "Epic Games"
            },

            new VideoGame
            {
                Id = 3,
                Title = "Cyberpunk 2077",
                Platform = "PC",
                Developer = "CD Projekt Red",
                Publisher = "CD Projekt"
            }
        };

        [HttpGet("List")]
        public ActionResult<List<VideoGame>> GetVideoGames()
        {
            return Ok(videoGames);
        }

        [HttpGet("ListId/{id}")]
        public ActionResult<VideoGame> GetVideoGameById(int id)
        {
            var game = videoGames.FirstOrDefault(g => g.Id == id);

            if (game is null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpPost("Includ")]

        public ActionResult<VideoGame> IncludVideoGame( VideoGame myGame)
        {
            var verifyGame = videoGames.Any(a => a.Title == myGame.Title);

            if (verifyGame)
            {
                return BadRequest("Já existe uma tarefa com esse nome");
            }

            myGame.Id = videoGames.Max(m => m.Id) + 1;
            videoGames.Add(myGame);
            return CreatedAtAction(nameof(GetVideoGameById), new {id = myGame.Id}, myGame);
        }

        [HttpPut("Modify/{id}")]
        public IActionResult ModifyVideoGame(int id, VideoGame updateGame)
        {
            var game = videoGames.FirstOrDefault(g => g.Id == id);

            if (game is null)
            {
                return NotFound();
            }

            game.Title = updateGame.Title;
            game.Platform = updateGame.Platform;
            game.Developer = updateGame.Developer;
            game.Publisher = updateGame.Publisher;

            return NoContent();
        }

        [HttpDelete("Delete{id}")]
        public IActionResult DeleteVideoGme(int id)
        {
            var game = videoGames.FirstOrDefault(g => g.Id == id);

            if (game is null)
            {
                return NotFound();
            }

            videoGames.Remove(game);
            return NoContent();
        }

    }
}
