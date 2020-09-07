using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NHIBERBNATE_CRUD.Models;
using NHibernate;
using NHibernate.Linq;

namespace NHIBERBNATE_CRUD.Controllers
{
    public class CarsController : Controller
    {
        private readonly ISession _session;

        public CarsController(ISession session)
        {
            _session = session;
        }
        
        public async Task<IActionResult> Index()
        {
            var cars = await _session.Query<Car>().ToListAsync();

            return View(cars);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                using (ITransaction tx = _session.BeginTransaction())
                {
                    await _session.SaveAsync(car);
                    await tx.CommitAsync();

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(car);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int carId)
        {
            var carToUpdate = await _session.GetAsync<Car>(carId);

            return View(carToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int carId, Car carToUpdate)
        {
            if (carId != carToUpdate.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (ITransaction tx = _session.BeginTransaction())
                {
                    await _session.SaveOrUpdateAsync(carToUpdate);
                    await tx.CommitAsync();

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(carToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int carId)
        {
            var carToDelete = await _session.GetAsync<Car>(carId);

            using (ITransaction tx = _session.BeginTransaction())
            {
                await _session.DeleteAsync(carToDelete);
                await tx.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
