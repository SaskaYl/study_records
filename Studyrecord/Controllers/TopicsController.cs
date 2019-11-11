using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Studyrecord.Models;

namespace Studyrecord.Controllers
{
    public class TopicsController : Controller
    {
        
            OpintopäiväkirjaContext db = new OpintopäiväkirjaContext();
    
        public async Task<IActionResult> Index()
        {
            return View(await db.Topic.ToListAsync());
        }
        public ActionResult Haku(string keyword)
        {
            ViewBag.sana = keyword;
            try
            {
                var haettu = db.Topic.Where(a => a.Title.Contains(keyword)).ToList();
                if (haettu.Count!=0)
                    return View("Index", haettu);
                else
                    return View();
            }


            catch
            {
                return View("Error", new ErrorViewModel());
            }
        }

     
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopicId,Title,Description,Estimatedtime,TimeSpent,Source,Start,InProgress,CompletionDate")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Add(topic);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topic);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await db.Topic.FindAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TopicId,Title,Description,Estimatedtime,TimeSpent,Source,Start,InProgress,CompletionDate")] Topic topic)
        {
            if (id != topic.TopicId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(topic);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicExists(topic.TopicId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(topic);
        }

      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topic = await db.Topic
                .FirstOrDefaultAsync(m => m.TopicId == id);
            if (topic == null)
            {
                return NotFound();
            }

            return View(topic);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topic = await db.Topic.FindAsync(id);
            db.Topic.Remove(topic);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicExists(int id)
        {
            return db.Topic.Any(e => e.TopicId == id);
        }
    }
}
