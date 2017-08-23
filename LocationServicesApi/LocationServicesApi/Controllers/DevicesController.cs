using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocationServicesApi;
using LocationServicesApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LocationServicesApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Devices")]
    public class DevicesController : Controller
    {
        private readonly Context _context;

        public DevicesController(Context context)
        {
            _context = context;
        }

        // GET: api/Devices
        [HttpGet]
        public IEnumerable<Device> GetDevices()
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.Claims.Where(c => c.Type == "user_id").FirstOrDefault().Value;
            return _context.Devices.Where(x=>x.UserId==userId).ToList();
        }

        // GET: api/Devices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var device = await _context.Devices.SingleOrDefaultAsync(m => m.Id == id);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        // PUT: api/Devices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice([FromRoute] int id, [FromBody] Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != device.Id)
            {
                return BadRequest();
            }

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Devices
        [HttpPost]
        public async Task<IActionResult> PostDevice([FromBody] Device device)
        {
            var identity = User.Identity as ClaimsIdentity;
            var userId = identity.Claims.Where(c => c.Type == "user_id").FirstOrDefault().Value;
            device.UserId = userId;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Devices.Add(device);
           
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetDevice", new { id = device.Id }, device);
        }

        // DELETE: api/Devices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var device = await _context.Devices.SingleOrDefaultAsync(m => m.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();

            return Ok(device);
        }

        private bool DeviceExists(int id)
        {
            return _context.Devices.Any(e => e.Id == id);
        }
    }
}