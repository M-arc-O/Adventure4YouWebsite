﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RaceVenturaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly ILogger<VersionController> _Logger;

        public VersionController(ILogger<VersionController> logger)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("getversion")]
        public IActionResult GetVersion()
        {
            try
            {
                _Logger.LogDebug("GetVersion called.");
                return Ok("1.0.1.0");
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, $"Error in {typeof(AccountsController)}: {ex.Message}");
                return StatusCode(500);
            }
        }
    }
}