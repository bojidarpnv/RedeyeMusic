﻿using Microsoft.AspNetCore.Mvc;
using RedeyeMusic.Services.Data.Interfaces;
using RedeyeMusic.Services.Data.Models.Song;
using RedeyeMusic.Web.ViewModels.Song;

namespace RedeyeMusic.WebApi.Controllers
{
    [Route("api/listen-count")]
    [ApiController]
    public class ListenCountApiController : ControllerBase
    {
        private readonly ISongService songService;
        public ListenCountApiController(ISongService songService)
        {
            this.songService = songService;
        }
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(ListenCountServiceModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetListenCount([FromQuery] int songId)
        {
            try
            {
                ListenCountServiceModel serviceModel = await this.songService.GetListenCountAsync(songId);
                return this.Ok(serviceModel);
            }
            catch (Exception)
            {
                return this.BadRequest();
            }

        }

        [HttpPost("increment")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> IncrementListenCount([FromBody] IncrementListenCountServiceModel model)
        {
            try
            {
                await this.songService.IncrementListenCountAsync(model.SongId);
                return this.Ok();
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }
    }
}
