using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocationServicesApi;
using LocationServicesApi.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LocationServicesApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Locations")]
    public class LocationsController : Controller
    {
        private readonly Context _context;

        public LocationsController(Context context)
        {
            _context = context;
        }

        // GET: api/Locations
        [HttpGet]
        public IEnumerable<Location> GetLocations()
        {
            return _context.Locations;
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = await _context.Locations.SingleOrDefaultAsync(m => m.Id == id);

            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // PUT: api/Locations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation([FromRoute] int id, [FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != location.Id)
            {
                return BadRequest();
            }

            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Locations
        [HttpPost]
        public async Task<IActionResult> PostLocation([FromBody] Location location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var client = new HttpClient())
            {
                var result = await client.GetAsync($"https://maps.googleapis.com/maps/api/geocode/json?latlng={location.Latitude},{location.Longitude}&key=AIzaSyCKb-hVu7N2XQHpq2dR-mWOTaqSxfOjP7U&result_type=street_address");
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    string address = (string)JObject.Parse(content)["results"][0]["formatted_address"];
                    location.Address = address;
                }
            }

            if (location.Date == null)
            {
                location.Date = DateTime.UtcNow;
            }

            _context.Locations.Add(location);

            var device = await _context.Devices.Where(x => x.Id == location.DeviceId).FirstOrDefaultAsync();

            device.Latitude = location.Latitude;
            device.Longitude = location.Longitude;
            device.Address = location.Address;
            device.Date = location.Date;

            _context.Entry(device).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocation", new { id = location.Id }, location);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var location = await _context.Locations.SingleOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return Ok(location);
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }
    }
}